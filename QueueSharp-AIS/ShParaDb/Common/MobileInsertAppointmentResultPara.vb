Namespace Common
    <Serializable()> Public Class MobileInsertAppointmentResultPara
        Dim _Response As Boolean = False
        Dim _ErrorMessage As String = ""

        Public Property RESPONSE() As Boolean
            Get
                Return _Response
            End Get
            Set(ByVal value As Boolean)
                _Response = value
            End Set
        End Property
        Public Property ErrorMessage() As String
            Get
                Return _ErrorMessage
            End Get
            Set(ByVal value As String)
                _ErrorMessage = value
            End Set
        End Property
    End Class
End Namespace
