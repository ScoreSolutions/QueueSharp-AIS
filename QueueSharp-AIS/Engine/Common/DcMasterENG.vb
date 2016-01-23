Namespace Common
    Public Class DcMasterENG
        Public Shared Function GetMasterItemList(ByVal WhText As String, ByVal orderBy As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbItemCenLinqDB
                dt = lnq.GetDataList(WhText, orderBy, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            dt.TableName = "GetMasterItemList"
            Return dt
        End Function
        Public Shared Function GetMasterItemByID(ByVal MasterID As String) As CenParaDB.TABLE.TbItemCenParaDB
            Dim ret As New CenParaDB.TABLE.TbItemCenParaDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbItemCenLinqDB
                ret = lnq.GetParameter(MasterID, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        'Public Shared Function GetMasterSegmentList(ByVal WhText As String, ByVal orderBy As String) As DataTable
        '    Dim dt As New DataTable
        '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '    If trans.Trans IsNot Nothing Then
        '        Dim lnq As New CenLinqDB.TABLE.TbSegmentCenLinqDB
        '        dt = lnq.GetDataList(WhText, orderBy, trans.Trans)
        '        lnq = Nothing
        '        trans.CommitTransaction()
        '    End If
        '    dt.TableName = "GetMasterSegmentList"
        '    Return dt
        'End Function

        Public Shared Function GetMasterCustomerTypeList(ByVal WhText As String, ByVal orderBy As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbCustomertypeCenLinqDB
                dt = lnq.GetDataList(WhText, orderBy, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            dt.TableName = "GetMasterCustomerTypeList"
            Return dt
        End Function
        Public Shared Function GetMasterCustomerTypeByID(ByVal MasterID As String) As CenParaDB.TABLE.TbCustomertypeCenParaDB
            Dim ret As New CenParaDB.TABLE.TbCustomertypeCenParaDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbCustomertypeCenLinqDB
                ret = lnq.GetParameter(MasterID, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        Public Shared Function GetMasterGroupUserList(ByVal WhText As String, ByVal orderBy As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbGroupUserCenLinqDB
                dt = lnq.GetDataList(WhText, orderBy, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            dt.TableName = "GetMasterGroupuserList"
            Return dt
        End Function
        Public Shared Function GetMasterGroupUserByID(ByVal MasterID As String) As CenParaDB.TABLE.TbGroupuserCenParaDB
            Dim ret As New CenParaDB.TABLE.TbGroupuserCenParaDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbGroupuserCenLinqDB
                ret = lnq.GetParameter(MasterID, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        Public Shared Function GetMasterSkillList(ByVal WhText As String, ByVal orderBy As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbSkillCenLinqDB
                dt = lnq.GetDataList(WhText, orderBy, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            dt.TableName = "GetMasterSkillList"
            Return dt
        End Function
        Public Shared Function GetMasterSkillByID(ByVal MasterID As String) As CenParaDB.TABLE.TbSkillCenParaDB
            Dim ret As New CenParaDB.TABLE.TbSkillCenParaDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbSkillCenLinqDB
                ret = lnq.GetParameter(MasterID, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        Public Shared Function GetMasterSkillItemList(ByVal MasterSkillID As String) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("master_itemid")
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbSkillItemCenLinqDB
                Dim dt As New DataTable
                dt = lnq.GetDataList("skill_id='" & MasterSkillID & "'", "", trans.Trans)
                For Each dr As DataRow In dt.Rows
                    Dim rdr As DataRow = ret.NewRow
                    rdr("master_itemid") = dr("item_id")
                    ret.Rows.Add(rdr)
                Next
                dt.Dispose()
                lnq = Nothing
                trans.CommitTransaction()
            End If
            ret.TableName = "GetMasterSkillItemList"
            Return ret
        End Function

        Public Shared Function GetMasterHoldReasonList(ByVal WhText As String, ByVal orderBy As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbHoldReasonCenLinqDB
                dt = lnq.GetDataList(WhText, orderBy, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            dt.TableName = "GetMasterHoldReasonList"
            Return dt
        End Function
        Public Shared Function GetMasterHoldReasonByID(ByVal MasterID As String) As CenParaDB.TABLE.TbHoldReasonCenParaDB
            Dim ret As New CenParaDB.TABLE.TbHoldReasonCenParaDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbHoldReasonCenLinqDB
                ret = lnq.GetParameter(MasterID, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        Public Shared Function GetMasterLogoutReasonList(ByVal WhText As String, ByVal orderBy As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbLogoutReasonCenLinqDB
                dt = lnq.GetDataList(WhText, orderBy, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            dt.TableName = "GetMasterLogoutReasonList"
            Return dt
        End Function
        Public Shared Function GetMasterLogoutReasonByID(ByVal MasterID As String) As CenParaDB.TABLE.TbLogoutReasonCenParaDB
            Dim ret As New CenParaDB.TABLE.TbLogoutReasonCenParaDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbLogoutReasonCenLinqDB
                ret = lnq.GetParameter(MasterID, trans.Trans)
                lnq = Nothing
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        Public Shared Function GetAppointmentPolicy() As String
            Dim ret As String = ""
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim sql As String = "select config_name, config_value from sysconfig where config_name in ('AppointmentNoShowQty','AppointmentWithinDay','NoBookDay')"
                Dim dt As New DataTable
                dt = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, trans.Trans)
                If dt.Rows.Count = 3 Then
                    dt.DefaultView.RowFilter = "config_name='AppointmentNoShowQty'"
                    If dt.DefaultView.Count = 1 Then
                        ret = dt.DefaultView(0)("config_value").ToString
                    End If

                    dt.DefaultView.RowFilter = "config_name='AppointmentWithinDay'"
                    If dt.DefaultView.Count = 1 Then
                        ret += "#" & dt.DefaultView(0)("config_value").ToString
                    End If

                    dt.DefaultView.RowFilter = "config_name='NoBookDay'"
                    If dt.DefaultView.Count = 1 Then
                        ret += "#" & dt.DefaultView(0)("config_value").ToString
                    End If
                End If
                dt.Dispose()
            End If

            If Split(ret, "#").Length <> 3 Then
                ret = ""
            End If

            Return ret
        End Function
    End Class
End Namespace

