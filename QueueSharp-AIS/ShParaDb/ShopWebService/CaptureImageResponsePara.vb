Namespace ShopWebService
    Public Class CaptureImageResponsePara
        Dim _CaptureResult As Boolean = False
        Dim _CaptureImage As Byte()

        Public Property CaptureResult() As Boolean
            Get
                Return _CaptureResult
            End Get
            Set(ByVal value As Boolean)
                _CaptureResult = value
            End Set
        End Property
        Public Property CaptureImage() As Byte()
            Get
                Return _CaptureImage
            End Get
            Set(ByVal value As Byte())
                _CaptureImage = value
            End Set
        End Property
    End Class
End Namespace

