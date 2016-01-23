Namespace TabletWebService
    Public Class TabletLogonUserPara
        Dim _return_result As String = "false"
        Dim _user_id As String = ""
        Dim _user_code As String = ""
        Dim _username As String = ""
        Dim _fulllname As String = ""
        Dim _counter_id As String = ""
        Dim _counter_name As String = ""
        Dim _item_id As String = ""
        Dim _ip_address As String = ""
        Dim _assign_appointment As String = ""

        Public Property RETURN_RESULT() As String
            Get
                Return _return_result.Trim
            End Get
            Set(ByVal value As String)
                _return_result = value
            End Set
        End Property

        Public Property USER_ID() As String
            Get
                Return _user_id.Trim
            End Get
            Set(ByVal value As String)
                _user_id = value
            End Set
        End Property

        Public Property USER_CODE() As String
            Get
                Return _user_code.Trim
            End Get
            Set(ByVal value As String)
                _user_code = value
            End Set
        End Property

        Public Property USERNAME() As String
            Get
                Return _username.Trim
            End Get
            Set(ByVal value As String)
                _username = value
            End Set
        End Property

        Public Property FULLNAME() As String
            Get
                Return _fulllname.Trim
            End Get
            Set(ByVal value As String)
                _fulllname = value
            End Set
        End Property

        Public Property COUNTER_ID() As String
            Get
                Return _counter_id.Trim
            End Get
            Set(ByVal value As String)
                _counter_id = value
            End Set
        End Property

        Public Property COUNTER_NAME() As String
            Get
                Return _counter_name.Trim
            End Get
            Set(ByVal value As String)
                _counter_name = value
            End Set
        End Property

        Public Property ITEM_ID() As String
            Get
                Return _item_id.Trim
            End Get
            Set(ByVal value As String)
                _item_id = value
            End Set
        End Property

        Public Property IP_ADDRESS() As String
            Get
                Return _ip_address.Trim
            End Get
            Set(ByVal value As String)
                _ip_address = value
            End Set
        End Property

        Public Property ASSIGN_APPOINTMENT() As String
            Get
                Return _assign_appointment.Trim
            End Get
            Set(ByVal value As String)
                _assign_appointment = value
            End Set
        End Property
    End Class
End Namespace

