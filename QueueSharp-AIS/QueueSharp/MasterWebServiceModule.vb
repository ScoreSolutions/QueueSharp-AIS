Imports Engine.Common.ShopConnectDBENG
Imports QueueSharp.Org.Mentalis.Files

Module MasterWebServiceModule

#Region "Master ITEM"
    Public Function GetMasterItemList() As DataTable
        Dim dt As New DataTable

        'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
        System.Net.ServicePointManager.ServerCertificateValidationCallback = _
          Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
          chain As System.Security.Cryptography.X509Certificates.X509Chain, _
          sslerror As System.Net.Security.SslPolicyErrors) True

        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"

        Try
            'If PingServer(ini.ReadString("Server")) = True Then
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Timeout = 20000
            ws.Url = GetWebServiceURL(True)
            dt = ws.GetMasterItemList()
            ws = Nothing
            'Else
            'If PingServer(ini.ReadString("Server1")) = True Then
            '    dt = GetMasterItemListFromDisplay()
            'End If
            'End If
        Catch ex As System.Net.WebException
            'If PingServer(ini.ReadString("Server1")) = True Then
            dt = GetMasterItemListFromDisplay()
            'End If
        Catch ex As Exception
            'If PingServer(ini.ReadString("Server1")) = True Then
            dt = GetMasterItemListFromDisplay()
            'End If
        End Try
        ini = Nothing

        Return dt
    End Function

    Private Function GetMasterItemListFromDisplay() As DataTable
        Dim dt As New DataTable
        Try
            Dim ws As New ShopWebServiceDisplay.WebServiceAPI
            ws.Timeout = 20000
            ws.Url = GetWebServiceURL(False)
            dt = ws.GetMasterItemList()
            ws = Nothing
        Catch ex As System.Net.WebException

        End Try
        
        Return dt
    End Function
#End Region

#Region "Master HOLD REASON"
    Public Function GetMasterHoldReasonList() As DataTable
        Dim dt As New DataTable

        'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
        System.Net.ServicePointManager.ServerCertificateValidationCallback = _
          Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
          chain As System.Security.Cryptography.X509Certificates.X509Chain, _
          sslerror As System.Net.Security.SslPolicyErrors) True

        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"

        Try
            'If PingServer(ini.ReadString("Server")) = True Then
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Timeout = 20000
            ws.Url = GetWebServiceURL(True)
            dt = ws.GetMasterHoldReasonList()
            ws = Nothing
            'Else
            'If PingServer(ini.ReadString("Server1")) = True Then
            '    dt = GetMasterHoldReasonListFromDisplay()
            'End If
            'End If
        Catch ex As System.Net.WebException
            'If PingServer(ini.ReadString("Server1")) = True Then
            dt = GetMasterHoldReasonListFromDisplay()
            'End If
        End Try
        ini = Nothing

        Return dt
    End Function

    Private Function GetMasterHoldReasonListFromDisplay() As DataTable
        Dim dt As New DataTable
        Try
            Dim ws As New ShopWebServiceDisplay.WebServiceAPI
            ws.Timeout = 20000
            ws.Url = GetWebServiceURL(False)
            dt = ws.GetMasterHoldReasonList()
            ws = Nothing
        Catch ex As System.Net.WebException

        End Try
        
        Return dt
    End Function
#End Region

