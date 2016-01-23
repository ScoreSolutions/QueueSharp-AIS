
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace VIEW
    'Represents a transaction for VW_AVG_TIME view Parameter.
    '[Create by  on March, 26 2012]

    <Table(Name:="VW_AVG_TIME")>  _
    Public Class VwAvgTimeShParaDB

        'Generate Field List
        Dim _NAME As  String  = ""
        Dim _ITEM_NAME As  String  = ""
        Dim _AVG_TIME As  System.Nullable(Of Long)  = 0

        'Generate Field Property 
        <Column(Storage:="_NAME", DbType:="VarChar(101)")>  _
        Public Property NAME() As  String 
            Get
                Return _NAME
            End Get
            Set(ByVal value As  String )
               _NAME = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_NAME", DbType:="VarChar(50)")>  _
        Public Property ITEM_NAME() As  String 
            Get
                Return _ITEM_NAME
            End Get
            Set(ByVal value As  String )
               _ITEM_NAME = value
            End Set
        End Property 
        <Column(Storage:="_AVG_TIME", DbType:="Int")>  _
        Public Property AVG_TIME() As  System.Nullable(Of Long) 
            Get
                Return _AVG_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _AVG_TIME = value
            End Set
        End Property 


    End Class
End Namespace
