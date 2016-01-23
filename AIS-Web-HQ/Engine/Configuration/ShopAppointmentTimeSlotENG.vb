Imports ShLinqDB.TABLE
Imports ShParaDB.TABLE
Imports Engine.Common

Namespace Configuration
    Public Class ShopAppointmentTimeSlotENG
        Dim _err As String = ""

        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property
        Public Function CheckCustomerAppointment(ByVal AppDateFrom As Date, ByVal AppDateTo As Date, ByVal ShopID As Long) As Boolean
            Dim lnq As New TbAppointmentCustomerShLinqDB
            Dim vDateFrom As String = AppDateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            Dim vDateTo As String = AppDateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))

            Dim ret As Boolean = False
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = FunctionEng.GetShTransction(ShopID, "ShopAppointmentTimeSlotENG.CheckCustomerAppointment")
            If shTrans.Trans IsNot Nothing Then
                ret = lnq.ChkDataByWhere("CONVERT(varchar(8),app_date,112)  between '" & vDateFrom & "' and '" & vDateTo & "'", shTrans.Trans)
                shTrans.CommitTransaction()
                If ret = True Then
                    _err = "Cannot Set Appointment. Because customer has booked"
                End If
            Else
                ret = False
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Sub New()
            _err = ""
        End Sub
    End Class
End Namespace

