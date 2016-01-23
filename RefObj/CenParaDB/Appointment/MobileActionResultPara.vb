Namespace Appointment
    <Serializable()> Public Class MobileActionResultPara
        Dim _ActionID As String = "0"
        Dim _ServiceID As String = ""
        Dim _ShopID As String = ""
        Dim _AppointmentTime As DateTime = New Date(1, 1, 1)

        Public Property ActionID() As String
            Get
                Return _ActionID.Trim
            End Get
            Set(ByVal value As String)
                _ActionID = value
            End Set
        End Property

        Public Property ServiceID() As String
            Get
                Return _ServiceID.Trim
            End Get
            Set(ByVal value As String)
                _ServiceID = value
            End Set
        End Property
        Public Property ShopID() As String
            Get
                Return _ShopID.Trim
            End Get
            Set(ByVal value As String)
                _ShopID = value
            End Set
        End Property

        Public Property AppointmentTime() As DateTime
            Get
                Return _AppointmentTime
            End Get
            Set(ByVal value As DateTime)
                _AppointmentTime = value
            End Set
        End Property
    End Class
End Namespace

