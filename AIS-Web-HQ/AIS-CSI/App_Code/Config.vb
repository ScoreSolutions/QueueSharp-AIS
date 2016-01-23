'Imports Microsoft.VisualBasic
Imports System.Web
'Imports System.Web.Security
'Imports System.Web.UI
Imports System.Configuration
Imports CenParaDB.Common
Imports CenParaDB.Common.Utilities
Imports CenParaDB.TABLE
Imports System.Xml.Serialization
Imports System.IO
Imports System
Imports System.Data
Imports Engine.Common

Public Class Config

    Public Shared Function GetLogOnUser() As UserProfilePara

        Dim ret As New UserProfilePara
        Try
            'Dim id As FormsIdentity = HttpContext.Current.User.Identity
            'Dim tik As FormsAuthenticationTicket = id.Ticket
            'Dim sr As New XmlSerializer(GetType(UserProfilePara))
            'Dim st As New MemoryStream(Convert.FromBase64String(tik.UserData))
            'ret = sr.Deserialize(st)

            ''Get UserProfile From Table in DB
            'Dim UPara As New TbUserCenParaDB
            'Dim eng As New LoginENG
            'UPara = eng.GetLoginUserProfile(ret.USERNAME)
            'ret.USER_PARA = UPara

            ret = Engine.Common.LoginENG.GetLogOnUser
        Catch ex As Exception

        End Try

        Return ret
    End Function
    Public Shared Function GetLoginHistoryID() As Long
        Dim tmp As LoginSessionPara
        tmp = Engine.Common.LoginENG.GetLoginSessionPara()
        Dim ret As Long = tmp.LOGIN_HISTORY_ID
        tmp = Nothing
        Return ret
    End Function

    Public Shared Function GetUserID() As String
        'Return GetLogOnUser().USER_PARA.USER_ID
        Return HttpContext.Current.User.Identity.Name
    End Function

    Public Shared WriteOnly Property SetLogOnUser() As UserProfilePara
        Set(ByVal value As UserProfilePara)
            System.Web.HttpContext.Current.Session(Constant.UserProfileSession) = value
        End Set
    End Property
    Public Shared Function GetCSIVersion() As String
        Return ConfigurationManager.AppSettings("Version").ToString()
    End Function


    'Public Shared Function ChkPermit(ByVal menuID As Long) As Boolean
    '    Dim ret As Boolean = False
    '    Dim uData As UserProfilePara
    '    uData = GetLogOnUser()
    '    If uData IsNot Nothing Then
    '        Dim eng As New AuthenENG
    '        ret = eng.CheckAuthen(menuID, uData)
    '    End If

    '    Return ret
    'End Function

    Public Shared Sub BuildNoColumn(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            If dt.Columns.Contains("NO") = False Then
                dt.Columns.Add("NO")
            End If
            Dim i As Integer = 1
            For Each dr As DataRow In dt.Rows
                dr("no") = i
                i += 1
            Next
        End If
    End Sub

    Public Shared Sub ReRuningNo(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim i As Integer = 1
            For Each dr As DataRow In dt.Rows
                dr("no") = i
                i += 1
            Next
        End If
    End Sub


    Public Shared Sub SetAlert(ByVal msg As String, ByVal frm As Page)
        Dim popupScript As String = "<script language='javascript'>  alert('" & msg & "');  </script>"
        ScriptManager.RegisterStartupScript(frm, GetType(String), "SetAlert1", popupScript, False)
    End Sub

    'Public Shared Sub SetAlert(ByVal msg As String, ByVal frm As Page)
    '    Dim scr As String = ""

    '    scr += " <script> "
    '    scr += " 	$(function() {"
    '    scr += " 		$('#dialog').dialog(); "
    '    scr += " 	});"
    '    scr += " </script>"


    'End Sub

    Public Shared Sub SetAlert(ByVal msg As String, ByVal frm As Page, ByVal ClientID As String)
        Dim popupScript As String = "<script language='javascript'> " & _
        " alert('" & msg & "'); " & _
        " document.getElementById('" & ClientID & "').focus();" & _
        " </script>"
        ScriptManager.RegisterStartupScript(frm, GetType(String), "SetAlert1", popupScript, False)
    End Sub

    Public Shared Function GetUploadPath() As String
        Return Engine.Common.FunctionEng.GetUploadPath()
    End Function

    'Public Shared Function getServerPath() As String
    '    Return ConfigurationManager.AppSettings("UploadPath").ToString
    'End Function
    Public Shared Function UploadFile(ByVal FileUpload1 As FileUpload, ByVal fileName As String, ByVal fldName As String) As Boolean
        'Upload and save file at directory
        Dim ret As Boolean = True

        If FileUpload1.HasFile = True Then
            Dim extension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
            Dim MIMEType As String = ""

            Try
                MIMEType = getMIMEType(FileUpload1.PostedFile.FileName)
                If MIMEType = "" Then
                    Return False
                    Exit Function
                End If

                Dim X As String = Path.GetFileName(fileName)
                X = fldName & X & MIMEType

                If Directory.Exists(fldName) = False Then
                    Directory.CreateDirectory(fldName)
                End If
                FileUpload1.PostedFile.SaveAs(X)
                ret = True
            Catch ex As Exception
                ret = False

            End Try
        End If

        Return ret
    End Function
    Public Shared Function getMIMEType(ByVal vFileName As String) As String
        Dim extension As String = System.IO.Path.GetExtension(vFileName).ToLower()
        Dim MIMEType As String = ""

        Select Case extension
            Case ".jpg", ".jpeg", ".jpe"
                MIMEType = ".jpg"
            Case ".csv", ".xls", ".xlsx", ".pdf", ".doc", ".docx", ".txt", ".png", ".gif"
                MIMEType = extension
            Case ".htm", ".html"
                MIMEType = ".html"
            Case Else
                MIMEType = ""
        End Select

        Return MIMEType

    End Function
    Public Shared Function GetFileURL(ByVal req As HttpRequest) As String
        Return ConfigurationManager.AppSettings("UploadURL").ToString()
    End Function
    Public Shared Function BaseURL(ByVal req As HttpRequest) As String
        Return req.Url.Host & ConfigurationManager.AppSettings("UploadURL").ToString()
    End Function

    Public Shared Sub SaveTransLog(ByVal ClassName As String, ByVal TransDesc As String)
        FunctionEng.SaveTransLog(ClassName, TransDesc)
    End Sub

    Public Shared Sub SaveErrorLog(ByVal ClassName As String, ByVal ErrDesc As String)
        FunctionEng.SaveErrorLog(ClassName, ErrDesc)
    End Sub

    Public Shared Sub GridViewSorting(ByVal gv As GridView, ByVal dt As DataTable, ByVal txtSortDir As TextBox, ByVal txtSortField As TextBox, ByVal e As GridViewSortEventArgs, ByVal PageIndex As Long)
        If e.SortExpression = "DEFAULT" Then
            txtSortDir.Text = e.SortDirection
            txtSortField.Text = e.SortExpression
        Else
            If txtSortField.Text = e.SortExpression Then
                txtSortDir.Text = IIf(txtSortDir.Text.Trim = "", "DESC", "")
            Else : txtSortField.Text = e.SortExpression
            End If
        End If

        Dim sortStr As String = ""
        If txtSortField.Text.Trim <> "" Then
            sortStr = " " & txtSortField.Text & " " & txtSortDir.Text
        End If

        gv.PageIndex = PageIndex
        dt.DefaultView.Sort = sortStr
        dt = dt.DefaultView.ToTable()
        If dt.Columns.Contains("no") Then
            Config.ReRuningNo(dt)
        Else
            Config.BuildNoColumn(dt)
        End If
    End Sub

    Public Shared Sub GridViewSorting(ByVal gv As GridView, ByVal dt As DataTable, ByVal txtSortDir As TextBox, ByVal txtSortField As TextBox, ByVal e As EventArgs, ByVal PageIndex As Long)
        'If e.SortExpression = "DEFAULT" Then
        '    txtSortDir.Text = e.SortDirection
        '    txtSortField.Text = e.SortExpression
        'Else
        '    If txtSortField.Text = e.SortExpression Then
        '        txtSortDir.Text = IIf(txtSortDir.Text.Trim = "", "DESC", "")
        '    Else : txtSortField.Text = e.SortExpression
        '    End If
        'End If

        Dim sortStr As String = ""
        If txtSortField.Text.Trim <> "" Then
            sortStr = " " & txtSortField.Text & " " & txtSortDir.Text
        End If

        gv.PageIndex = PageIndex
        dt.DefaultView.Sort = sortStr
        dt = dt.DefaultView.ToTable()
        If dt.Columns.Contains("no") Then
            Config.ReRuningNo(dt)
        Else
            Config.BuildNoColumn(dt)
        End If
    End Sub

End Class
