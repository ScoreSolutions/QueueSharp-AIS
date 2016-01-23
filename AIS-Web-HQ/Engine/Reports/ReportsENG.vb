Imports Engine.Common
Namespace Reports
    Public Class ReportsENG
        Public Shared Sub SetDataToTable(ByVal Dt As DataTable, ByVal ProcessUser As String, ByVal CenTrans As CenLinqDB.Common.Utilities.TransactionDB)
            If ChkTable(Dt.TableName, CenTrans) = False Then
                'Create Temp Table
                Dim crSql As String = " CREATE TABLE [" & Dt.TableName & "] (" & vbNewLine
                crSql += "ProcessUser varchar(50) null, " & vbNewLine
                crSql += "ProcessTime datetime null , " & vbNewLine

                If Dt.Columns.Count > 0 Then
                    For i As Integer = 0 To Dt.Columns.Count - 1
                        Dim dr As DataColumn = Dt.Columns(i)
                        Dim ColDataType As String = ""
                        If dr.DataType.Name = "String" Then
                            ColDataType = "varchar(200)"
                        ElseIf dr.DataType.Name = "Int64" Then
                            ColDataType = "bigint"
                        ElseIf dr.DataType.Name = "DateTime" Then
                            ColDataType = "DateTime"
                        End If

                        Dim icolName As String = dr.ColumnName.Replace(" ", "").Replace("/", "_")
                        If i <> Dt.Columns.Count - 1 Then
                            crSql += icolName & " " & ColDataType & " null, " & vbNewLine
                        Else
                            crSql += icolName & " " & ColDataType & " null " & vbNewLine
                        End If
                    Next
                    crSql += ")"
                End If
                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(crSql, CenTrans.Trans)
                CenTrans.CommitTransaction()
            End If


            'Clear Old Data
            CenTrans.CreateTransaction()
            Dim odSql As String = "delete from [" & Dt.TableName & "] where ProcessUser = '" & ProcessUser & "'"
            CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(odSql, CenTrans.Trans)
            CenTrans.CommitTransaction()
            'Insert New Data

            If Dt.Rows.Count > 0 Then
                For j As Integer = 0 To Dt.Rows.Count - 1
                    Dim dr As DataRow = Dt.Rows(j)

                    Dim ColName As String = ""
                    Dim ColData As String = ""
                    Dim InsSql As String = "insert into [" & Dt.TableName & "] ("
                    If Dt.Columns.Count > 0 Then
                        ColName += "ProcessUser, ProcessTime,"
                        ColData += PopulateData(ProcessUser, "String") & ", " & PopulateData(DateTime.Now, "DateTime") & ", "
                        For i As Integer = 0 To Dt.Columns.Count - 1
                            Dim dc As DataColumn = Dt.Columns(i)
                            If ChkTableColumn(Dt.TableName, dc.ColumnName, CenTrans) = False Then
                                CenTrans.CreateTransaction()
                                Dim acSql As String = "alter table " & Dt.TableName
                                acSql += " add  " & dc.ColumnName & " " & dc.DataType.Namespace & " null"
                                CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(acSql, CenTrans.Trans)
                                CenTrans.CommitTransaction()
                            End If

                            If i <> Dt.Columns.Count - 1 Then
                                ColName += dc.ColumnName.Replace(" ", "").Replace("/", "_") & ", "
                                If dr(dc.ColumnName).ToString.Trim <> "" Then
                                    ColData += PopulateData(dr(dc.ColumnName), dc.DataType.Name) & ", "
                                Else
                                    ColData += "null, "
                                End If
                            Else
                                ColName += dc.ColumnName.Replace(" ", "").Replace("/", "_")
                                If dr(dc.ColumnName).ToString.Trim <> "" Then
                                    ColData += PopulateData(dr(dc.ColumnName), dc.DataType.Name)
                                Else
                                    ColData += "null "
                                End If
                            End If
                        Next
                    End If
                    InsSql += ColName
                    InsSql += ") "
                    InsSql += " values ("
                    InsSql += ColData
                    InsSql += ") "

                    CenTrans.CreateTransaction()
                    If CenLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(InsSql, CenTrans.Trans) > 0 Then
                        CenTrans.CommitTransaction()
                    Else
                        CenTrans.RollbackTransaction()
                    End If
                Next
            End If
        End Sub

        Public Shared Function PopulateData(ByVal ColData As Object, ByVal DataType As String) As String
            Dim ColDataType As String = ""
            If DataType = "String" Then
                ColDataType = CenLinqDB.Common.Utilities.SqlDB.SetString(ColData)
            ElseIf DataType.IndexOf("Int") > -1 Then
                ColDataType = CenLinqDB.Common.Utilities.SqlDB.SetDouble(Convert.ToDouble(ColData))
            ElseIf DataType = "DateTime" Then
                ColDataType = CenLinqDB.Common.Utilities.SqlDB.SetDateTime(ColData)
            End If
            Return ColDataType
        End Function

        Public Shared Function ChkTable(ByVal _TableName As String, ByVal CenTrans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim Sql As String = "EXEC SP_TABLES '" & _TableName & "',null,null"
            Dim dt As DataTable = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(Sql, CenTrans.Trans)
            Return dt.Rows.Count > 0
        End Function

        Public Shared Function ChkTableColumn(ByVal _TableName As String, ByVal ColumnName As String, ByVal CenTrans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim Sql As String = "EXEC SP_COLUMNS N'" & _TableName & "',null,null,'" & ColumnName & "'"
            Dim dt As DataTable = CenLinqDB.Common.Utilities.SqlDB.ExecuteTable(Sql, CenTrans.Trans)
            Return dt.Rows.Count > 0
        End Function

        Public Shared Function getShopList() As DataTable
            Return FunctionEng.GetActiveShop()
        End Function
        Public Shared Function getSkillList() As DataTable
            Return FunctionEng.GetActiveSkill()
        End Function

        Public Shared Function GetShopListByUser() As DataTable
            Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            Dim dt As DataTable
            If uPara.USERNAME.ToUpper <> "ADMIN" Then
                dt = lnq.GetDataList("id in (" & uPara.USER_PARA.VIEW_OTHERS_VDO & ") and active='Y'", "", trans.Trans)
                If dt.Rows.Count > 0 Then
                    dt.Columns.Add("location_group") 'location_group, r.region_name_en
                    dt.Columns.Add("region_name_en")
                    dt.Columns.Add("region_code")

                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim reLnq As New CenLinqDB.TABLE.TbRegionCenLinqDB
                        reLnq.GetDataByPK(dt.Rows(i)("region_id"), trans.Trans)

                        If reLnq.ID > 0 Then
                            dt.Rows(i)("location_group") = reLnq.LOCATION_GROUP
                            dt.Rows(i)("region_name_en") = reLnq.REGION_NAME_EN
                            dt.Rows(i)("region_code") = reLnq.REGION_CODE
                        End If
                        reLnq = Nothing
                    Next
                End If

                Dim lEng As New Engine.Common.LoginENG
                dt.Merge(lEng.GetShopListByUser(uPara.USERNAME))
                lEng = Nothing

                dt.DefaultView.Sort = "shop_name_en"
                dt = dt.DefaultView.ToTable(True)
            Else
                dt = FunctionEng.GetActiveShop
            End If
            lnq = Nothing
            uPara = Nothing

            Return dt
        End Function

        Public Shared Function GetIntervalTime() As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbReportIntervalTimeCenLinqDB
                dt = lnq.GetDataList("active='Y'", "interval_time", trans.Trans)
                trans.CommitTransaction()
            End If
            Return dt
        End Function

        Public Shared Function GetFormatTimeFromSec(ByVal TimeSec As Integer) As String
            'แปลงเวลาจากวินาทีไปเป็น HH:mm:ss
            Dim tHour As Integer = 0
            Dim tMin As Integer = 0
            Dim tSec As Integer = 0
            If TimeSec >= 3600 Then
                tHour = Math.Floor(TimeSec / 3600) 'ชม.
                tMin = Math.Floor((TimeSec - (tHour * 3600)) / 60) ' นาที
                tSec = (TimeSec - (tHour * 3600)) Mod 60
            Else
                tMin = Math.Floor(TimeSec / 60)
                tSec = TimeSec Mod 60
            End If

            Return tHour.ToString.PadLeft(2, "0") & ":" & tMin.ToString.PadLeft(2, "0") & ":" & tSec.ToString.PadLeft(2, "0")
        End Function

        Public Shared Function GetSecFromTimeFormat(ByVal TimeFormat As String) As Integer
            'แปลงเวลาในรูปแบบ HH:mm:ss ไปเป็นวินาที

            Dim ret As Int32 = 0
            If TimeFormat.Trim <> "" Then
                Dim tmp() As String = Split(TimeFormat, ":")
                Dim TimeSec As Integer = 0
                If Convert.ToInt64(tmp(0)) > 0 Then
                    TimeSec += (Convert.ToInt64(tmp(0)) * 60 * 60)
                End If
                If Convert.ToInt64(tmp(1)) > 0 Then
                    TimeSec += (Convert.ToInt64(tmp(1)) * 60)
                End If
                ret = TimeSec + Convert.ToInt32(tmp(2))
            End If
            Return ret
        End Function

        Public Shared Function GroupDate_Shop(ByVal dt As DataTable, ByVal ColDateName As String, ByVal ColShopID As String) As DataTable
            Dim dt_ As New DataTable
            Dim dt_group As New DataTable
            Try
                With dt_
                    .Columns.Add(ColDateName, GetType(System.String))
                    .Columns.Add(ColShopID, GetType(System.String))
                End With
                If dt.Rows.Count > 0 Then
                    For i As Int32 = 0 To dt.Rows.Count - 1
                        Dim dr As DataRow
                        dr = dt_.NewRow
                        dr(ColDateName) = dt.Rows(i).Item(ColDateName).ToString.Trim
                        dr(ColShopID) = dt.Rows(i).Item(ColShopID).ToString.Trim
                        dt_.Rows.Add(dr)
                    Next
                    dt_group = dt_.DefaultView.ToTable(True)
                End If

            Catch ex As Exception : End Try

            Return dt_group
        End Function
        Public Shared Function GetServiceAtShop(ByVal ShopID As Long) As DataTable
            Dim ret As New DataTable

            Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            shLnq = FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB

            If trans.Trans IsNot Nothing Then
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = FunctionEng.GetShTransction(shTrans, trans, shLnq)
                If shTrans.Trans IsNot Nothing Then
                    Dim lnq As New ShLinqDB.TABLE.TbItemShLinqDB
                    ret = lnq.GetDataList("active_status='1'", "id", shTrans.Trans)
                    lnq = Nothing
                End If
                trans.CommitTransaction()
            End If
            shLnq = Nothing

            Return ret
        End Function

        Public Shared Function GetDataServiceList(ByVal ShopDT As DataTable) As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("id", GetType(Integer))
            ret.Columns.Add("item_name")

            Dim sql As String = "select id,item_name from tb_item where active_status='1' order by id"

            For Each dr As DataRow In ShopDT.Rows
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = FunctionEng.GetShTransction(dr("shop_id"), "ReportsENG.GetDataServiceList")
                Dim lnq As New ShLinqDB.TABLE.TbItemShLinqDB
                Dim tmpDt As New DataTable
                tmpDt = lnq.GetListBySql(sql, shTrans.Trans)
                If tmpDt.Rows.Count > 0 Then
                    For Each tmpDr As DataRow In tmpDt.Rows
                        ret.DefaultView.RowFilter = "id='" & tmpDr("id") & "'"
                        If ret.DefaultView.Count = 0 Then
                            Dim rDr As DataRow = ret.NewRow
                            rDr("id") = tmpDr("id")
                            rDr("item_name") = tmpDr("item_name")
                            ret.Rows.Add(rDr)
                        End If
                    Next
                End If
                tmpDt.Dispose()
            Next
            Return ret
        End Function
    End Class
End Namespace

