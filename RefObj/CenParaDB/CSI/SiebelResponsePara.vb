Namespace CSI
    Public Class SiebelResponsePara
        Dim _RESULT As Boolean = False
        Dim _ACTIVITY_ID As String = ""
        Dim _ErrorMessage As String = ""

        Public Property RESULT() As String
            Get
                Return _RESULT
            End Get
            Set(ByVal value As String)
                _RESULT = value
            End Set
        End Property
        Public Property ACTIVITY_ID() As String
            Get
                Return _ACTIVITY_ID
            End Get
            Set(ByVal value As String)
                _ACTIVITY_ID = value
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

