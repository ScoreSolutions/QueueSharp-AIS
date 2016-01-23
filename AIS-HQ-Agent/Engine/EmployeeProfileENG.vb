Imports Engine.Common
Imports System.Windows.Forms

Public Class EmployeeProfileENG
    Dim _err As String = ""
    Dim _TXT_LOG As TextBox

    Public Property ErrorMessage() As String
        Get
            Return _err
        End Get
        Set(ByVal value As String)
            _err = value
        End Set
    End Property

    Public Sub SetTextLog(ByVal txtLog As TextBox)
        _TXT_LOG = txtLog
    End Sub

    Dim _OldLog As String = ""
    Private Sub PrintLog(ByVal LogDesc As String)
        If _OldLog <> LogDesc Then
            _TXT_LOG.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "  " & LogDesc & vbNewLine & _TXT_LOG.Text
            _OldLog = LogDesc
            Application.DoEvents()
        End If
    End Sub

    Public Function UpdateEmployeeProfileByShop(ByVal ShopID As Long) As Boolean
        Dim ret As Boolean = False
        Dim eDt As New DataTable
        eDt.Columns.Add("username")
        eDt.Columns.Add("TbShopCenLinqDB", GetType(CenLinqDB.TABLE.TbShopCenLinqDB))
        Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        If shTrans.CreateTransaction(shLnq.SHOP_DB_SERVER, shLnq.SHOP_DB_USERID, shLnq.SHOP_DB_PWD, shLnq.SHOP_DB_NAME) = True Then
            eDt = GetUserByShop(shLnq, shTrans, eDt)
            shTrans.CommitTransaction()
        Else
            shTrans.RollbackTransaction()
            PrintLog("Cannon Connect To " & shLnq.SHOP_DB_SERVER & "." & shLnq.SHOP_DB_NAME)
            FunctionEng.SaveErrorLog("EmployeeProfileENG.UpdateEmployeeProfileByShop", "Cannon Connect To " & shLnq.SHOP_DB_SERVER & "." & shLnq.SHOP_DB_NAME, Application.StartupPath & "\ErrorLog\", "EmployeeProfileENG")
        End If
        shLnq = Nothing

        ret = UpdateEmployeeProfile(eDt)
        eDt = Nothing

        Return ret
    End Function

    Private Function UpdateEmployeeProfile(eDt As DataTable) As Boolean
        Dim ret As Boolean = False
        If eDt.Rows.Count > 0 Then
            Dim uDt As New DataTable
            uDt = eDt.DefaultView.ToTable(True, "username").Copy
            If uDt.Rows.Count > 0 Then
                Dim OMAuth() As String = Split(FunctionEng.GetQisDBConfig("OM-Auth"), "||")

                For Each uDr As DataRow In uDt.Rows
                    'Call OM WebService with udr("username")
                    Try
                        Dim uPara As New CenParaDB.OMWS.GetEmployeeProfileByUsernamePara
                        Dim ws As New OMLinqWS.EmployeeLinqWS
                        uPara = ws.GetEmployeeProfileByUsername(uDr("username"), OMAuth(0), OMAuth(1), OMAuth(2), OMAuth(3))
                        If uPara.USERID <> "" Then
                            eDt.DefaultView.RowFilter = "username = '" & uDr("username") & "'"
                            Dim eDv As DataView = eDt.DefaultView
                            If eDv.Count > 0 Then
                                For Each e As DataRowView In eDv
                                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = DirectCast(e("TbShopCenLinqDB"), CenLinqDB.TABLE.TbShopCenLinqDB)
                                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                                    If shTrans.CreateTransaction(shLnq.SHOP_DB_SERVER, shLnq.SHOP_DB_USERID, shLnq.SHOP_DB_PWD, shLnq.SHOP_DB_NAME) = True Then
                                        Dim uLnq As New ShLinqDB.TABLE.TbUserShLinqDB
                                        If uLnq.ChkDataByUSERNAME(e("username").ToString, shTrans.Trans) = True Then
                                            uLnq.TITLE_ID = 1
                                            'If uPara.PIN <> "" Then uLnq.USER_CODE = Convert.ToInt64(uPara.PIN)
                                            'If uPara.EN_FIRST_NAME <> "" Then uLnq.FNAME = uPara.EN_FIRST_NAME
                                            'If uPara.EN_LAST_NAME <> "" Then uLnq.LNAME = uPara.EN_LAST_NAME
                                            'If uPara.POSITION_DESC <> "" Then uLnq.POSITION = uPara.POSITION_DESC

                                            uLnq.USER_CODE = uPara.PIN
                                            uLnq.FNAME = uPara.EN_FIRST_NAME
                                            uLnq.LNAME = uPara.EN_LAST_NAME
                                            uLnq.POSITION = uPara.POSITION_DESC

                                            If uLnq.UpdateByPK("AisEmpProfileAgent", shTrans.Trans) = True Then
                                                shTrans.CommitTransaction()
                                                ret = True
                                            Else
                                                shTrans.RollbackTransaction()
                                                FunctionEng.SaveErrorLog("EmployeeProfileENG.UpdateEmployeeProfileAllShop", uLnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "EmployeeProfileENG")
                                                PrintLog(uLnq.ErrorMessage)
                                                ret = False
                                            End If
                                        Else
                                            shTrans.RollbackTransaction()
                                            FunctionEng.SaveErrorLog("EmployeeProfileENG.UpdateEmployeeProfileAllShop", uLnq.ErrorMessage, Application.StartupPath & "\ErrorLog\", "EmployeeProfileENG")
                                            PrintLog(uLnq.ErrorMessage)
                                            ret = False
                                        End If
                                        shTrans.CommitTransaction()
                                        uLnq = Nothing
                                        ret = False
                                    Else
                                        PrintLog(shTrans.ErrorMessage)
                                        FunctionEng.SaveErrorLog("EmployeeProfileENG.UpdateEmployeeProfileAllShop", shTrans.ErrorMessage, Application.StartupPath & "\ErrorLog\", "EmployeeProfileENG")
                                    End If
                                Next
                            Else
                                ret = False
                            End If
                            eDv = Nothing
                            eDt.DefaultView.RowFilter = ""
                            uPara = Nothing
                            ws = Nothing
                        Else
                            PrintLog("OM Service Not Found USER ID : " & uDr("username").ToString & " at Shop : " & GetShopNameFromUserName(uDr("username"), eDt))
                            FunctionEng.SaveErrorLog("EmployeeProfileENG.UpdateEmployeeProfile", "OM Service Not Found USER ID : " & uDr("username").ToString, Application.StartupPath & "\ErrorLog\", "EmployeeProfileENG")
                        End If
                    Catch ex As System.Net.WebException
                        PrintLog(uDr("username") & " " & ex.Message & " ##### " & ex.StackTrace)
                        FunctionEng.SaveErrorLog("EmployeeProfileENG.UpdateEmployeeProfile", uDr("username") & " " & ex.Message & " " & ex.StackTrace, Application.StartupPath & "\ErrorLog\", "EmployeeProfileENG")
                    End Try
                Next
            Else
                ret = False
            End If
            uDt = Nothing
        Else
            ret = False
        End If

        Return ret
    End Function

    Private Function GetShopNameFromUserName(ByVal UserName As String, ByVal eDt As DataTable) As String
        Dim ret As String = ""
        If eDt.Rows.Count > 0 Then
            eDt.DefaultView.RowFilter = "username = '" & UserName & "'"
            If eDt.DefaultView.Count > 0 Then
                For Each eDr As DataRowView In eDt.DefaultView
                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = DirectCast(eDr("TbShopCenLinqDB"), CenLinqDB.TABLE.TbShopCenLinqDB)
                    If ret = "" Then
                        ret = shLnq.SHOP_NAME_EN
                    Else
                        ret += ", " & shLnq.SHOP_NAME_EN
                    End If
                Next
            End If
        End If

        Return ret
    End Function

    Public Function UpdateEmployeeProfileAllShop() As Boolean
        Dim ret As Boolean = False
        Dim eDt As New DataTable
        eDt.Columns.Add("username")
        eDt.Columns.Add("TbShopCenLinqDB", GetType(CenLinqDB.TABLE.TbShopCenLinqDB))

        Dim sDt As DataTable = FunctionEng.GetActiveShop()
        If sDt.Rows.Count > 0 Then
            For Each sDr As DataRow In sDt.Rows
                Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(sDr("id"))
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                If shTrans.CreateTransaction(shLnq.SHOP_DB_SERVER, shLnq.SHOP_DB_USERID, shLnq.SHOP_DB_PWD, shLnq.SHOP_DB_NAME) = True Then
                    eDt = GetUserByShop(shLnq, shTrans, eDt)
                End If
                shTrans.CommitTransaction()
                shLnq = Nothing
            Next
        End If
        sDt = Nothing

        ret = UpdateEmployeeProfile(eDt)

        eDt = Nothing
    End Function

    'Public Function TestGetEmployeeProfileByUsername(ByVal UserName As String) As CenParaDB.OMWS.GetEmployeeProfileByUsernamePara
    '    Dim uPara As New CenParaDB.OMWS.GetEmployeeProfileByUsernamePara
    '    Dim ws As New OMLinqWS.EmployeeLinqWS
    '    Dim OMAuth() As String = Split(FunctionEng.GetQisDBConfig("OM-Auth"), "||")
    '    'OM-Auth = URL||WsUserName||WsPassword||WsDomain
    '    'uPara = ws.GetEmployeeProfileByUsername(UserName, "http://test-omservices.ais.co.th/ExternalServices/WS_OM_OMEHRServices.asmx", "ehr_test1", "Ais.co.th", "corp-aispilot")
    '    'uPara = ws.GetEmployeeProfileByUsername(UserName, "http://omservices.ais.co.th/ExternalServices/WS_OM_OMEHRServices.asmx", "ipfmservice", "IPS#serv3", "corp-ais900")
    '    uPara = ws.GetEmployeeProfileByUsername(UserName, OMAuth(0), OMAuth(1), OMAuth(2), OMAuth(3))
    '    Return uPara
    'End Function

    Private Function GetUserByShop(ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal eDt As DataTable) As DataTable
        Dim sql As String = "select [username] from tb_user where  UPPER([username])<>'ADMIN'"
        Dim uLnq As New ShLinqDB.TABLE.TbUserShLinqDB
        Dim uDt As New DataTable
        uDt = uLnq.GetListBySql(sql, shTrans.Trans)
        uLnq = Nothing

        If uDt.Rows.Count > 0 Then
            For Each uDr As DataRow In uDt.Rows
                Dim eDr As DataRow = eDt.NewRow
                eDr("username") = uDr("username")
                eDr("TbShopCenLinqDB") = shLnq
                eDt.Rows.Add(eDr)
            Next
            uDt = Nothing
        End If
        Return eDt
    End Function
End Class
