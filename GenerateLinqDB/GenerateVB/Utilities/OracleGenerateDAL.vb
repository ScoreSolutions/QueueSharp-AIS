Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.OracleClient
Imports System.Text


Namespace Utilities
    Public Class OracleGenerateDAL
        Dim _DataSource As String = ""
        Dim _DataBaseName As String = ""
        Dim _UserID As String = ""
        Dim _Password As String = ""
        Dim _TableName As String = ""
        Dim _DatabaseType As String = "Oracle"
        Dim _ErrorTableNotFound As String = MessageResources.MSGEC014

        Public Property DataSource() As String
            Get
                Return _DataSource
            End Get
            Set(ByVal value As String)
                _DataSource = value
            End Set
        End Property
        Public Property DataBaseName() As String
            Get
                Return _DataBaseName
            End Get
            Set(ByVal value As String)
                _DataBaseName = value
            End Set
        End Property
        Public Property UserID() As String
            Get
                Return _UserID
            End Get
            Set(ByVal value As String)
                _UserID = value
            End Set
        End Property
        Public Property Password() As String
            Get
                Return _Password
            End Get
            Set(ByVal value As String)
                _Password = value
            End Set
        End Property
        Public Property TableName() As String
            Get
                Return _TableName
            End Get
            Set(ByVal value As String)
                _TableName = value
            End Set
        End Property
        Public Property DatabaseType() As String
            Get
                Return _DatabaseType
            End Get
            Set(ByVal value As String)
                _DatabaseType = value
            End Set
        End Property

        Private ReadOnly Property ConnectionString() As String
            Get
                Return "Data Source=" & _DataSource & ";Persist Security Info=True;User ID=" & _UserID & ";Password=" & _Password & ";Unicode=True"
            End Get
        End Property

        Public Function GetUniqueColumn() As DataTable
            Dim Sql As String = ""
            Sql += "SELECT A.COLUMN_NAME, B.CONSTRAINT_TYPE, C.DATA_TYPE TYPE_NAME,A.CONSTRAINT_NAME "
            Sql += "FROM USER_CONS_COLUMNS A INNER JOIN USER_CONSTRAINTS B ON A.CONSTRAINT_NAME = B.CONSTRAINT_NAME "
            Sql += "INNER JOIN USER_TAB_COLUMNS C ON C.TABLE_NAME = A.TABLE_NAME AND C.COLUMN_NAME = A.COLUMN_NAME "
            Sql += "WHERE A.TABLE_NAME = " + OracleDB.SetString(_TableName) + " AND B.CONSTRAINT_TYPE = 'U' "
            Sql += "ORDER BY B.CONSTRAINT_TYPE, A.COLUMN_NAME"

            Dim dt As DataTable = OracleDB.ExecuteTable(Sql, OracleDB.GetConnection(ConnectionString))
            Dim ret As New DataTable
            ret.Columns.Add("constraint_keys")
            ret.Columns.Add("constraint_type")
            ret.Columns.Add("constraint_name")

            If dt.Rows.Count > 0 Then
                Dim constraintName As String = ""
                For Each dRow As DataRow In dt.Rows
                    If constraintName <> UCase(dRow("CONSTRAINT_NAME").ToString().Trim()) Then

                        constraintName = UCase(dRow("CONSTRAINT_NAME").ToString().Trim())
                        dt.DefaultView.RowFilter = "CONSTRAINT_NAME='" & constraintName & "'"
                        Dim dr As DataRow = ret.NewRow
                        If dt.DefaultView.Count > 1 Then   'ถ้า Constraints นั้นมีฟิลด์ที่เกี่ยวข้องมากกว่า 1 ฟิลด์ ให้อ้างอิงจากชื่อ Constraints
                            Dim constraintKeys As String = ""
                            For i As Integer = 0 To dt.DefaultView.Count - 1
                                If constraintKeys = "" Then
                                    constraintKeys = dt.DefaultView(i).Item("COLUMN_NAME").ToString()
                                Else
                                    constraintKeys += "," & dt.DefaultView(i).Item("COLUMN_NAME").ToString()
                                End If
                            Next

                            dr("constraint_keys") = constraintKeys
                            dr("constraint_type") = "U"
                            dr("constraint_name") = UCase(dRow("CONSTRAINT_NAME").ToString().Trim())
                        Else
                            dr("constraint_keys") = dRow("COLUMN_NAME").ToString()
                            dr("constraint_type") = "U"
                            dr("constraint_name") = dRow("COLUMN_NAME").ToString()
                        End If

                        ret.Rows.Add(dr)

                    End If
                Next
            End If

            Return ret

        End Function

        Public Function GetPKColumn() As DataTable
            Dim Sql As String = ""
            Sql += "SELECT A.COLUMN_NAME, B.CONSTRAINT_TYPE, C.DATA_TYPE TYPE_NAME,A.CONSTRAINT_NAME "
            Sql += "FROM USER_CONS_COLUMNS A INNER JOIN USER_CONSTRAINTS B ON A.CONSTRAINT_NAME = B.CONSTRAINT_NAME "
            Sql += "INNER JOIN USER_TAB_COLUMNS C ON C.TABLE_NAME = A.TABLE_NAME AND C.COLUMN_NAME = A.COLUMN_NAME "
            Sql += "WHERE A.TABLE_NAME = " + OracleDB.SetString(_TableName) + " AND B.CONSTRAINT_TYPE = 'P' "
            Sql += "ORDER BY B.CONSTRAINT_TYPE, A.COLUMN_NAME"

            Return OracleDB.ExecuteTable(Sql, OracleDB.GetConnection(ConnectionString))

        End Function

        Public Function GetTableColumn()
            Dim Sql As String = "SELECT COLUMN_NAME, DATA_TYPE TYPE_NAME "
            Sql += "FROM USER_TAB_COLUMNS WHERE TABLE_NAME = " + OracleDB.SetString(_TableName) + " "
            Sql += "ORDER BY COLUMN_ID"

            Dim dt As DataTable = OracleDB.ExecuteTable(Sql, OracleDB.GetConnection(ConnectionString))
            If dt.Rows.Count = 0 Then
                Throw New ApplicationException(String.Format(_ErrorTableNotFound, _TableName))
            Else
                Return dt
            End If
        End Function

        Public Function IsView() As Boolean
            Dim Sql As String = "SELECT VIEW_NAME FROM USER_VIEWS WHERE VIEW_NAME = " + OracleDB.SetString(_TableName) + " "
            Return (OracleDB.ExecuteTable(Sql, OracleDB.GetConnection(ConnectionString)).Rows.Count > 0)
        End Function

        Public Function GetTableList() As DataTable
            Dim Sql As String = "SELECT OBJECT_NAME TABLE_NAME, OBJECT_TYPE TABLE_TYPE"
            Sql += " FROM USER_OBJECTS "
            Sql += " WHERE OBJECT_TYPE IN ('TABLE','VIEW')"
            Sql += " ORDER BY OBJECT_NAME"

            Dim dt As DataTable = OracleDB.ExecuteTable(Sql, OracleDB.GetConnection(ConnectionString))
            Dim dtList As New DataTable()
            dtList.Columns.Add("TABLE_NAME")
            dtList.Columns.Add("TABLE_VALUE")

            For Each dr As DataRow In dt.Rows
                Dim drL As DataRow = dtList.NewRow
                drL("TABLE_NAME") = dr("TABLE_NAME").ToString() & " : " & dr("TABLE_TYPE").ToString()
                drL("TABLE_VALUE") = dr("TABLE_NAME").ToString()
                dtList.Rows.Add(drL)
            Next
            Return dtList
        End Function

        Public Function GetAllTable() As DataTable
            Dim Sql As String = "SELECT OBJECT_NAME TABLE_NAME, OBJECT_TYPE TABLE_TYPE"
            Sql += " FROM USER_OBJECTS "
            Sql += " WHERE OBJECT_TYPE IN ('TABLE','VIEW')"
            Sql += " ORDER BY OBJECT_NAME"

            Dim dt As DataTable = OracleDB.ExecuteTable(Sql, OracleDB.GetConnection(ConnectionString))
            Dim dtList As New DataTable()
            dtList.Columns.Add("TABLE_NAME")
            dtList.Columns.Add("TABLE_VALUE")

            For Each dr As DataRow In dt.Rows
                Dim drL As DataRow = dtList.NewRow
                drL("TABLE_NAME") = dr("TABLE_NAME").ToString()
                drL("TABLE_VALUE") = dr("TABLE_NAME").ToString()
                dtList.Rows.Add(drL)
            Next
            Return dtList
        End Function
    End Class
End Namespace

