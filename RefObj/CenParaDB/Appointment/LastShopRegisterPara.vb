Namespace Appointment
    <Serializable()> Public Class LastShopRegisterPara
        Dim _SHOP_ID As Long = 0
        Dim _REGION_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _SERVICE_DATE As DateTime = New DateTime(1, 1, 1)
        Dim _SERVICE_ID As String = ""

        Public Property SHOP_ID() As Long
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As Long)
                _SHOP_ID = value
            End Set
        End Property
        Public Property REGION_ID() As Long
            Get
                Return _REGION_ID
            End Get
            Set(ByVal value As Long)
                _REGION_ID = value
            End Set
        End Property
        Public Property SHOP_NAME_TH() As String
            Get
                Return _SHOP_NAME_TH
            End Get
            Set(ByVal value As String)
                _SHOP_NAME_TH = value
            End Set
        End Property
        Public Property SHOP_NAME_EN() As String
            Get
                Return _SHOP_NAME_EN
            End Get
            Set(ByVal value As String)
                _SHOP_NAME_EN = value
            End Set
        End Property
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
                _SERVICE_DATE = value
            End Set
        End Property
        Public Property SERVICE_ID() As String
            Get
                Return _SERVICE_ID.Trim
            End Get
            Set(ByVal value As String)
                _SERVICE_ID = value
            End Set
        End Property
    End Class
End Namespace

