Imports System.IO
Imports Engine.Common
Namespace CSI
    Public Class FilterTemplateENG
        Dim _FilterTemplateID As Long = 0
        Dim _err As String = ""

        Public ReadOnly Property FilterTemplateID() As Long
            Get
                Return _FilterTemplateID
            End Get
        End Property
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property

        Public Function SaveFilterService(ByVal LoginName As String, ByVal para As CenParaDB.TABLE.TbFilterServiceCenParaDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim lnq As New CenLinqDB.TABLE.TbFilterServiceCenLinqDB
            lnq.TB_FILTER_ID = para.TB_FILTER_ID
            lnq.TB_ITEM_ID = para.TB_ITEM_ID
            lnq.TARGET_PERCENT = para.TARGET_PERCENT
            lnq.CHK = para.CHK

            Dim ret As Boolean = lnq.InsertData(LoginName, trans.Trans)
            If ret = False Then
                _err = lnq.ErrorMessage
            End If
            Return ret
        End Function

        Public Function SaveFilterStaff(ByVal LoginName As String, ByVal para As CenParaDB.TABLE.TbFilterStaffCenParaDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim lnq As New CenLinqDB.TABLE.TbFilterStaffCenLinqDB
            lnq.TB_FILTER_ID = para.TB_FILTER_ID
            lnq.SHOP_ID = para.SHOP_ID
            lnq.USERNAME = para.USERNAME

            Dim ret As Boolean = lnq.InsertData(LoginName, trans.Trans)
            If ret = False Then
                _err = lnq.ErrorMessage
            End If
            Return ret
        End Function

        Public Function SaveSetFilterToShop(ByVal LoginName As String, ByVal FilterID As Long, ByVal ShopSelect As DataTable, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = True
            _FilterTemplateID = FilterID
            DeleteFilterShop(trans)
            If Not ShopSelect Is Nothing AndAlso ShopSelect.Rows.Count > 0 Then
                For i As Integer = 0 To ShopSelect.Rows.Count - 1
                    Dim shop_id As String = ShopSelect.Rows(i)("ShopID") & ""
                    Dim template_code As String = ShopSelect.Rows(i)("ShTemplateCode") & ""

                    Dim lnq As New CenLinqDB.TABLE.TbFilterShopCenLinqDB
                    lnq.TB_SHOP_ID = Convert.ToInt64(shop_id)
                    lnq.TB_FILTER_ID = FilterID
                    lnq.SH_TEMPLATE_CODE = template_code

                    ret = lnq.InsertData(LoginName, trans.Trans)
                    If ret = False Then
                        _err = lnq.ErrorMessage
                        Exit For
                    End If
                Next
            End If

            Return ret
        End Function

#Region "Filter Backlist"
        Public Function SaveFileBlackListFileData(ByVal LoginName As String, ByVal f As CenParaDB.Common.TmpFileUploadPara) As Long
            Dim ret As Long = 0
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbFilterAttBacklistFileCenLinqDB
            lnq.FILE_NAME = f.FileName
            lnq.FILE_SIZE = f.TmpFileByte.Length
            lnq.FILE_EXT = f.FileExtent
            If lnq.InsertData(LoginName, trans.Trans) = True Then
                ret = lnq.ID
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                _err = lnq.ErrorMessage
            End If
            Return ret
        End Function


        Public Function InsertMobileFromExcel(ByVal LoginName As String, ByVal fleName As String, ByVal BlackListFileID As Long) As Boolean
            Dim ret As Boolean = False
            Dim edt As DataTable = ReadMobileNoFromExcel(fleName)
            If edt.Rows.Count > 0 Then
                For Each edr As DataRow In edt.Rows
                    If edr(0).ToString.Trim <> "" Then
                        If edr(0).ToString.Trim.Length = 10 Then
                            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                            Dim mLnq As New CenLinqDB.TABLE.TbFilterAttBacklistMobileCenLinqDB

                            If mLnq.ChkDuplicateByMOBILE_NO_TB_FILTER_ATT_BACKLIST_FILE_ID(edr(0), BlackListFileID, 0, trans.Trans) = False Then
                                Try
                                    Dim mobile_no As Integer = edr(0)
                                    mLnq.TB_FILTER_ATT_BACKLIST_FILE_ID = BlackListFileID
                                    mLnq.MOBILE_NO = edr(0)    'เอาคอลัมน์แรก ไม่รู้ชื่อคอลัมน์ที่แน่ชัด

                                    ret = mLnq.InsertData(LoginName, trans.Trans)
                                    If ret = False Then
                                        trans.RollbackTransaction()
                                        _err += "Duplicate MobileNo " & mLnq.MOBILE_NO & vbNewLine
                                    Else
                                        trans.CommitTransaction()
                                    End If

                                Catch ex As Exception
                                    _err += ex.Message
                                    trans.RollbackTransaction()
                                End Try
                            Else
                                trans.CommitTransaction()
                            End If
                            mLnq = Nothing
                        End If
                    End If
                Next
            End If
            Return ret
        End Function
        'Public Function SaveBackListFile(ByVal LoginName As String, ByVal f As CenParaDB.Common.TmpFileUploadPara) As Boolean
        '    Dim ret As Boolean = True
        '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '    Dim lnq As New CenLinqDB.TABLE.TbFilterAttBacklistFileCenLinqDB
        '    lnq.FILE_NAME = f.FileName
        '    lnq.FILE_SIZE = f.TmpFileByte.Length
        '    lnq.FILE_EXT = f.FileExtent
        '    ret = lnq.InsertData(LoginName, trans.Trans)
        '    If ret = True Then
        '        trans.CommitTransaction()

        '        If f.TmpFileByte.Length > 0 Then
        '            Dim fleName As String = FunctionEng.GetUploadPath & lnq.ID & "_" & f.FileName
        '            If File.Exists(fleName) = True Then
        '                Try
        '                    File.SetAttributes(fleName, FileAttributes.Normal)
        '                    File.Delete(fleName)
        '                Catch ex As Exception
        '                    _err = ex.Message
        '                    FunctionEng.SaveErrorLog("FilterTemplateENG.SaveBackListFile", "Cannot Delete File :" & fleName & vbNewLine & ex.Message)
        '                End Try
        '            End If

        '            Dim IsHasFile As Boolean = False
        '            Try
        '                Dim fs As New FileStream(fleName, FileMode.CreateNew)
        '                Dim fileByte() As Byte = f.TmpFileByte
        '                fs.Write(fileByte, 0, fileByte.Length)
        '                fs.Close()
        '                IsHasFile = True
        '            Catch ex As Exception
        '                _err = ex.Message
        '                FunctionEng.SaveErrorLog("FilterTemplateENG.SaveBackListFile", "Cannot Create File :" & fleName & vbNewLine & ex.Message)
        '            End Try

        '            If IsHasFile = True Then
        '                Dim edt As DataTable = ReadMobileNoFromExcel(fleName)
        '                If edt.Rows.Count > 0 Then
        '                    For Each edr As DataRow In edt.Rows
        '                        If edr(0).ToString.Trim <> "" Then
        '                            If edr(0).ToString.Trim.Length = 10 Then
        '                                Try
        '                                    Dim mobile_no As Integer = edr(0)

        '                                    trans = New CenLinqDB.Common.Utilities.TransactionDB
        '                                    Dim mLnq As New CenLinqDB.TABLE.TbFilterAttBacklistMobileCenLinqDB
        '                                    mLnq.TB_FILTER_ATT_BACKLIST_FILE_ID = lnq.ID
        '                                    mLnq.MOBILE_NO = edr(0)    'เอาคอลัมน์แรก ไม่รู้ชื่อคอลัมน์ที่แน่ชัด

        '                                    ret = mLnq.InsertData(LoginName, trans.Trans)
        '                                    If ret = False Then
        '                                        trans.RollbackTransaction()
        '                                        _err = mLnq.ErrorMessage
        '                                        Exit For
        '                                    Else
        '                                        trans.CommitTransaction()
        '                                    End If
        '                                Catch ex As Exception

        '                                End Try
        '                            End If
        '                        End If
        '                    Next
        '                End If
        '            End If
        '        End If
        '    Else
        '        trans.RollbackTransaction()
        '        _err = lnq.ErrorMessage
        '    End If
        '    lnq = Nothing

        '    Return ret
        'End Function

        Public Function CheckDupFileName(ByVal FileName As String, ByVal FileID As Long) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbFilterAttBacklistFileCenLinqDB
            ret = lnq.ChkDuplicateByFILE_NAME(FileName, FileID, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return ret
        End Function

        'Dim XLSConnect As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;"
        Dim XLSConnect As String = "Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 8.0;"
        Dim XLXSConnect As String = "Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 12.0;"

        Public Function ReadMobileNoFromExcel(ByVal ExcelFile As String) As DataTable
            Dim dt As New DataTable
            Dim Sheet1Dt As New DataTable

            Try
                Dim conn As System.Data.OleDb.OleDbConnection
                If ExcelFile.ToUpper.EndsWith("XLS") Then
                    Dim xls As String = XLSConnect & "Data Source=" & ExcelFile & ";"
                    conn = New System.Data.OleDb.OleDbConnection(xls)
                    'Sheet1Dt = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, Nothing)
                ElseIf ExcelFile.ToUpper.EndsWith("XLSX") Then
                    Dim xlsx As String = XLXSConnect & "Data Source=" & ExcelFile & ";"
                    conn = New System.Data.OleDb.OleDbConnection(xlsx)
                    'Sheet1Dt = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, Nothing)
                End If
                conn.Open()
                Sheet1Dt = conn.GetSchema("Tables")

                If Sheet1Dt.Rows.Count > 0 Then
                    Dim ShName As String = Sheet1Dt.Rows(0)("TABLE_NAME")
                    Dim strExcel As String = "select * from [" & ShName & "]"
                    Dim cmd As New OleDb.OleDbCommand
                    cmd.Connection = conn
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = strExcel
                    Dim da As New OleDb.OleDbDataAdapter(cmd)
                    da.Fill(dt)
                End If
                conn.Close()
            Catch ex As Exception
                LogDebug("$$$$$" & ex.Message & " $$$$$$$$$ " & ex.StackTrace)
                FunctionEng.SaveErrorLog("FilterTemplateENG.ReadMobileNoFromExcel", ex.Message & " $$$$$$$$$ " & ex.StackTrace)
            End Try
            Sheet1Dt = Nothing

            Return dt
        End Function

        Public Function GetMobileList(ByVal wh As String) As DataTable
            Dim sql As String = "select bm.id, bm.mobile_no, bf.file_name, bm.create_date, bm.create_by "
            sql += " from tb_filter_att_backlist_file bf"
            sql += " inner join tb_filter_att_backlist_mobile bm on bm.tb_filter_att_backlist_file_id=bf.id"
            sql += " where 1=1 " & wh
            sql += " order by bm.create_date desc, bm.mobile_no"

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbFilterAttBacklistMobileCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Function GetDuplicateMobileList() As DataTable
            Dim sql As String = " select bm.id,bm.mobile_no,fb.file_name,bm.create_date,bm.create_by "
            sql += " from TB_FILTER_ATT_BACKLIST_MOBILE bm"
            sql += " inner join TB_FILTER_ATT_BACKLIST_FILE fb on fb.id=bm.tb_filter_att_backlist_file_id"
            sql += " inner join (select COUNT(id) QtyID,mobile_no"
            sql += "            from TB_FILTER_ATT_BACKLIST_MOBILE "
            sql += "            group by mobile_no"
            sql += "            having COUNT(id)>1) b on b.mobile_no=bm.mobile_no"
            sql += " order by bm.mobile_no "

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbFilterAttBacklistMobileCenLinqDB
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

#End Region


        Public Function GetShopList() As DataTable
            Dim sql As String = ""
            sql += " select id, shop_code , shop_name_en shop_name,'' sh_template_code"
            sql += " from tb_shop "
            sql += " where active='Y' "
            sql += " order by shop_code "

            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbShopCenLinqDB
                dt = lnq.GetListBySql(sql, trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If
            Return dt
        End Function

        Public Function GetSegmentList() As DataTable
            Dim sql As String = "select segment_name from tb_segment order by segment_name"
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbSegmentCenLinqDB
                dt = lnq.GetListBySql(sql, trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If
            Return dt
        End Function



        Public Function GetFilterShopList(ByVal FilterID As Long) As DataTable
            Dim sql As String = ""
            sql += " select tb_shop_id,isnull(sh_template_code,'') sh_template_code"
            sql += " from tb_filter_shop "
            sql += " where tb_filter_id  = " & FilterID

            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbFilterShopCenLinqDB
                dt = lnq.GetListBySql(sql, trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If
            Return dt
        End Function

        Public Function GetFilterServiceList(ByVal FilterID As Long) As DataTable
            Dim ret As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbFilterServiceCenLinqDB
                Dim sql As String = ""

                Dim Sdt As New DataTable
                Sdt = lnq.GetDataList("tb_filter_id = '" & FilterID & "'", "", trans.Trans)
                If Sdt.Rows.Count > 0 Then
                    sql += " select t.id, t.item_name, f.target_percent, f.tb_filter_id, f.chk "
                    sql += " from tb_item t "
                    sql += " left join tb_filter_service f on f.tb_item_id=t.id "
                    sql += " where t.active_status='1' and (f.tb_filter_id = " & FilterID & " or f.id is null)"
                    sql += " order by t.item_order "
                    Sdt = Nothing

                Else
                    sql += " select t.id, t.item_name, null target_percent, null tb_filter_id, 'N' chk "
                    sql += " from tb_item t "
                    sql += " where t.active_status='1' "
                    sql += " order by t.item_order "
                End If

                Dim dt As DataTable = lnq.GetListBySql(sql, trans.Trans)
                ret = dt
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        Public Function GetFilterBacklistFileList() As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbFilterAttBacklistFileCenLinqDB
                dt = lnq.GetDataList("1=1", "file_name", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function

        Public Function GetFilterUser(ByVal FilterID As Long) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("shop_id")
            ret.Columns.Add("shop_name")
            ret.Columns.Add("shop_abb")
            ret.Columns.Add("user_id")
            ret.Columns.Add("username")
            ret.Columns.Add("staff_name")
            ret.Columns.Add("fname")
            ret.Columns.Add("lname")
            ret.Columns.Add("UQID")

            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbFilterStaffCenLinqDB
                Dim dt As DataTable = lnq.GetDataList("tb_filter_id = " & FilterID, "username", trans.Trans)
                For Each dr As DataRow In dt.Rows
                    Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
                    shLnq = FunctionEng.GetTbShopCenLinqDB(dr("shop_id"))
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)

                    If shTrans.Trans IsNot Nothing Then
                        Dim uLnq As New ShLinqDB.TABLE.TbUserShLinqDB

                        Dim sql As String = ""
                        sql += " select u.id, isnull(t.title_name ,'') + u.fname + ' ' + u.lname staff_name, u.fname, u.lname, u.username"
                        sql += " from tb_user u"
                        sql += " left join TB_TITLE t on t.id=u.title_id"
                        sql += " where u.username = '" & dr("username") & "'"

                        Dim uDT As DataTable = uLnq.GetListBySql(sql, shTrans.Trans)
                        If uDT.Rows.Count > 0 Then
                            Dim rDr As DataRow = ret.NewRow
                            rDr("shop_id") = dr("shop_id")
                            rDr("shop_name") = shLnq.SHOP_NAME_EN
                            rDr("shop_abb") = shLnq.SHOP_ABB
                            rDr("user_id") = uDT.Rows(0)("id")
                            rDr("username") = dr("username")
                            rDr("staff_name") = uDT.Rows(0)("staff_name")
                            rDr("fname") = uDT.Rows(0)("fname")
                            rDr("lname") = uDT.Rows(0)("lname")
                            rDr("UQID") = dr("shop_id") & uDT.Rows(0)("id")
                            ret.Rows.Add(rDr)
                        End If
                    End If
                Next
            End If

            Return ret
        End Function



        Public Function GetServiceAtShop(ByVal ShopID As Long) As DataTable
            Dim ret As New DataTable
            Dim trans As New CenDbTrans
            If trans.Trans IsNot Nothing Then
                Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = FunctionEng.GetTbShopCenLinqDB(ShopID)
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
                If shTrans.Trans IsNot Nothing Then
                    Dim lnq As New ShLinqDB.TABLE.TbItemShLinqDB
                    Dim sql As String = "select item_name, '' template_code from tb_item where active_status='1' order by item_order"
                    ret = lnq.GetListBySql(sql, shTrans.Trans)
                    lnq = Nothing
                End If
                shLnq = Nothing
            End If
            Return ret
        End Function

        Public Function SaveFilterTemplate(ByVal LoginName As String, ByVal para As CenParaDB.TABLE.TbFilterCenParaDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim lnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
            If para.ID <> 0 Then
                lnq.GetDataByPK(para.ID, trans.Trans)
            End If

            lnq.FILTER_NAME = para.FILTER_NAME
            lnq.CATEGORY = para.CATEGORY
            lnq.CONTACT_CLASS = para.CONTACT_CLASS
            lnq.NATIONALITY = para.NATIONALITY
            lnq.SEGMENT = para.SEGMENT
            lnq.PREIOD_DATEFROM = para.PREIOD_DATEFROM
            lnq.PREIOD_DATETO = para.PREIOD_DATETO
            lnq.PREIOD_TIMEFROM = para.PREIOD_TIMEFROM
            lnq.PREIOD_TIMETO = para.PREIOD_TIMETO
            lnq.SCHEDULETYPEDAY = para.SCHEDULETYPEDAY
            lnq.SCHEDULEMONDAY = para.SCHEDULEMONDAY
            lnq.SCHEDULETUEDAY = para.SCHEDULETUEDAY
            lnq.SCHEDULEWEDDAY = para.SCHEDULEWEDDAY
            lnq.SCHEDULETHUDAY = para.SCHEDULETHUDAY
            lnq.SCHEDULEFRIDAY = para.SCHEDULEFRIDAY
            lnq.SCHEDULESATDAY = para.SCHEDULESATDAY
            lnq.SCHEDULESUNDAY = para.SCHEDULESUNDAY
            lnq.TARGET = para.TARGET
            lnq.TARGET_UNLIMITED = para.TARGET_UNLIMITED
            lnq.TEMPLATE_CODE = para.TEMPLATE_CODE
            lnq.DURATION_TYPE = para.DURATION_TYPE
            lnq.DURATION_AFTER_MIN = para.DURATION_AFTER_MIN
            lnq.DURATION_EVERY_MIN = para.DURATION_EVERY_MIN
            lnq.ACTIVE_STATUS = para.ACTIVE_STATUS
            lnq.LAST_SAVE_FILTER = DateTime.Now
            lnq.CAL_TARGET = para.CAL_TARGET
            lnq.NETWORK_TYPE = para.NETWORK_TYPE
            lnq.BLANK_CATEGORY = para.BLANK_CATEGORY
            lnq.BLANK_CONTACT_CLASS = para.BLANK_CONTACT_CLASS
            lnq.SURVEY_ALL_SHOP = para.SURVEY_ALL_SHOP
            lnq.GEN_ACT_ALL_SURVEY = para.GEN_ACT_ALL_SURVEY
            lnq.CATEGORY_GEN_ACT_ALL_SURVEY = para.CATEGORY_GEN_ACT_ALL_SURVEY
            lnq.SUBCATEGORY_GEN_ACT_ALL_SURVEY = para.SUBCATEGORY_GEN_ACT_ALL_SURVEY
            lnq.CAT_GEN_ACT_ALL_SURVEY_UPC = para.CAT_GEN_ACT_ALL_SURVEY_UPC
            lnq.SUBCAT_GEN_ACT_ALL_SURVEY_UPC = para.SUBCAT_GEN_ACT_ALL_SURVEY_UPC
            lnq.OWNER_GEN_ACT_ALL_SURVEY = para.OWNER_GEN_ACT_ALL_SURVEY

            lnq.GEN_ACT_RESULT3 = para.GEN_ACT_RESULT3
            lnq.CATEGORY_GEN_ACT_RESULT3 = para.CATEGORY_GEN_ACT_RESULT3
            lnq.SUBCATEGORY_GEN_ACT_RESULT3 = para.SUBCATEGORY_GEN_ACT_RESULT3
            lnq.CAT_GEN_ACT_RESULT3_UPC = para.CAT_GEN_ACT_RESULT3_UPC
            lnq.SUBCAT_GEN_ACT_RESULT3_UPC = para.SUBCAT_GEN_ACT_RESULT3_UPC
            lnq.OWNER_GEN_ACT_RESULT3 = para.OWNER_GEN_ACT_RESULT3

            Dim ret As Boolean = False
            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(LoginName, trans.Trans)
            Else
                ret = lnq.InsertData(LoginName, trans.Trans)
            End If

            If ret = True Then
                _FilterTemplateID = lnq.ID
            Else
                _err = lnq.ErrorMessage
            End If
            Return ret
        End Function

        Public Sub DeleteFilterService(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB)
            'Dim lnq As New CenLinqDB.TABLE.TbFilterServiceCenLinqDB
            'Dim dt As DataTable = lnq.GetDataList("tb_filter_id = " & _FilterTemplateID, "", trans.Trans)
            'If dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        lnq.DeleteByPK(Convert.ToInt64(dr("id")), trans.Trans)
            '    Next
            'End If
            'dt = Nothing
            'lnq = Nothing

            Dim dSql As String = "delete from tb_filter_service where tb_filter_id=" & _FilterTemplateID
            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, trans.Trans)
        End Sub

        Public Sub DeleteFilterStaff(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB)
            'Dim lnq As New CenLinqDB.TABLE.TbFilterStaffCenLinqDB
            'Dim dt As DataTable = lnq.GetDataList("tb_filter_id = " & _FilterTemplateID, "", trans.Trans)
            'If dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        lnq.DeleteByPK(Convert.ToInt64(dr("id")), trans.Trans)
            '    Next
            'End If
            'lnq = Nothing
            'dt = Nothing

            Dim dSql As String = "delete from tb_filter_staff where tb_filter_id=" & _FilterTemplateID
            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, trans.Trans)
        End Sub

        Private Sub DeleteFilterShop(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB)
            'Dim lnq As New CenLinqDB.TABLE.TbFilterShopCenLinqDB
            'Dim dt As DataTable = lnq.GetDataList("tb_filter_id = " & _FilterTemplateID, "", trans.Trans)
            'If dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        lnq.DeleteByPK(Convert.ToInt64(dr("id")), trans.Trans)
            '    Next
            'End If
            'dt = Nothing
            'lnq = Nothing

            Dim dSql As String = "delete from tb_filter_shop where tb_filter_id=" & _FilterTemplateID
            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, trans.Trans)
        End Sub

        Public Sub DeleteFilterBacklistFile(ByVal FileID As Long)
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbFilterAttBacklistFileCenLinqDB
            Dim dt As DataTable = lnq.GetDataList("id = " & FileID, "", trans.Trans)
            trans.CommitTransaction()
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    trans = New CenLinqDB.Common.Utilities.TransactionDB
                    Dim dSql As String = "delete from TB_FILTER_ATT_BACKLIST_MOBILE where tb_filter_att_backlist_file_id='" & dr("id") & "'"
                    CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, trans.Trans)

                    'Dim mLnq As New CenLinqDB.TABLE.TbFilterAttBacklistMobileCenLinqDB
                    'Dim mDt As DataTable = mLnq.GetDataList("tb_filter_att_backlist_file_id = " & dr("id"), "", trans.Trans)
                    'If mDt.Rows.Count > 0 Then
                    '    For Each mDr As DataRow In mDt.Rows
                    '        mLnq.DeleteByPK(Convert.ToInt64(mDr("id")), trans.Trans)
                    '    Next
                    'End If
                    'mLnq = Nothing
                    'mDt = Nothing
                    Try
                        lnq.DeleteByPK(Convert.ToInt64(dr("id")), trans.Trans)
                        trans.CommitTransaction()

                        'Delete File in Folder
                        Dim fleName As String = FunctionEng.GetUploadPath & dr("id") & "_" & dr("file_name")
                        If File.Exists(fleName) = True Then
                            Try
                                File.SetAttributes(fleName, FileAttributes.Normal)
                                File.Delete(fleName)
                            Catch ex As Exception

                            End Try
                        End If
                    Catch ex As Exception
                        trans.RollbackTransaction()
                    End Try
                Next
            End If
            lnq = Nothing
            dt.Dispose()
        End Sub

        Public Sub DeleteBacklistMobile(ByVal vID As Long)
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim mLnq As New CenLinqDB.TABLE.TbFilterAttBacklistMobileCenLinqDB
            mLnq.DeleteByPK(vID, trans.Trans)
            trans.CommitTransaction()
        End Sub

        Public Sub DeleteTempTarget(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB)
            'Dim lnq As New CenLinqDB.TABLE.TbFilterTempTargetCenLinqDB
            'Dim dt As DataTable = lnq.GetDataList("tb_filter_id = " & _FilterTemplateID, "", trans.Trans)
            'If dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        lnq.DeleteByPK(Convert.ToInt64(dr("id")), trans.Trans)
            '    Next
            'End If
            'dt = Nothing
            'lnq = Nothing

            Dim dSql As String = "delete from tb_filter_temp_target where tb_filter_id=" & _FilterTemplateID
            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, trans.Trans)
        End Sub

        Public Function GetFilterTemplatePara(ByVal vID As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As CenParaDB.TABLE.TbFilterCenParaDB
            Dim para As New CenParaDB.TABLE.TbFilterCenParaDB
            Dim lnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
            para = lnq.GetParameter(vID, trans.Trans)

            Return para
        End Function

        'Public Function Get

        Public Function GetFilterList(ByVal WhText As String) As DataTable
            Dim sql As String = ""
            sql += "select f.id, f.filter_name, f.template_code, f.active_status,  "
            sql += " case f.active_status when 'Y' then 'Active' else 'Hold' end status_name, "
            sql += " case f.target_unlimited when 'Y' then 'Unlimited' else str(f.[target]) end [target], count(fs.id) shop_use, "
            sql += " f.preiod_datefrom, f.preiod_dateto"
            sql += " from tb_filter f "
            sql += " left join TB_FILTER_SHOP fs on fs.tb_filter_id=f.id"
            sql += " where " & WhText
            sql += " group by f.id, f.filter_name, f.category, f.template_code, f.active_status,   "
            sql += " f.active_status,  f.target_unlimited, f.[target],f.preiod_datefrom, f.preiod_dateto"
            sql += " order by f.filter_name "

            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
                dt = lnq.GetListBySql(sql, trans.Trans)
                trans.CommitTransaction()
            End If

            Return dt
        End Function

        Public Function DeleteFilterTemplate(ByVal FilterID As Long) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                _FilterTemplateID = FilterID
                Dim lnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
                DeleteFilterService(trans)
                DeleteFilterStaff(trans)
                DeleteFilterShop(trans)
                DeleteTempTarget(trans)
                'DeleteFilterBacklistFile(FilterID)

                ret = lnq.DeleteByPK(FilterID, trans.Trans)
                If ret = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
            End If

            Return ret
        End Function

        Public Function CheckDuplicateFilter(ByVal FilterName As String, ByVal FilterID As Long) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
                ret = lnq.ChkDuplicateByFILTER_NAME(FilterName, FilterID, trans.Trans)
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        Public Shared Sub LogDebug(ByVal sqlText As String)
            Dim FILE_NAME As String = "C:\CSIDebugLog" & DateTime.Now.ToString("yyyyMMdd") & ".txt"
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " " & sqlText)
            objWriter.Close()
        End Sub

#Region "TW"
      
        Public Sub DeleteTWTempTarget(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB)
            Dim dSql As String = "delete from TW_FILTER_TEMP_TARGET where tw_filter_id=" & _FilterTemplateID
            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, trans.Trans)
        End Sub

        Public Sub DeleteTWFilterStaff(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB)
            Dim dSql As String = "delete from TW_FILTER_STAFF where tw_filter_id=" & _FilterTemplateID
            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, trans.Trans)
        End Sub

        Public Sub DeleteTWFilterService(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB)
            Dim dSql As String = "delete from TW_FILTER_ORDER_TYPE where tw_filter_id=" & _FilterTemplateID
            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, trans.Trans)
        End Sub

        Public Function GetTWFilterServiceList(ByVal FilterID As Long) As DataTable
            Dim ret As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TwFilterOrderTypeCenLinqDB
                Dim sql As String = ""

                Dim Sdt As New DataTable
                Sdt = lnq.GetDataList("tw_filter_id = '" & FilterID & "'", "", trans.Trans)
                If Sdt.Rows.Count > 0 Then
                    sql += " select t.id, t.order_type_name as item_name, f.target_percent, f.tw_filter_id"
                    sql += " from TW_SFF_ORDER_TYPE t "
                    sql += " left join TW_FILTER_ORDER_TYPE f on f.tw_sff_order_type_id=t.id "
                    sql += " where t.active='Y' and (f.tw_filter_id = " & FilterID & " or f.id is null)"
                    sql += " and upper(t.order_type_name)<>'PAYMENT'"
                    sql += " order by t.order_type_name"
                    Sdt = Nothing
                Else
                    sql += " select t.id, t.order_type_name as item_name, null target_percent, null tb_filter_id, 'N' chk "
                    sql += " from TW_SFF_ORDER_TYPE t "
                    sql += " where t.active='Y' "
                    sql += " and upper(t.order_type_name)<>'PAYMENT'"
                    sql += " order by t.order_type_name "
                End If

                Dim dt As DataTable = lnq.GetListBySql(sql, trans.Trans)
                ret = dt
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        Public Function SaveTWFilterTemplate(ByVal LoginName As String, ByVal para As CenParaDB.TABLE.TwFilterCenParaDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim lnq As New CenLinqDB.TABLE.TwFilterCenLinqDB
            If para.ID <> 0 Then
                lnq.GetDataByPK(para.ID, trans.Trans)
            End If

            lnq.FILTER_NAME = para.FILTER_NAME
            'lnq.CATEGORY = para.CATEGORY
            'lnq.CONTACT_CLASS = para.CONTACT_CLASS
            lnq.NATIONALITY = para.NATIONALITY
            lnq.SEGMENT = para.SEGMENT
            lnq.PREIOD_DATEFROM = para.PREIOD_DATEFROM
            lnq.PREIOD_DATETO = para.PREIOD_DATETO
            lnq.PREIOD_TIMEFROM = para.PREIOD_TIMEFROM
            lnq.PREIOD_TIMETO = para.PREIOD_TIMETO
            lnq.SCHEDULETYPEDAY = para.SCHEDULETYPEDAY
            lnq.SCHEDULEMONDAY = para.SCHEDULEMONDAY
            lnq.SCHEDULETUEDAY = para.SCHEDULETUEDAY
            lnq.SCHEDULEWEDDAY = para.SCHEDULEWEDDAY
            lnq.SCHEDULETHUDAY = para.SCHEDULETHUDAY
            lnq.SCHEDULEFRIDAY = para.SCHEDULEFRIDAY
            lnq.SCHEDULESATDAY = para.SCHEDULESATDAY
            lnq.SCHEDULESUNDAY = para.SCHEDULESUNDAY
            lnq.TARGET = para.TARGET
            lnq.TARGET_UNLIMITED = para.TARGET_UNLIMITED
            lnq.TEMPLATE_CODE = para.TEMPLATE_CODE
            lnq.ACTIVE_STATUS = para.ACTIVE_STATUS
            lnq.LAST_SAVE_FILTER = DateTime.Now
            lnq.CAL_TARGET = para.CAL_TARGET
            lnq.NETWORK_TYPE = para.NETWORK_TYPE
            lnq.ORDER_PAYMENT_PER = para.ORDER_PAYMENT_PER
            lnq.ORDER_SFF_PER = para.ORDER_SFF_PER
            lnq.CHK_ORDER_SFF = para.CHK_ORDER_SFF
            lnq.GEN_ACT_ALL_SURVEY = para.GEN_ACT_ALL_SURVEY
            lnq.CATEGORY_GEN_ACT_ALL_SURVEY = para.CATEGORY_GEN_ACT_ALL_SURVEY
            lnq.SUBCATEGORY_GEN_ACT_ALL_SURVEY = para.SUBCATEGORY_GEN_ACT_ALL_SURVEY
            lnq.CAT_GEN_ACT_ALL_SURVEY_UPC = para.CAT_GEN_ACT_ALL_SURVEY_UPC
            lnq.SUBCAT_GEN_ACT_ALL_SURVEY_UPC = para.SUBCAT_GEN_ACT_ALL_SURVEY_UPC
            lnq.OWNER_GEN_ACT_ALL_SURVEY = para.OWNER_GEN_ACT_ALL_SURVEY

            lnq.GEN_ACT_RESULT3 = para.GEN_ACT_RESULT3
            lnq.CATEGORY_GEN_ACT_RESULT3 = para.CATEGORY_GEN_ACT_RESULT3
            lnq.SUBCATEGORY_GEN_ACT_RESULT3 = para.SUBCATEGORY_GEN_ACT_RESULT3
            lnq.CAT_GEN_ACT_RESULT3_UPC = para.CAT_GEN_ACT_RESULT3_UPC
            lnq.SUBCAT_GEN_ACT_RESULT3_UPC = para.SUBCAT_GEN_ACT_RESULT3_UPC
            lnq.OWNER_GEN_ACT_RESULT3 = para.OWNER_GEN_ACT_RESULT3

            Dim ret As Boolean = False
            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(LoginName, trans.Trans)
            Else
                ret = lnq.InsertData(LoginName, trans.Trans)
            End If

            If ret = True Then
                _FilterTemplateID = lnq.ID
            Else
                _err = lnq.ErrorMessage
            End If
            Return ret
        End Function

        Public Function SaveTWFilterService(ByVal LoginName As String, ByVal para As CenParaDB.TABLE.TwFilterOrderTypeCenParaDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim lnq As New CenLinqDB.TABLE.TwFilterOrderTypeCenLinqDB
            lnq.TW_FILTER_ID = para.TW_FILTER_ID
            lnq.TW_SFF_ORDER_TYPE_ID = para.TW_SFF_ORDER_TYPE_ID
            lnq.TARGET_PERCENT = para.TARGET_PERCENT

            Dim ret As Boolean = lnq.InsertData(LoginName, trans.Trans)
            If ret = False Then
                _err = lnq.ErrorMessage
            End If
            Return ret
        End Function


        Public Function SaveSetTWFilterToShop(ByVal LoginName As String, ByVal FilterID As Long, ByVal ShopID As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = True
            _FilterTemplateID = FilterID
            DeleteTWFilterShop(trans)
            If ShopID.Trim <> "" Then
                For Each sh As String In Split(ShopID, ",")
                    Dim lnq As New CenLinqDB.TABLE.TwFilterBranchCenLinqDB
                    lnq.TW_LOCATION_ID = Convert.ToInt64(sh)
                    lnq.TW_FILTER_ID = FilterID

                    ret = lnq.InsertData(LoginName, trans.Trans)
                    If ret = False Then
                        _err = lnq.ErrorMessage
                        Exit For
                    End If
                Next
            End If

            Return ret
        End Function

        Private Sub DeleteTWFilterShop(ByVal trans As CenLinqDB.Common.Utilities.TransactionDB)
            Dim dSql As String = "delete from TW_FILTER_BRANCH where tw_filter_id=" & _FilterTemplateID
            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(dSql, trans.Trans)
        End Sub

        Public Function GetTWFilterList(ByVal WhText As String) As DataTable
            Dim sql As String = ""

            sql += " select f.id, f.filter_name, f.template_code, f.active_status, "
            sql += "case f.active_status when 'Y' then 'Active' else 'Hold' end status_name,  "
            sql += "case f.target_unlimited when 'Y' then 'Unlimited' else str(f.[target]) end [target], "
            sql += "count(fs.id) shop_use,  f.preiod_datefrom, f.preiod_dateto "
            sql += " from tw_filter f  "
            sql += " left join TW_FILTER_BRANCH fs on fs.tw_filter_id=f.id "
            sql += " where " & WhText
            sql += " group by f.id, f.filter_name,  f.template_code, f.active_status,  "
            sql += "f.active_status, f.target_unlimited, f.[target], f.preiod_datefrom, f.preiod_dateto"
            sql += " order by f.filter_name "



            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbFilterCenLinqDB
                dt = lnq.GetListBySql(sql, trans.Trans)
                trans.CommitTransaction()
            End If

            Return dt
        End Function

        Public Function GetTWFilterTemplatePara(ByVal vID As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As CenParaDB.TABLE.TwFilterCenParaDB
            Dim para As New CenParaDB.TABLE.TwFilterCenParaDB
            Dim lnq As New CenLinqDB.TABLE.TwFilterCenLinqDB
            para = lnq.GetParameter(vID, trans.Trans)

            Return para
        End Function

        Public Function DeleteTWFilterTemplate(ByVal FilterID As Long) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                _FilterTemplateID = FilterID
                Dim lnq As New CenLinqDB.TABLE.TwFilterCenLinqDB
                DeleteTWFilterService(trans)
                DeleteTWFilterShop(trans)
                DeleteTWTempTarget(trans)

                ret = lnq.DeleteByPK(FilterID, trans.Trans)
                If ret = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
            End If

            Return ret
        End Function

        Public Function CheckDuplicateTwFilter(ByVal FilterName As String, ByVal FilterID As Long) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TwFilterCenLinqDB
                ret = lnq.ChkDuplicateByFILTER_NAME(FilterName, FilterID, trans.Trans)
                trans.CommitTransaction()
            End If
            Return ret
        End Function
#End Region
    End Class
End Namespace

