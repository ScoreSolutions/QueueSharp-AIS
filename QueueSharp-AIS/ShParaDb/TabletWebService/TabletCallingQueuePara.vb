Namespace TabletWebService
    Public Class TabletCallingQueuePara

        Dim _ReturnResult As String = "false"
        Dim _ErrorMessage As String = ""
        Dim _QueueNo As String = ""
        Dim _CustomerTypeID As String = ""
        Dim _CustomerTypeName As String = ""
        Dim _MobileNo As String = ""
        Dim _CustomerName As String = ""
        Dim _CustomerEmail As String = ""
        Dim _MobileStatus As String = ""
        Dim _BirthDate As String = ""
        Dim _Category As String = ""
        Dim _AccBalance As String = ""
        Dim _ConClass As String = ""
        Dim _ServiceYear As String = ""
        Dim _Churn As String = ""
        Dim _NetworkType As String = ""
        Dim _SegmentLevel As String = ""
        Dim _PreLang As String = ""
        Dim _BillingSystem As String = ""
        Dim _BrandName As String = ""
        Dim _CampaignCode As String = ""
        Dim _CampaignName As String = ""
        Dim _CampaignDesc As String = ""
        Dim _ImageBase64String As String = ""
        Dim _TimeHold As String = ""

        
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

        Public Property CustomerTypeID() As String
            Get
                Return _CustomerTypeID.Trim
            End Get
            Set(ByVal value As String)
                _CustomerTypeID = value
            End Set
        End Property

        Public Property CustomerTypeName() As String
            Get
                Return _CustomerTypeName.Trim
            End Get
            Set(ByVal value As String)
                _CustomerTypeName = value
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

        Public Property CustomerName() As String
            Get
                Return _CustomerName.Trim
            End Get
            Set(ByVal value As String)
                _CustomerName = value
            End Set
        End Property


        Public Property CustomerEmail() As String
            Get
                Return _CustomerEmail.Trim
            End Get
            Set(ByVal value As String)
                _CustomerEmail = value
            End Set
        End Property
        Public Property MobileStatus() As String
            Get
                Return _MobileStatus.Trim
            End Get
            Set(ByVal value As String)
                _MobileStatus = value
            End Set
        End Property
        Public Property BirthDate() As String
            Get
                Return _BirthDate.Trim
            End Get
            Set(ByVal value As String)
                _BirthDate = value
            End Set
        End Property
        Public Property Catagory() As String
            Get
                Return _Category.Trim
            End Get
            Set(ByVal value As String)
                _Category.Trim()
            End Set
        End Property
        Public Property AccBalance() As String
            Get
                Return _AccBalance.Trim
            End Get
            Set(ByVal value As String)
                _AccBalance = value
            End Set
        End Property

        Public Property ConClass() As String
            Get
                Return _ConClass.Trim
            End Get
            Set(ByVal value As String)
                _ConClass = value
            End Set
        End Property

        Public Property ServiceYear() As String
            Get
                Return _ServiceYear.Trim
            End Get
            Set(ByVal value As String)
                _ServiceYear = value
            End Set
        End Property
        Public Property Churn() As String
            Get
                Return _Churn.Trim
            End Get
            Set(ByVal value As String)
                _Churn = value
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

        Public Property SegmentLevel() As String
            Get
                Return _SegmentLevel.Trim
            End Get
            Set(ByVal value As String)
                _SegmentLevel = value
            End Set
        End Property

        Public Property PreLang() As String
            Get
                Return _PreLang.Trim
            End Get
            Set(ByVal value As String)
                _PreLang = value
            End Set
        End Property
        Public Property BillingSystem() As String
            Get
                Return _BillingSystem.Trim
            End Get
            Set(ByVal value As String)
                _BillingSystem = value
            End Set
        End Property
        Public Property BrandName() As String
            Get
                Return _BrandName.Trim
            End Get
            Set(ByVal value As String)
                _BrandName = value
            End Set
        End Property
        Public Property CampaignCode() As String
            Get
                Return _CampaignCode.Trim
            End Get
            Set(ByVal value As String)
                _CampaignCode = value
            End Set
        End Property
        Public Property CampaignName() As String
            Get
                Return _CampaignName.Trim
            End Get
            Set(ByVal value As String)
                _CampaignName = value
            End Set
        End Property
        
        Public Property CampaignDesc() As String
            Get
                Return _CampaignDesc.Trim
            End Get
            Set(ByVal value As String)
                _CampaignDesc = value
            End Set
        End Property

        Public Property ImageBase64String() As String
            Get
                Return _ImageBase64String.Trim
            End Get
            Set(ByVal value As String)
                _ImageBase64String = value
            End Set
        End Property

        Public Property TimeHold() As String
            Get
                Return _TimeHold.Trim
            End Get
            Set(ByVal value As String)
                _TimeHold = value
            End Set
        End Property
    End Class
End Namespace

