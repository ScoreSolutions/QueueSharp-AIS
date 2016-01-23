

Namespace ReportCriteria
    Public Class WaitingTimeHandlingTimeByAgentPara
        Dim _ShopID As String = ""
        Dim _DateFrom As Date = New Date(1, 1, 1)
        Dim _DateTo As Date = New Date(1, 1, 1)
        Dim _TimeFrom As DateTime = New Date(1, 1, 1)
        Dim _TimeTo As DateTime = New Date(1, 1, 1)
        Dim _IntervalMinute As Integer = 0
        Dim _ProcessUser As String = ""

        Public Property ShopID() As String
            Get
                Return _ShopID
            End Get
            Set(ByVal value As String)
                _ShopID = value
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
        Public Property TimePeroidFrom() As DateTime
            Get
                Return _TimeFrom
            End Get
            Set(ByVal value As DateTime)
                _TimeFrom = value
            End Set
        End Property
        Public Property TimePeroidTo() As DateTime
            Get
                Return _TimeTo
            End Get
            Set(ByVal value As DateTime)
                _TimeTo = value
            End Set
        End Property
        Public Property IntervalMinute() As Integer
            Get
                Return _IntervalMinute
            End Get
            Set(ByVal value As Integer)
                _IntervalMinute = value
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

