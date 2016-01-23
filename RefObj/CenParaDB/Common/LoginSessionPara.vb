Namespace Common
    <Serializable()> Public Class LoginSessionPara
        Dim _LOGIN_HISTORY_ID As Long = 0
        Dim _USERNAME As String = ""


        Public Property LOGIN_HISTORY_ID() As Long
            Get
                Return _LOGIN_HISTORY_ID
            End Get
            Set(ByVal value As Long)
                _LOGIN_HISTORY_ID = value
            End Set
        End Property
        Public Property USERNAME() As String
            Get
                Return _USERNAME.Trim
            End Get
            Set(ByVal value As String)
                _USERNAME = value
            End Set
        End Property
        
    End Class
End Namespace
