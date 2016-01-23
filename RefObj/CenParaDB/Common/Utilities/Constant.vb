Namespace Common.Utilities
    Public Class Constant
        Public Const CultureSessionID = "Culture"
        Public Const ApplicationErrorSessionID = "ErrorMessage"
        Public Const IntFormat = "#,##0"
        Public Const DoubleFormat = "#,##0.00"
        Public Const DateFormat = "dd/MM/yyyy"
        Public Const UserProfileSession As String = "UserProfile"
        Public Const UserMenuListSession As String = "MenuList"
        Public Const ForceChangePasswordSession As String = "ForceChangePassword"
        Public Const UserJoinCaseSession As String = "UserJoinCaseSession"
        Public Const UserMenuSession As String = "UserMenuSession"

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
            Public Const Defaults As String = "th-TH"
            Public Const Eng As String = "en-US"
            Public Const Thai As String = "th-TH"
        End Class
        
        
        Partial Public Class TbAppointmentCustomer
            Partial Public Class ActiveStatus
                Public Const ConfirmAppointment As String = "1"
                Public Const RegisterAtKiosk As String = "2"
                Public Const EndQueue As String = "3"
                Public Const Missed As String = "4"
                Public Const NoShow As String = "5"
                Public Const Cancel As String = "6"
            End Class
            Partial Public Class AppointmentChannel
                Public Const Kiosk As String = "1"
                Public Const WebAppoint As String = "2"
                Public Const Mobile As String = "3"
                Public Const CallCenter As String = "4"
            End Class
            Partial Public Class Siebel
                Partial Public Class SubCatategory
                    Partial Public Class Kiosk
                        Public Const CatID As String = "1"
                        Public Const CatName As String = "Kiosk"
                    End Class
                    Partial Public Class eService
                        Public Const CatID As String = "2"
                        Public Const CatName As String = "eService"
                    End Class
                    Partial Public Class MobileApp
                        Public Const CatID As String = "3"
                        Public Const CatName As String = "MobileApp"
                    End Class
                    Partial Public Class CallInbound
                        Public Const CatID As String = "4"
                        Public Const CatName As String = "CallInbound"
                    End Class
                    
                End Class
                Partial Public Class Status
                    Public Const OPEN As String = "Open"
                    Public Const REGISTERED As String = "Registered"
                    Public Const COMPLETED As String = "Completed"
                    Public Const MISSED As String = "Missed"
                    Public Const NO_SHOW As String = "No Show"
                    Public Const CANCELLED As String = "Cancelled"
                End Class
            End Class
        End Class

        Partial Public Class TbCounterQueue
            Partial Public Class Status
                Public Const Waiting As String = "1"
                Public Const Serving As String = "2"
                Public Const EndService As String = "3"
                Public Const Calling As String = "4"
                Public Const Cancel As String = "5"
                Public Const Hold As String = "6"
                Public Const MissedCall As String = "8"
            End Class
        End Class

        Partial Public Class ReportName
            Public Const CustomerBySegmentByTime As String = "CustomerBySegmentByTime"
            Public Const CustomerByNetworkTypeByTime As String = "CustomerByNetworkTypeByTime"
            Public Const AverageServiceTimeComparingWidthKPIByTime As String = "AverageServiceTimeComparingWidthKPIByTime"
            Public Const AverageServiceTimeComparingWidthKPIStaffByDate As String = "AverageServiceTimeComparingWidthKPIStaffByDate"
            Public Const WaitingTimeAndHandlingTimeReportByAgentByTime As String = "WaitingTimeAndHandlingTimeReportByAgentTime"
            Public Const WaitingTimeHandlingTimeByShopByTime As String = "WaitingTimeHandlingTimeByShopByTime"
            Public Const ProductivityReportByDate As String = "ProductivityReportByDate"
            Public Const ReportsStaffAttendanceReport As String = "ReportsStaffAttendanceReport"
            Public Const IntervalPerformanceReportByTime As String = "IntervalPerformanceReportByTime"
            Public Const SummaryDaily As String = "SummaryDaily"
            Public Const SummaryInterval As String = "SummaryInterval"
            Public Const SummaryStaff As String = "SummaryStaff"
        End Class

        Partial Public Class TbCustomer
            Partial Public Class Category
                Partial Public Class Residential
                    Public Const Code As String = "R"
                    Public Const Name As String = "Residential"
                    Partial Public Class SubCategory
                        Public Const THA As String = "Thai Citizen"
                        Public Const FORE As String = "Foreigner"
                    End Class
                End Class
                Partial Public Class Business
                    Public Const Code As String = "B"
                    Public Const Name As String = "Business"
                    Partial Public Class SubCategory
                        Public Const KEY As String = "Key Account"
                        Public Const SME As String = "SME"
                    End Class
                End Class
                Partial Public Class GovernmentAneNonProfit
                    Public Const Code As String = "G"
                    Public Const Name As String = "Government & Non Profit"
                    Partial Public Class SubCategory
                        Public Const GOV As String = "Government"
                        Public Const STA As String = "State Enterprise"
                        Public Const EMB As String = "Embassy"
                        Public Const NON As String = "Non Profit"
                    End Class
                End Class
                Partial Public Class Exclusive
                    Public Const Code As String = "E"
                    Public Const Name As String = "Exclusive"
                    Partial Public Class SubCategory
                        Public Const ROY As String = "Royal"
                        Public Const TOT As String = "TOT"
                    End Class
                End Class
                Partial Public Class InHouse
                    Public Const Code As String = "I"
                    Public Const Name As String = "In House"
                    Partial Public Class SubCategory
                        Public Const PRE As String = "Pre-paid"
                        Public Const AIS As String = "AIS"
                    End Class
                End Class
            End Class
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

