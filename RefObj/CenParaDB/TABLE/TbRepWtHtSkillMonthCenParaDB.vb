
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 

Namespace TABLE
    'Represents a transaction for TB_REP_WT_HT_SKILL_MONTH table Parameter.
    '[Create by  on March, 22 2013]

    <Table(Name:="TB_REP_WT_HT_SKILL_MONTH")>  _
    Public Class TbRepWtHtSkillMonthCenParaDB

        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _MONTH_NO As Long = 0
        Dim _SHOW_MONTH As String = ""
        Dim _SHOW_YEAR As Long = 0
        Dim _USER_CODE As String = ""
        Dim _STAFF_NAME As String = ""
        Dim _SKILL_ID As Long = 0
        Dim _SKILL_NAME As String = ""
        Dim _NUM_OF_QUEUE As Long = 0
        Dim _REGIS As Long = 0
        Dim _SERVE As Long = 0
        Dim _MISS_CALL As Long = 0
        Dim _CANCEL As Long = 0
        Dim _NOT_CALL As Long = 0
        Dim _NOT_CON As Long = 0
        Dim _NOT_END As Long = 0
        Dim _AWT As String = ""
        Dim _SUM_WT As Long = 0
        Dim _COUNT_WT As Long = 0
        Dim _AHT As String = ""
        Dim _SUM_HT As Long = 0
        Dim _COUNT_HT As Long = 0
        Dim _ACONT As String = ""
        Dim _SUM_CONT As Long = 0
        Dim _COUNT_CONT As Long = 0

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
               _ID = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(20) NOT NULL ",CanBeNull:=false)>  _
        Public Property CREATE_BY() As String
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As String)
               _CREATE_BY = value
            End Set
        End Property 
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property CREATE_DATE() As DateTime
            Get
                Return _CREATE_DATE
            End Get
            Set(ByVal value As DateTime)
               _CREATE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(20)")>  _
        Public Property UPDATE_BY() As  String 
            Get
                Return _UPDATE_BY
            End Get
            Set(ByVal value As  String )
               _UPDATE_BY = value
            End Set
        End Property 
        <Column(Storage:="_UPDATE_DATE", DbType:="DateTime")>  _
        Public Property UPDATE_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _UPDATE_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _UPDATE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_ID() As Long
            Get
                Return _SHOP_ID
            End Get
            Set(ByVal value As Long)
               _SHOP_ID = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_NAME_TH() As String
            Get
                Return _SHOP_NAME_TH
            End Get
            Set(ByVal value As String)
               _SHOP_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_SHOP_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOP_NAME_EN() As String
            Get
                Return _SHOP_NAME_EN
            End Get
            Set(ByVal value As String)
               _SHOP_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_MONTH_NO", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MONTH_NO() As Long
            Get
                Return _MONTH_NO
            End Get
            Set(ByVal value As Long)
               _MONTH_NO = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_MONTH", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_MONTH() As String
            Get
                Return _SHOW_MONTH
            End Get
            Set(ByVal value As String)
               _SHOW_MONTH = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_YEAR", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_YEAR() As Long
            Get
                Return _SHOW_YEAR
            End Get
            Set(ByVal value As Long)
               _SHOW_YEAR = value
            End Set
        End Property 
        <Column(Storage:="_USER_CODE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property USER_CODE() As String
            Get
                Return _USER_CODE
            End Get
            Set(ByVal value As String)
               _USER_CODE = value
            End Set
        End Property 
        <Column(Storage:="_STAFF_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property STAFF_NAME() As String
            Get
                Return _STAFF_NAME
            End Get
            Set(ByVal value As String)
               _STAFF_NAME = value
            End Set
        End Property 
        <Column(Storage:="_SKILL_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SKILL_ID() As Long
            Get
                Return _SKILL_ID
            End Get
            Set(ByVal value As Long)
               _SKILL_ID = value
            End Set
        End Property 
        <Column(Storage:="_SKILL_NAME", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SKILL_NAME() As String
            Get
                Return _SKILL_NAME
            End Get
            Set(ByVal value As String)
               _SKILL_NAME = value
            End Set
        End Property 
        <Column(Storage:="_NUM_OF_QUEUE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NUM_OF_QUEUE() As Long
            Get
                Return _NUM_OF_QUEUE
            End Get
            Set(ByVal value As Long)
               _NUM_OF_QUEUE = value
            End Set
        End Property 
        <Column(Storage:="_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property REGIS() As Long
            Get
                Return _REGIS
            End Get
            Set(ByVal value As Long)
               _REGIS = value
            End Set
        End Property 
        <Column(Storage:="_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVE() As Long
            Get
                Return _SERVE
            End Get
            Set(ByVal value As Long)
               _SERVE = value
            End Set
        End Property 
        <Column(Storage:="_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property MISS_CALL() As Long
            Get
                Return _MISS_CALL
            End Get
            Set(ByVal value As Long)
               _MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property CANCEL() As Long
            Get
                Return _CANCEL
            End Get
            Set(ByVal value As Long)
               _CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_CALL() As Long
            Get
                Return _NOT_CALL
            End Get
            Set(ByVal value As Long)
               _NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_CON() As Long
            Get
                Return _NOT_CON
            End Get
            Set(ByVal value As Long)
               _NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NOT_END() As Long
            Get
                Return _NOT_END
            End Get
            Set(ByVal value As Long)
               _NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_AWT", DbType:="VarChar(10) NOT NULL ",CanBeNull:=false)>  _
        Public Property AWT() As String
            Get
                Return _AWT
            End Get
            Set(ByVal value As String)
               _AWT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_WT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_WT() As Long
            Get
                Return _SUM_WT
            End Get
            Set(ByVal value As Long)
               _SUM_WT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_WT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_WT() As Long
            Get
                Return _COUNT_WT
            End Get
            Set(ByVal value As Long)
               _COUNT_WT = value
            End Set
        End Property 
        <Column(Storage:="_AHT", DbType:="VarChar(10) NOT NULL ",CanBeNull:=false)>  _
        Public Property AHT() As String
            Get
                Return _AHT
            End Get
            Set(ByVal value As String)
               _AHT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_HT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_HT() As Long
            Get
                Return _SUM_HT
            End Get
            Set(ByVal value As Long)
               _SUM_HT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_HT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_HT() As Long
            Get
                Return _COUNT_HT
            End Get
            Set(ByVal value As Long)
               _COUNT_HT = value
            End Set
        End Property 
        <Column(Storage:="_ACONT", DbType:="VarChar(10) NOT NULL ",CanBeNull:=false)>  _
        Public Property ACONT() As String
            Get
                Return _ACONT
            End Get
            Set(ByVal value As String)
               _ACONT = value
            End Set
        End Property 
        <Column(Storage:="_SUM_CONT", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property SUM_CONT() As Long
            Get
                Return _SUM_CONT
            End Get
            Set(ByVal value As Long)
               _SUM_CONT = value
            End Set
        End Property 
        <Column(Storage:="_COUNT_CONT", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property COUNT_CONT() As Long
            Get
                Return _COUNT_CONT
            End Get
            Set(ByVal value As Long)
               _COUNT_CONT = value
            End Set
        End Property 


    End Class
End Namespace
