Imports CenParaDB.TABLE
Namespace Common
    <Serializable()> Public Class UserProfilePara
        Dim _USER_ID As String
        'Dim _USERS_PARA As UsersPara
        Dim _LOGIN_HISTORY_ID As Long

        Public Property USER_ID() As String
            Get
                Return _USER_ID
            End Get
            Set(ByVal value As String)
                _USER_ID = value
            End Set
        End Property
        'Public Property USER_PARA() As UsersPara
        '    Get
        '        Return _USERS_PARA
        '    End Get
        '    Set(ByVal value As UsersPara)
        '        _USERS_PARA = value
        '    End Set
        'End Property

        Public Property LOGIN_HISTORY_ID() As Long
            Get
                Return _LOGIN_HISTORY_ID
            End Get
            Set(ByVal value As Long)
                _LOGIN_HISTORY_ID = value
            End Set
        End Property

    End Class
End Namespace