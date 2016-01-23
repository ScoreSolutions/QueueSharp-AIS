
Partial Class FormControls_ctlByMonth
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property MonthFrom() As Int16
        Get
            Return cmbMonthFrom.SelectedValue
        End Get
    End Property
    Public ReadOnly Property MonthTo() As Int16
        Get
            Return cmbMonthTo.SelectedValue
        End Get
    End Property
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetDropdownlist()
        End If
    End Sub

    Private Sub SetDropdownlist()
        cmbMonthFrom.SetItemList("Select", "0")
        cmbMonthTo.SetItemList("Select", "0")

        For i As Integer = 1 To 12
            cmbMonthFrom.SetItemList(Engine.Common.FunctionEng.GetMonthNameEN(i), i)
            cmbMonthTo.SetItemList(Engine.Common.FunctionEng.GetMonthNameEN(i), i)
        Next
    End Sub
End Class
