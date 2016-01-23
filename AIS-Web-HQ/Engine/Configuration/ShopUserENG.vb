Imports Engine.Common
Imports ShLinqDB.Common.Utilities

Namespace Configuration
    Public Class ShopUserENG

        Public Function GetShopTitleList(ByVal ShopID As Long) As DataTable
            Dim dt As New DataTable
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(ShopID, "ShopUserEng.GetShopTitleList")
            If shTrans.Trans IsNot Nothing Then
                Dim tLnq As New ShLinqDB.TABLE.TbTitleShLinqDB
                dt = tLnq.GetDataList("1=1", "title_name", shTrans.Trans)
                shTrans.CommitTransaction()
                tLnq = Nothing
            End If

            Return dt
        End Function

        Public Function GetShopGroupUserList(ByVal ShopID As Long) As DataTable
            Dim dt As New DataTable
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(ShopID, "ShopUserEng.GetShopGroupUserList")
            If shTrans.Trans IsNot Nothing Then
                Dim tLnq As New ShLinqDB.TABLE.TbGroupuserShLinqDB
                dt = tLnq.GetDataList("active_status='1'", "group_name", shTrans.Trans)
                shTrans.CommitTransaction()
                tLnq = Nothing
            End If

            Return dt
        End Function



        Public Function GetShopUser(ByVal ShopID As Long) As DataTable
            Dim dt As New DataTable
            Dim sql As String = "select u.id,t.title_name + u.fname + ' ' + u.lname as fullname, u.id user_id, "
            sql += " u.fname + ' ' + u.lname + ' ' + u.user_code fullname2,u.user_code"
            sql += vbCrLf & " from TB_USER u "
            sql += vbCrLf & " left join TB_TITLE t on u.title_id=t.id"
            sql += vbCrLf & " where u.active_status='1' "
            sql += vbCrLf & " order by u.fname,u.lname"

            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopUserEng.GetShopUser")
            Return dt
        End Function

        Public Function GetShopUser2(ByVal ShopID As Long) As DataTable
            Dim dt As New DataTable
            Dim sql As String = "select u.username,u.fname + ' ' + u.lname as fullname "
            sql += vbCrLf & " from TB_USER u "
            sql += vbCrLf & " left join TB_TITLE t on u.title_id=t.id"
            sql += vbCrLf & " where u.active_status='1' "
            sql += vbCrLf & " order by u.fname,u.lname"

            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopUserEng.GetShopUser2")
            Return dt
        End Function

        Public Function GetShopAllUser(ByVal ShopID As Long) As DataTable
            Dim dt As New DataTable
            Dim sql As String = "select u.id as userid, u.username,u.fname + ' ' + u.lname as fullname "
            sql += vbCrLf & " from TB_USER u "
            sql += vbCrLf & " left join TB_TITLE t on u.title_id=t.id"
            sql += vbCrLf & " order by u.fname,u.lname"

            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopUserEng.GetShopAllUser")
            Return dt
        End Function

        Public Function GetShopUserByUserName(ByVal ShopID As Long, ByVal username As String) As DataTable
            Dim dt As New DataTable
            Dim sql As String = "select u.username,t.title_name + u.fname + ' ' + u.lname as fullname "
            sql += vbCrLf & " from TB_USER u "
            sql += vbCrLf & " left join TB_TITLE t on u.title_id=t.id"
            sql += vbCrLf & " where u.active_status='1'"
            If username <> "" Then
                sql += vbCrLf & " and username='" & username & "'"
            End If
            sql += vbCrLf & " order by u.fname,u.lname"

            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopUserEng.GetShopUser2")
            Return dt
        End Function

        Public Function GetShopUserNotAdmin(ByVal ShopID As Long) As DataTable
            Dim dt As New DataTable
            Dim sql As String = "select u.username,u.fname + ' ' + u.lname as fullname "
            sql += vbCrLf & " from TB_USER u "
            sql += vbCrLf & " left join TB_TITLE t on u.title_id=t.id"
            sql += vbCrLf & " where u.active_status='1' and lower(username) <> 'admin'"
            sql += vbCrLf & " order by u.fname,u.lname"

            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopUserEng.GetShopUser2")
            Return dt
        End Function

        Public Function GetShopAllUserNotAdmin(ByVal ShopID As Long) As DataTable
            Dim dt As New DataTable
            Dim sql As String = "select u.username,u.fname + ' ' + u.lname as fullname "
            sql += vbCrLf & " from TB_USER u "
            sql += vbCrLf & " left join TB_TITLE t on u.title_id=t.id"
            sql += vbCrLf & " where lower(username) <> 'admin'"
            sql += vbCrLf & " order by u.fname,u.lname"

            dt = Engine.Common.FunctionEng.ExecuteShopSQL(sql, ShopID, "ShopUserEng.GetShopUser2")
            Return dt
        End Function

        Public Function GetShopUserPara(ByVal ShopID As Long, ByVal UserID As Long) As ShParaDB.TABLE.TbUserShParaDB
            Dim ret As New ShParaDB.TABLE.TbUserShParaDB
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(ShopID, "ShopUserEng.GetShopUserPara")
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New ShLinqDB.TABLE.TbUserShLinqDB
                ret = lnq.GetParameter(UserID, shTrans.Trans)
                lnq = Nothing
                shTrans.CommitTransaction()
            End If

            Return ret
        End Function

        Public Function GetShopUserSkill(ByVal ShopID As Long, ByVal UserID As Long) As DataTable
            Dim ret As New DataTable
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(ShopID, "ShopUserEng.GetShopUserSkill")
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New ShLinqDB.TABLE.TbUserSkillShLinqDB
                ret = lnq.GetDataList("user_id='" & UserID & "'", "", shTrans.Trans)
                lnq = Nothing
                shTrans.CommitTransaction()
            End If

            Return ret
        End Function

    End Class
End Namespace

