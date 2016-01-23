Imports System.Data.SqlClient

Public Class MonthCalendar

    Public Event onValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event onDateChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    Dim SelDateValue As Date = Date.FromOADate(Date.Today.ToOADate)
    Public dt_SeleteDate As New DataTable
    Public DateNow As Int32 = 0
    Public Month As Int32 = 0
    Public Year As Int32 = 0
    Public Day As Int32 = 0
    Public SelectDate As Boolean = False
    Public ColorDate As String = ""
    Public DD(31) As String
    Public SelectMonth As Boolean = False
    Dim AllSelect As Boolean = False


    Dim ModuelType As ModuelEnum = ModuelEnum.Appointment

    Enum ModuelEnum
        Appointment = 0
        Workflow = 1
    End Enum

    Public Property Moduel() As ModuelEnum
        Get
            Return ModuelType
        End Get
        Set(ByVal value As ModuelEnum)
            'Remain
            ModuelType = value
        End Set
    End Property

    Private Sub MonthCalendar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        Month = SelDateValue.Month
        Year = SelDateValue.Year
        Day = SelDateValue.Day
        SelectDate = False
        RaiseEvent onValueChanged(sender, e)
        LoadDate()
        ClearDay()
      
    End Sub

    Function MonthName(ByVal Month As Int32) As String
        Dim txt As String = ""
        Select Case Month
            Case 1
                txt = "January"
            Case 2
                txt = "February"
            Case 3
                txt = "March"
            Case 4
                txt = "April"
            Case 5
                txt = "May"
            Case 6
                txt = "June"
            Case 7
                txt = "July"
            Case 8
                txt = "August"
            Case 9
                txt = "September"
            Case 10
                txt = "October"
            Case 11
                txt = "November"
            Case 12
                txt = "December"
        End Select
        Return txt
    End Function

    Sub LoadDate()
        lblMonth.Text = MonthName(SelDateValue.Month) & " " & SelDateValue.Year.ToString

        Dim DayInMonth As Integer = Date.DaysInMonth(Year, Month)
        Dim CurrentDay As Integer
        Select Case SelDateValue.AddDays(-SelDateValue.Day + 1).DayOfWeek 'Start First Day
            Case DayOfWeek.Sunday : CurrentDay = 7
            Case DayOfWeek.Monday : CurrentDay = 1
            Case DayOfWeek.Tuesday : CurrentDay = 2
            Case DayOfWeek.Wednesday : CurrentDay = 3
            Case DayOfWeek.Thursday : CurrentDay = 4
            Case DayOfWeek.Friday : CurrentDay = 5
            Case DayOfWeek.Saturday : CurrentDay = 6
        End Select
        Dim i As Integer

        'Clear All unuse Date Before Use
        For i = 1 To 42
            Dim L As Label = FindLabelDate(i)
            L.Text = ""
            L.BackColor = Color.White
            L.ForeColor = Color.Black
            L.Cursor = Cursors.Default
            RemoveHandler L.MouseDown, AddressOf D_MouseDown
        Next


        'set All Use Date
        For i = 1 To DayInMonth ' Start With 1
            Dim Obj As Label = FindLabelDate(CurrentDay)
            With Obj
                For j As Int32 = 0 To dt_SeleteDate.Rows.Count - 1
                    If i = dt_SeleteDate.Rows(j).Item("DD") Then
                        .BackColor = Color.Green
                        .ForeColor = Color.White
                    End If
                Next
                .Text = i
                .Cursor = Cursors.Hand
                AddHandler Obj.MouseDown, AddressOf D_MouseDown

                CurrentDay += 1
            End With
        Next


        'For j As Int32 = 0 To dt_SeleteDate.Rows.Count - 1
        '    If DateNow = Year & Month.ToString.PadLeft(2, "0") & Day.ToString.PadLeft(2, "0") Then
        '        If i = dt_SeleteDate.Rows(j).Item("DD") Then
        '            .BackColor = Color.Red
        '            .ForeColor = Color.White
        '        Else
        '            .BackColor = Color.LightSeaGreen
        '            .ForeColor = Color.White
        '        End If
        '    Else
        '        If i = dt_SeleteDate.Rows(j).Item("DD") Then
        '            .BackColor = Color.Green
        '            .ForeColor = Color.White
        '        End If
        '    End If

        'Next
        '.Text = i
        '.Cursor = Cursors.Hand
    End Sub

    Private Sub D_MouseDown(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim L As Label = sender
        Day = L.Text
        SelectDate = True
        RaiseEvent onDateChanged(sender, e)
        LoadDate()
        If L.BackColor = Color.Green Then
            L.BackColor = Color.Red
            DD(CInt(L.Text)) = L.Text
            ColorDate = "Red"
        ElseIf L.BackColor = Color.Red Then
            L.BackColor = Color.Green
            DD(CInt(L.Text)) = ""
            ColorDate = "Green"
        ElseIf L.BackColor = Color.White Then
            L.BackColor = Color.LightSeaGreen
            L.ForeColor = Color.White
            DD(CInt(L.Text)) = L.Text
            ColorDate = "LightSeaGreen"
        Else
            L.BackColor = Color.White
            L.ForeColor = Color.Black
            DD(CInt(L.Text)) = ""
            ColorDate = "White"
        End If
    End Sub

    Private Function FindLabelDate(ByVal ID As Integer) As Label
        Dim i As Integer
        Dim L As Label = New Label
        For i = 0 To P.Controls.Count - 1
            If P.Controls(i).Name = "D" & ID Then
                L = P.Controls(i)
                Exit For
            End If
        Next
        FindLabelDate = L
    End Function

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        SelDateValue = SelDateValue.AddMonths(1)
        Month = SelDateValue.Month
        Year = SelDateValue.Year
        RaiseEvent onValueChanged(sender, e)
        LoadDate()
        ClearDay()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        SelDateValue = SelDateValue.AddMonths(-1)
        Month = SelDateValue.Month
        Year = SelDateValue.Year
        RaiseEvent onValueChanged(sender, e)
        LoadDate()
        ClearDay()
    End Sub

    Sub ClearDateSelect()
        For i = 1 To 42
            Dim D As Label = FindLabelDate(i)
            If D.BackColor = Color.Red Then
                D.BackColor = Color.Green
            ElseIf D.BackColor <> Color.Green Then
                D.BackColor = Color.White
                D.ForeColor = Color.Black
            End If
        Next
    End Sub

    Sub ClearDay()
        For i As Int32 = 0 To DD.Length - 1
            DD(i) = ""
        Next
        SelectMonth = True
    End Sub

End Class
