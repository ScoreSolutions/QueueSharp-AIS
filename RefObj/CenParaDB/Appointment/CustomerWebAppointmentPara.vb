Namespace Appointment
    <Serializable()> Public Class CustomerWebAppointmentPara
        Dim _MobileNo As String = ""
        Dim _PreferLang As String = ""
        Dim _CurrentLang As String = ""
        Dim _MasterPage As String = ""
        Dim _NetworkType As String = ""

        Public Property MobileNo() As String
            Get
                Return _MobileNo.Trim
            End Get
            Set(ByVal value As String)
                _MobileNo = value
            End Set
        End Property
        Public Property PreferLang() As String
            Get
                Return _PreferLang.Trim
            End Get
            Set(ByVal value As String)
                _PreferLang = value
            End Set
        End Property
        Public Property CurrentLang() As String
            Get
                Return _CurrentLang.Trim
            End Get
            Set(ByVal value As String)
                _CurrentLang = value
            End Set
        End Property
        Public Property MasterPage() As String
            Get
                Return _MasterPage.Trim
            End Get
            Set(ByVal value As String)
                _MasterPage = value
            End Set
        End Property
        Public Property NetworkType() As String
            Get
                Return _NetworkType.Trim
            End Get
            Set(ByVal value As String)
                _NetworkType = value
            End Set
        End Property
    End Class
End Namespace

