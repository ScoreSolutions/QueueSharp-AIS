Namespace ReportCriteria
    Public Class StaffAttendancePara
        Dim _SHOP_ID As String = ""   'ตัวอย่าง "1,2,3"
        Dim _DateFrom As Date = New Date(1, 1, 1)
        Dim _DateTo As Date = New Date(1, 1, 1)
        Dim _WeekInYearFrom As Long = 0
        Dim _WeekInYearTo As Long = 0
        Dim _MonthFrom As Int16 = 0
        Dim _MonthTo As Int16 = 0
        Dim _YearFrom As Long = 0
        Dim _YearTo As Long = 0

        Public Property SHOP_ID() As String
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As String)
                _SHOP_ID = value
            End Set
        End Property
        Public Property DateFrom() As Date
            Get
                Return _DateFrom
            End Get
            Set(ByVal value As Date)
                _DateFrom = value
            End Set
        End Property
        Public Property DateTo() As Date
            Get
                Return _DateTo
            End Get
            Set(ByVal value As Date)
                _DateTo = value
            End Set
        End Property
        Public Property WeekInYearFrom() As Long
            Get
                Return _WeekInYearFrom
            End Get
            Set(ByVal value As Long)
                _WeekInYearFrom = value
            End Set
        End Property
        Public Property WeekInYearTo() As Long
            Get
                Return _WeekInYearTo
            End Get
            Set(ByVal value As Long)
                _WeekInYearTo = value
            End Set
        End Property
        Public Property MonthFrom() As Int16
            Get
                Return _MonthFrom
            End Get
            Set(ByVal value As Int16)
                _MonthFrom = value
            End Set
        End Property
        Public Property MonthTo() As Int16
            Get
                Return _MonthTo
            End Get
            Set(ByVal value As Int16)
                _MonthTo = value
            End Set
        End Property
        Public Property YearFrom() As Long
            Get
                Return _YearFrom
            End Get
            Set(ByVal value As Long)
                _YearFrom = value
            End Set
        End Property
        Public Property YearTo() As Long
            Get
                Return _YearTo
            End Get
            Set(ByVal value As Long)
                _YearTo = value
            End Set
        End Property
    End Class
End Namespace

