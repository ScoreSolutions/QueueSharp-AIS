Imports System.Data
Imports Engine.Common

Public Class CompareDataENG

    Dim _MailMsg As String = ""
    Public Property MailMessage() As String
        Get
            Return _MailMsg.Trim
        End Get
        Set(ByVal value As String)
            _MailMsg = value
        End Set
    End Property

    Public Sub SendAlertEmail(ByVal MailSjb As String)
        Dim gw As New Engine.GateWay.GateWayServiceENG
        Dim dt As New DataTable
        Dim lnq As New CenLinqDB.TABLE.TbEmailBatchAlertCenLinqDB
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        dt = lnq.GetDataList("convert(varchar(8),getdate(),112) between convert(varchar(8),ef_date,112) and convert(varchar(8),isnull(ep_date,getdate()),112) ", "", trans.Trans)

        If dt.Rows.Count > 0 Then
            Dim tmp As String = "<font color='#0000CC'  size='5px' ><b>Queue transaction จำนวนไม่เท่ากันที่</b></font>"

            Dim LoopStep As Integer = 50
            Dim LoopCount As Integer = Math.Ceiling(dt.Rows.Count / LoopStep)
            For i As Integer = 0 To LoopCount - 1
                Dim MailTo As String = ""
                For j As Integer = 0 To LoopStep - 1
                    Dim RowIndex As Integer = j + (LoopStep * i)
                    If RowIndex < dt.Rows.Count Then
                        Dim dr As DataRow = dt.Rows(RowIndex)
                        If MailTo = "" Then
                            MailTo = dr("email_addr").ToString
                        Else
                            MailTo += ";" & dr("email_addr").ToString
                        End If
                    End If
                Next

                gw.SendEmail(MailTo, MailSjb, tmp & _MailMsg)
            Next
        End If
        dt = Nothing
        trans.CommitTransaction()
        lnq = Nothing
        gw = Nothing
    End Sub

#Region "Before"
    Public Sub CompareBeforeCutOffByShopID(ByVal ShopID As Long, ByVal ServiceDate As DateTime)
        Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
        CompareBeforeCutOff(shLnq, ServiceDate)
        shLnq = Nothing
    End Sub
    Public Sub CompareBeforeCutOff(ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal ServiceDate As DateTime)
        Dim dtM As New DataTable
        Dim dtD As New DataTable
        Dim mTrans As New ShLinqDB.Common.Utilities.TransactionDB
        If mTrans.CreateTransaction(shLnq.MAIN_SERVERNAME, shLnq.MAIN_DB_USERID, shLnq.MAIN_DB_PWD, shLnq.MAIN_DB_NAME) = True Then
            Dim qLnq As New ShLinqDB.TABLE.TbCounterQueueShLinqDB
            dtM = qLnq.GetDataList("DATEDIFF(day,service_date,getdate())=0", "", mTrans.Trans)
            qLnq = Nothing
            Dim MaxHistoryID As Long = GetMaxHistoryID(mTrans)
            mTrans.CommitTransaction()

            SaveTempBeforeCutoff(dtM.Rows.Count, shLnq.ID, ServiceDate, MaxHistoryID)
        End If

        Dim dTrans As New ShLinqDB.Common.Utilities.TransactionDB
        If dTrans.CreateTransaction(shLnq.SHOP_DB_SERVER, shLnq.SHOP_DB_USERID, shLnq.SHOP_DB_PWD, shLnq.SHOP_DB_NAME) = True Then
            Dim qLnq As New ShLinqDB.TABLE.TbCounterQueueShLinqDB
            dtD = qLnq.GetDataList("DATEDIFF(day,service_date,getdate())=0", "", dTrans.Trans)
            qLnq = Nothing
            dTrans.CommitTransaction()
        End If

        If dtM.Rows.Count <> dtD.Rows.Count Then
            _MailMsg += "<br />" & shLnq.SHOP_NAME_EN & "(Different " & (dtM.Rows.Count - dtD.Rows.Count) & " Records)"
        End If
    End Sub

    Private Sub SaveTempBeforeCutoff(ByVal RecCountBefore As Integer, ByVal ShopID As Long, ByVal ServiceDate As DateTime, ByVal MaxHistoryID As Long)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim lnq As New CenLinqDB.TABLE.TbTempCompareTransactionCenLinqDB
        Dim dt As New DataTable
        dt = lnq.GetDataList("convert(varchar(8),service_date,112)='" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "' and shop_id = '" & ShopID & "'", "", trans.Trans)
        If dt.Rows.Count > 0 Then
            lnq.GetDataByPK(dt.Rows(0)("id"), trans.Trans)
        End If

        lnq.SHOP_ID = ShopID
        lnq.SERVICE_DATE = New Date(ServiceDate.Year, ServiceDate.Month, ServiceDate.Day)
        lnq.COUNT_BEFORE = RecCountBefore
        lnq.COUNT_AFTER = 0
        lnq.STATUS = "1"
        lnq.LAST_HISTORY_ID = MaxHistoryID

        Dim ret As Boolean = False
        If lnq.ID <> 0 Then
            ret = lnq.UpdateByPK("CutOffDataENG.SaveTempBeforeCutoff", trans.Trans)
        Else
            ret = lnq.InsertData("CutOffDataENG.SaveTempBeforeCutoff", trans.Trans)
        End If

        If ret = True Then
            trans.CommitTransaction()
        Else
            trans.RollbackTransaction()
        End If
        dt = Nothing
        lnq = Nothing
    End Sub

    Private Function GetMaxHistoryID(ByVal mTrans As ShLinqDB.Common.Utilities.TransactionDB) As Long
        Dim ret As Long = 0
        Dim sql As String = "select max(id) max_id from tb_counter_queue_history "
        Dim dt As New DataTable
        Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
        dt = lnq.GetListBySql(sql, mTrans.Trans)
        If dt.Rows.Count > 0 Then
            If Convert.IsDBNull(dt.Rows(0)("max_id")) = False Then
                ret = Convert.ToInt64(dt.Rows(0)("max_id"))
            End If
        End If

        Return ret
    End Function
