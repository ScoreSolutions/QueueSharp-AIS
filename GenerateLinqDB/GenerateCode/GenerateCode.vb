Imports System
Imports System.Data
Imports System.Configuration
Imports System.Text
Imports System.IO
Imports VBd = GenerateVB.Data
Imports VBf = GenerateVB.Flow
Imports VBg = GenerateVB.Utilities
Imports Cd = GenerateC.Data
Imports Cf = GenerateC.Flow
Imports Cg = GenerateC.Utilities

Public Class GenerateCode

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

    Private Sub GenerateCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        TextLostFocus(txtProjectCode, Constant.ProjectCodeWaterMarkText)
        TextLostFocus(txtDataSource, Constant.DataSourcesWaterMarkText)
        TextLostFocus(txtUserID, Constant.UserIDWaterMarkText)
        TextLostFocus(txtDatabaseName, Constant.DatabaseNameWaterMarkText)
        TextLostFocus(txtPassword, Constant.PasswordWaterMarkText)

    End Sub

    Private Sub TextGotFocus(ByVal txt As TextBox, ByVal WaterMarkText As String)
        If txt.Text = WaterMarkText Then
            txt.Text = ""
        Else
            txt.Select()
        End If
    End Sub
    Private Sub TextLostFocus(ByVal txt As TextBox, ByVal WaterMarkText As String)
        If txt.Text = "" Then
            txt.Text = WaterMarkText
        End If
    End Sub

    Private Sub txtDatabaseName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDatabaseName.GotFocus
        TextGotFocus(txtDatabaseName, Constant.DatabaseNameWaterMarkText)
    End Sub

    Private Sub txtDatabaseName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDatabaseName.LostFocus
        TextLostFocus(txtDatabaseName, Constant.DatabaseNameWaterMarkText)
    End Sub

    Private Sub txtDataSource_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataSource.GotFocus
        TextGotFocus(txtDataSource, Constant.DataSourcesWaterMarkText)
    End Sub

    Private Sub txtDataSource_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataSource.LostFocus
        TextLostFocus(txtDataSource, Constant.DataSourcesWaterMarkText)
    End Sub

    Private Sub txtPassword_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword.GotFocus
        TextGotFocus(txtPassword, Constant.PasswordWaterMarkText)
    End Sub

    Private Sub txtPassword_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword.LostFocus
        TextLostFocus(txtPassword, Constant.PasswordWaterMarkText)
    End Sub

    Private Sub txtProjectCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProjectCode.GotFocus
        TextGotFocus(txtProjectCode, Constant.ProjectCodeWaterMarkText)
    End Sub

    Private Sub txtProjectCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProjectCode.LostFocus
        TextLostFocus(txtProjectCode, Constant.ProjectCodeWaterMarkText)
    End Sub

    Private Sub txtUserID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.GotFocus
        TextGotFocus(txtUserID, Constant.UserIDWaterMarkText)
    End Sub

    Private Sub txtUserID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.LostFocus
        TextLostFocus(txtUserID, Constant.UserIDWaterMarkText)
    End Sub

    'Private Function GetVBData(ByVal type As String) As VBd.GenerateData
    '    Dim data As New VBd.GenerateData
    '    data.DataSource = txtDataSource.Text.Trim()
    '    data.DataBaseName = txtDatabaseName.Text.Trim()
    '    data.UserID = txtUserID.Text.Trim()
    '    data.Password = txtPassword.Text.Trim()
    '    data.TableName = cmbTableList.SelectedValue
    '    data.DatabaseType = IIf(rdiTypeSQL.Checked, "SQL", "Oracle")
    '    data.ClassName = txtClassName.Text.Trim()
    '    data.ProjectName = txtProjectCode.Text
    '    data.NameSpaces = Type & IIf(txtNamespace.Text.Trim() = "", "", ".") & txtNamespace.Text.Trim()
    '    data.Language = IIf(rdiLangVB.Checked, "VB", "C#")
    '    data.ObjType = type

    '    Return data
    'End Function

    Private Function GetConnectPara() As VBd.GenerateData
        Dim data As New VBd.GenerateData
        data.DataSource = txtDataSource.Text.Trim()
        data.DataBaseName = txtDatabaseName.Text.Trim()
        data.UserID = txtUserID.Text.Trim()
        data.Password = txtPassword.Text.Trim()
        'data.TableName = cmbTableList.SelectedValue
        data.DatabaseType = IIf(rdiTypeSQL.Checked, "SQL", "Oracle")
        'data.ClassName = txtClassName.Text.Trim()
        data.ProjectName = txtProjectCode.Text
        'data.NameSpaces = type & IIf(txtNamespace.Text.Trim() = "", "", ".") & txtNamespace.Text.Trim()
        data.Language = IIf(rdiLangVB.Checked, "VB", "C#")

        Return data
    End Function


    Private Function GetVBData(ByVal type As String, ByVal dr As DataRow, ByVal className As String) As VBd.GenerateData
        Dim data As New VBd.GenerateData
        data.DataSource = txtDataSource.Text.Trim()
        data.DataBaseName = txtDatabaseName.Text.Trim()
        data.UserID = txtUserID.Text.Trim()
        data.Password = txtPassword.Text.Trim()
        data.TableName = dr("TABLE_NAME")
        data.DatabaseType = IIf(rdiTypeSQL.Checked, "SQL", "Oracle")
        data.ClassName = className
        data.ProjectName = txtProjectCode.Text
        '        data.NameSpaces = type & IIf(dr("TABLE_TYPE").ToString() = "", "", ".") & dr("TABLE_TYPE").ToString()
        data.NameSpaces = dr("TABLE_TYPE").ToString()
        data.Language = IIf(rdiLangVB.Checked, "VB", "C#")
        data.ObjType = type
        data.LinqDB = txtLinqDB.Text
        data.ParaDB = txtParameter.Text

        Return data
    End Function

    'Private Function GetCData(ByVal type As String) As Cd.GenerateData
    '    Dim data As New Cd.GenerateData
    '    data.DataSource = txtDataSource.Text.Trim()
    '    data.DataBaseName = txtDatabaseName.Text.Trim()
    '    data.UserID = txtUserID.Text.Trim()
    '    data.Password = txtPassword.Text.Trim()
    '    data.TableName = cmbTableList.SelectedValue
    '    data.DatabaseType = IIf(rdiTypeSQL.Checked, "SQL", "Oracle")
    '    data.ClassName = txtClassName.Text.Trim()
    '    'data.UserHostName = Request.UserHostName
    '    data.ProjectName = txtProjectCode.Text
    '    data.NameSpaces = type & IIf(txtNamespace.Text.Trim() = "", "", ".") & txtNamespace.Text.Trim()
    '    data.Language = IIf(rdiLangVB.Checked, "VB", "C#")
    '    data.ObjType = type

    '    Return data
    'End Function



    Private Function ValidateData(ByVal chkCon As Boolean) As Boolean
        Dim ret As Boolean = True
        If txtProjectCode.Text.Trim() = Constant.ProjectCodeWaterMarkText Then
            MsgBox("กรุณาระบุ" & Constant.ProjectCodeRequire, MsgBoxStyle.OkOnly + 48, "Validate Data")
            ret = False
            txtProjectCode.Focus()
        ElseIf txtDataSource.Text.Trim() = Constant.DataSourcesWaterMarkText Then
            MsgBox("กรุณาระบุ" & Constant.DataSourceRequire, MsgBoxStyle.OkOnly + 48, "Validate Data")
            ret = False
            txtDataSource.Focus()
        ElseIf txtUserID.Text.Trim = Constant.UserIDWaterMarkText Then
            MsgBox("กรุณาระบุ" & Constant.UserIDRequire, MsgBoxStyle.OkOnly + 48, "Validate Data")
            ret = False
            txtUserID.Focus()
        ElseIf txtDatabaseName.Text.Trim = Constant.DatabaseNameWaterMarkText Then
            MsgBox("กรุณาระบุ" & Constant.DatabaseNameRequire, MsgBoxStyle.OkOnly + 48, "Validate Data")
            ret = False
            txtDatabaseName.Focus()
        ElseIf txtPassword.Text.Trim = Constant.PasswordWaterMarkText Then
            MsgBox("กรุณาระบุ" & Constant.PasswordRequire, MsgBoxStyle.OkOnly + 48, "Validate Data")
            ret = False
            txtPassword.Focus()
        ElseIf txtLinqDB.Text.Trim = "" Then
            MsgBox("กรุณาระบุชื่อคลาส Linq", MsgBoxStyle.OkOnly + 48, "Valicate Data")
            ret = False
            txtLinqDB.Focus()
        ElseIf txtParameter.Text.Trim = "" Then
            MsgBox("กรุณาระบุชื่อคลาส Parameter", MsgBoxStyle.OkOnly + 48, "Valicate Data")
            ret = False
            txtParameter.Focus()
        End If

        Return ret
    End Function

    Private Sub GenerateTable(ByVal dr As DataRow)
        'Dim dr As DataRow = dt.Rows(i)
        If rdiTypeSQL.Checked Then
            If rdiLangVB.Checked Then
                'Set Default

                Dim PathLinq As String = txtOutputPath.Text & txtLinqDB.Text & "\" & dr("TABLE_TYPE")
                Dim PathPara As String = txtOutputPath.Text & txtParameter.Text & "\" & dr("TABLE_TYPE")
                If Directory.Exists(PathLinq) = False Then
                    Directory.CreateDirectory(PathLinq)
                End If
                If Directory.Exists(PathPara) = False Then
                    Directory.CreateDirectory(PathPara)
                End If

                tabLinq.TabPages.Add(dr("TABLE_NAME"), dr("TABLE_NAME"))
                tabPara.TabPages.Add(dr("TABLE_NAME"), dr("TABLE_NAME"))

                Dim txtLinq As New TextBox
                txtLinq.Multiline = True
                txtLinq.Dock = DockStyle.Fill
                txtLinq.ScrollBars = ScrollBars.Both

                Dim txtPara As New TextBox
                txtPara.Multiline = True
                txtPara.Dock = DockStyle.Fill
                txtPara.ScrollBars = ScrollBars.Both


                Dim className As String = ""
                For Each Str As String In Split(dr("TABLE_NAME").ToString().ToLower, "_")
                    className += Strings.Left(Str, 1).ToUpper & Strings.Right(Str, Str.Length - 1)
                Next

                _sqlVBFlow = New VBf.SqlGenerateFlow
                txtLinq.Text = _sqlVBFlow.GenerateCodeDAL(GetVBData(txtLinqDB.Text, dr, className))
                txtPara.Text = _sqlVBFlow.GenerateCodeData(GetVBData(txtParameter.Text, dr, className))
                tabLinq.TabPages(dr("TABLE_NAME")).Controls.Add(txtLinq)
                tabPara.TabPages(dr("TABLE_NAME")).Controls.Add(txtPara)

                SaveToOutput(PathLinq & "\" & className & txtLinqDB.Text & ".vb", txtLinq.Text)
                SaveToOutput(PathPara & "\" & className & txtParameter.Text & ".vb", txtPara.Text)
            Else

            End If
        Else
            If rdiLangVB.Checked Then

            Else

            End If
        End If
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If ValidateData(True) = True Then
            Dim dt As New DataTable
            If rdiTypeSQL.Checked Then
                dt = SqlVBFlow.GetTable(txtTableName.Text, GetConnectPara())
            Else
                'dt = OraVBFlow.GetTable(txtTableName.Text, GetConnectPara())
            End If
            If dt.Rows.Count > 0 Then
                GenerateTable(dt.Rows(0))
            End If
        End If
    End Sub

    Private Sub btnGenAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenAll.Click
        If ValidateData(True) = True Then
            Try
                For i As Integer = tabLinq.TabPages.Count - 1 To 0 Step -1
                    tabLinq.TabPages.RemoveAt(i)
                Next
                For j As Integer = tabPara.TabPages.Count - 1 To 0 Step -1
                    tabPara.TabPages.RemoveAt(j)
                Next

                Dim dt As DataTable
                If rdiTypeSQL.Checked Then
                    dt = SqlVBFlow.GetAllTable(GetConnectPara())
                Else
                    dt = OraVBFlow.GetAllTable(GetConnectPara())
                End If

                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        GenerateTable(dt.Rows(i))
                    Next
                End If
            Catch ex As ApplicationException
                Throw New ApplicationException(ex.Message)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub SaveToOutput(ByVal fleName As String, ByVal txtFile As String)
        Dim oWrite As StreamWriter
        oWrite = File.CreateText(fleName)
        oWrite.WriteLine(txtFile)
        oWrite.Close()
    End Sub

    Private Sub btnBrowseFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFolder.Click
        Dim fbd As New FolderBrowserDialog
        Dim result As DialogResult = fbd.ShowDialog
        If result = DialogResult.OK Then
            txtOutputPath.Text = fbd.SelectedPath & "\"
        End If
    End Sub

End Class