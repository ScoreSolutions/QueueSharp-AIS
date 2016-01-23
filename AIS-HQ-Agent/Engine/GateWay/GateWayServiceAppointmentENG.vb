Imports System.Text
Imports CenParaDB.Common.Utilities
Imports Engine.Common
Imports System.Net
Imports System.IO
Imports System.Collections.Specialized

Namespace GateWay
    Public Class GateWayServiceAppointmentENG
        Dim _err As String = ""

        Public Function SendEmailAttFile(ByVal EmailTo As String, ByVal MailSubject As String, ByVal msg As String, ByVal file() As String) As Boolean
            Dim ret As Boolean = False
            'Dim MAIL_URL1 As String = "http://10.13.174.95/AISQ/interface/sendemailembedimg.php"
            Dim MAIL_URL1 As String = FunctionEng.GetQisDBConfig("SendMailEmbdURL1") '"http://10.13.181.100/AISQ/interface/sendemailembedimg.php"
            Dim tmp As String = BuiltMailAttFile(EmailTo, MailSubject, msg, MAIL_URL1, file)
            If tmp.Trim = "DONE" Then
                ret = True
                FunctionEng.SaveTransLog("GateWayServiceENG.SendEmailAttFile", MAIL_URL1)
            Else
                FunctionEng.SaveErrorLog("GateWayServiceENG.SendEmailAttFile", "Send Email Fail" & " URL :" & MAIL_URL1)

                Dim MAIL_URL2 As String = FunctionEng.GetQisDBConfig("SendMailEmbdURL2") '"http://10.13.181.100/AISQ/interface/sendemailembedimg.php"
                tmp = BuiltMailAttFile(EmailTo, MailSubject, msg, MAIL_URL2, file)
                If tmp.Trim = "DONE" Then
                    ret = True
                    FunctionEng.SaveTransLog("GateWayServiceENG.SendEmailAttFile", MAIL_URL2)
                Else
                    ret = False
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendEmailAttFile", "Send Email Fail" & tmp & " URL :" & MAIL_URL1)
                End If
            End If
            Return ret
        End Function

        Private Function BuiltMailAttFile(ByVal EmailTo As String, ByVal MailSubject As String, ByVal msg As String, ByVal MailURL As String, ByVal files() As String) As String
            Dim ret As String = ""
            Dim length As Long = 0
            Dim boundary As String = "----------------------------" & DateTime.Now.Ticks.ToString("x")

            Dim httpWebRequest2 As HttpWebRequest = DirectCast(WebRequest.Create(MailURL), HttpWebRequest)
            httpWebRequest2.ContentType = "multipart/form-data; boundary=" & boundary
            httpWebRequest2.Method = "POST"
            httpWebRequest2.KeepAlive = True
            httpWebRequest2.Credentials = System.Net.CredentialCache.DefaultCredentials

            Dim memStream As Stream = New System.IO.MemoryStream()
            Dim boundarybytes As Byte() = System.Text.Encoding.ASCII.GetBytes(vbCr & vbLf & "--" & boundary & vbCr & vbLf)
            Dim formdataTemplate As String = vbCr & vbLf & "--" & boundary & vbCr & vbLf & "Content-Disposition: form-data; name={0};" & vbCr & vbLf & vbCr & vbLf & "{1}"

            Dim nvc As New NameValueCollection
            nvc.Add("ADDRTO", EmailTo)
            nvc.Add("MAILSUB", MailSubject)
            nvc.Add("MAILMSG", msg)
            For Each key As String In nvc.Keys

                'Dim formitem As String = String.Format(formdataTemplate, key, nvc(key))
                'Dim formitembytes As Byte() = System.Text.Encoding.UTF8.GetBytes(formitem)
                'memStream.Write(formitembytes, 0, formitembytes.Length)

                Dim thaiEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-11")
                Dim formitem As String = String.Format(formdataTemplate, key, nvc(key))
                Dim formitembytes As Byte() = thaiEnc.GetBytes(formitem)
                memStream.Write(formitembytes, 0, formitembytes.Length)
            Next
            memStream.Write(boundarybytes, 0, boundarybytes.Length)

            Dim headerTemplate As String = "Content-Disposition: form-data; name={0}; filename={1}" & vbCr & vbLf & " Content-Type: application/octet-stream" & vbCr & vbLf & vbCr & vbLf
            For i As Integer = 0 To files.Length - 1
                Dim header As String = String.Format(headerTemplate, "ATTIMG" & (i + 1), files(i))
                Dim headerbytes As Byte() = System.Text.Encoding.UTF8.GetBytes(header)
                memStream.Write(headerbytes, 0, headerbytes.Length)
                Dim fileStream As New FileStream(files(i), FileMode.Open, FileAccess.Read)
                Dim buffer As Byte() = New Byte(fileStream.Length - 1) {}
                Dim bytesRead As Integer = 0
                Do
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length)
                    memStream.Write(buffer, 0, bytesRead)
                Loop While bytesRead <> 0
                memStream.Write(boundarybytes, 0, boundarybytes.Length)
                fileStream.Close()
            Next

            httpWebRequest2.ContentLength = memStream.Length
            Dim requestStream As Stream = httpWebRequest2.GetRequestStream()
            memStream.Position = 0
            Dim tempBuffer As Byte() = New Byte(memStream.Length - 1) {}
            memStream.Read(tempBuffer, 0, tempBuffer.Length)
            memStream.Close()
            requestStream.Write(tempBuffer, 0, tempBuffer.Length)
            requestStream.Close()

            Dim webResponse2 As WebResponse = httpWebRequest2.GetResponse()
            Dim stream2 As Stream = webResponse2.GetResponseStream()
            Dim reader2 As New StreamReader(stream2)
            ret = reader2.ReadToEnd

            webResponse2.Close()
            httpWebRequest2 = Nothing
            webResponse2 = Nothing

            Return ret
        End Function

