Imports CenParaDB.TABLE
Imports CenLinqDB.TABLE
Imports CenLinqDB.Common.Utilities

Namespace Configuration
    Public Class RoleENG
        Dim _err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property

        Public Function GetUserList(ByVal WhText As String) As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbUserCenLinqDB
            Dim sql As String = "select id, case when isnull(fname,'')='' and isnull(lname,'')='' then username else isnull(fname,'') + ' ' + isnull(lname,'')  end as name"
            sql += " from TB_USER "
            sql += " where " & WhText & " "
            sql += " order by name"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function GetProgramList(ByVal WhText As String) As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.SysmenuCenLinqDB
            Dim sql As String = "select s.id, sm.module_name_en + ' >> ' + s.menu_name_en as name, "
            sql += " s.ref_sysmenu_id, sm.module_name_en, s.menu_name_en, s.menu_url,"
            sql += " sm.order_seq module_order, s.order_seq menu_order,s.sysmodule_id"
            sql += " from SYSMENU s"
            sql += " inner join SYSMODULE sm on sm.id=s.sysmodule_id"
            sql += " where " & WhText & " and s.active='Y' and sm.active='Y'"
            'sql += " order by sm.module_name_en, s.menu_name_en"

            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("ref_sysmenu_id") > 0 Then
                        Dim tmpDt As New DataTable
                        tmpDt = dt.Copy
                        tmpDt.DefaultView.RowFilter = "id = '" & dt.Rows(i)("ref_sysmenu_id") & "'"
                        If tmpDt.DefaultView.Count > 0 Then
                            dt.Rows(i)("name") = dt.Rows(i)("module_name_en") & " >> " & tmpDt.DefaultView(0)("menu_name_en") & " >> " & dt.Rows(i)("menu_name_en")
                        End If
                        tmpDt.Dispose()
                    End If
                Next
                'dt.DefaultView.RowFilter = " menu_url<>'#' and ref_sysmenu_id=0"
                'dt.DefaultView.RowFilter = " menu_url<>'#' or  sysmodule_id in (2,4)"
                dt.DefaultView.Sort = "module_order,menu_order"
                dt = dt.DefaultView.ToTable
            End If

            Return dt
        End Function

        Public Function GetRolePara(ByVal RoleID As Long) As TbRoleCenParaDB
            Dim p As New TbRoleCenParaDB
            Dim trans As New TransactionDB
            Dim lnq As New TbRoleCenLinqDB
            p = lnq.GetParameter(RoleID, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
            Return p
        End Function

        Public Function GetRoleList(ByVal WhText As String) As DataTable
            _err = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbRoleCenLinqDB
            Dim sql As String = "select id,role_name,role_desc,"
            sql += " active "
            sql += " from TB_ROLE "
            sql += " where " & WhText & " "
            sql += " order by role_name"
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function SaveRole(ByVal UserName As String, ByVal p As TbRoleCenParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbRoleCenLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, trans.Trans)
            End If
            lnq.ROLE_NAME = p.ROLE_NAME
            lnq.ROLE_DESC = p.ROLE_DESC
            lnq.ACTIVE = p.ACTIVE

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
                trans.RollbackTransaction()
            Else
                p.ID = lnq.ID
                trans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function SaveRoleUser(ByVal UserName As String, ByVal p As TbRoleUserCenParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbRoleUserCenLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, trans.Trans)
            End If
            lnq.TB_ROLE_ID = p.TB_ROLE_ID
            lnq.TB_USER_ID = p.TB_USER_ID
            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
                trans.RollbackTransaction()
            Else
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        Public Function SaveRoleProgramID(ByVal UserName As String, ByVal p As TbRoleSysmenuCenParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbRoleSysmenuCenLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, trans.Trans)
            End If
            lnq.TB_ROLE_ID = p.TB_ROLE_ID
            lnq.SYSMENU_ID = p.SYSMENU_ID
            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
                trans.RollbackTransaction()
            Else
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        Public Function DeleteRoleUserByID(ByVal user_id As String, ByVal role_id As String)
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Try
                Dim uSql As String = "delete from tb_role_user "
                uSql += " where tb_role_id = '" & role_id & "'  and tb_user_id='" & user_id & "'"
                SqlDB.ExecuteNonQuery(uSql, trans.Trans)
                trans.CommitTransaction()
                ret = True
            Catch ex As Exception
                _err = ex.Message.ToString
                trans.RollbackTransaction()
                ret = False
            End Try

            Return ret
        End Function

        Public Function DeleteRoleProgramByID(ByVal sysmenu_id As String, ByVal role_id As String)
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Try
                Dim uSql As String = "delete from tb_role_sysmenu "
                uSql += " where tb_role_id = '" & role_id & "'  and sysmenu_id='" & sysmenu_id & "'"
                SqlDB.ExecuteNonQuery(uSql, trans.Trans)
                trans.CommitTransaction()
                ret = True
            Catch ex As Exception
                _err = "RoleENG.DeleteRoleProgramByID : " & ex.Message.ToString
                trans.RollbackTransaction()
                ret = False
            End Try

            Return ret
        End Function

        Public Function CheckDuplicateRole(ByVal id As Long, ByVal Role As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB
            Dim lnq As New TbRoleCenLinqDB
            ret = lnq.ChkDataByWhere(" role_name = '" & Role & "' and id <>'" & id & "'", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return ret
        End Function


        Public Function SaveUserShop(ByVal ShopID As String, ByVal UserID As String, ByVal UserName As String, ByVal trans As TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New TbUserShopCenLinqDB
            lnq.ChkDataByTB_SHOP_ID_TB_USER_ID(ShopID, UserID, trans.Trans)

            lnq.TB_SHOP_ID = ShopID
            lnq.TB_USER_ID = UserID

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(UserName, trans.Trans)
            Else
                ret = lnq.InsertData(UserName, trans.Trans)
            End If
            If ret = False Then
                _err = "RoleENG.SaveUserShop : " & lnq.ErrorMessage
            End If
            Return ret
        End Function

        Public Function DeleteUserShopByID(ByVal UserID As String, ByVal ShopID As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New TransactionDB

            Try
                Dim sql As String = "delete from tb_user_shop "
                sql += " where tb_user_id='" & UserID & "' and tb_shop_id='" & ShopID & "'"
                SqlDB.ExecuteNonQuery(sql, trans.Trans)
                trans.CommitTransaction()
                ret = True
            Catch ex As Exception
                trans.RollbackTransaction()
                _err = "RoleENG.DeleteUserShopByID : " & ex.Message
                ret = False
            End Try

            Return ret
        End Function
    End Class
End Namespace
