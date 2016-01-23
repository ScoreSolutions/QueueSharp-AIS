Namespace ShopWebService
    Public Class SMSResponsePara
        Dim _RESULT As Boolean = False
        Dim _ERROR_RESPONSE As String = ""

        Public Property RESULT() As Boolean
            Get
                Return _RESULT
            End Get
            Set(ByVal value As Boolean)
                _RESULT = value
            End Set
        End Property
        Public Property ERROR_RESPONSE() As String
            Get
                Return _ERROR_RESPONSE
            End Get
            Set(ByVal value As String)
                _ERROR_RESPONSE = value
            End Set
        End Property
    End Class
End Namespace

