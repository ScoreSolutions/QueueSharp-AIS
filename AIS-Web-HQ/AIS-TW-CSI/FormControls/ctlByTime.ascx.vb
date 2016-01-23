Imports System.Data

Partial Class FormControls_ctlByTime
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property IntervalMinute() As Integer
        Get
            Return cmbIntervalMinute.SelectedValue
        End Get
    End Property
    Public ReadOnly Property TimeFrom() As String
        Get
            Return DTimePeroidFrom.SelectedValue
        End Get
    End Property
    Public ReadOnly Property TimeTo() As String
        Get
            Return DTimePeroidTo.SelectedValue
        End Get
    End Property
    Public ReadOnly Property DateFrom() As Date
        Get
            Return txtDateFrom.DateValue
        End Get
    End Property
    Public ReadOnly Property DateTo() As Date
        Get
            Return txtDateTo.DateValue
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            DTimePeroidFrom.Enabled = False
            DTimePeroidTo.Enabled = False
            SetDropDownlist()
        End If
    End Sub

    Private Sub SetDropDownlist()
        Dim iDT As DataTable = Engine.Reports.ReportsENG.GetIntervalTime()
        If iDT.Rows.Count > 0 Then
            cmbIntervalMinute.SetItemList(iDT, "interval_time", "interval_time")
            SetTimePreiod()
        End If
    End Sub

    Protected Sub cmbIntervalMinute_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIntervalMinute.SelectedIndexChanged
        If cmbIntervalMinute.SelectedValue <> "" Then
            SetTimePreiod()
        End If
    End Sub
    Private Sub SetTimePreiod()
        Dim strTime As DateTime = New DateTime(1, 1, 1, 6, 0, 0)
        Dim endTime As DateTime = New DateTime(1, 1, 1, 22, 0, 0)
        Dim currTime As DateTime = strTime
        DTimePeroidFrom.ClearCombo()
        DTimePeroidTo.ClearCombo()
        Do
            DTimePeroidFrom.SetItemList(currTime.ToString("HH:mm"), currTime.ToString("HH:mm"))
            DTimePeroidTo.SetItemList(currTime.ToString("HH:mm"), currTime.ToString("HH:mm"))
            currTime = DateAdd(DateInterval.Minute, Convert.ToDouble(cmbIntervalMinute.SelectedValue), currTime)
        Loop While currTime <= endTime
        If cmbIntervalMinute.SelectedValue = "90" Then
            DTimePeroidFrom.SetItemList("22:00", "22:00")
            DTimePeroidTo.SetItemList("22:00", "22:00")
        ElseIf cmbIntervalMinute.SelectedValue = "150" Then
            DTimePeroidFrom.SetItemList("22:00", "22:00")
            DTimePeroidTo.SetItemList("22:00", "22:00")
        ElseIf cmbIntervalMinute.SelectedValue = "180" Then
            DTimePeroidFrom.SetItemList("22:00", "22:00")
            DTimePeroidTo.SetItemList("22:00", "22:00")
        ElseIf cmbIntervalMinute.SelectedValue = "210" Then
            DTimePeroidFrom.SetItemList("22:00", "22:00")
            DTimePeroidTo.SetItemList("22:00", "22:00")
        End If


        DTimePeroidFrom.Enabled = True
        DTimePeroidTo.Enabled = True
    End Sub
End Class
