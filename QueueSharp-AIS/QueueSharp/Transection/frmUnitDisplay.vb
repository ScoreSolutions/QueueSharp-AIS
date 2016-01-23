Imports QueueSharp.Org.Mentalis.Files
Imports System.IO

Public Class frmUnitDisplay

    Private isOwnerAvail As Boolean = False
    Private Shared PFC As Drawing.Text.PrivateFontCollection
    Private Shared NewFont_FF As Drawing.FontFamily
    Private bc As Integer = BlinkCount
    Private cc As Integer = 0
    Public QueueNo As String = ""
 
    Private Sub frmUnitDisplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim f As Form = Nothing
        Try
            f = Me.Owner
            isOwnerAvail = True
            AddHandler f.Move, AddressOf updateMyPosition
        Catch ex As Exception
            isOwnerAvail = False
        End Try
        updateMyPosition(f, Nothing)
        initFont()
        tt.Enabled = True
    End Sub

    Function genSum(ByVal txt As String) As Int16
        Dim tmp As Integer = 0
        If txt.Length > 0 Then tmp = Asc(txt(0)) Else tmp = 0
        For i As Integer = 1 To txt.Length - 1
            tmp = tmp Xor Asc(txt(i))
        Next
        Return tmp
    End Function


    Sub initFont()
        txtLED.Font = CreateFont("LED.ttf", FontStyle.Regular, 24, GraphicsUnit.Point)
        txtLED.Text = QueueNo
    End Sub


    Sub updateMyPosition(ByVal o As Object, ByVal e As EventArgs)
        Try
            If Not o Is Nothing Then
                Dim parent As Form = CType(o, Form)
                Me.Top = parent.Top + parent.Height
                Me.Left = parent.Left
                Me.Width = parent.Width
            End If
        Catch ex As Exception
            'Do nothing
        End Try
    End Sub

    Private Function CreateFont(ByVal name As String, ByVal style As Drawing.FontStyle, ByVal size As Single, ByVal unit As Drawing.GraphicsUnit) As Drawing.Font
        PFC = New Drawing.Text.PrivateFontCollection
        PFC.AddFontFile(name)
        NewFont_FF = PFC.Families(0)
        Return New Drawing.Font(NewFont_FF, size, style, unit)
    End Function

    Public Sub btnReCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReCall.Click
        bc = BlinkCount
        cc = 0
        'ShowUnitDisplay(QueueNo, bc)
        'frmServiceConfirm.ReCall(QueueNo)
        CallSpeaker(QueueNo, Nothing)

    End Sub


    Private Sub tt_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tt.Tick
        tt.Enabled = False
        txtLED.ForeColor = Color.Red
        ttt.Enabled = True
    End Sub

    Private Sub ttt_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ttt.Tick
        ttt.Enabled = False
        cc = cc + 1
        If cc <= bc Then
            tt.Enabled = True
            txtLED.ForeColor = txtLED.BackColor
        Else
            cc = 0
        End If
    End Sub


End Class