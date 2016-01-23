
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace VIEW
    'Represents a transaction for VW_Available_Counter view Parameter.
    '[Create by  on March, 26 2012]

    <Table(Name:="VW_Available_Counter")>  _
    Public Class VwAvailableCounterShParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _ITEM_NAME As  String  = ""
        Dim _CUSWAIT As  System.Nullable(Of Long)  = 0
        Dim _MAXWAIT As String = ""

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
               _ID = value
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
        <Column(Storage:="_CUSWAIT", DbType:="Int")>  _
        Public Property CUSWAIT() As  System.Nullable(Of Long) 
            Get
                Return _CUSWAIT
            End Get
            Set(ByVal value As  System.Nullable(Of Long) )
               _CUSWAIT = value
            End Set
        End Property 
        <Column(Storage:="_MAXWAIT", DbType:="VarChar(8) NOT NULL ",CanBeNull:=false)>  _
        Public Property MAXWAIT() As String
            Get
                Return _MAXWAIT
            End Get
            Set(ByVal value As String)
               _MAXWAIT = value
            End Set
        End Property 


    End Class
End Namespace
