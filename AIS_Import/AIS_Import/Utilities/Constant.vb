Namespace Utilities
    Public Class Constant
        Public Const CultureSessionID = "Culture"
        Public Const ApplicationErrorSessionID = "ErrorMessage"
        Public Const IntFormat = "#,##0"
        Public Const DoubleFormat = "#,##0.00"
        Public Const DateFormat = "dd/MM/yyyy"

        Public Shared ReadOnly Property HomeFolder() As String
            Get
                Return System.Web.HttpContext.Current.Request.ApplicationPath & "/"
            End Get
        End Property
        Public ReadOnly Property ImageFolder() As String
            Get
                Return HomeFolder & "Images/"
            End Get
        End Property
        Partial Public Class CultureName
            Public Const Defaults As String = "en-US"
            Public Const Eng As String = "en-US"
            Public Const Thai As String = "th-TH"
        End Class

        Partial Public Class SystemConfig
            Public Const LoginFailLimit As String = "LoginFailLimit"
            Public Const HelthClaimServiceTypeID As String = "HelthClaimServiceTypeID"
            Public Const LifeClaimServiceTypeID As String = "LifeClaimServiceTypeID"
            Public Const PwdHisLimit As String = "PwdHisLimit"
            Public Const MailFrom As String = "MailFrom"
            Public Const AlertPasswordExpireDate As String = "AlertPasswordExpireDate"
            Public Const FirstPageModuleID As String = "FirstPageModuleID"
        End Class

        Partial Public Class FilePrefix
            Public Const CLAIM As String = "CLAIM_"
            Public Const FAQ As String = "FAQ_"
            Public Const DOWNLOAD As String = "DOWNLOAD_"
        End Class

        Partial Public Class FileExt
            Public Const PDF As String = ".PDF"
            Public Const DOC As String = ".DOC"
            Public Const DOCX As String = ".DOCX"
            Public Const XLS As String = ".XLS"
            Public Const XLSX As String = ".XLSX"
        End Class
        Partial Public Class UserType
            Public Const HR As String = "H"
            Public Const BROKER As String = "B"
            Public Const AGENT As String = "A"
            Public Const MEMBER As String = "M"
            Public Const ADMIN As String = "I"
            Public Const USER As String = "U"
            Public Const SUPER_USER As String = "S"
        End Class
        Partial Public Class PolicyServiceType
            Public Const POLICY As Long = 1
            Public Const HEALTH As Long = 2
            Public Const LIFE As Long = 3
        End Class
        Partial Public Class MailContactType
            Public Const PolicyService As Long = 6
            Public Const HealthClaimService As Long = 7
            Public Const LifeClaimService As Long = 8
        End Class

        Partial Public Class ReportType
            Public Const PDF As String = "PDF"
            Public Const WORD As String = "WORD"
            Public Const EXCEL As String = "EXCEL"
            Public Const CRYSTAL As String = "CRYSTAL"
        End Class

        Public Shared Function GetFullDate() As String
            Dim month As String = ""
            Select Case DateTime.Now.Month
                Case 1
                    month = "January"
                Case 2
                    month = "Febuary"
                Case 3
                    month = "March"
                Case 4
                    month = "April"
                Case 5
                    month = "May"
                Case 6
                    month = "June"
                Case 7
                    month = "July"
                Case 8
                    month = "August"
                Case 9
                    month = "September"
                Case 10
                    month = "October"
                Case 11
                    month = "November"
                Case 12
                    month = "December"
            End Select
            Return month & ", " & DateTime.Now.Day.ToString() & " " & DateTime.Now.Year.ToString()
        End Function
    End Class

End Namespace

