Namespace TabletWebService
    Public Class TabletConfirmQueue
        Dim _ReturnResult As String = "false"
        Dim _ErrorMessage As String = ""
        Dim _QueueNo As String = ""
        Dim _MobileNo As String = ""
        Dim _StartTime As String = ""
        Dim _QueueServiceList As New DataTable
        Dim _IsTransferQueue As String = "false"
        Dim _IsAddService As String = "false"

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
        Public Property StartTime() As String
            Get
                Return _StartTime.Trim
            End Get
            Set(ByVal value As String)
                _StartTime = value
            End Set
        End Property
        Public Property QueueServiceList() As DataTable
            Get
                Return _QueueServiceList
            End Get
            Set(ByVal value As DataTable)
                _QueueServiceList = value
            End Set
        End Property
        Public Property IsTransferQueue() As String
            Get
                Return _IsTransferQueue.Trim
            End Get
            Set(ByVal value As String)
                _IsTransferQueue = value
            End Set
        End Property
        Public Property IsAddService() As String
            Get
                Return _IsAddService.Trim
            End Get
            Set(ByVal value As String)
                _IsAddService = value
            End Set
        End Property
    End Class
End Namespace

