Public Class TDateControl

    Public Event onValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    Dim SelDateValue As Date = Date.FromOADate(Date.Today.ToOADate)
    Private MonthArrayEng(11) As String
    Private MonthArrayThai(11) As String
    Enum DateEnum
        ThaiYear = 543
        EngYear = 0
    End Enum
    Enum LangEnum
        ThaiLang = 1
        EngLang = 0
    End Enum
    Dim YearType As DateEnum = DateEnum.EngYear
    Dim LangType As LangEnum = LangEnum.EngLang

    Public Property Value() As Date
        Get
            Return SelDateValue
        End Get
        Set(ByVal value As Date)
            SelDateValue = value
            SortDayInMonth()
        End Set
    End Property

    Public Property DateType() As DateEnum
        Get
            Return YearType
        End Get
        Set(ByVal value As DateEnum)
            'Remain
            Select Case value
                Case DateEnum.EngYear
                    NumericYear.Maximum = 2027
                    NumericYear.Minimum = 1957
                Case DateEnum.ThaiYear
                    NumericYear.Maximum = 2570
                    NumericYear.Minimum = 2500
            End Select
            YearType = value
            DisplayMonthYear()
            DisplayTextOut()

        End Set
    End Property

    Public Property MonthType() As LangEnum
        Get
            Return LangType
        End Get
        Set(ByVal value As LangEnum)
            'Remain
            LangType = value
            DisplayMonthYear()
            DisplayTextOut()
        End Set
    End Property

    Private Sub TDateControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'PanelCalendar.Visible = False
        MonthArrayEng(0) = "January"
        MonthArrayEng(1) = "February"
        MonthArrayEng(2) = "March"
        MonthArrayEng(3) = "April"
        MonthArrayEng(4) = "May"
        MonthArrayEng(5) = "June"
        MonthArrayEng(6) = "July"
        MonthArrayEng(7) = "August"
        MonthArrayEng(8) = "September"
        MonthArrayEng(9) = "October"
        MonthArrayEng(10) = "November"
        MonthArrayEng(11) = "December"

        MonthArrayThai(0) = "มกราคม"
        MonthArrayThai(1) = "กุมภาพันธ์"
        MonthArrayThai(2) = "มีนาคม"
        MonthArrayThai(3) = "เมษายน"
        MonthArrayThai(4) = "พฤษภาคม"
        MonthArrayThai(5) = "มิถุนายน"
        MonthArrayThai(6) = "กรกฎาคม"
        MonthArrayThai(7) = "สิงหาคม"
        MonthArrayThai(8) = "กันยายน"
        MonthArrayThai(9) = "ตุลาคม"
        MonthArrayThai(10) = "พฤศจิกายน"
        MonthArrayThai(11) = "ธันวาคม"

        DateType = DateEnum.ThaiYear
        Value = Now
        Me.BringToFront()

        Dim Font As Font = New Font("Microsoft Sans Serif", 8.25)
        'Microsoft Sans Serif, 8.25pt
        Me.Font = Font
    End Sub
    'Private Sub BCollapse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BCollapse.Click
    '    PanelCalendar.Visible = Not PanelCalendar.Visible
    '    Select Case PanelCalendar.Visible
    '        Case True
    '            Me.Height = 234
    '        Case False
    '            Me.Height = 20
    '    End Select
    'End Sub

    'Private Sub TextValue_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    PanelCalendar.Visible = Not PanelCalendar.Visible
    '    Select Case PanelCalendar.Visible
    '        Case True
    '            Me.Height = 234
    '        Case False
    '            Me.Height = 20
    '    End Select
    'End Sub

    Private Sub SortDayInMonth()
        Dim DayInMonth As Integer = Date.DaysInMonth(SelDateValue.Year, SelDateValue.Month)
        Dim CurrentDay As Integer
        Select Case SelDateValue.AddDays(-SelDateValue.Day + 1).DayOfWeek 'Start First Day
            Case DayOfWeek.Sunday : CurrentDay = 1
            Case DayOfWeek.Monday : CurrentDay = 2
            Case DayOfWeek.Tuesday : CurrentDay = 3
            Case DayOfWeek.Wednesday : CurrentDay = 4
            Case DayOfWeek.Thursday : CurrentDay = 5
            Case DayOfWeek.Friday : CurrentDay = 6
            Case DayOfWeek.Saturday : CurrentDay = 7
        End Select
        Dim i As Integer

        'Clear All unuse Date Before Use
        For i = 1 To 40
            Dim L As Label = FindLabelDate(i)
            L.Text = ""
            L.BackColor = Color.White
            L.Cursor = Cursors.Default
            RemoveHandler L.MouseHover, AddressOf D_MouseHover
            RemoveHandler L.MouseLeave, AddressOf D_MouseOut
            'RemoveHandler L.Click, AddressOf D_Click
        Next

        'set All Use Date
        For i = 1 To DayInMonth ' Start With 1
            Dim Obj As Label = FindLabelDate(CurrentDay)
            With Obj
                .Text = i
                .Cursor = Cursors.Hand
                AddHandler Obj.MouseHover, AddressOf D_MouseHover
                AddHandler Obj.MouseLeave, AddressOf D_MouseOut
                'AddHandler Obj.Click, AddressOf D_Click
                If i = SelDateValue.Day Then
                    Obj.BackColor = Color.FromArgb(150, 248, 150)
                ElseIf SelDateValue.Month = Now.Month And SelDateValue.Year = Now.Year And Now.Day = i Then
                    Obj.BackColor = Color.FromArgb(212, 228, 228)
                Else
                    Obj.BackColor = Color.White
                End If
                CurrentDay += 1
            End With
        Next

        DisplayMonthYear()
        DisplayTextOut()

    End Sub

    Private Sub DisplayMonthYear()
        LMonth.Text = ""
        Select Case LangType
            Case LangEnum.EngLang
                LMonth.Text = MonthArrayEng(SelDateValue.Month - 1)
            Case LangEnum.ThaiLang
                LMonth.Text = MonthArrayThai(SelDateValue.Month - 1)
        End Select
        NumericYear.Value = SelDateValue.Year + YearType
    End Sub

    Private Sub DisplayTextOut()
        Select Case LangType
            Case LangEnum.EngLang
                LabelToDay.Text = "Today : " & Now.Day & " " & MonthArrayEng(Now.Month - 1) & " " & SelDateValue.Year + YearType
            Case LangEnum.ThaiLang
                LabelToDay.Text = "วันนี้ : " & Now.Day & " " & MonthArrayThai(Now.Month - 1) & " " & SelDateValue.Year + YearType
        End Select
    End Sub

    'Protected Sub D_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim L As Label = sender
    '    If L.Text = "" Then Exit Sub
    '    SelDateValue = SelDateValue.AddDays(CInt(L.Text) - SelDateValue.Day)
    '    SortDayInMonth()
    '    PanelCalendar.Visible = False
    '    Me.Height = 20
    '    RaiseEvent onValueChanged(sender, e)
    'End Sub

    Private Function FindLabelDate(ByVal ID As Integer) As Label
        Dim i As Integer
        Dim L As Label = New Label
        For i = 0 To PanelCalendar.Controls.Count - 1
            If PanelCalendar.Controls(i).Name = "D" & ID Then
                L = PanelCalendar.Controls(i)
                Exit For
            End If
        Next
        FindLabelDate = L
    End Function

    Private Sub D_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim L As Label = sender
        L.BackColor = Color.FromArgb(206, 248, 202)
    End Sub

    Private Sub D_MouseOut(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim L As Label = sender
        If L.Text = "" Then Exit Sub
        If CInt(L.Text) = SelDateValue.Day Then
            L.BackColor = Color.FromArgb(150, 248, 150)
        ElseIf SelDateValue.Month = Now.Month And SelDateValue.Year = Now.Year And Now.Day = CInt(L.Text) Then
            L.BackColor = Color.FromArgb(212, 228, 228)
        Else
            L.BackColor = Color.White
        End If
    End Sub

    Private Sub ButtonLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLeft.Click
        SelDateValue = SelDateValue.AddMonths(-1)
        SortDayInMonth()
    End Sub

    Private Sub ButtonRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRight.Click
        SelDateValue = SelDateValue.AddMonths(1)
        SortDayInMonth()
    End Sub

    Private Sub NumericYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericYear.Click
        NumericYear.Enabled = False
        Dim Temp As Integer
        Do
            Temp = CInt(NumericYear.Value)
            If YearType = DateEnum.ThaiYear Then Temp -= 543
            SelDateValue = SelDateValue.AddYears(Temp - SelDateValue.Year)
            SortDayInMonth()
        Loop Until Temp = SelDateValue.Year
        NumericYear.Enabled = True

    End Sub
    Private Sub NumericYear_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NumericYear.MouseUp
        NumericYear_Click(New Object, New EventArgs)
    End Sub

    Private Sub LabelToDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelToDay.Click
        SelDateValue = Now
        SortDayInMonth()
    End Sub

End Class
