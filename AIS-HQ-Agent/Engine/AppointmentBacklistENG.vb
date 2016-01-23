Imports Cen = CenLinqDB.TABLE
Imports Engine.Common
Imports System.Windows.Forms
Public Class AppointmentBacklistENG
    Dim _err As String = ""
    Dim _TXT_LOG As TextBox
    Public Property ErrorMessage() As String
        Get
            Return _err
        End Get
        Set(ByVal value As String)
            _err = value
        End Set
    End Property

    Public Sub SetTextLog(ByVal txtLog As TextBox)
        _TXT_LOG = txtLog
    End Sub

    Dim _OldLog As String = ""
    Private Sub PrintLog(ByVal LogDesc As String)
        If _OldLog <> LogDesc Then
            _TXT_LOG.Text += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "  " & LogDesc & vbNewLine & _TXT_LOG.Text
            _OldLog = LogDesc
            Application.DoEvents()
        End If
    End Sub

    Public Sub SetBlacklist(ByVal lblTime As Label)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim csLnq As New Cen.TbShopCenLinqDB
            Dim dt As DataTable = FunctionEng.GetActiveShop()
            If dt.Rows.Count > 0 Then
                Dim WithinDay As String = FunctionEng.GetQisDBConfig("AppointmentWithinDay")   'ภายในกี่วัน
                If WithinDay.Trim <> "" Then

                    Dim tmpDt As New DataTable
                    tmpDt.Columns.Add("customer_id")
                    tmpDt.Columns.Add("siebel_activity_id")
                    tmpDt.Columns.Add("start_slot")

                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim dr As DataRow = dt.Rows(i)
                        Application.DoEvents()
                        Dim shLnq As Cen.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(dr("id"))
                        CheckQueueData(Convert.ToInt16(WithinDay), tmpDt, trans, shLnq, lblTime)
                        shLnq = Nothing
                    Next
                    dt.Dispose()

                    If tmpDt.Rows.Count > 0 Then
                        CreateBacklist(WithinDay, tmpDt, lblTime)
                    End If
                    tmpDt.Dispose()
                End If
            End If
            csLnq = Nothing
            trans.CommitTransaction()
        Else
            PrintLog("BackListENG.SetBlackList : " & trans.ErrorMessage)
        End If
    End Sub

    Private Sub CreateBacklist(ByVal WithinDay As Integer, ByVal tmpDt As DataTable, ByVal lblTime As Label)
        If tmpDt.Rows.Count > 0 Then
            Dim tmpM As New DataTable
            tmpM = tmpDt.DefaultView.ToTable(True, "customer_id")
            If tmpM.Rows.Count > 0 Then
                Dim NoShowQty As Integer = FunctionEng.GetQisDBConfig("AppointmentNoShowQty")  'ผิดนัดเป็นจำนวนกี่ครั้ง
                Dim NoBookDay As Integer = FunctionEng.GetQisDBConfig("NoBookDay") 'จะถูกระงับการจองไปนานกี่วัน

                For Each drM As DataRow In tmpM.Rows
                    tmpDt.DefaultView.RowFilter = " customer_id = '" & drM("customer_id").ToString & "'"
                    If tmpDt.DefaultView.Count >= NoShowQty Then
                        tmpDt.DefaultView.Sort = "start_slot desc"

                        Dim vStartDate As DateTime = FunctionEng.cStrToDateTime(Split(tmpDt.DefaultView(0)("start_slot"), " ")(0), Split(tmpDt.DefaultView(0)("start_slot"), " ")(1))

                        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                        If CheckCurrentBacklist(drM("customer_id"), trans) = False Then
                            'Insert Blacklist
                            Dim bLnq As New CenLinqDB.TABLE.TbAppointmentBlacklistCenLinqDB
                            bLnq.CUSTOMER_ID = drM("customer_id")
                            bLnq.START_DATE = New Date(vStartDate.Year, vStartDate.Month, vStartDate.Day)
                            bLnq.END_DATE = DateAdd(DateInterval.Day, NoBookDay, bLnq.START_DATE.Value)
                            If bLnq.InsertData("BacklistENG.CreateBacklist", trans.Trans) = True Then
                                trans.CommitTransaction()
                            Else
                                trans.RollbackTransaction()
                            End If
                            bLnq = Nothing
                        End If
                        trans.CommitTransaction()
                    End If

                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                    Application.DoEvents()
                Next
            End If
            tmpM.Dispose()
        End If
    End Sub

    Private Sub CheckQueueData(ByVal WithinDay As Integer, ByVal tmpDt As DataTable, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal lblTime As Label)
        'ตรวจสอบคิวที่ NoShow ภายในระยะเวลาที่กำหนด (WithInDay)
        Dim retTrans As Boolean = False
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "BacklistENG.CheckQueueData")
        If shTrans.Trans IsNot Nothing Then
            Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
            Dim sqlRs As String = "select distinct customer_id, siebel_activity_id,start_slot "
            sqlRs += " from TB_APPOINTMENT_CUSTOMER "
            sqlRs += " where active_status = '" & CenParaDB.Common.Utilities.Constant.TbAppointmentCustomer.ActiveStatus.NoShow & "' "
            sqlRs += " and CONVERT(varchar(10),start_slot,120) between CONVERT(varchar(10),DATEADD(day," & (0 - WithinDay) & ", GETDATE()),120) and CONVERT(varchar(10),GETDATE(),120) "
            sqlRs += " order by customer_id"

            Dim dt As DataTable = lnq.GetListBySql(sqlRs, shTrans.Trans)
            shTrans.CommitTransaction()
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim tmpDr As DataRow = tmpDt.NewRow
                    tmpDr("customer_id") = dr("customer_id")
                    tmpDr("siebel_activity_id") = dr("siebel_activity_id")
                    tmpDr("start_slot") = Convert.ToDateTime(dr("start_slot")).ToString("yyyy-MM-dd HH:mm:ss", New Globalization.CultureInfo("en-US"))
                    tmpDt.Rows.Add(tmpDr)

                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                    Application.DoEvents()
                Next
            End If
            dt.Dispose()
            lnq = Nothing
        Else
            PrintLog("BacklistENG.CheckQueueData : " & shTrans.ErrorMessage)
        End If
    End Sub

    Private Function CheckCurrentBacklist(ByVal MobileNo As String, ByVal Trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
        Dim ret As Boolean = False
        Dim lnq As New CenLinqDB.TABLE.TbAppointmentBlacklistCenLinqDB
        Dim whText As String = "customer_id = '" & MobileNo & "' "
        whText += " and convert(varchar(10),getdate(), 120) between convert(varchar(10),start_date, 120) and convert(varchar(10),end_date, 120)"
        Dim dt As DataTable = lnq.GetDataList(whText, "", Trans.Trans)
        If dt.Rows.Count > 0 Then
            ret = True
            dt.Dispose()
        End If
        lnq = Nothing
        Return ret
    End Function
End Class