#Region "Master LOGOUT REASON"
    Public Function GetMasterLogoutReasonList() As DataTable
        Dim dt As New DataTable

        'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
        System.Net.ServicePointManager.ServerCertificateValidationCallback = _
          Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
          chain As System.Security.Cryptography.X509Certificates.X509Chain, _
          sslerror As System.Net.Security.SslPolicyErrors) True

        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"

        Try
            'If PingServer(ini.ReadString("Server")) = True Then
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Timeout = 20000
            ws.Url = GetWebServiceURL(True)
            dt = ws.GetMasterLogoutReasonList()
            ws = Nothing
            'Else
            'If PingServer(ini.ReadString("Server1")) = True Then
            '    dt = GetMasterLogoutReasonListFromDisplay()
            'End If
            'End If
        Catch ex As System.Net.WebException
            'If PingServer(ini.ReadString("Server1")) = True Then
            dt = GetMasterLogoutReasonListFromDisplay()
            'End If
        End Try
        ini = Nothing

        Return dt
    End Function

    Private Function GetMasterLogoutReasonListFromDisplay() As DataTable
        Dim dt As New DataTable
        Try
            Dim ws As New ShopWebServiceDisplay.WebServiceAPI
            ws.Timeout = 20000
            ws.Url = GetWebServiceURL(False)
            dt = ws.GetMasterLogoutReasonList()
            ws = Nothing
        Catch ex As System.Net.WebException

        End Try
        
        Return dt
    End Function
#End Region

#Region "Master CUSTOMER TYPE"
    Public Function GetMasterCustomerTypeList() As DataTable
        Dim dt As New DataTable

        'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
        System.Net.ServicePointManager.ServerCertificateValidationCallback = _
          Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
          chain As System.Security.Cryptography.X509Certificates.X509Chain, _
          sslerror As System.Net.Security.SslPolicyErrors) True

        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"

        Try
            'If PingServer(ini.ReadString("Server")) = True Then
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Timeout = 20000
            ws.Url = GetWebServiceURL(True)
            dt = ws.GetMasterCustomerTypeList()
            ws = Nothing
            'Else
            'If PingServer(ini.ReadString("Server1")) = True Then
            '    dt = GetMasterCustomerTypeListFromDisplay()
            'End If
            'End If
        Catch ex As System.Net.WebException
            'If PingServer(ini.ReadString("Server1")) = True Then
            dt = GetMasterCustomerTypeListFromDisplay()
            'End If
        End Try
        ini = Nothing

        Return dt
    End Function

    Private Function GetMasterCustomerTypeListFromDisplay() As DataTable
        Dim dt As New DataTable
        Try
            Dim ws As New ShopWebServiceDisplay.WebServiceAPI
            ws.Timeout = 20000
            ws.Url = GetWebServiceURL(False)
            dt = ws.GetMasterCustomerTypeList()
            ws = Nothing
        Catch ex As System.Net.WebException

        End Try
        
        Return dt
    End Function
#End Region

#Region "Master SKILL"
    Public Function GetMasterSkillList() As DataTable
        Dim dt As New DataTable

        'สำหรับ Bypass SSL กรณีเรียก WebService ผ่าน https://
        System.Net.ServicePointManager.ServerCertificateValidationCallback = _
          Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
          chain As System.Security.Cryptography.X509Certificates.X509Chain, _
          sslerror As System.Net.Security.SslPolicyErrors) True

        Dim ini As New IniReader(INIFileName)
        ini.Section = "Setting"
        Try
            'If PingServer(ini.ReadString("Server")) = True Then
            Dim ws As New ShopWebServiceMain.WebServiceAPI
            ws.Timeout = 20000
            ws.Url = GetWebServiceURL(True)
            dt = ws.GetMasterSkillList()
            ws = Nothing
        Catch ex As System.Net.WebException
            'If PingServer(ini.ReadString("Server1")) = True Then
            dt = GetMasterSkillListFromDisplay()
            'End If
        Catch ex As Exception
            'If PingServer(ini.ReadString("Server1")) = True Then
            dt = GetMasterSkillListFromDisplay()
            'End If
        End Try
        ini = Nothing

        Return dt
    End Function

    Private Function GetMasterSkillListFromDisplay() As DataTable
        Dim dt As New DataTable
        Try
            Dim ws As New ShopWebServiceDisplay.WebServiceAPI
            ws.Timeout = 20000
            ws.Url = GetWebServiceURL(False)
            dt = ws.GetMasterSkillList()
            ws = Nothing
        Catch ex As System.Net.WebException

        End Try

        Return dt
    End Function
#End Region
    
End Module
