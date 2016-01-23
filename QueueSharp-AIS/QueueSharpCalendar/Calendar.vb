Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Windows.Forms

Namespace Calendar
	''' <summary>
	''' Summary description for Calendar
	''' </summary>
    Public Class Calendar : Inherits System.Windows.Forms.UserControl
        Public Event DateChange(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs)

#Region "Variables"
        Private dateValue_Renamed As String
        Private acceptOnlyBoldedDates_Renamed As Boolean
#End Region

#Region "Properties "
        ''' <summary>
        ''' Public Accessor to dateValue variable
        ''' </summary>
        Public Property DateValue() As String
            Get
                Return Me.dateValue_Renamed
            End Get
            Set(ByVal value As String)
                Me.dateValue_Renamed = Value
            End Set
        End Property

        Public ReadOnly Property Year() As Integer
            Get
                Return monthCalendarDate.SelectionStart.Year
            End Get
        End Property
        Public ReadOnly Property Month() As Integer
            Get
                Return monthCalendarDate.SelectionStart.Month
            End Get
        End Property
        Public ReadOnly Property Day() As Integer
            Get
                Return monthCalendarDate.SelectionStart.Day
            End Get
        End Property


        ''' <summary>
        ''' Public Accessor to acceptOnlyBoldedDates variable
        ''' </summary>
        Public Property AcceptOnlyBoldedDates() As Boolean
            Get
                Return Me.acceptOnlyBoldedDates_Renamed
            End Get
            Set(ByVal value As Boolean)
                Me.acceptOnlyBoldedDates_Renamed = Value
            End Set
        End Property

        Public Property StartDate() As Date
            Get
                Return monthCalendarDate.SelectionStart
            End Get
            Set(ByVal value As Date)
                monthCalendarDate.SelectionStart = value
            End Set
        End Property
        Public Property EndDate() As Date
            Get
                Return monthCalendarDate.SelectionEnd.Date
            End Get
            Set(ByVal value As Date)
                monthCalendarDate.SelectionEnd = value
            End Set
        End Property
#End Region

        Private WithEvents monthCalendarDate As System.Windows.Forms.MonthCalendar
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing

        Public Sub New()
            ' This call is required by the Windows.Forms Form Designer.
            InitializeComponent()

            ' TODO: Add any initialization after the InitComponent call

        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Component Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.monthCalendarDate = New System.Windows.Forms.MonthCalendar
            Me.SuspendLayout()
            '
            'monthCalendarDate
            '
            Me.monthCalendarDate.BackColor = System.Drawing.Color.WhiteSmoke
            Me.monthCalendarDate.Location = New System.Drawing.Point(2, 3)
            Me.monthCalendarDate.Name = "monthCalendarDate"
            Me.monthCalendarDate.TabIndex = 2
            '
            'Calendar
            '
            Me.Controls.Add(Me.monthCalendarDate)
            Me.Name = "Calendar"
            Me.Size = New System.Drawing.Size(182, 167)
            Me.ResumeLayout(False)

        End Sub
#End Region

#Region "CustomEvents"
        ''' <summary>
        ''' Occurs when the Date of the Calendar changes
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub monthCalendarDate_DateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles monthCalendarDate.DateChanged
            Me.dateValue_Renamed = Me.monthCalendarDate.SelectionStart.ToShortDateString()
            RaiseEvent DateChange(sender, e)
        End Sub

        Private Sub Calendar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            monthCalendarDate.MaxSelectionCount = 31
            monthCalendarDate.SetDate(Today)
        End Sub

        Public Sub SetAnnuallyBoldedDates(ByVal dates As DateTime())
            Me.monthCalendarDate.AnnuallyBoldedDates = dates
        End Sub

        Public Sub SetEventDates(ByVal dt As DataTable, ByVal fldDateName As String)
            If dt.Rows.Count > 0 Then
                Dim dDate(dt.Rows.Count) As DateTime
                For i As Integer = 0 To dt.Rows.Count - 1
                    dDate(i) = Convert.ToDateTime(dt.Rows(i)(fldDateName))
                Next
                monthCalendarDate.AnnuallyBoldedDates = dDate
            End If
        End Sub
        Public Sub ClearEventDate()
            monthCalendarDate.AnnuallyBoldedDates = Nothing
        End Sub
#End Region

    End Class
End Namespace