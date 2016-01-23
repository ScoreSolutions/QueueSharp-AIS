Namespace TabletWebService
    Public Class TabletAddServicePara
        Dim _ReturnResult As String = "false"
        Dim _ErrorMessage As String = ""
        Dim _QueueNo As String = ""
        Dim _MobileNo As String = ""
        Dim _AddItemID As String = ""
        Dim _AddItemName As String = ""
        Dim _ServiceActive As String = ""


        Public Property ReturnResult() As String
            Get
                Return _ReturnResult.Trim
            End Get
            Set(ByVal value As String)
                _ReturnResult = value
            End Set
        End Property

        Public Property ErrorMessage() As String
            Get
                Return _ErrorMessage.Trim
            End Get
            Set(ByVal value As String)
                _ErrorMessage = value
            End Set
        End Property
        Public Property QueueNo() As String
            Get
                Return _QueueNo.Trim
            End Get
            Set(ByVal value As String)
                _QueueNo = value
            End Set
        End Property

        Public Property MobileNo() As String
            Get
                Return _MobileNo.Trim
            End Get
            Set(ByVal value As String)
                _MobileNo = value
            End Set
        End Property

        Public Property AddItemID() As String
            Get
                Return _AddItemID.Trim
            End Get
            Set(ByVal value As String)
                _AddItemID = value
            End Set
        End Property

        Public Property AddItemName() As String
            Get
                Return _AddItemName.Trim
            End Get
            Set(ByVal value As String)
                _AddItemName = value
            End Set
        End Property
        Public Property ServiceActive() As String
            Get
                Return _ServiceActive.Trim
            End Get
            Set(ByVal value As String)
                _ServiceActive = value
            End Set
        End Property
    End Class
End Namespace

