Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports VBd = GenerateVB.Data
Imports VBf = GenerateVB.Flow
Imports VBg = GenerateVB.Utilities
Imports Cd = GenerateC.Data
Imports Cf = GenerateC.Flow
Imports Cg = GenerateC.Utilities

Partial Public Class GenerateCode
    Inherits System.Web.UI.Page

    Dim _sqlVBFlow As VBf.SqlGenerateFlow
    Dim _oraVBFlow As VBf.OracleGenerateFlow
    Dim _constVB As VBg.Constant.GenerateConstant

    Dim _sqlCFlow As Cf.SqlGenerateFlow
    Dim _oraCFlow As Cf.OracleGenerateFlow


    Private ReadOnly Property SqlVBFlow() As VBf.SqlGenerateFlow
        Get
            If _sqlVBFlow Is Nothing Then
                _sqlVBFlow = New VBf.SqlGenerateFlow
            End If
            Return _sqlVBFlow
        End Get
    End Property

    Private ReadOnly Property OraVBFlow() As VBf.OracleGenerateFlow
        Get
            If _oraVBFlow Is Nothing Then
                _oraVBFlow = New VBf.OracleGenerateFlow
            End If
            Return _oraVBFlow
        End Get
    End Property

    Private ReadOnly Property SqlCFlow() As Cf.SqlGenerateFlow
        Get
            If _sqlCFlow Is Nothing Then
                _sqlCFlow = New Cf.SqlGenerateFlow
            End If
            Return _sqlCFlow
        End Get
    End Property
    Private ReadOnly Property OraCFlow() As Cf.OracleGenerateFlow
        Get
            If _oraCFlow Is Nothing Then
                _oraCFlow = New Cf.OracleGenerateFlow
            End If
            Return _oraCFlow
        End Get
    End Property

    Private ReadOnly Property Constant() As VBg.Constant.GenerateConstant
        Get
            If _constVB Is Nothing Then
                _constVB = New VBg.Constant.GenerateConstant
            End If
            Return _constVB
        End Get
    End Property
    Private Sub SetPageLoad()
        txtServerWatermark.WatermarkText = Constant.DataSourcesWaterMarkText
        txtDatabaseWatermark.WatermarkText = Constant.DatabaseNameWaterMarkText
        txtUserIDWatermark.WatermarkText = Constant.UserIDWaterMarkText
        txtPasswordWatermark.WatermarkText = Constant.PasswordWaterMarkText
        txtTableWatermark.WatermarkText = Constant.TableNameWaterMarkText
        txtNamespaceWatermark.WatermarkText = Constant.NameSpaceWaterMarkText
        txtClassWatermark.WatermarkText = Constant.ClassNameWaterMarkText

        Dim script As String = ""
        script += "if (document.getElementById('" + txtServer.ClientID + "').value == '' || document.getElementById('" + txtServer.ClientID + "').value == '" + txtServerWatermark.WatermarkText + "') "
        script += "{ alert('" + Constant.DataSourceRequire + "'); return false; }"
        script += "else if (document.getElementById('" + rdiType.ClientID + "').checked && (document.getElementById('" + txtDatabase.ClientID + "').value == '' || document.getElementById('" + txtDatabase.ClientID + "').value == '" + txtDatabaseWatermark.WatermarkText + "')) "
        script += "{ alert('" + Constant.DatabaseNameRequire + "'); return false; }"
        script += "else if (document.getElementById('" + txtUserID.ClientID + "').value == '' || document.getElementById('" + txtUserID.ClientID + "').value == '" + txtUserIDWatermark.WatermarkText + "') "
        script += "{ alert('" + Constant.UserIDRequire + "'); return false; }"
        script += "else if (document.getElementById('" + txtPassword.ClientID + "').value == '' || document.getElementById('" + txtPassword.ClientID + "').value == '" + txtPasswordWatermark.WatermarkText + "') "
        script += "{ alert('" + Constant.PasswordRequire + "'); return false; }"
        script += "else if (document.getElementById('" + txtTable.ClientID + "').value == '' || document.getElementById('" + txtTable.ClientID + "').value == '" + txtTableWatermark.WatermarkText + "') "
        script += "{ alert('" + Constant.TableNameRequire + "'); return false; }"
        script += "else if (document.getElementById('" + txtClass.ClientID + "').value == '' || document.getElementById('" + txtClass.ClientID + "').value == '" + txtClassWatermark.WatermarkText + "') "
        script += "{ alert('" + Constant.ClassNameRequire + "'); " & txtClass.ClientID & ".focus(); return false; }"
        btnGenerateCode.OnClientClick = script
    End Sub

    Private Sub GenerateCode_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'MyBase.OnInit(e)
        If Context.Session Is Nothing Then
            If Session.IsNewSession = True Then
                Dim kCookieHeader = Request.Headers("Cookie")
                If kCookieHeader <> Nothing And kCookieHeader.IndexOf("ASP.NET_SessionId") >= 0 Then
                    txtCodeData.Text = "session timeout"
                    txtCodeDAL.Text = "session timeout"
                End If
            End If
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetPageLoad()
        End If
    End Sub
    Private Function GetVBData(ByVal type As String) As VBd.GenerateData
        Dim data As New VBd.GenerateData
        data.DataSource = txtServer.Text.Trim()
        data.DataBaseName = txtDatabase.Text.Trim()
        data.UserID = txtUserID.Text.Trim()
        data.Password = txtPassword.Text.Trim()
        data.TableName = txtTable.Text.Trim()
        data.DatabaseType = rdiType.SelectedValue
        data.ClassName = txtClass.Text.Trim()
        data.UserHostName = Request.UserHostName
        data.ProjectName = txtProjectCode.Text
        data.NameSpaces = type & IIf(txtNamespace.Text.Trim() = "", "", ".") & txtNamespace.Text.Trim()
        data.Language = rdiLang.SelectedValue

        Return data
    End Function

    Private Function GetCData(ByVal type As String) As Cd.GenerateData
        Dim data As New Cd.GenerateData
        data.DataSource = txtServer.Text.Trim()
        data.DataBaseName = txtDatabase.Text.Trim()
        data.UserID = txtUserID.Text.Trim()
        data.Password = txtPassword.Text.Trim()
        data.TableName = txtTable.Text.Trim()
        data.DatabaseType = rdiType.SelectedValue
        data.ClassName = txtClass.Text.Trim()
        data.UserHostName = Request.UserHostName
        data.ProjectName = txtProjectCode.Text
        data.NameSpaces = type & IIf(txtNamespace.Text.Trim() = "", "", ".") & txtNamespace.Text.Trim()
        data.Language = rdiLang.SelectedValue

        Return data
    End Function

    Private Sub btnGenerateCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateCode.Click
        txtCodeDAL.Text = ""
        txtCodeData.Text = ""

        Try
            If rdiType.SelectedValue = "SQL" Then
                If rdiLang.SelectedValue = "VB" Then
                    txtCodeDAL.Text = SqlVBFlow.GenerateCodeDAL(GetVBData("DAL"))
                    txtCodeData.Text = SqlVBFlow.GenerateCodeData(GetVBData("Data"))
                Else
                    txtCodeDAL.Text = SqlCFlow.GenerateDAL(GetCData("DAL"))
                    txtCodeData.Text = SqlCFlow.GenerateData(GetCData("Data"))
                End If
            Else
                If rdiLang.SelectedValue = "VB" Then
                    txtCodeDAL.Text = OraVBFlow.GenerateCodeDAL(GetVBData("DAL"))
                    txtCodeData.Text = OraVBFlow.GenerateCodeData(GetVBData("Data"))
                Else
                    txtCodeDAL.Text = OraCFlow.GenerateDAL(GetCData("DAL"))
                    txtCodeData.Text = OraCFlow.GenerateData(GetCData("Data"))
                End If
            End If
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Catch ex As Exception
            txtCodeDAL.Text = ex.Message
            txtCodeData.Text = ex.Message
        End Try
    End Sub
End Class