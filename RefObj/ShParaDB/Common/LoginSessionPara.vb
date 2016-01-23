Namespace Common
    <Serializable()> Public Class LoginSessionPara
        Dim _LOGIN_HISTORY_ID As Long = 0
        Dim _USER_ID As String = ""
        Dim _USERS_ID As Long = 0


        Public Property LOGIN_HISTORY_ID() As Long
            Get
                Return _LOGIN_HISTORY_ID
            End Get
            Set(ByVal value As Long)
                _LOGIN_HISTORY_ID = value
            End Set
        End Property
        Public Property USER_ID() As String
            Get
                Return _USER_ID
            End Get
            Set(ByVal value As String)
                _USER_ID = value
            End Set
        End Property
        Public Property USERS_ID() As Long
            Get
                Return _USERS_ID
            End Get
            Set(ByVal value As Long)
                _USERS_ID = value
            End Set
        End Property
    End Class
End Namespace
