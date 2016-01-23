Namespace ReportCriteria
    Public Class ProductivityPara
        Dim _SHOP_ID As String = ""   'ตัวอย่าง "1,2,3"
        Dim _DateFrom As Date = New Date(1, 1, 1)
        Dim _DateTo As Date = New Date(1, 1, 1)
        Dim _DayInWeek As String = ""   'ตัวอย่าง 1,2,3 (1=วันอาทิตย์  , 7=วันเสาร์)
        Dim _WeekInYearFrom As Integer = 0
        Dim _WeekInYearTo As Integer = 0
        Dim _MonthFrom As Integer = 0
        Dim _MonthTo As Integer = 0
        Dim _QuarterFrom As Integer = 0
        Dim _QuarterTo As Integer = 0
        Dim _YearFrom As Integer = 0
        Dim _YearTo As Integer = 0
        Dim _ProcessUser As String = ""

        Public Property ShopID() As String
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
        Public Property DayInWeek() As String
            Get
                Return _DayInWeek
            End Get
            Set(ByVal value As String)
                _DayInWeek = value
            End Set
        End Property
        Public Property WeekInYearFrom() As Integer
            Get
                Return _WeekInYearFrom
            End Get
            Set(ByVal value As Integer)
                _WeekInYearFrom = value
            End Set
        End Property
        Public Property WeekInYearTo() As Integer
            Get
                Return _WeekInYearTo
            End Get
            Set(ByVal value As Integer)
                _WeekInYearTo = value
            End Set
        End Property
        Public Property MonthFrom() As Integer
            Get
                Return _MonthFrom
            End Get
            Set(ByVal value As Integer)
                _MonthFrom = value
            End Set
        End Property
        Public Property MonthTo() As Integer
            Get
                Return _MonthTo
            End Get
            Set(ByVal value As Integer)
                _MonthTo = value
            End Set
        End Property
        Public Property QuarterFrom() As Integer
            Get
                Return _QuarterFrom
            End Get
            Set(ByVal value As Integer)
                _QuarterFrom = value
            End Set
        End Property
        Public Property QuarterTo() As Integer
            Get
                Return _QuarterTo
            End Get
            Set(ByVal value As Integer)
                _QuarterTo = value
            End Set
        End Property
        Public Property YearFrom() As Integer
            Get
                Return _YearFrom
            End Get
            Set(ByVal value As Integer)
                _YearFrom = value
            End Set
        End Property
        Public Property YearTo() As Integer
            Get
                Return _YearTo
            End Get
            Set(ByVal value As Integer)
                _YearTo = value
            End Set
        End Property
        Public Property ProcessUser() As String
            Get
                Return _ProcessUser
            End Get
            Set(ByVal value As String)
                _ProcessUser = value
            End Set
        End Property
    End Class
End Namespace

