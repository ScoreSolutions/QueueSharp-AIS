
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace VIEW
    'Represents a transaction for VW_AVG_SERVICE_TIME view Parameter.
    '[Create by  on Febuary, 23 2012]

    <Table(Name:="VW_AVG_SERVICE_TIME")>  _
    Public Class VwAvgServiceTimeShParaDB

        'Generate Field List
        Dim _USER_ID As Long = 0
        Dim _ITEM_ID As Long = 0
        Dim _ITEM_TIME As  System.Nullable(Of Long)  = 0

        'Generate Field Property 
        <Column(Storage:="_USER_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property USER_ID() As Long
            Get
                Return _USER_ID
            End Get
            Set(ByVal value As Long)
               _USER_ID = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ITEM_ID() As Long
            Get
                Return _ITEM_ID
            End Get
            Set(ByVal value As Long)
               _ITEM_ID = value
            End Set
        End Property 
        <Column(Storage:="_ITEM_TIME", DbType:="Int")>  _
        Public Property ITEM_TIME() As  System.Nullable(Of Long) 
            Get
                Return _ITEM_TIME
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _ITEM_TIME = value
            End Set
        End Property 


    End Class
End Namespace
