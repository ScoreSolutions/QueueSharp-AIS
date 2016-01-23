Imports System.Data
Partial Class TWCSIWebApp_twPopAddDataForSend
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("id") IsNot Nothing Then
            lblID.Text = Request("id")
            lblServiceID.Text = Request("ServiceID")
            lblShopID.Text = Request("ShopID")
            lblDate.Text = Request("vDate")

            Dim eng As New Engine.CSI.VerifyDataENG
            Dim dt As New DataTable
            dt = eng.GetTWNoResultData(lblDate.Text, lblShopID.Text, lblID.Text, lblServiceID.Text)
            If dt.Rows.Count > 0 Then
                'For Each dr As DataRow In dt.Rows
                'Dim Geng As New Engine.GeteWay.GateWayServiceENG
                'Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(dr("shop_id"))

                ''Queue_unique_id Format = QueueNo + ShopAbb+yyyyMMddHHmmss  EX. A104PK25550326172423
                'Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                'Dim iLnq As New CenLinqDB.TABLE.TbFilterDataCenLinqDB
                'iLnq.GetDataByPK(dr("id"), trans.Trans)
                'Dim DateNow As String = DateTime.Now.ToString("yyyyMMddHHmmss") & DateTime.Now.Millisecond
                'iLnq.QUEUE_UNIQUE_ID = iLnq.QUEUE_NO & shLnq.SHOP_ABB & DateNow

                'If iLnq.UpdateByPK("CSIWebApp_frmPopAddDataForSend.Page_Load", trans.Trans) = True Then
                '    If Geng.SendATSR(dr("mobile_no"), iLnq.QUEUE_UNIQUE_ID, shLnq.SHOP_ABB, dr("template_code")) = True Then
                '        iLnq.SEND_TIME = DateTime.Now
                '        iLnq.RESULT_STATUS = "1"

                '        If iLnq.UpdateByPK("CSIWebApp_frmPopAddDataForSend.Page_Load", trans.Trans) = True Then
                '            trans.CommitTransaction()
                '        Else
                '            trans.RollbackTransaction()
                '            _err = "ID= " & iLnq.ID & " MobileNO=" & iLnq.MOBILE_NO & " " & iLnq.ErrorMessage
                '            Engine.Common.FunctionEng.SaveErrorLog("CSIWebApp_frmPopAddDataForSend.Page_Load", _err)

                '            ret = False
                '            Exit For
                '        End If
                '    End If
                'Else
                '    trans.RollbackTransaction()
                '    _err = "ID= " & iLnq.ID & " MobileNO=" & iLnq.MOBILE_NO & " " & iLnq.ErrorMessage
                '    Engine.Common.FunctionEng.SaveErrorLog("CSIWebApp_frmPopAddDataForSend.Page_Load", _err)

                '    ret = False
                '    Exit For
                'End If
                'Next

                dt.Columns.Add("no")
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("no") = (i + 1)
                Next

                gvResultList.DataSource = dt
                gvResultList.DataBind()
                dt = Nothing
            End If

            Dim txt As String = ""
            ' txt += "<input type='button' value=' Close ' class='formDialog' onclick='window.returnValue=true;window.close();' />"
            txt += "<input type='button' value=' Close ' class='formDialog' onclick='window.close();' />"
            lblShowResult.Text = txt
        End If
    End Sub
End Class
