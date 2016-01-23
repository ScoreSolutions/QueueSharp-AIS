Namespace ShopWebService
    Public Class SiebelResponsePara
        Dim _RESULT As Boolean = False
        Dim _ACTIVITY_ID As String = ""
        Dim _APPOINTMENT_JOB_ID As Long = 0
        Dim _ErrorMessage As String = ""
        Dim _CALL_GW2 As Boolean = True

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
        Public Property APPOINTMENT_JOB_ID() As Long
            Get
                Return _APPOINTMENT_JOB_ID
            End Get
            Set(ByVal value As Long)
                _APPOINTMENT_JOB_ID = value
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
        Public Property CALL_GW2() As Boolean
            Get
                Return _CALL_GW2
            End Get
            Set(ByVal value As Boolean)
                _CALL_GW2 = value
            End Set
        End Property
    End Class
End Namespace

