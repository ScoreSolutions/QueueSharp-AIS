Public Class RamInfoPara
    Dim _AvailablePhysicalMemoryGB As Double = 0
    Dim _TotalPhysicalMemoryGB As Double = 0
    Dim _PercentUsageGB As Double = 0

    Public Property AvailablePhysicalMemoryGB() As Double
        Get
            Return _AvailablePhysicalMemoryGB
        End Get
        Set(ByVal value As Double)
            _AvailablePhysicalMemoryGB = value
        End Set
    End Property

    Public Property TotalPhysicalMemoryGB() As Double
        Get
            Return _TotalPhysicalMemoryGB
        End Get
        Set(ByVal value As Double)
            _TotalPhysicalMemoryGB = value
        End Set
    End Property

    Public Property PercentUsageGB() As Double
        Get
            Return _PercentUsageGB
        End Get
        Set(ByVal value As Double)
            _PercentUsageGB = value
        End Set
    End Property
End Class
