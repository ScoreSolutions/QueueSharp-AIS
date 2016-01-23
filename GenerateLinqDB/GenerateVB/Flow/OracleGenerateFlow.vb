Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Text
Imports GenerateVB.Utilities

Namespace Flow
    Public Class OracleGenerateFlow
        Inherits Flow.BaseGenerateFlow

        Private _dal As OracleGenerateDAL

        Private ReadOnly Property DALObj() As OracleGenerateDAL
            Get
                If _dal Is Nothing Then
                    _dal = New OracleGenerateDAL()
                End If
                Return _dal
            End Get

        End Property

        Private Sub SetData(ByVal Data As GenerateVB.Data.GenerateData)
            DALObj.DataSource = Data.DataSource
            DALObj.DataBaseName = Data.DataBaseName
            DALObj.UserID = Data.UserID
            DALObj.Password = Data.Password
            DALObj.TableName = Data.TableName
            DALObj.DatabaseType = Data.DatabaseType
            _IsView = DALObj.IsView()
            _databaseType = DALObj.DatabaseType
            _columnTable = DALObj.GetTableColumn()
            _pkColumnTable = DALObj.GetPKColumn()
            _uniqueColumnTable = DALObj.GetUniqueColumn()
            _tableName = DALObj.TableName

        End Sub

        Private Sub SetConnDesc(ByVal Data As GenerateVB.Data.GenerateData)
            DALObj.DataSource = Data.DataSource
            DALObj.DataBaseName = Data.DataBaseName
            DALObj.UserID = Data.UserID
            DALObj.Password = Data.Password
            DALObj.DatabaseType = Data.DatabaseType
            _databaseType = DALObj.DatabaseType
        End Sub

        Public Function GenerateCodeDAL(ByVal Data As Data.GenerateData) As String
            SetData(Data)
            Return GenerateLinq(Data)
        End Function

        Public Function GenerateCodeData(ByVal Data As Data.GenerateData) As String
            SetData(Data)
            Return GeneratePara(Data)
        End Function


        Public Function GetTableList(ByVal Data As Data.GenerateData) As DataTable
            SetConnDesc(Data)
            Return DALObj.GetTableList()
        End Function

        Public Function GetAllTable(ByVal Data As Data.GenerateData) As DataTable
            SetConnDesc(Data)
            Return DALObj.GetAllTable()
        End Function
    End Class
End Namespace

