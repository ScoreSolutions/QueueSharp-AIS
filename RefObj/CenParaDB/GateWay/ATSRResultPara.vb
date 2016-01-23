Namespace GateWay
    Public Class ATSRResultPara
        Dim _RESULT As String = ""
        Dim _RESULT_STATE As String = ""
        Dim _RESULT_VALUE As String = ""
        Dim _ATSR_CALL_TIME As String = ""
        Dim _ERROR_MESSAGE As String = ""
        Dim _NPS_SCORE As String = ""
        Dim _HAVE_LEAVE_VOICE As Boolean = False

        Public Property HAVE_LEAVE_VOICE() As Boolean
            Get
                Return _HAVE_LEAVE_VOICE
            End Get
            Set(ByVal value As Boolean)
                _HAVE_LEAVE_VOICE = value
            End Set
        End Property
        Public Property NPS_SCORE() As String
            Get
                Return _NPS_SCORE.Trim
            End Get
            Set(ByVal value As String)
                _NPS_SCORE = value
            End Set
        End Property

        Public Property RESULT() As String
            Get
                Return _RESULT.Trim
            End Get
            Set(ByVal value As String)
                _RESULT = value
            End Set
        End Property
        Public Property RESULT_STATE() As String
            Get
                Return _RESULT_STATE.Trim
            End Get
            Set(ByVal value As String)
                _RESULT_STATE = value
            End Set
        End Property
        Public Property RESULT_VALUE() As String
            Get
                Return _RESULT_VALUE.Trim
            End Get
            Set(ByVal value As String)
                _RESULT_VALUE = value
            End Set
        End Property
        Public Property ATSR_CALL_TIME() As String
            Get
                Return _ATSR_CALL_TIME.Trim
            End Get
            Set(ByVal value As String)
                _ATSR_CALL_TIME = value
            End Set
        End Property
        Public Property ERROR_MESSAGE() As String
            Get
                Return _ERROR_MESSAGE.Trim
            End Get
            Set(ByVal value As String)
                _ERROR_MESSAGE = value
            End Set
        End Property
    End Class
End Namespace

