Imports Cen = CenLinqDB.TABLE
Imports Engine.Common
Imports System.Windows.Forms

Public Class ArchMasterENG
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
            _TXT_LOG.Text += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "  " & LogDesc & vbNewLine & _TXT_LOG.Text
            _OldLog = LogDesc
        End If
    End Sub

    Public Sub ArchiveData()
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim csLnq As New Cen.TbShopCenLinqDB
            Dim dt As DataTable = FunctionEng.GetActiveShop()
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Application.DoEvents()
                    Dim shLnq As Cen.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(dr("id"))

                    Dim cmdPam(1) As Object
                    cmdPam(0) = trans
                    cmdPam(1) = shLnq

                    Try
                        Dim cmdCt As DegProcessArchiveCounter = AddressOf ProcessArchiveCounter
                        cmdCt.DynamicInvoke(cmdPam)
                    Catch ex As Exception
                        FunctionEng.SaveErrorLog("ArchMasterENG.ArchiveData", "ArchMasterENG.ProcessArciveCounter DegProcessArciveCounter: " & ex.Message, Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                        PrintLog("ArchMasterENG.ArchiveData ArchMasterENG.ProcessArciveCounter DegProcessArciveCounter: " & ex.Message)
                    End Try

                    Try
                        Dim cmdCCT As DegProcessArchiveCounterCustomertype = AddressOf ProcessArchiveConterCustomertype
                        cmdCCT.DynamicInvoke(cmdPam)
                    Catch ex As Exception
                        FunctionEng.SaveErrorLog("ArchMasterENG.ArchiveData", "ArchMasterENG.ProcessArchiveConterCustomertype DegProcessArchiveCounterCustomertype: " & ex.Message, Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                        PrintLog("ArchMasterENG.ArchiveData ArchMasterENG.ProcessArchiveConterCustomertype DegProcessArchiveCounterCustomertype: " & ex.Message)
                    End Try

                    Try
                        Dim cmdCCA As DegProcessArchiveCounterCustomertypeAllow = AddressOf ProcessArchiveConterCustomertypeAllow
                        cmdCCA.DynamicInvoke(cmdPam)
                    Catch ex As Exception
                        FunctionEng.SaveErrorLog("ArchMasterENG.ArchiveData", "ArchMasterENG.DegProcessArchiveCounterCustomertypeAllow ProcessArchiveConterCustomertypeAllow: " & ex.Message, Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                        PrintLog("ArchMasterENG.ArchiveData ArchMasterENG.DegProcessArchiveCounterCustomertypeAllow ProcessArchiveConterCustomertypeAllow: " & ex.Message)
                    End Try

                    Try
                        Dim cmdGroupUser As DegProcessArchiveGroupUser = AddressOf ProcessArchiveGroupUser
                        cmdGroupUser.DynamicInvoke(cmdPam)
                    Catch ex As Exception
                        FunctionEng.SaveErrorLog("ArchMasterENG.ProcessArchiveGroupUser", "ArchMasterENG.DegProcessArchiveGroupUser ProcessArchiveGroupUser: " & ex.Message, Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                        PrintLog("ArchMasterENG.ArchiveData ArchMasterENG.DegProcessArchiveGroupUser ProcessArchiveGroupUser: " & ex.Message)
                    End Try

                    Try
                        Dim cmdGroupUserMenu As DegProcessArchiveGroupUserMenu = AddressOf ProcessArchiveGroupUserMenu
                        cmdGroupUserMenu.DynamicInvoke(cmdPam)
                    Catch ex As Exception
                        FunctionEng.SaveErrorLog("ArchMasterENG.ProcessArchiveGroupUser", "ArchMasterENG.DegProcessArchiveGroupUserMenu ProcessArchiveGroupUserMenu: " & ex.Message, Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                        PrintLog("ArchMasterENG.ArchiveData ArchMasterENG.DegProcessArchiveGroupUserMenu ProcessArchiveGroupUserMenu: " & ex.Message)
                    End Try

                    Try
                        Dim cmdITEM As DegProcessArchiveItem = AddressOf ProcessArchiveItem
                        cmdITEM.DynamicInvoke(cmdPam)
                    Catch ex As Exception
                        FunctionEng.SaveErrorLog("ArchMasterENG.ArchiveData", "ArchMasterENG.DegProcessArchiveItem ProcessArchiveItem: " & ex.Message, Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                        PrintLog("ArchMasterENG.ArchiveData ArchMasterENG.DegProcessArchiveItem ProcessArchiveItem: " & ex.Message)
                    End Try
                Next
            End If
            trans.CommitTransaction()
        End If
    End Sub

    Private Function GetProcDT(ByVal TbName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
        Dim sql As String = "select id from " & TbName
        Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        shTrans.CommitTransaction()

        Return dt
    End Function

    Private Function CheckArchiveData(ByVal TbName As String, ByVal FieldName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
        Dim sql As String = "select id from " & TbName
        sql += " where convert(varchar(10)," & FieldName & ",120) = '" & DateTime.Now.ToString("yyyy-MM-dd", New Globalization.CultureInfo("en-US")) & "'"
        Dim dt As DataTable = ShLinqDB.Common.Utilities.SqlDB.ExecuteTable(sql, shTrans.Trans)
        shTrans.CommitTransaction()

        Return dt.Rows.Count > 0
    End Function

    Private Delegate Sub DegProcessArchiveCounter(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
    Private Sub ProcessArchiveCounter(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "archMasterENG.ProcessArchiveCounter")
        If shTrans.Trans IsNot Nothing Then
            Dim dt As DataTable = GetProcDT("tb_counter", shTrans)
            If dt.Rows.Count > 0 Then
                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "archMasterENG.ProcessArchiveCounter")
                If shTrans.Trans IsNot Nothing Then
                    If CheckArchiveData("tb_counter_history", "create_date", shTrans) = False Then
                        FunctionEng.SaveTransLog("ArchMasterENG.ProcessArciveCounter", "Start Archive Master Data(TB_COUNTER) on Shop " & shLnq.SHOP_NAME_EN)
                        For i As Integer = 0 To dt.Rows.Count - 1
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "archMasterENG.ProcessArchiveCounter")
                            If shTrans.Trans IsNot Nothing Then
                                Dim src As New ShLinqDB.TABLE.TbCounterShLinqDB
                                src.GetDataByPK(dt.Rows(i)("id"), shTrans.Trans)

                                Dim dst As New ShLinqDB.TABLE.TbCounterHistoryShLinqDB
                                dst.COUNTER_CODE = src.COUNTER_CODE
                                dst.COUNTER_NAME = src.COUNTER_NAME
                                dst.COUNTER_STATUS = src.COUNTER_STATUS
                                dst.QUICKSERVICE = src.QUICKSERVICE
                                dst.BEEP = src.BEEP
                                dst.RETURN_CASE = src.RETURN_CASE
                                dst.AUTO_SWAP = src.AUTO_SWAP
                                dst.SPEED_LANE = src.SPEED_LANE
                                dst.UNITDISPLAY = src.UNITDISPLAY
                                dst.AVAILABLE = src.AVAILABLE
                                dst.BACK_OFFICE = src.BACK_OFFICE
                                dst.COUNTER_MANAGER = src.COUNTER_MANAGER
                                dst.ACTIVE_STATUS = src.ACTIVE_STATUS
                                dst.TB_COUNTER_ID = src.ID

                                If dst.InsertData("ArchiveData", shTrans.Trans) = True Then
                                    shTrans.CommitTransaction()
                                Else
                                    shTrans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ArchMasterENG.ProcessArchiveCounter", dst.ErrorMessage & " TB_COUNTER.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"), Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                                    PrintLog("ArchMasterENG.ProcessArchiveCounter " & dst.ErrorMessage & " TB_COUNTER.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End If
    End Sub

    Private Delegate Sub DegProcessArchiveCounterCustomertype(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
    Private Sub ProcessArchiveConterCustomertype(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveConterCustomertype")
        If shTrans.Trans IsNot Nothing Then
            Dim dt As DataTable = GetProcDT("tb_counter_customertype", shTrans)
            If dt.Rows.Count > 0 Then
                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveConterCustomertype")
                If shTrans.Trans IsNot Nothing Then
                    If CheckArchiveData("tb_counter_customertype_history", "create_date", shTrans) = False Then
                        FunctionEng.SaveTransLog("ArchMasterENG.ProcessArchiveConterCustomertype", "Start Archive Master Data(TB_COUNTER_CUSTOMERTYPE) on Shop " & shLnq.SHOP_NAME_EN)
                        For i As Integer = 0 To dt.Rows.Count - 1
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveConterCustomertype")
                            If shTrans.Trans IsNot Nothing Then
                                Dim src As New ShLinqDB.TABLE.TbCounterCustomertypeShLinqDB
                                src.GetDataByPK(dt.Rows(i)("id"), shTrans.Trans)

                                Dim dst As New ShLinqDB.TABLE.TbCounterCustomertypeHistoryShLinqDB
                                dst.COUNTER_ID = src.COUNTER_ID
                                dst.CUSTOMERTYPE_ID = src.CUSTOMERTYPE_ID
                                dst.TB_COUNTER_CUSTOMERTYPE_ID = src.ID
                                
                                If dst.InsertData("ArchiveData", shTrans.Trans) = True Then
                                    shTrans.CommitTransaction()
                                Else
                                    shTrans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ArchMasterENG.ProcessArchiveConterCustomertype", dst.ErrorMessage & " TB_COUNTER_CUSTOMERTYPE.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"), Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                                    PrintLog("ArchMasterENG.ProcessArchiveConterCustomertype " & dst.ErrorMessage & " TB_COUNTER_CUSTOMERTYPE.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End If
    End Sub

    Private Delegate Sub DegProcessArchiveCounterCustomertypeAllow(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
    Private Sub ProcessArchiveConterCustomertypeAllow(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveConterCustomertypeAllow")
        If shTrans.Trans IsNot Nothing Then
            Dim dt As DataTable = GetProcDT("tb_counter_customertype_allow", shTrans)
            If dt.Rows.Count > 0 Then
                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveConterCustomertypeAllow")
                If shTrans.Trans IsNot Nothing Then
                    If CheckArchiveData("tb_counter_customertype_allow_history", "create_date", shTrans) = False Then
                        FunctionEng.SaveTransLog("ArchMasterENG.ProcessArchiveConterCustomertypeAllow", "Start Archive Master Data(TB_COUNTER_CUSTOMERTYPE_ALLOW) on Shop " & shLnq.SHOP_NAME_EN)
                        For i As Integer = 0 To dt.Rows.Count - 1
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveConterCustomertypeAllow")
                            If shTrans.Trans IsNot Nothing Then
                                Dim src As New ShLinqDB.TABLE.TbCounterCustomertypeAllowShLinqDB
                                src.GetDataByPK(dt.Rows(i)("id"), shTrans.Trans)

                                Dim dst As New ShLinqDB.TABLE.TbCounterCustomertypeAllowHistoryShLinqDB
                                dst.COUNTER_ID = src.COUNTER_ID
                                dst.CUSTOMERTYPE_ID = src.CUSTOMERTYPE_ID
                                dst.TB_COUNTER_CUSTOMERTYPE_ALLOW_ID = src.ID

                                If dst.InsertData("ArchiveData", shTrans.Trans) = True Then
                                    shTrans.CommitTransaction()
                                Else
                                    shTrans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ArchMasterENG.ProcessArchiveConterCustomertypeAllow", dst.ErrorMessage & " TB_COUNTER_CUSTOMERTYPE_ALLOW.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"), Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                                    PrintLog("ArchMasterENG.ProcessArchiveConterCustomertypeAllow " & dst.ErrorMessage & " TB_COUNTER_CUSTOMERTYPE_ALLOW.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End If
    End Sub


    Private Delegate Sub DegProcessArchiveGroupUser(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
    Private Sub ProcessArchiveGroupUser(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveGroupUser")
        If shTrans.Trans IsNot Nothing Then
            Dim dt As DataTable = GetProcDT("tb_groupuser", shTrans)
            If dt.Rows.Count > 0 Then
                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveGroupUser")
                If shTrans.Trans IsNot Nothing Then
                    If CheckArchiveData("tb_groupuser_history", "create_date", shTrans) = False Then
                        FunctionEng.SaveTransLog("ArchMasterENG.ProcessArchiveGroupUser", "Start Archive Master Data(TB_GROUPUSER) on Shop " & shLnq.SHOP_NAME_EN)
                        For i As Integer = 0 To dt.Rows.Count - 1
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveGroupUser")
                            If shTrans.Trans IsNot Nothing Then
                                Dim src As New ShLinqDB.TABLE.TbGroupuserShLinqDB
                                src.GetDataByPK(dt.Rows(i)("id"), shTrans.Trans)

                                Dim dst As New ShLinqDB.TABLE.TbGroupuserHistoryShLinqDB
                                dst.GROUP_CODE = src.GROUP_CODE
                                dst.GROUP_NAME = src.GROUP_NAME
                                dst.ACTIVE_STATUS = src.ACTIVE_STATUS
                                dst.TB_GROUPUSER_ID = src.ID

                                If dst.InsertData("ArchiveData", shTrans.Trans) = True Then
                                    shTrans.CommitTransaction()
                                Else
                                    shTrans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ArchMasterENG.ProcessArchiveGroupUser", dst.ErrorMessage & " TB_GROUPUSER.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"), Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                                    PrintLog("ArchMasterENG.ProcessArchiveGroupUser " & dst.ErrorMessage & " TB_GROUPUSER.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End If
    End Sub

    Private Delegate Sub DegProcessArchiveGroupUserMenu(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
    Private Sub ProcessArchiveGroupUserMenu(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveGroupUserMenu")
        If shTrans.Trans IsNot Nothing Then
            Dim dt As DataTable = GetProcDT("tb_groupuser_menu", shTrans)
            If dt.Rows.Count > 0 Then
                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveGroupUserMenu")
                If shTrans.Trans IsNot Nothing Then
                    If CheckArchiveData("tb_groupuser_menu_history", "create_date", shTrans) = False Then
                        FunctionEng.SaveTransLog("ArchMasterENG.ProcessArchiveGroupUserMenu", "Start Archive Master Data(TB_GROUPUSER_MENU) on Shop " & shLnq.SHOP_NAME_EN)
                        For i As Integer = 0 To dt.Rows.Count - 1
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveGroupUserMenu")
                            If shTrans.Trans IsNot Nothing Then
                                Dim src As New ShLinqDB.TABLE.TbGroupuserMenuShLinqDB
                                src.GetDataByPK(dt.Rows(i)("id"), shTrans.Trans)

                                Dim dst As New ShLinqDB.TABLE.TbGroupuserMenuHistoryShLinqDB
                                dst.GROUP_ID = src.GROUP_ID
                                dst.MENU_ID = src.MENU_ID
                                dst.TB_GROUPUSER_MENU_ID = src.ID

                                If dst.InsertData("ArchiveData", shTrans.Trans) = True Then
                                    shTrans.CommitTransaction()
                                Else
                                    shTrans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ArchMasterENG.ProcessArchiveGroupUserMenu", dst.ErrorMessage & " TB_GROUPUSER_MENU.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"), Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                                    PrintLog("ArchMasterENG.ProcessArchiveGroupUserMenu " & dst.ErrorMessage & " TB_GROUPUSER_MENU.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End If
    End Sub


    Private Delegate Sub DegProcessArchiveItem(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
    Private Sub ProcessArchiveItem(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB)
        Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
        shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveItem")
        If shTrans.Trans IsNot Nothing Then
            Dim dt As DataTable = GetProcDT("tb_item", shTrans)
            If dt.Rows.Count > 0 Then
                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveItem")
                If shTrans.Trans IsNot Nothing Then
                    If CheckArchiveData("tb_item_history", "create_date", shTrans) = False Then
                        FunctionEng.SaveTransLog("ArchMasterENG.ProcessArchiveItem", "Start Archive Master Data(TB_ITEMW) on Shop " & shLnq.SHOP_NAME_EN)
                        For i As Integer = 0 To dt.Rows.Count - 1
                            shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq, "ArchMasterENG.ProcessArchiveItem")
                            If shTrans.Trans IsNot Nothing Then
                                Dim src As New ShLinqDB.TABLE.TbItemShLinqDB
                                src.GetDataByPK(dt.Rows(i)("id"), shTrans.Trans)

                                Dim dst As New ShLinqDB.TABLE.TbItemHistoryShLinqDB
                                dst.ITEM_CODE = src.ITEM_CODE
                                dst.ITEM_NAME = src.ITEM_NAME
                                dst.ITEM_NAME_TH = src.ITEM_NAME_TH
                                dst.ITEM_TIME = src.ITEM_TIME
                                dst.ITEM_WAIT = src.ITEM_WAIT
                                dst.ITEM_ORDER = src.ITEM_ORDER
                                dst.TXT_QUEUE = src.TXT_QUEUE
                                dst.COLOR = src.COLOR
                                dst.ACTIVE_STATUS = src.ACTIVE_STATUS
                                dst.Q_TYPE_ID = src.Q_TYPE_ID
                                dst.APP_MIN_QUEUE = src.APP_MIN_QUEUE
                                dst.APP_MAX_QUEUE = src.APP_MAX_QUEUE
                                dst.TB_ITEM_ID = src.ID

                                If dst.InsertData("ArchiveData", shTrans.Trans) = True Then
                                    shTrans.CommitTransaction()
                                Else
                                    shTrans.RollbackTransaction()
                                    FunctionEng.SaveErrorLog("ArchMasterENG.ProcessArchiveItem", dst.ErrorMessage & " TB_ITEM.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"), Application.StartupPath & "\ErrorLog\", "ArchMasterENG")
                                    PrintLog("ArchMasterENG.ProcessArchiveItem " & dst.ErrorMessage & " TB_ITEM.id=" & src.ID & " ArchiveDate: " & Today.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End If
    End Sub

End Class
