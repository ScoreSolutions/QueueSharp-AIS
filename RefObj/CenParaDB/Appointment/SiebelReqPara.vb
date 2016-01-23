Namespace Appointment
    Public Class SiebelReqPara
        Dim _SIEBEL_TYPE As String = ""
        Dim _ACTIVITYCATEGORY As String = ""
        Dim _ACTIVITYSUBCATEGORY As String = ""
        Dim _DESCRIPTION As String = ""
        Dim _STATUS As String = ""
        Dim _MOBILE_NO As String = ""

        Public Property SIEBEL_TYPE() As String
            Get
                Return _SIEBEL_TYPE
            End Get
            Set(ByVal value As String)
                _SIEBEL_TYPE = value
            End Set
        End Property
        Public Property ACTIVITYCATEGORY() As String
            Get
                Return _ACTIVITYCATEGORY
            End Get
            Set(ByVal value As String)
                _ACTIVITYCATEGORY = value
            End Set
        End Property
        Public Property ACTIVITYSUBCATEGORY() As String
            Get
                Return _ACTIVITYSUBCATEGORY
            End Get
            Set(ByVal value As String)
                _ACTIVITYSUBCATEGORY = value
            End Set
        End Property

        Public Property DESCRIPTION() As String
            Get
                Return _DESCRIPTION
            End Get
            Set(ByVal value As String)
                _DESCRIPTION = value
            End Set
        End Property
        Public Property STATUS() As String
            Get
                Return _STATUS
            End Get
            Set(ByVal value As String)
                _STATUS = value
            End Set
        End Property
        Public Property MOBILE_NO() As String
            Get
                Return _MOBILE_NO
            End Get
            Set(ByVal value As String)
                _MOBILE_NO = value
            End Set
        End Property

    End Class
End Namespace

