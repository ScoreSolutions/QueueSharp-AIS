Imports System.IO.Ports
Imports QueueSharp.Org.Mentalis.Files

Public Class frmConfigPrinter

    Public ButtonClick As Boolean = False

    Private Sub ConfigPrinter_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim t As String = ""
        For Each t In System.Drawing.Printing.PrinterSettings.InstalledPrinters
            'If t.ToUpper = "BIXOLON SAMSUNG SRP-270" Then
            cbPrinter.Items.Add(t)
            'End If
        Next
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"

        Dim PrinterName As String = ini.ReadString("PrinterName")
        Dim Copy As String = ini.ReadString("copy")
        cbPrinter.SelectedIndex = cbPrinter.FindString(PrinterName)
        If PrinterName <> "" Then
            NUDCopy.Value = Copy
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ini As New IniReader(INIFileName)
        ini.Section = "SETTING"
        ini.Write("PrinterName", cbPrinter.SelectedItem.ToString)
        ini.Write("copy", NUDCopy.Value.ToString)
        MsgBox("บันทึกข้อมูลเรียบร้อย")
        ButtonClick = True
        Me.Close()
    End Sub

End Class