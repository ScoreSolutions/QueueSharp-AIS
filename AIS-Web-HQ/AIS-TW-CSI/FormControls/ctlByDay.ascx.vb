
Partial Class FormControls_ctlByDay
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property WeekFrom() As Int16
        Get
            Return cmbWeekFrom.SelectedValue
        End Get
    End Property
    Public ReadOnly Property WeekTo() As Int16
        Get
            Return cmbWeekTo.SelectedValue
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
        cmbWeekFrom.SetItemList("", "0")
        cmbWeekTo.SetItemList("", "0")

        For i As Integer = 1 To 52
            cmbWeekFrom.SetItemList(i, i)
            cmbWeekTo.SetItemList(i, i)
        Next
    End Sub
End Class
