Namespace Para
    Public Class InputCustomerPara
        Dim _FST_NAME As String = ""
        Dim _LAST_NAME As String = ""
        Dim _X_TITLE As String = ""
        Dim _EMAIL_ADDR As String = ""
        Dim _MOBILE_NO As String = ""
        Dim _STATUS As String = ""
        Dim _BDATE As String = ""
        Dim _SEGMENT As String = ""
        Dim _CATEGORY As String = ""
        Dim _Acc_Balance As String = ""
        Dim _CONTACT_CLASS As String = ""
        Dim _Service_Year As String = ""
        Dim _CHURN_SCORE As String = ""
        Dim _Camp_Code As String = ""
        Dim _Camp_Name As String = ""
        Dim _PRE_LANG As String = ""
        Dim _Network_Type As String = ""

        Public Property FIRST_NAME() As String
            Get
                Return _FST_NAME.Trim
            End Get
            Set(ByVal value As String)
                _FST_NAME = value
            End Set
        End Property
        Public Property LAST_NAME() As String
            Get
                Return _LAST_NAME.Trim
            End Get
            Set(ByVal value As String)
                _LAST_NAME = value
            End Set
        End Property

        Public Property X_TITLE() As String
            Get
                Return _X_TITLE.Trim
            End Get
            Set(ByVal value As String)
                _X_TITLE = value
            End Set
        End Property
        Public Property EMAIL_ADDR() As String
            Get
                Return _EMAIL_ADDR.Trim
            End Get
            Set(ByVal value As String)
                _EMAIL_ADDR = value
            End Set
        End Property
        Public Property MOBILE_NO() As String
            Get
                Return _MOBILE_NO.Trim
            End Get
            Set(ByVal value As String)
                _MOBILE_NO = value
            End Set
        End Property
        Public Property STATUS() As String
            Get
                Return _STATUS.Trim
            End Get
            Set(ByVal value As String)
                _STATUS = value
            End Set
        End Property

        Public Property BDATE() As String
            Get
                Return _BDATE.Trim
            End Get
            Set(ByVal value As String)
                _BDATE = value
            End Set
        End Property

        Public Property SEGMENT() As String
            Get
                Return _SEGMENT.Trim
            End Get
            Set(ByVal value As String)
                _SEGMENT = value
            End Set
        End Property
        Public Property CATEGORY() As String
            Get
                Return _CATEGORY.Trim
            End Get
            Set(ByVal value As String)
                _CATEGORY = value
            End Set
        End Property

        Public Property ACC_BALANCE() As String
            Get
                Return _Acc_Balance.Trim
            End Get
            Set(ByVal value As String)
                _Acc_Balance = value
            End Set
        End Property

        Public Property CONTACT_CLASS() As String
            Get
                Return _CONTACT_CLASS.Trim
            End Get
            Set(ByVal value As String)
                _CONTACT_CLASS = value
            End Set
        End Property
        Public Property SERVICE_YEAR() As String
            Get
                Return _Service_Year.Trim
            End Get
            Set(ByVal value As String)
                _Service_Year = value
            End Set
        End Property
        Public Property CHURN_SCORE() As String
            Get
                Return _CHURN_SCORE.Trim
            End Get
            Set(ByVal value As String)
                _CHURN_SCORE = value
            End Set
        End Property

        Public Property CAMP_CODE() As String
            Get
                Return _Camp_Code
            End Get
            Set(ByVal value As String)
                _Camp_Code = value
            End Set
        End Property
        Public Property CAMP_NAME() As String
            Get
                Return _Camp_Name
            End Get
            Set(ByVal value As String)
                _Camp_Name = value
            End Set
        End Property
        Public Property PRE_LANG() As String
            Get
                Return _PRE_LANG.Trim
            End Get
            Set(ByVal value As String)
                _PRE_LANG = value
            End Set
        End Property
        Public Property NETWORK_TYPE() As String
            Get
                Return _Network_Type.Trim
            End Get
            Set(ByVal value As String)
                _Network_Type = value
            End Set
        End Property

    End Class
End Namespace

