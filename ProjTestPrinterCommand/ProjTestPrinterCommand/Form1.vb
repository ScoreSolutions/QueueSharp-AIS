Imports System.IO
Imports System.Drawing.Printing
Imports System.Runtime.InteropServices
Public Class Form1



    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    Dim s As String
    '    Dim pd As New PrintDialog()
    '    ' You need a string to send.
    '    s = "Hello, this is a test"
    '    ' Open the printer dialog box, and then allow the user to select a printer.
    '    pd.PrinterSettings = New PrinterSettings()
    '    pd.PrinterSettings.PrinterName=
    '    'If (pd.ShowDialog() = DialogResult.OK) Then
    '    RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, s)
    '    'End If

    'End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Dim s As Byte() = File.ReadAllBytes(Application.StartupPath & "\Logo_TH.bmp")
        Dim pd As New PrintDocument
        'RawPrinterHelper.SendFileToPrinter(pd.DefaultPageSettings.PrinterSettings.PrinterName, Application.StartupPath & "\Logo_TH.bmp")
        ' You need a string to send.

        ' Open the printer dialog box, and then allow the user to select a printer.
        'pd.PrintController = New Printing.StandardPrintController


        'pd.PrinterSettings.PrinterName=
        'If (pd.ShowDialog() = DialogResult.OK) Then
        Dim s As String = "Print ทดสอบ"
        RawPrinterHelper.SendStringToPrinter(pd.DefaultPageSettings.PrinterSettings.PrinterName, s)
        'End If'

        'RawPrinterHelper.SendBytesToPrinter(pd.DefaultPageSettings.PrinterSettings.PrinterName, s, 0)

    End Sub
End Class
