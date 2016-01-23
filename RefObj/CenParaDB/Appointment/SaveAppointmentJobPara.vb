Namespace Appointment
    <Serializable()> Public Class SaveAppointmentJobPara
        Dim _SaveResult As Boolean = False
        Dim _para As CenParaDB.TABLE.TbAppointmentJobCenParaDB
        Dim _Err As String = ""

        Public Property SaveResult() As Boolean
            Get
                Return _SaveResult
            End Get
            Set(ByVal value As Boolean)
                _SaveResult = value
            End Set
        End Property
        Public Property AppointmentJobPara() As CenParaDB.TABLE.TbAppointmentJobCenParaDB
            Get
                Return _para
            End Get
            Set(ByVal value As CenParaDB.TABLE.TbAppointmentJobCenParaDB)
                _para = value
            End Set
        End Property
        Public Property ErrorMessage() As String
            Get
                Return _Err.Trim
            End Get
            Set(ByVal value As String)
                _Err = value
            End Set
        End Property
    End Class
End Namespace

