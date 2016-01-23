
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace Utilities
    'Represents a transaction for LOG_ERROR table Parameter.
    '[Create by  on Febuary, 2 2012]

    <Table(Name:="LOG_ERROR")> _
    Public Class LogErrorCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1, 1, 1)
        Dim _UPDATE_BY As String = ""
        Dim _UPDATE_DATE As System.Nullable(Of DateTime) = New DateTime(1, 1, 1)
        Dim _LOGIN_HIS_ID As Long = 0
        Dim _ERROR_TIME As DateTime = New DateTime(1, 1, 1)
        Dim _ERROR_DESC As String = ""

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="BigInt NOT NULL ", CanBeNull:=False)> _
        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
                _ID = value
            End Set
        End Property
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(50) NOT NULL ", CanBeNull:=False)> _
        Public Property CREATE_BY() As String
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As String)
                _CREATE_BY = value
            End Set
        End Property
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime2 NOT NULL ", CanBeNull:=False)> _
        Public Property CREATE_DATE() As DateTime
            Get
                Return _CREATE_DATE
            End Get
            Set(ByVal value As DateTime)
                _CREATE_DATE = value
            End Set
        End Property
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(50)")> _
        Public Property UPDATE_BY() As String
            Get
                Return _UPDATE_BY
            End Get
            Set(ByVal value As String)
                _UPDATE_BY = value
            End Set
        End Property
        <Column(Storage:="_UPDATE_DATE", DbType:="DateTime2")> _
        Public Property UPDATE_DATE() As System.Nullable(Of DateTime)
            Get
                Return _UPDATE_DATE
            End Get
            Set(ByVal value As System.Nullable(Of DateTime))
                _UPDATE_DATE = value
            End Set
        End Property
        <Column(Storage:="_LOGIN_HIS_ID", DbType:="BigInt NOT NULL ", CanBeNull:=False)> _
        Public Property LOGIN_HIS_ID() As Long
            Get
                Return _LOGIN_HIS_ID
            End Get
            Set(ByVal value As Long)
                _LOGIN_HIS_ID = value
            End Set
        End Property
        <Column(Storage:="_ERROR_TIME", DbType:="DateTime2 NOT NULL ", CanBeNull:=False)> _
        Public Property ERROR_TIME() As DateTime
            Get
                Return _ERROR_TIME
            End Get
            Set(ByVal value As DateTime)
                _ERROR_TIME = value
            End Set
        End Property
        <Column(Storage:="_ERROR_DESC", DbType:="VarChar(500) NOT NULL ", CanBeNull:=False)> _
        Public Property ERROR_DESC() As String
            Get
                Return _ERROR_DESC
            End Get
            Set(ByVal value As String)
                _ERROR_DESC = value
            End Set
        End Property


    End Class
End Namespace
