Imports Engine.Common
Imports System.Windows.Forms

Public Class UpdateCustomerCoorperateENG
    Public Function ProcUpdateCustomerCoorperateTypeByShop(ByVal ShopID As Long, ByVal lblTime As Label, ByVal Prog As ProgressBar, ByVal lblStatus As Label) As Boolean
        Dim ret As Boolean = False
        Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        If shTrans.CreateTransaction(shLnq.MAIN_SERVERNAME, shLnq.MAIN_DB_USERID, shLnq.MAIN_DB_PWD, shLnq.MAIN_DB_NAME) = True Then
            UpdateCustomerData(shLnq, lblTime, shTrans, Prog, lblStatus)
            shTrans.CommitTransaction()

            Dim gw As New Engine.GateWay.GateWayServiceENG
            gw.SendSMS("0897682500", "Load Customer Corperate at " & shLnq.SHOP_ABB & " Complete")
            gw = Nothing
        End If
        shTrans = Nothing
        shLnq = Nothing

        Return ret
    End Function

    Private Sub UpdateCustomerData(ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal lblTime As Label, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal Prog As ProgressBar, ByVal lblStatus As Label)
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB

        If shTrans.Trans IsNot Nothing Then
            Dim lnq As New ShLinqDB.TABLE.TbCounterQueueHistoryShLinqDB
            Dim sql As String = "select distinct mobile_no "
            sql += " from tb_customer "
            sql += " where mobile_no<>''"

            Dim dt As DataTable = lnq.GetListBySql(sql, shTrans.Trans)
            shTrans.CommitTransaction()
            If dt.Rows.Count > 0 Then
                Prog.Value = 0
                Prog.Maximum = 0
                Prog.Maximum = dt.Rows.Count
                lblStatus.Text = ""
                For Each dr As DataRow In dt.Rows
                    Dim gw As New Engine.GateWay.GateWayServiceENG
                    Dim cPara As CenParaDB.TABLE.TbCustomerCenParaDB = gw.GetCustomerProfile(dr("mobile_no"))
                    gw = Nothing

                    If cPara.RESPONSE_MSG.Trim = "" Then
                        If shTrans.CreateTransaction(shLnq.MAIN_SERVERNAME, shLnq.MAIN_DB_USERID, shLnq.MAIN_DB_PWD, shLnq.MAIN_DB_NAME) = True Then
                            Dim sh As New ShLinqDB.TABLE.TbCustomerShLinqDB
                            sh.ChkDataByMOBILE_NO(cPara.MOBILE_NO, shTrans.Trans)
                            sh.CATEGORY = cPara.CATEGORY
                            sh.CORPORATE_TYPE = cPara.CORPORATE_TYPE
                            If sh.ID <> 0 Then
                                If sh.UpdateByPK("UpdateCustomerCoorperateENG.UpdateCustomerData", shTrans.Trans) = True Then
                                    shTrans.CommitTransaction()
                                Else
                                    shTrans.RollbackTransaction()
                                End If
                            End If
                            sh = Nothing
                        End If
                    End If
                    cPara = Nothing

                    Prog.Value += 1
                    lblStatus.Text = "Rows " & Prog.Value & " Total :" & Prog.Maximum & " Rows"

                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss")
                    Application.DoEvents()
                Next
            End If
            dt.Dispose()
            lnq = Nothing
        End If
        shTrans = Nothing
    End Sub
End Class