#End Region


#Region "After"
    Public Sub CompareAfterCutOffByShopID(ByVal ServiceDate As DateTime, ByVal ShopID As Long)
        Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
        CompareAfterCutOff(ServiceDate, shLnq)
        shLnq = Nothing
    End Sub
    Private Sub CompareAfterCutOff(ByVal ServiceDate As DateTime, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
        Dim ServDate As String = ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim dt As New DataTable
        Dim lnq As New CenLinqDB.TABLE.TbTempCompareTransactionCenLinqDB
        dt = lnq.GetDataList("convert(varchar(8),service_date,112)='" & ServDate & "' and status='2' and shop_id = '" & shLnq.ID & "'", "", trans.Trans)
        If dt.Rows.Count > 0 Then
            lnq.GetDataByPK(dt.Rows(0)("id"), trans.Trans)
            If lnq.COUNT_BEFORE <> lnq.COUNT_AFTER Then
                _MailMsg += "<br />" & shLnq.SHOP_NAME_EN & "(Different " & (lnq.COUNT_BEFORE - lnq.COUNT_AFTER) & " Records)"
            End If

            dt = Nothing
            lnq = Nothing
        End If
        trans.CommitTransaction()
    End Sub

    Public Sub SaveTempAfterCutoff(ByVal ShopID As Long, ByVal ServiceDate As DateTime, ByVal RecAfter As Integer)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim dt As New DataTable
        Dim lnq As New CenLinqDB.TABLE.TbTempCompareTransactionCenLinqDB
        dt = lnq.GetDataList("shop_id='" & ShopID & "' and convert(varchar(8),service_date,112)='" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'", "", trans.Trans)
        If dt.Rows.Count > 0 Then
            lnq.GetDataByPK(dt.Rows(0)("id"), trans.Trans)
            If lnq.ID <> 0 Then
                lnq.COUNT_AFTER = RecAfter
                lnq.STATUS = "2"
            End If

            If lnq.UpdateByPK("CutOffDataENG.SaveTempBeforeCutoff", trans.Trans) = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
            dt = Nothing
            lnq = Nothing
        End If
    End Sub



    Public Sub ManualCompareAfterCutOffByShopID(ByVal ServiceDate As DateTime, ByVal ShopID As Long)
        Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
        ManualCompareAfterCutOff(ServiceDate, shLnq)
        shLnq = Nothing
    End Sub

    Private Sub ManualCompareAfterCutOff(ByVal ServiceDate As DateTime, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim dt As New DataTable
        Dim lnq As New CenLinqDB.TABLE.TbTempCompareTransactionCenLinqDB
        dt = lnq.GetDataList("shop_id='" & shLnq.ID & "' and convert(varchar(8),service_date,112)='" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'", "", trans.Trans)
        If dt.Rows.Count > 0 Then
            'If dt.Rows(0)("status").ToString = "1" Then
            lnq.GetDataByPK(dt.Rows(0)("id"), trans.Trans)
            If lnq.ID <> 0 Then
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                If shTrans.CreateTransaction(shLnq.SHOP_DB_SERVER, shLnq.SHOP_DB_USERID, shLnq.SHOP_DB_PWD, shLnq.SHOP_DB_NAME) = True Then
                    Dim hDt As New DataTable
                    Dim hLnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
                    'hDt = hLnq.GetDataList("convert(varchar(8),service_date,112)='" & ServiceDate.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'", "", shTrans.Trans)
                    hDt = hLnq.GetDataList("id between " & (Convert.ToInt64(dt.Rows(0)("last_history_id")) + 1) & " and " & (Convert.ToInt64(dt.Rows(0)("last_history_id")) + Convert.ToInt64(dt.Rows(0)("count_before"))) & "", "", shTrans.Trans)

                    SaveTempAfterCutoff(shLnq.ID, ServiceDate, hDt.Rows.Count)
                    shTrans.CommitTransaction()

                    CompareAfterCutOff(ServiceDate, shLnq)
                End If
            Else
                _MailMsg += "<br />" & shLnq.SHOP_NAME_EN & " No. of temp replicate is zero"
            End If
            'Else
            '    CompareAfterCutOff(ServiceDate, shLnq)
            'End If
        Else
        _MailMsg += "<br />" & shLnq.SHOP_NAME_EN & " No. of temp replicate is zero"
        End If
        trans.CommitTransaction()
        dt = Nothing
        lnq = Nothing
    End Sub
#End Region

End Class
