Imports System.IO
Imports Engine.Common
Imports CenLinqDB.Common.Utilities
Imports OfficeOpenXml

Namespace CSI


    Public Class CSIMasterENG
        Dim _err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property

        Public Function SaveTWLocation(ByVal LoginName As String, ByVal para As CenParaDB.TABLE.TwLocationCenParaDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
            If para.ID <> 0 Then
                lnq.GetDataByPK(para.ID, trans.Trans)
            End If
            lnq.LOCATION_CODE = para.LOCATION_CODE
            lnq.LOCATION_NAME_TH = para.LOCATION_NAME_TH
            lnq.LOCATION_SEGMENT = para.LOCATION_SEGMENT
            lnq.PROVINCE_CODE = para.PROVINCE_CODE
            lnq.REGION_CODE = para.REGION_CODE
            lnq.LOCATION_TYPE = para.LOCATION_TYPE
            lnq.ACTIVE = para.ACTIVE
            Dim ret As Boolean
            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(LoginName, trans.Trans)
            Else
                ret = lnq.InsertData(LoginName, trans.Trans)
            End If
            If ret = False Then
                _err = lnq.ErrorMessage
            End If
            Return ret
        End Function

        Public Function CheckDuplicateLocation(ByVal LocationCode As String, ByVal LocationID As Long) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
                ret = lnq.ChkDuplicateByLOCATION_CODE(LocationCode, LocationID, trans.Trans)
                trans.CommitTransaction()
            End If
            Return ret
        End Function

        Public Function GetTWLocationPara(ByVal vID As Long, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As CenParaDB.TABLE.TwLocationCenParaDB
            Dim para As New CenParaDB.TABLE.TwLocationCenParaDB
            Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
            para = lnq.GetParameter(vID, trans.Trans)

            Return para
        End Function

        Public Function GetTWLocationByCode(ByVal LocationCode As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As CenLinqDB.TABLE.TwLocationCenLinqDB
            'Dim para As New CenParaDB.TABLE.TwLocationCenParaDB
            Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
            lnq.ChkDataByLOCATION_CODE(LocationCode, trans.Trans)

            Return lnq
        End Function

        Public Function GetTWLocationList(ByVal WhText As String) As DataTable
            Dim sql As String = ""
            sql += " select location_code,location_name_th,location_segment,province_code,region_code,location_type,id from TW_LOCATION"
            sql += " where active = 'Y' and " & WhText
            sql += " order by location_code "

            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
                dt = lnq.GetListBySql(sql, trans.Trans)
                trans.CommitTransaction()
            End If

            Return dt
        End Function

        Public Function GetTWLocationListAll(ByVal WhText As String) As DataTable
            Dim sql As String = ""
            sql += " select location_code,location_name_th,location_segment,province_code,region_code,location_type,id from TW_LOCATION"
            sql += " where 1=1 and " & WhText
            sql += " order by location_code "

            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
                dt = lnq.GetListBySql(sql, trans.Trans)
                trans.CommitTransaction()
            End If

            Return dt
        End Function

        Public Function DeleteTWLocation(ByVal ID As Long) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
                ret = lnq.DeleteByPK(ID, trans.Trans)
                If ret = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
            End If

            Return ret
        End Function

        Public Function SaveTWLocationFromFile(ByVal LoginName As String, ByVal f As CenParaDB.Common.TmpFileUploadPara) As Boolean
            Dim ret As Boolean = True
            Try
                If f.TmpFileByte.Length > 0 Then
                    Dim fleName As String = FunctionEng.GetUploadPath & "_" & f.FileName
                    If File.Exists(fleName) = True Then
                        File.Delete(fleName)
                    End If

                    Dim fs As New FileStream(fleName, FileMode.CreateNew)
                    Dim fileByte() As Byte = f.TmpFileByte
                    fs.Write(fileByte, 0, fileByte.Length)
                    fs.Close()

                    Dim objFilterTemplate As New Engine.CSI.FilterTemplateENG
                    Dim edt As DataTable = objFilterTemplate.ReadMobileNoFromExcel(fleName)
                    If edt.Rows.Count > 0 Then
                        For Each edr As DataRow In edt.Rows
                            If edr(0).ToString.Trim <> "" Then
                                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                                Dim lnq As New CenLinqDB.TABLE.TwLocationCenLinqDB
                                lnq.ChkDataByLOCATION_CODE(edr(0).ToString, trans.Trans)
                                

                                lnq.LOCATION_CODE = edr(0)
                                lnq.LOCATION_NAME_TH = edr(1)
                                lnq.LOCATION_SEGMENT = edr(2)
                                lnq.LOCATION_TYPE = edr(3)
                                lnq.PROVINCE_CODE = edr(4)
                                lnq.REGION_CODE = edr(5)

                                If lnq.ID > 0 Then
                                    ret = lnq.UpdateByPK(LoginName, trans.Trans)
                                Else
                                    lnq.ACTIVE = "Y"
                                    ret = lnq.InsertData(LoginName, trans.Trans)
                                End If
                                If ret = False Then
                                    _err = lnq.ErrorMessage
                                    trans.RollbackTransaction()
                                    Exit For
                                Else
                                    trans.CommitTransaction()
                                End If

                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                ret = False
                _err = ex.Message
            End Try


            Return ret
        End Function

        Public Function GetActivityListAll(ByVal WhText As String, ByVal module_name As String) As DataTable
            Dim sql As String = ""
            sql += "select sas.id,sac.category_name,isnull(subcategory_name,'') subcategory_name from ("
            sql += " (select category_name from TB_SIEBEL_ACTIVITY_CATEGORY  where module_name ='" & module_name & "') sac"
            sql += " Left Join "
            sql += " (select id,category_name,isnull(subcategory_name,'') subcategory_name "
            sql += " from TB_SIEBEL_ACTIVITY_SUBCATEGORY sas where sas.module_name ='" & module_name & "' ) sas on sac.category_name = sas.category_name) "
            sql += " where 1 = 1 and " & WhText & " order by sac.category_name,subcategory_name"

            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbSiebelActivityCategoryCenLinqDB
                dt = lnq.GetListBySql(sql, trans.Trans)
                trans.CommitTransaction()
            End If

            Return dt
        End Function

        Public Function DeleteCategory(ByVal category_name As String, ByVal subcategory_name As String, ByVal module_name As String) As Boolean
            Dim ret As Boolean = True
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Try
                    Dim Sql As String = "delete from TB_SIEBEL_ACTIVITY_SUBCATEGORY "
                    Sql += " where category_name = '" & category_name & "' and subcategory_name ='" & subcategory_name & "' and module_name ='" & module_name & "'"
                    SqlDB.ExecuteNonQuery(Sql, trans.Trans)

                    Dim lnq As New CenLinqDB.TABLE.TbSiebelActivitySubcategoryCenLinqDB
                    Sql = "Select id from TB_SIEBEL_ACTIVITY_SUBCATEGORY where category_name = '" & category_name & "' and module_name ='" & module_name & "'"
                    Dim dt As New DataTable
                    dt = lnq.GetListBySql(Sql, trans.Trans)
                    If dt.Rows.Count = 0 Then
                        Sql = "delete from TB_SIEBEL_ACTIVITY_CATEGORY where category_name = '" & category_name & "' and module_name ='" & module_name & "'"
                        SqlDB.ExecuteNonQuery(Sql, trans.Trans)
                    End If

                Catch ex As Exception
                    ret = False
                End Try

                If ret = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
            End If

            Return ret
        End Function

        Public Function SaveActivityFromFile(ByVal LoginName As String, ByVal f As CenParaDB.Common.TmpFileUploadPara, ByVal module_name As String) As Boolean
            Dim ret As Boolean = True
            Try
                If f.TmpFileByte.Length > 0 Then

                    Dim dtCategory As New DataTable
                    With dtCategory
                        .Columns.Add("category")
                    End With

                    Dim dtSubCategory As New DataTable
                    With dtSubCategory
                        .Columns.Add("category")
                        .Columns.Add("SubCategory")
                    End With


                    Dim str As New MemoryStream(f.TmpFileByte)
                    Dim dr As DataRow
                    Dim excel As New OfficeOpenXml.ExcelPackage()
                    excel.Load(str)


                    Dim worksheet As ExcelWorksheet = excel.Workbook.Worksheets(1)
                    If worksheet.Name.ToLower <> "category" Then
                        _err = "รูปแบบไฟล์ไม่ถูกต้อง"
                        Return False
                    End If
                    If worksheet.Dimension.[End].Row > 0 Then
                        If worksheet.Cells(1, 1).Value.ToString.ToLower <> "category" Then
                            _err = "รูปแบบไฟล์ไม่ถูกต้อง"
                            Return False
                        End If
                    End If

                    For row As Integer = 2 To worksheet.Dimension.[End].Row
                        Dim category As String = worksheet.Cells(row, 1).Value
                        dr = dtCategory.NewRow
                        dr("category") = category
                        dtCategory.Rows.Add(dr)
                    Next

                    If dtCategory.Rows.Count = 0 Then
                        _err = "ไม่พบข้อมูลนำเข้า"
                        Return False
                    End If

                    Dim worksheetSub = excel.Workbook.Worksheets(2)
                    If worksheetSub.Name.ToLower <> "subcategory" Then
                        _err = "รูปแบบไฟล์ไม่ถูกต้อง"
                        Return False
                    End If
                    If worksheetSub.Dimension.[End].Row > 0 Then
                        If worksheetSub.Cells(1, 1).Value.ToString.ToLower <> "category" Then
                            _err = "รูปแบบไฟล์ไม่ถูกต้อง"
                            Return False
                        End If
                        If worksheetSub.Cells(1, 2).Value.ToString.ToLower <> "sub category" Then
                            _err = "รูปแบบไฟล์ไม่ถูกต้อง"
                            Return False
                        End If
                    End If
                    For row As Integer = 2 To worksheetSub.Dimension.[End].Row
                        Dim category As String = worksheetSub.Cells(row, 1).Value
                        Dim subcategory As String = worksheetSub.Cells(row, 2).Value
                        dr = dtSubCategory.NewRow
                        dr("category") = category
                        dr("SubCategory") = subcategory
                        dtSubCategory.Rows.Add(dr)
                    Next

                    str.Close()
                    str = Nothing



                    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                    For Each edr As DataRow In dtCategory.Rows
                        If edr(0).ToString.Trim <> "" Then
                            Dim lnq As New CenLinqDB.TABLE.TbSiebelActivityCategoryCenLinqDB
                            lnq.ChkDataByWhere(" category_name ='" & edr(0).ToString & "' and module_name ='" & module_name & "'", trans.Trans)
                            If lnq.ID = 0 Then
                                lnq.CATEGORY_NAME = edr(0)
                                lnq.ACTIVE_STATUS = "Y"
                                lnq.MODULE_NAME = module_name
                                ret = lnq.InsertData(LoginName, trans.Trans)

                                If ret = False Then
                                    _err = lnq.ErrorMessage
                                    trans.RollbackTransaction()
                                    Exit For
                                End If
                            End If
                        End If
                    Next


                    For Each edr As DataRow In dtSubCategory.Rows
                        If edr(0).ToString.Trim <> "" Then
                            Dim lnq As New CenLinqDB.TABLE.TbSiebelActivitySubcategoryCenLinqDB
                            lnq.ChkDataByWhere(" category_name = '" & edr(0).ToString & "' and subcategory_name = '" & edr(1).ToString & "' and module_name ='" & module_name & "'", trans.Trans)
                            If lnq.ID = 0 Then
                                lnq.CATEGORY_NAME = edr(0)
                                lnq.SUBCATEGORY_NAME = edr(1)
                                lnq.ACTIVE_STATUS = "Y"
                                lnq.MODULE_NAME = module_name
                                ret = lnq.InsertData(LoginName, trans.Trans)

                                If ret = False Then
                                    _err = lnq.ErrorMessage
                                    trans.RollbackTransaction()
                                    Exit For
                                End If
                            End If
                        End If
                    Next

                    If ret = True Then
                        trans.CommitTransaction()
                    End If

                    worksheet = Nothing
                    excel.Dispose()
                    'End Using

                End If
            Catch ex As Exception
                ret = False
                _err = ex.Message
                FunctionEng.SaveErrorLog("CSIMasterENG.SaveActivityFormFile", ex.Message & vbNewLine & ex.StackTrace)
            End Try


            Return ret
        End Function
    End Class
End Namespace