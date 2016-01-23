
Partial Class FormControls_ctlByYear
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property YearFrom() As Int16
        Get
            Return Convert.ToInt16(IIf(txtYearFrom.Text.Trim = "", 0, txtYearFrom.Text))
        End Get
    End Property
    Public ReadOnly Property YearTo() As Int16
        Get
            Return Convert.ToInt16(IIf(txtYearTo.Text.Trim = "", 0, txtYearTo.Text))
        End Get
    End Property
End Class
