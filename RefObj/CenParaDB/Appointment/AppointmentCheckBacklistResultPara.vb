Namespace Appointment
    <Serializable()> Public Class AppointmentCheckBacklistResultPara
        Dim _IsBackList As Boolean = False
        Dim _Msg As String = ""
        Dim _NewAppointmentDate As Date = New Date(1, 1, 1)

        Public Property IsBackList() As Boolean
            Get
                Return _IsBackList
            End Get
            Set(ByVal value As Boolean)
                _IsBackList = value
            End Set
        End Property

        Public Property MSG() As String
            Get
                Return _Msg
            End Get
            Set(ByVal value As String)
                _Msg = value
            End Set
        End Property
        Public Property NewAppointmentDate() As Date
            Get
                Return _NewAppointmentDate
            End Get
            Set(ByVal value As Date)
                _NewAppointmentDate = value
            End Set
        End Property
    End Class
End Namespace

