Imports CenParaDB.TABLE
Namespace Common
    <Serializable()> Public Class UserProfilePara
        Dim _USERNAME As String = ""
        Dim _USERS_PARA As TbUserCenParaDB
        Dim _LOGIN_HISTORY_ID As Long = 0

        Public Property USERNAME() As String
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As String)
                _USERNAME = value
            End Set
        End Property
        Public Property USER_PARA() As TbUserCenParaDB
            Get
                Return _USERS_PARA
            End Get
            Set(ByVal value As TbUserCenParaDB)
                _USERS_PARA = value
            End Set
        End Property

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