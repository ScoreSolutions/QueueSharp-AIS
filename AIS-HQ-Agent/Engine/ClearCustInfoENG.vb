Imports Cen = CenLinqDB.TABLE
Imports Engine.Common
Imports System.Windows.Forms

Public Class ClearCustInfoENG
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

    Public Sub ClearCustInfo()
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim csLnq As New Cen.TbShopCenLinqDB
            Dim dt As DataTable = FunctionEng.GetActiveShop()
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Application.DoEvents()
                    Dim shLnq As Cen.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(dr("id"))
                    ClearCustomerInfo(trans, shLnq)
                Next
            End If
            trans.CommitTransaction()
        End If
    End Sub

    Private Sub ClearCustomerInfo(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ClearCustomerInfo.ClearCustomerInfo")
        If shTrans.Trans IsNot Nothing Then
            Dim CloseTime As String = Today.ToString("yyyyMMdd") & " " & FunctionEng.GetShopConfig("s_close", shLnq)
            Dim CurrDate As String = Today.Now.ToString("yyyyMMdd HH:mm")

            If CloseTime < CurrDate Then
                'ถ้าเลยเวลาที่ Shop ปิดแล้ว ค่อยทำนะครับ

                Dim lnq As New ShLinqDB.TABLE.TbCounterQueueShLinqDB
                'Dim sql As String = "select distinct customer_id from tb_counter_queue where convert(varchar,service_date,112) = convert(varchar,getdate(),112) "
                Dim sql As String = "select distinct customer_id from tb_counter_queue "
                Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)
                shTrans.CommitTransaction()
                If dt.Rows.Count > 0 Then
                    FunctionEng.SaveTransLog("ClearCustInfoENG.ClearCustInfo", "Start Clear Customer Profile on Shop " & shLnq.SHOP_NAME_EN)
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i)("customer_id").ToString.Trim() <> "" Then
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ClearCustomerInfo.ClearCustomerInfo")
                            If shTrans.Trans IsNot Nothing Then
                                Dim custLnq As New ShLinqDB.TABLE.TbCustomerShLinqDB
                                If custLnq.ChkDataByMOBILE_NO(dt.Rows(i)("customer_id"), shTrans.Trans) = True Then
                                    custLnq.TITLE_NAME = ""
                                    custLnq.F_NAME = ""
                                    custLnq.L_NAME = ""
                                    custLnq.MOBILE_STATUS = ""
                                    custLnq.EMAIL = ""
                                    custLnq.BIRTH_DATE = ""
                                    custLnq.CATEGORY = ""
                                    custLnq.ACCOUNT_BALANCE = ""
                                    custLnq.CONTACT_CLASS = ""
                                    custLnq.CHUM_SCORE = ""
                                    custLnq.CAMPAIGN_CODE = ""
                                    custLnq.CAMPAIGN_NAME = ""
                                    custLnq.CAMPAIGN_NAME_EN = ""
                                    custLnq.CAMPAIGN_DESC = ""
                                    custLnq.CAMPAIGN_DESC_TH2 = ""
                                    custLnq.CAMPAIGN_DESC_EN2 = ""

                                    If custLnq.UpdateByPK(0, shTrans.Trans) = True Then
                                        shTrans.CommitTransaction()
                                    Else
                                        shTrans.RollbackTransaction()
                                        FunctionEng.SaveErrorLog("ClearCustInfoENG.ClearCustomerInfo", custLnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ClearCustInfoENG")
                                    End If
                                End If
                            End If
                        End If
                    Next
                    FunctionEng.SaveTransLog("ClearCustInfoENG.ClearCustInfo", "End Clear Customer Profile on Shop " & shLnq.SHOP_NAME_EN)
                End If
            End If
        Else
            FunctionEng.SaveErrorLog("ClearCustInfoENG.ClearCustomerInfo", shTrans.ErrorMessage, Application.StartupPath & "\ErrorLog\", "ClearCustInfoENG")
        End If
    End Sub

    Dim _OldLog As String = ""
    Private Sub PrintLog(ByVal LogDesc As String)
        If _OldLog <> LogDesc Then
            _TXT_LOG.Text += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "  " & LogDesc & vbNewLine & _TXT_LOG.Text
            _OldLog = LogDesc
        End If
    End Sub
End Class
