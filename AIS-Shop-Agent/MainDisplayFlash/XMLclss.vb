Imports System.Xml
Imports System.Text
Public Class XMLclss
    Public str_flashFilename As String = "flashFilename"
    Public str_flashHeight As String = "flashHeight"
    Public str_flashWidth As String = "flashWidth"
    Public str_flashPosX As String = "flashPosX"
    Public str_flashPosY As String = "flashPosY"

    Public Sub SetWriteXML(ByVal ConfigXMLFile As String, ByVal flashFilename As String, ByVal flashHeight As String, ByVal flashWidth As String, ByVal flashPosX As String, ByVal flashPosY As String)
        If Not IO.File.Exists(ConfigXMLFile) Then
            Dim xmlText = "<config>" & "</config>"
            Dim xmlDoc As New XmlDocument()
            xmlDoc.LoadXml(xmlText)
            xmlDoc.Save(ConfigXMLFile)
        End If
        Try
            'Create file, overwrite if exists
            'enc is encoding object required by constructor
            'It is null, so default encoding is used
            Dim objXMLTW As New XmlTextWriter(ConfigXMLFile, Encoding.UTF8)
            With objXMLTW
                .IndentChar = vbTab
                .Indentation = 1
                .Formatting = Formatting.Indented

                .WriteStartDocument()
                'Top level (Parent element)
                .WriteStartElement("config")

                'Child elements, from request form
                .WriteElementString(str_flashFilename, flashFilename)
                .WriteElementString(str_flashHeight, flashHeight)
                .WriteElementString(str_flashWidth, flashWidth)
                .WriteElementString(str_flashPosX, flashPosX)
                .WriteElementString(str_flashPosY, flashPosY)

                .WriteEndElement() 'End top level element
                .WriteEndDocument() 'End Document
                .Flush() 'Write to file
                .Close()
            End With
        Catch Ex As Exception

        End Try
    End Sub

    Public Function GetReadXML(ByVal ConfigXMLFile As String, ByVal keyName As String) As String
        Dim getvalue As String = ""
        Dim XmlDoc As XmlDocument = New XmlDocument()
        Try
            With XmlDoc
                XmlDoc.Load(ConfigXMLFile)
                getvalue = .SelectSingleNode("config/" & keyName).InnerText()
                XmlDoc = Nothing
            End With
        Catch ex As Exception
        End Try
        Return getvalue
    End Function
End Class
