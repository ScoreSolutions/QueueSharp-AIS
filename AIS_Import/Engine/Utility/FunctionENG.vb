Imports System.Net
Imports Engine.Utilities
Imports System.Text
Imports System.IO

Namespace Utilities
    Public Class FunctionENG
        Public Shared Function GetQisDBConfig(ByVal ParaName As String) As String
            Dim trans As New TransactionDB
            Dim sql As String = "select config_value from sysconfig where config_name = '" & ParaName & "'"
            Dim dt As DataTable = SqlDB.ExecuteTable(sql, trans.Trans)
            trans.CommitTransaction()
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("config_value")) = False Then
                    Return dt.Rows(0)("config_value").ToString
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        End Function

        Public Shared Sub SendErrorToSMS(ByVal ErrorDesc As String)
            'Dim MobileNo = ""
            'Dim InputPara As String = "MOBILENO=" & MobileNo & "&SMSTXT=" & DateTime.Now.ToString("yyyyMMddHHmmss") & ": IP:" & GetIPAddress() & ErrorDesc

            'Try
            '    Dim SMS_URL1 As String = FunctionENG.GetQisDBConfig("send_sms_url1") & InputPara
            '    Dim webRequest As WebRequest
            '    Dim webresponse As WebResponse
            '    webRequest = webRequest.Create(SMS_URL1)
            '    GetSMSMsg(webRequest, webresponse, InputPara)
            'Catch ex As Exception
            '    Dim SMS_URL2 As String = FunctionENG.GetQisDBConfig("send_sms_url2") & InputPara
            '    Dim webRequest As WebRequest
            '    Dim webresponse As WebResponse
            '    webRequest = webRequest.Create(SMS_URL2)
            '    GetSMSMsg(webRequest, webresponse, InputPara)
            'End Try
        End Sub

        Private Shared Sub GetSMSMsg(ByVal webrequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String)
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
                    If tmp <> "DONE" Then

                    End If
                End If
                Stream.Close()
                Stream = Nothing

            Catch ex As WebException

            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try
        End Sub

        Public Shared Function GetIPAddress() As String
            Dim oAddr As System.Net.IPAddress
            Dim sAddr As String
            With System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName())
                oAddr = New System.Net.IPAddress(.AddressList(0).Address)
                sAddr = oAddr.ToString
            End With
            GetIPAddress = sAddr
        End Function

        Public Shared Function SaveTransLog(ByVal ClassName As String, ByVal transDesc As String) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New LogTransCenLinqDB
            Dim trans As New TransactionDB(True)
            lnq.LOGIN_HIS_ID = 0
            lnq.TRANS_DESC = transDesc
            lnq.TRANS_DATE = DateTime.Now

            ret = lnq.InsertData(ClassName, trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If

            Return ret
        End Function

        Public Shared Function SaveErrorLog(ByVal ClassName As String, ByVal ErrorDesc As String) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New LogErrorCenLinqDB
            Dim trans As New TransactionDB(True)
            lnq.LOGIN_HIS_ID = 0
            lnq.ERROR_DESC = ErrorDesc
            lnq.ERROR_TIME = DateTime.Now
            ret = lnq.InsertData(ClassName, trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If

            Return ret
        End Function
    End Class
End Namespace