#Region "Siebel"

        Public Function SiebelCreateActivity(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal ItemName As String, ByVal AppointmentChannel As String, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As CenParaDB.Appointment.SiebelResponsePara
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara
            Dim SubCat As String = ""
            If AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.Kiosk.CatID Then
                SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.Kiosk.CatName
            ElseIf AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.eService.CatID Then
                SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.eService.CatName
            ElseIf AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.CallInbound.CatID Then
                SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.CallInbound.CatName
            ElseIf AppointmentChannel = Constant.TbAppointmentCustomer.Siebel.SubCatategory.MobileApp.CatID Then
                SubCat = Constant.TbAppointmentCustomer.Siebel.SubCatategory.MobileApp.CatName
            End If

            Dim sName() As String = Split(ItemName, ",")
            Dim tmpStr As String = ""
            For Each Str As String In sName
                If tmpStr = "" Then
                    tmpStr = "  - " & Str
                Else
                    tmpStr += vbNewLine & "  - " & Str
                End If
            Next

            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "GateWayServiceAppointmentENG.SiebelCreateActivity")

            Dim SiebelDesc As String = ""
            SiebelDesc += " จองคิว รับบริการ" & vbNewLine & tmpStr & vbNewLine
            SiebelDesc += " วันที่ " & StartSlot.ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น."
            SiebelDesc += " สาขา" & FunctionEng.GetShopConfig("s_name_th", shTrans)

            Dim InputPara As String = ""
            InputPara += "SIEBEL_TYPE=Appointment"
            InputPara += "&SIEBEL_ACTIVITYCATEGORY=Appointment"
            InputPara += "&SIEBEL_ACTIVITYSUBCATEGORY=" & SubCat
            InputPara += "&SIEBEL_DESCRIPTION=" & SiebelDesc
            InputPara += "&SIEBEL_STATUS=" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN
            InputPara += "&SIEBEL_MOBILENO=" & MobileNo

            Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
            ret = CallWebService(InputPara)
            If ret.RESULT = True Then
                'Update To shop.TB_APPOINTMENT_CUSTOMER
                Dim sqlU As String = "update tb_appointment_customer "
                sqlU += " set siebel_activity_id='" & ret.ACTIVITY_ID & "', "
                sqlU += " siebel_status = '" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN & "', "
                sqlU += " siebel_desc = '" & SiebelDesc & "'"
                sqlU += " where CONVERT(varchar(16),start_slot,120) = '" & StartSlot.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US")) & "' and customer_id = '" & MobileNo & "' "
                sqlU += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'"

                Dim re As Boolean = lnq.UpdateBySql(sqlU, shTrans.Trans)
                If re = False Then
                    ret.RESULT = False
                    ret.ACTIVITY_ID = ""
                    ret.ErrorMessage = lnq.ErrorMessage

                    shTrans.RollbackTransaction()
                    FunctionEng.SaveErrorLog("ShopWebServiceENG.CreateSiebelActivity", lnq.ErrorMessage & " MobileNo:" & MobileNo)
                Else
                    shTrans.CommitTransaction()
                End If
            Else
                FunctionEng.SaveErrorLog("ShopWebServiceENG.CreateSiebelActivity", ret.ErrorMessage & " MobileNo:" & MobileNo)

                Dim sqlE As String = "update tb_appointment_customer "
                sqlE += " set siebel_desc='GenSiebelENG.CreateSiebelActivity : " & ret.ErrorMessage & " MobileNo:" & MobileNo & "' "
                sqlE += " where customer_id = '" & MobileNo & "' and CONVERT(varchar(16),start_slot,120)= '" & StartSlot.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US")) & "'"
                Dim re As Boolean = lnq.UpdateBySql(sqlE, shTrans.Trans)
                If re = True Then
                    shTrans.CommitTransaction()
                Else
                    shTrans.RollbackTransaction()
                End If
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Function SiebelUpdateDescription(ByVal MobileNo As String, ByVal StartSlot As DateTime, ByVal ItemName As String, ByVal AppointmentChannel As String, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal ActivityID As String) As CenParaDB.Appointment.SiebelResponsePara
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara

            Dim sName() As String = Split(ItemName, ",")
            Dim tmpStr As String = ""
            For Each Str As String In sName
                If tmpStr = "" Then
                    tmpStr = "  - " & Str
                Else
                    tmpStr += vbNewLine & "  - " & Str
                End If
            Next

            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "GateWayServiceENG.SiebelUpdateDescription")

            Dim SiebelDesc As String = ""
            SiebelDesc += " จองคิว รับบริการ" & vbNewLine & tmpStr & vbNewLine
            SiebelDesc += " วันที่ " & StartSlot.ToString("dd/MM/yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & "น."
            SiebelDesc += " สาขา" & FunctionEng.GetShopConfig("s_name_th", shTrans)

            Dim InputPara As String = ""
            InputPara += "SIEBEL_ACTID=" & ActivityID
            InputPara += "&SIEBEL_DESCRIPTION=" & SiebelDesc
            InputPara += "&SIEBEL_STATUS=" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN

            Dim lnq As New ShLinqDB.TABLE.TbAppointmentCustomerShLinqDB
            ret = CallWebServiceUpdate(InputPara)
            If ret.RESULT = True Then
                'Update To shop.TB_APPOINTMENT_CUSTOMER
                Dim sqlU As String = "update tb_appointment_customer "
                sqlU += " set siebel_activity_id='" & ret.ACTIVITY_ID & "', "
                sqlU += " siebel_status = '" & Constant.TbAppointmentCustomer.Siebel.Status.OPEN & "', "
                sqlU += " siebel_desc = '" & SiebelDesc & "'"
                sqlU += " where CONVERT(varchar(16),start_slot,120) = '" & StartSlot.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US")) & "' and customer_id = '" & MobileNo & "' "
                sqlU += " and active_status = '" & Constant.TbAppointmentCustomer.ActiveStatus.ConfirmAppointment & "'"

                Dim re As Boolean = lnq.UpdateBySql(sqlU, shTrans.Trans)
                If re = False Then
                    ret.RESULT = False
                    ret.ACTIVITY_ID = ""
                    ret.ErrorMessage = lnq.ErrorMessage

                    shTrans.RollbackTransaction()
                    FunctionEng.SaveErrorLog("ShopWebServiceENG.CreateSiebelActivity", lnq.ErrorMessage & " MobileNo:" & MobileNo)
                Else
                    shTrans.CommitTransaction()
                End If
            Else
                FunctionEng.SaveErrorLog("ShopWebServiceENG.CreateSiebelActivity", ret.ErrorMessage & " MobileNo:" & MobileNo)

                Dim sqlE As String = "update tb_appointment_customer "
                sqlE += " set siebel_desc='GenSiebelENG.CreateSiebelActivity : " & ret.ErrorMessage & " MobileNo:" & MobileNo & "' "
                sqlE += " where customer_id = '" & MobileNo & "' and CONVERT(varchar(16),start_slot,120)= '" & StartSlot.ToString("yyyy-MM-dd HH:mm", New System.Globalization.CultureInfo("en-US")) & "'"
                Dim re As Boolean = lnq.UpdateBySql(sqlE, shTrans.Trans)
                If re = True Then
                    shTrans.CommitTransaction()
                Else
                    shTrans.RollbackTransaction()
                End If
            End If
            lnq = Nothing

            Return ret
        End Function

        Private Function CallWebService(ByVal InputPara As String) As CenParaDB.Appointment.SiebelResponsePara
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara
            Dim SIEBEL_URL1 As String = FunctionEng.GetQisDBConfig("seibel_url1") & InputPara
            Dim webRequest As WebRequest
            Dim webresponse As WebResponse
            webRequest = webRequest.Create(SIEBEL_URL1)
            ret = GetSiebelMsg(webRequest, webresponse, InputPara)

            If ret.RESULT = False Then
                FunctionEng.SaveErrorLog("GateWayServiceENG.CallWebService", ret.ErrorMessage & " " & SIEBEL_URL1)
                Dim SIEBEL_URL2 As String = FunctionEng.GetQisDBConfig("seibel_url2") & InputPara
                webRequest = webRequest.Create(SIEBEL_URL2)
                ret = GetSiebelMsg(webRequest, webresponse, InputPara)

                If ret.RESULT = False Then
                    _err = ret.ErrorMessage
                    FunctionEng.SaveErrorLog("GateWayServiceENG.CallWebService", "GenSiebelENG.CallWebService : " & ret.ErrorMessage & " " & SIEBEL_URL2)
                End If
            End If
            Return ret
        End Function

        Private Function GetSiebelMsg(ByVal webrequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String, Optional ByVal GenType As String = "CREATE") As CenParaDB.Appointment.SiebelResponsePara
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara
            webrequest.ContentType = "application/x-www-form-urlencoded"
            webrequest.Method = "POST"
            Dim thaiEnc As Encoding = Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webrequest.ContentLength = bytes.Length
                os = webrequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webrequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If GenType = "UPDATE" Then
                        If tmp.IndexOf("SUCCESS") > -1 Then
                            ret.RESULT = True
                        Else
                            ret.RESULT = False
                            ret.ErrorMessage = tmp
                        End If
                    Else
                        If tmp.IndexOf("SUCCESS-") > -1 Then
                            ret.RESULT = True
                            ret.ACTIVITY_ID = tmp.Replace("SUCCESS-", "")
                        Else
                            ret.RESULT = False
                            ret.ErrorMessage = tmp
                        End If
                    End If
                End If
                Stream.Close()
                Stream = Nothing

            Catch ex As WebException
                ret.RESULT = False
                ret.ErrorMessage = ex.Message
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try
            Return ret
        End Function

        Private Function CallWebServiceUpdate(ByVal InputPara As String) As CenParaDB.Appointment.SiebelResponsePara
            Dim ret As New CenParaDB.Appointment.SiebelResponsePara
            Dim SIEBEL_URL1 As String = FunctionEng.GetQisDBConfig("seibel_update_url1") & InputPara
            Dim webRequest As WebRequest
            Dim webresponse As WebResponse
            webRequest = webRequest.Create(SIEBEL_URL1)
            ret = GetSiebelMsg(webRequest, webresponse, InputPara, "UPDATE")

            If ret.RESULT = False Then
                FunctionEng.SaveErrorLog("GateWayServiceENG.CallWebServiceUpdate", "ShopWebServiceENG.CallWebServiceUpdate : " & ret.ErrorMessage & " " & SIEBEL_URL1)
                Dim SIEBEL_URL2 As String = FunctionEng.GetQisDBConfig("seibel_update_url2") & InputPara
                webRequest = webRequest.Create(SIEBEL_URL2)
                ret = GetSiebelMsg(webRequest, webresponse, InputPara, "UPDATE")

                If ret.RESULT = False Then
                    _err = ret.ErrorMessage
                    FunctionEng.SaveErrorLog("GateWayServiceENG.CallWebServiceUpdate", "ShopWebServiceENG.CallWebServiceUpdate : " & ret.ErrorMessage & " " & SIEBEL_URL2)
                End If
            End If

            Return ret
        End Function
#End Region
    End Class
End Namespace

