Imports System.Data
Partial Class Template_ContentMasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim UserName As String = Config.GetLogOnUser.USERNAME
        If UserName <> "" Then
            If IsPostBack = False Then
                CreateCSIMenu(UserName)
            End If
        Else
            Session.RemoveAll()
            Session.Abandon()
            Response.Redirect("../frmLogin.aspx?rnd=" & DateTime.Now.Millisecond)
        End If
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Session.RemoveAll()
        'Session.Abandon()
        Engine.Common.LoginENG.LogOut(Config.GetLoginHistoryID)
        Response.Redirect("../frmLogin.aspx?rnd=" & DateTime.Now.Millisecond)
    End Sub

    Private Sub CreateCSIMenu(ByVal UserName As String)
        Dim ret As String = ""
        Dim dt As New DataTable
        If UserName.ToUpper = "ADMIN" Then
            dt = Engine.Common.MenuENG.GetMenuList(4)
        Else
            dt = Engine.Common.MenuENG.GetMenuList(4, UserName)
        End If
        If dt.Rows.Count > 0 Then
            dt.DefaultView.RowFilter = "ref_sysmenu_id=0"
            Dim mDt As New DataTable
            mDt = dt.DefaultView.ToTable

            ret += "<div class='arrowlistmenu'> " & vbNewLine
            For Each mDr As DataRow In mDt.Rows
                Dim ChildMenu As String = CreateSubMenu(dt, mDr("id"), 1)
                If ChildMenu.Trim <> "" Then
                    If ChildMenu.Trim <> "" Then
                        ret += "    <h3 class='menuheader expandable' style='background-image:url(../images/MenuHeadBG.png); background-repeat: repeat-x;' >" & mDr("menu_name_th") & "</h3>" & vbNewLine
                        ret += "    <ul class='categoryitems'>" & vbNewLine
                        ret += ChildMenu
                        ret += "<br /><br /><br />"
                        ret += "    </ul>" & vbNewLine
                    End If
                End If


                'ret += "    <h3 class='menuheader expandable' style='background-image:url(../images/MenuHeadBG.png); background-repeat: repeat-x;' >" & mDr("menu_name_th") & "</h3>" & vbNewLine
                'ret += "    <ul class='categoryitems'>" & vbNewLine
                'Dim dtS As DataTable = Engine.Common.MenuENG.GetSubMenuList(mDr("id"))
                'If dtS.Rows.Count > 0 Then
                '    For Each drS As DataRow In dtS.Rows
                '        ret += "    <a href='" & drS("menu_url") & "?rnd=" & DateTime.Now.Millisecond & "' >" & drS("menu_name_en") & "</a><br />" & vbNewLine
                '    Next
                'End If
                'ret += "<br /><br /><br />"
                'ret += "    </ul>" & vbNewLine
            Next
            ret += "</div>" & vbNewLine
            lblMenu.Text = ret
            mDt.Dispose()
        End If
        dt.Dispose()
    End Sub

    Private Function CreateSubMenu(ByVal dt As DataTable, ByVal RefMenuID As String, ByVal IndenLevel As Integer) As String
        Dim ret As String = ""
        dt.DefaultView.RowFilter = "ref_sysmenu_id='" & RefMenuID & "'"
        Dim dtS As DataView
        dtS = dt.DefaultView
        If dtS.Count > 0 Then
            For Each drS As DataRowView In dtS
                Dim TmpRet As String = ""
                Dim Space As String = ""
                For j As Integer = 1 To IndenLevel
                    Space += "&nbsp;&nbsp;&nbsp;"
                Next
                TmpRet = "    <ul class='categoryitems'>" & vbNewLine
                TmpRet += Space & " <a href='" & drS("menu_url") & "?rnd=" & DateTime.Now.Millisecond & "&MenuID=" & drS("id") & "' >" & drS("menu_name_en") & "</a>" & vbNewLine
                TmpRet += CreateSubMenu(dt, drS("id"), IndenLevel + 3)
                TmpRet += "    </ul>" & vbNewLine

                If drS("menu_url").ToString = "#" Then
                    'ถ้าเป็นเมนูที่ไม่มี Link ให้ดูว่ามีเมนูย่อยที่ไม่ได้กำหนดสิทธิ์หรือไม่
                    dt.DefaultView.RowFilter = "ref_sysmenu_id='" & drS("id") & "'"
                    If dt.DefaultView.Count = 0 Then
                        TmpRet = ""
                    End If
                End If

                ret += TmpRet
            Next
        End If

        Return ret
    End Function
End Class

