Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 
Imports DB = CenLinqDB.Common.Utilities.SQLDB
Imports CenParaDB.TABLE
Imports CenParaDB.Common.Utilities

Namespace TABLE
    'Represents a transaction for TB_REP_CUST_CATEGORY_DAY table CenLinqDB.
    '[Create by  on March, 27 2013]
    Public Class TbRepCustCategoryDayCenLinqDB
        Public sub TbRepCustCategoryDayCenLinqDB()

        End Sub 
        ' TB_REP_CUST_CATEGORY_DAY
        Const _tableName As String = "TB_REP_CUST_CATEGORY_DAY"
        Dim _deletedRow As Int16 = 0

        'Set Common Property
        Dim _error As String = ""
        Dim _information As String = ""
        Dim _haveData As Boolean = False

        Public ReadOnly Property TableName As String
            Get
                Return _tableName
            End Get
        End Property
        Public ReadOnly Property ErrorMessage As String
            Get
                Return _error
            End Get
        End Property
        Public ReadOnly Property InfoMessage As String
            Get
                Return _information
            End Get
        End Property
        Public ReadOnly Property HaveData As Boolean
            Get
                Return _haveData
            End Get
        End Property


        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATE_BY As  String  = ""
        Dim _UPDATE_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _SHOP_ID As Long = 0
        Dim _SHOP_NAME_TH As String = ""
        Dim _SHOP_NAME_EN As String = ""
        Dim _SERVICE_DATE As DateTime = New DateTime(1,1,1)
        Dim _SHOW_DATE As String = ""
        Dim _SERVICE_ID As Long = 0
        Dim _SERVICE_NAME_EN As String = ""
        Dim _SERVICE_NAME_TH As String = ""
        Dim _KEY_REGIS As Long = 0
        Dim _KEY_SERVE As Long = 0
        Dim _KEY_MISS_CALL As Long = 0
        Dim _KEY_CANCEL As Long = 0
        Dim _KEY_NOT_CALL As Long = 0
        Dim _KEY_NOT_CON As Long = 0
        Dim _KEY_NOT_END As Long = 0
        Dim _SME_REGIS As Long = 0
        Dim _SME_SERVE As Long = 0
        Dim _SME_MISS_CALL As Long = 0
        Dim _SME_CANCEL As Long = 0
        Dim _SME_NOT_CALL As Long = 0
        Dim _SME_NOT_CON As Long = 0
        Dim _SME_NOT_END As Long = 0
        Dim _ROY_REGIS As Long = 0
        Dim _ROY_SERVE As Long = 0
        Dim _ROY_MISS_CALL As Long = 0
        Dim _ROY_CANCEL As Long = 0
        Dim _ROY_NOT_CALL As Long = 0
        Dim _ROY_NOT_CON As Long = 0
        Dim _ROY_NOT_END As Long = 0
        Dim _TOT_REGIS As Long = 0
        Dim _TOT_SERVE As Long = 0
        Dim _TOT_MISS_CALL As Long = 0
        Dim _TOT_CANCEL As Long = 0
        Dim _TOT_NOT_CALL As Long = 0
        Dim _TOT_NOT_CON As Long = 0
        Dim _TOT_NOT_END As Long = 0
        Dim _GOV_REGIS As Long = 0
        Dim _GOV_SERVE As Long = 0
        Dim _GOV_MISS_CALL As Long = 0
        Dim _GOV_CANCEL As Long = 0
        Dim _GOV_NOT_CALL As Long = 0
        Dim _GOV_NOT_CON As Long = 0
        Dim _GOV_NOT_END As Long = 0
        Dim _EMB_REGIS As Long = 0
        Dim _EMB_SERVE As Long = 0
        Dim _EMB_MISS_CALL As Long = 0
        Dim _EMB_CANCEL As Long = 0
        Dim _EMB_NOT_CALL As Long = 0
        Dim _EMB_NOT_CON As Long = 0
        Dim _EMB_NOT_END As Long = 0
        Dim _NON_REGIS As Long = 0
        Dim _NON_SERVE As Long = 0
        Dim _NON_MISS_CALL As Long = 0
        Dim _NON_CANCEL As Long = 0
        Dim _NON_NOT_CALL As Long = 0
        Dim _NON_NOT_CON As Long = 0
        Dim _NON_NOT_END As Long = 0
        Dim _STA_REGIS As Long = 0
        Dim _STA_SERVE As Long = 0
        Dim _STA_MISS_CALL As Long = 0
        Dim _STA_CANCEL As Long = 0
        Dim _STA_NOT_CALL As Long = 0
        Dim _STA_NOT_CON As Long = 0
        Dim _STA_NOT_END As Long = 0
        Dim _AIS_REGIS As Long = 0
        Dim _AIS_SERVE As Long = 0
        Dim _AIS_MISS_CALL As Long = 0
        Dim _AIS_CANCEL As Long = 0
        Dim _AIS_NOT_CALL As Long = 0
        Dim _AIS_NOT_CON As Long = 0
        Dim _AIS_NOT_END As Long = 0
        Dim _PRE_REGIS As Long = 0
        Dim _PRE_SERVE As Long = 0
        Dim _PRE_MISS_CALL As Long = 0
        Dim _PRE_CANCEL As Long = 0
        Dim _PRE_NOT_CALL As Long = 0
        Dim _PRE_NOT_CON As Long = 0
        Dim _PRE_NOT_END As Long = 0
        Dim _FOR_REGIS As Long = 0
        Dim _FOR_SERVE As Long = 0
        Dim _FOR_MISS_CALL As Long = 0
        Dim _FOR_CANCEL As Long = 0
        Dim _FOR_NOT_CALL As Long = 0
        Dim _FOR_NOT_CON As Long = 0
        Dim _FOR_NOT_END As Long = 0
        Dim _THA_REGIS As Long = 0
        Dim _THA_SERVE As Long = 0
        Dim _THA_MISS_CALL As Long = 0
        Dim _THA_CANCEL As Long = 0
        Dim _THA_NOT_CALL As Long = 0
        Dim _THA_NOT_CON As Long = 0
        Dim _THA_NOT_END As Long = 0
        Dim _NO_REGIS As Long = 0
        Dim _NO_SERVE As Long = 0
        Dim _NO_MISS_CALL As Long = 0
        Dim _NO_CANCEL As Long = 0
        Dim _NO_NOT_CALL As Long = 0
        Dim _NO_NOT_CON As Long = 0
        Dim _NO_NOT_END As Long = 0

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
        <Column(Storage:="_SERVICE_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_DATE() As DateTime
            Get
                Return _SERVICE_DATE
            End Get
            Set(ByVal value As DateTime)
               _SERVICE_DATE = value
            End Set
        End Property 
        <Column(Storage:="_SHOW_DATE", DbType:="VarChar(10) NOT NULL ",CanBeNull:=false)>  _
        Public Property SHOW_DATE() As String
            Get
                Return _SHOW_DATE
            End Get
            Set(ByVal value As String)
               _SHOW_DATE = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_ID() As Long
            Get
                Return _SERVICE_ID
            End Get
            Set(ByVal value As Long)
               _SERVICE_ID = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_NAME_EN", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_NAME_EN() As String
            Get
                Return _SERVICE_NAME_EN
            End Get
            Set(ByVal value As String)
               _SERVICE_NAME_EN = value
            End Set
        End Property 
        <Column(Storage:="_SERVICE_NAME_TH", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property SERVICE_NAME_TH() As String
            Get
                Return _SERVICE_NAME_TH
            End Get
            Set(ByVal value As String)
               _SERVICE_NAME_TH = value
            End Set
        End Property 
        <Column(Storage:="_KEY_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_REGIS() As Long
            Get
                Return _KEY_REGIS
            End Get
            Set(ByVal value As Long)
               _KEY_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_KEY_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_SERVE() As Long
            Get
                Return _KEY_SERVE
            End Get
            Set(ByVal value As Long)
               _KEY_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_KEY_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_MISS_CALL() As Long
            Get
                Return _KEY_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _KEY_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_KEY_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_CANCEL() As Long
            Get
                Return _KEY_CANCEL
            End Get
            Set(ByVal value As Long)
               _KEY_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_KEY_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_NOT_CALL() As Long
            Get
                Return _KEY_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _KEY_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_KEY_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_NOT_CON() As Long
            Get
                Return _KEY_NOT_CON
            End Get
            Set(ByVal value As Long)
               _KEY_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_KEY_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property KEY_NOT_END() As Long
            Get
                Return _KEY_NOT_END
            End Get
            Set(ByVal value As Long)
               _KEY_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_SME_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_REGIS() As Long
            Get
                Return _SME_REGIS
            End Get
            Set(ByVal value As Long)
               _SME_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_SME_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_SERVE() As Long
            Get
                Return _SME_SERVE
            End Get
            Set(ByVal value As Long)
               _SME_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_SME_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_MISS_CALL() As Long
            Get
                Return _SME_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _SME_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_SME_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_CANCEL() As Long
            Get
                Return _SME_CANCEL
            End Get
            Set(ByVal value As Long)
               _SME_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_SME_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_NOT_CALL() As Long
            Get
                Return _SME_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _SME_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_SME_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_NOT_CON() As Long
            Get
                Return _SME_NOT_CON
            End Get
            Set(ByVal value As Long)
               _SME_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_SME_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property SME_NOT_END() As Long
            Get
                Return _SME_NOT_END
            End Get
            Set(ByVal value As Long)
               _SME_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_ROY_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_REGIS() As Long
            Get
                Return _ROY_REGIS
            End Get
            Set(ByVal value As Long)
               _ROY_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_ROY_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_SERVE() As Long
            Get
                Return _ROY_SERVE
            End Get
            Set(ByVal value As Long)
               _ROY_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_ROY_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_MISS_CALL() As Long
            Get
                Return _ROY_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _ROY_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_ROY_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_CANCEL() As Long
            Get
                Return _ROY_CANCEL
            End Get
            Set(ByVal value As Long)
               _ROY_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_ROY_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_NOT_CALL() As Long
            Get
                Return _ROY_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _ROY_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_ROY_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_NOT_CON() As Long
            Get
                Return _ROY_NOT_CON
            End Get
            Set(ByVal value As Long)
               _ROY_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_ROY_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ROY_NOT_END() As Long
            Get
                Return _ROY_NOT_END
            End Get
            Set(ByVal value As Long)
               _ROY_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_TOT_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_REGIS() As Long
            Get
                Return _TOT_REGIS
            End Get
            Set(ByVal value As Long)
               _TOT_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_TOT_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_SERVE() As Long
            Get
                Return _TOT_SERVE
            End Get
            Set(ByVal value As Long)
               _TOT_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_TOT_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_MISS_CALL() As Long
            Get
                Return _TOT_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _TOT_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_TOT_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_CANCEL() As Long
            Get
                Return _TOT_CANCEL
            End Get
            Set(ByVal value As Long)
               _TOT_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_TOT_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_NOT_CALL() As Long
            Get
                Return _TOT_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _TOT_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_TOT_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_NOT_CON() As Long
            Get
                Return _TOT_NOT_CON
            End Get
            Set(ByVal value As Long)
               _TOT_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_TOT_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TOT_NOT_END() As Long
            Get
                Return _TOT_NOT_END
            End Get
            Set(ByVal value As Long)
               _TOT_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_GOV_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_REGIS() As Long
            Get
                Return _GOV_REGIS
            End Get
            Set(ByVal value As Long)
               _GOV_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_GOV_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_SERVE() As Long
            Get
                Return _GOV_SERVE
            End Get
            Set(ByVal value As Long)
               _GOV_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_GOV_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_MISS_CALL() As Long
            Get
                Return _GOV_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _GOV_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_GOV_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_CANCEL() As Long
            Get
                Return _GOV_CANCEL
            End Get
            Set(ByVal value As Long)
               _GOV_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_GOV_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_NOT_CALL() As Long
            Get
                Return _GOV_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _GOV_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_GOV_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_NOT_CON() As Long
            Get
                Return _GOV_NOT_CON
            End Get
            Set(ByVal value As Long)
               _GOV_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_GOV_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property GOV_NOT_END() As Long
            Get
                Return _GOV_NOT_END
            End Get
            Set(ByVal value As Long)
               _GOV_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_EMB_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_REGIS() As Long
            Get
                Return _EMB_REGIS
            End Get
            Set(ByVal value As Long)
               _EMB_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_EMB_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_SERVE() As Long
            Get
                Return _EMB_SERVE
            End Get
            Set(ByVal value As Long)
               _EMB_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_EMB_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_MISS_CALL() As Long
            Get
                Return _EMB_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _EMB_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_EMB_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_CANCEL() As Long
            Get
                Return _EMB_CANCEL
            End Get
            Set(ByVal value As Long)
               _EMB_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_EMB_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_NOT_CALL() As Long
            Get
                Return _EMB_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _EMB_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_EMB_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_NOT_CON() As Long
            Get
                Return _EMB_NOT_CON
            End Get
            Set(ByVal value As Long)
               _EMB_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_EMB_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property EMB_NOT_END() As Long
            Get
                Return _EMB_NOT_END
            End Get
            Set(ByVal value As Long)
               _EMB_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_NON_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_REGIS() As Long
            Get
                Return _NON_REGIS
            End Get
            Set(ByVal value As Long)
               _NON_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_NON_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_SERVE() As Long
            Get
                Return _NON_SERVE
            End Get
            Set(ByVal value As Long)
               _NON_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_NON_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_MISS_CALL() As Long
            Get
                Return _NON_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _NON_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NON_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_CANCEL() As Long
            Get
                Return _NON_CANCEL
            End Get
            Set(ByVal value As Long)
               _NON_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_NON_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOT_CALL() As Long
            Get
                Return _NON_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _NON_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NON_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOT_CON() As Long
            Get
                Return _NON_NOT_CON
            End Get
            Set(ByVal value As Long)
               _NON_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_NON_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NON_NOT_END() As Long
            Get
                Return _NON_NOT_END
            End Get
            Set(ByVal value As Long)
               _NON_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_STA_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_REGIS() As Long
            Get
                Return _STA_REGIS
            End Get
            Set(ByVal value As Long)
               _STA_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_STA_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_SERVE() As Long
            Get
                Return _STA_SERVE
            End Get
            Set(ByVal value As Long)
               _STA_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_STA_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_MISS_CALL() As Long
            Get
                Return _STA_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _STA_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_STA_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_CANCEL() As Long
            Get
                Return _STA_CANCEL
            End Get
            Set(ByVal value As Long)
               _STA_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_STA_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_NOT_CALL() As Long
            Get
                Return _STA_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _STA_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_STA_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_NOT_CON() As Long
            Get
                Return _STA_NOT_CON
            End Get
            Set(ByVal value As Long)
               _STA_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_STA_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property STA_NOT_END() As Long
            Get
                Return _STA_NOT_END
            End Get
            Set(ByVal value As Long)
               _STA_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_AIS_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_REGIS() As Long
            Get
                Return _AIS_REGIS
            End Get
            Set(ByVal value As Long)
               _AIS_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_AIS_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_SERVE() As Long
            Get
                Return _AIS_SERVE
            End Get
            Set(ByVal value As Long)
               _AIS_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_AIS_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_MISS_CALL() As Long
            Get
                Return _AIS_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _AIS_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_AIS_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_CANCEL() As Long
            Get
                Return _AIS_CANCEL
            End Get
            Set(ByVal value As Long)
               _AIS_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_AIS_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_NOT_CALL() As Long
            Get
                Return _AIS_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _AIS_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_AIS_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_NOT_CON() As Long
            Get
                Return _AIS_NOT_CON
            End Get
            Set(ByVal value As Long)
               _AIS_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_AIS_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property AIS_NOT_END() As Long
            Get
                Return _AIS_NOT_END
            End Get
            Set(ByVal value As Long)
               _AIS_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_PRE_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_REGIS() As Long
            Get
                Return _PRE_REGIS
            End Get
            Set(ByVal value As Long)
               _PRE_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_PRE_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_SERVE() As Long
            Get
                Return _PRE_SERVE
            End Get
            Set(ByVal value As Long)
               _PRE_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_PRE_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_MISS_CALL() As Long
            Get
                Return _PRE_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _PRE_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_PRE_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_CANCEL() As Long
            Get
                Return _PRE_CANCEL
            End Get
            Set(ByVal value As Long)
               _PRE_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_PRE_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_NOT_CALL() As Long
            Get
                Return _PRE_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _PRE_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_PRE_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_NOT_CON() As Long
            Get
                Return _PRE_NOT_CON
            End Get
            Set(ByVal value As Long)
               _PRE_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_PRE_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property PRE_NOT_END() As Long
            Get
                Return _PRE_NOT_END
            End Get
            Set(ByVal value As Long)
               _PRE_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_FOR_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_REGIS() As Long
            Get
                Return _FOR_REGIS
            End Get
            Set(ByVal value As Long)
               _FOR_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_FOR_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_SERVE() As Long
            Get
                Return _FOR_SERVE
            End Get
            Set(ByVal value As Long)
               _FOR_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_FOR_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_MISS_CALL() As Long
            Get
                Return _FOR_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _FOR_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_FOR_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_CANCEL() As Long
            Get
                Return _FOR_CANCEL
            End Get
            Set(ByVal value As Long)
               _FOR_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_FOR_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_NOT_CALL() As Long
            Get
                Return _FOR_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _FOR_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_FOR_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_NOT_CON() As Long
            Get
                Return _FOR_NOT_CON
            End Get
            Set(ByVal value As Long)
               _FOR_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_FOR_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property FOR_NOT_END() As Long
            Get
                Return _FOR_NOT_END
            End Get
            Set(ByVal value As Long)
               _FOR_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_THA_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_REGIS() As Long
            Get
                Return _THA_REGIS
            End Get
            Set(ByVal value As Long)
               _THA_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_THA_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_SERVE() As Long
            Get
                Return _THA_SERVE
            End Get
            Set(ByVal value As Long)
               _THA_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_THA_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_MISS_CALL() As Long
            Get
                Return _THA_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _THA_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_THA_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_CANCEL() As Long
            Get
                Return _THA_CANCEL
            End Get
            Set(ByVal value As Long)
               _THA_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_THA_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_NOT_CALL() As Long
            Get
                Return _THA_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _THA_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_THA_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_NOT_CON() As Long
            Get
                Return _THA_NOT_CON
            End Get
            Set(ByVal value As Long)
               _THA_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_THA_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property THA_NOT_END() As Long
            Get
                Return _THA_NOT_END
            End Get
            Set(ByVal value As Long)
               _THA_NOT_END = value
            End Set
        End Property 
        <Column(Storage:="_NO_REGIS", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_REGIS() As Long
            Get
                Return _NO_REGIS
            End Get
            Set(ByVal value As Long)
               _NO_REGIS = value
            End Set
        End Property 
        <Column(Storage:="_NO_SERVE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_SERVE() As Long
            Get
                Return _NO_SERVE
            End Get
            Set(ByVal value As Long)
               _NO_SERVE = value
            End Set
        End Property 
        <Column(Storage:="_NO_MISS_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_MISS_CALL() As Long
            Get
                Return _NO_MISS_CALL
            End Get
            Set(ByVal value As Long)
               _NO_MISS_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NO_CANCEL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_CANCEL() As Long
            Get
                Return _NO_CANCEL
            End Get
            Set(ByVal value As Long)
               _NO_CANCEL = value
            End Set
        End Property 
        <Column(Storage:="_NO_NOT_CALL", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_NOT_CALL() As Long
            Get
                Return _NO_NOT_CALL
            End Get
            Set(ByVal value As Long)
               _NO_NOT_CALL = value
            End Set
        End Property 
        <Column(Storage:="_NO_NOT_CON", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_NOT_CON() As Long
            Get
                Return _NO_NOT_CON
            End Get
            Set(ByVal value As Long)
               _NO_NOT_CON = value
            End Set
        End Property 
        <Column(Storage:="_NO_NOT_END", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property NO_NOT_END() As Long
            Get
                Return _NO_NOT_END
            End Get
            Set(ByVal value As Long)
               _NO_NOT_END = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1,1,1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1,1,1)
            _SHOP_ID = 0
            _SHOP_NAME_TH = ""
            _SHOP_NAME_EN = ""
            _SERVICE_DATE = New DateTime(1,1,1)
            _SHOW_DATE = ""
            _SERVICE_ID = 0
            _SERVICE_NAME_EN = ""
            _SERVICE_NAME_TH = ""
            _KEY_REGIS = 0
            _KEY_SERVE = 0
            _KEY_MISS_CALL = 0
            _KEY_CANCEL = 0
            _KEY_NOT_CALL = 0
            _KEY_NOT_CON = 0
            _KEY_NOT_END = 0
            _SME_REGIS = 0
            _SME_SERVE = 0
            _SME_MISS_CALL = 0
            _SME_CANCEL = 0
            _SME_NOT_CALL = 0
            _SME_NOT_CON = 0
            _SME_NOT_END = 0
            _ROY_REGIS = 0
            _ROY_SERVE = 0
            _ROY_MISS_CALL = 0
            _ROY_CANCEL = 0
            _ROY_NOT_CALL = 0
            _ROY_NOT_CON = 0
            _ROY_NOT_END = 0
            _TOT_REGIS = 0
            _TOT_SERVE = 0
            _TOT_MISS_CALL = 0
            _TOT_CANCEL = 0
            _TOT_NOT_CALL = 0
            _TOT_NOT_CON = 0
            _TOT_NOT_END = 0
            _GOV_REGIS = 0
            _GOV_SERVE = 0
            _GOV_MISS_CALL = 0
            _GOV_CANCEL = 0
            _GOV_NOT_CALL = 0
            _GOV_NOT_CON = 0
            _GOV_NOT_END = 0
            _EMB_REGIS = 0
            _EMB_SERVE = 0
            _EMB_MISS_CALL = 0
            _EMB_CANCEL = 0
            _EMB_NOT_CALL = 0
            _EMB_NOT_CON = 0
            _EMB_NOT_END = 0
            _NON_REGIS = 0
            _NON_SERVE = 0
            _NON_MISS_CALL = 0
            _NON_CANCEL = 0
            _NON_NOT_CALL = 0
            _NON_NOT_CON = 0
            _NON_NOT_END = 0
            _STA_REGIS = 0
            _STA_SERVE = 0
            _STA_MISS_CALL = 0
            _STA_CANCEL = 0
            _STA_NOT_CALL = 0
            _STA_NOT_CON = 0
            _STA_NOT_END = 0
            _AIS_REGIS = 0
            _AIS_SERVE = 0
            _AIS_MISS_CALL = 0
            _AIS_CANCEL = 0
            _AIS_NOT_CALL = 0
            _AIS_NOT_CON = 0
            _AIS_NOT_END = 0
            _PRE_REGIS = 0
            _PRE_SERVE = 0
            _PRE_MISS_CALL = 0
            _PRE_CANCEL = 0
            _PRE_NOT_CALL = 0
            _PRE_NOT_CON = 0
            _PRE_NOT_END = 0
            _FOR_REGIS = 0
            _FOR_SERVE = 0
            _FOR_MISS_CALL = 0
            _FOR_CANCEL = 0
            _FOR_NOT_CALL = 0
            _FOR_NOT_CON = 0
            _FOR_NOT_END = 0
            _THA_REGIS = 0
            _THA_SERVE = 0
            _THA_MISS_CALL = 0
            _THA_CANCEL = 0
            _THA_NOT_CALL = 0
            _THA_NOT_CON = 0
            _THA_NOT_END = 0
            _NO_REGIS = 0
            _NO_SERVE = 0
            _NO_MISS_CALL = 0
            _NO_CANCEL = 0
            _NO_NOT_CALL = 0
            _NO_NOT_CON = 0
            _NO_NOT_END = 0
        End Sub

       'Define Public Method 
        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=orderBy>The fields for sort data.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetDataList(whClause As String, orderBy As String, trans As SQLTransaction) As DataTable
            Return DB.ExecuteTable(SqlSelect & IIf(whClause = "", "", " WHERE " & whClause) & IIF(orderBy = "", "", " ORDER BY  " & orderBy), trans)
        End Function


        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetListBySql(Sql As String, trans As SQLTransaction) As DataTable
            Return DB.ExecuteTable(Sql, trans)
        End Function


        '/// Returns an indication whether the current data is inserted into TB_REP_CUST_CATEGORY_DAY table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Public Function InsertData(LoginName As String,trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                _ID = DB.GetNextID("ID",tableName, trans)
                _CREATE_BY = LoginName
                _CREATE_DATE = DateTime.Now
                Return doInsert(trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_REP_CUST_CATEGORY_DAY table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateByPK(LoginName As String,trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                _UPDATE_BY = LoginName
                _UPDATE_DATE = DateTime.Now
                Return doUpdate("ID = " & DB.SetDouble(_ID), trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_REP_CUST_CATEGORY_DAY table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_CUST_CATEGORY_DAY table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Public Function DeleteByPK(cID As Long, trans As SQLTransaction) As Boolean
            If trans IsNot Nothing Then 
                Return doDelete("ID = " & DB.SetDouble(cID), trans)
            Else 
                _error = "Transaction Is not null"
                Return False
            End If 
        End Function


        '/// Returns an indication whether the record of TB_REP_CUST_CATEGORY_DAY by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_REP_CUST_CATEGORY_DAY by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbRepCustCategoryDayCenLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Para Class of TB_REP_CUST_CATEGORY_DAY by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetParameter(cID As Long, trans As SQLTransaction) As TbRepCustCategoryDayCenParaDB
            Return doMappingParameter("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_CUST_CATEGORY_DAY by specified SERVICE_DATE_SHOP_ID key is retrieved successfully.
        '/// <param name=cSERVICE_DATE_SHOP_ID>The SERVICE_DATE_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataBySERVICE_DATE_SHOP_ID(cSERVICE_DATE As DateTime, cSHOP_ID As Integer, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID), trans)
        End Function

        '/// Returns an duplicate data record of TB_REP_CUST_CATEGORY_DAY by specified SERVICE_DATE_SHOP_ID key is retrieved successfully.
        '/// <param name=cSERVICE_DATE_SHOP_ID>The SERVICE_DATE_SHOP_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateBySERVICE_DATE_SHOP_ID(cSERVICE_DATE As DateTime, cSHOP_ID As Integer, cid As Long, trans As SQLTransaction) As Boolean
            Return doChkData("SERVICE_DATE = " & DB.SetDateTime(cSERVICE_DATE) & " AND SHOP_ID = " & DB.SetDouble(cSHOP_ID) & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TB_REP_CUST_CATEGORY_DAY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_REP_CUST_CATEGORY_DAY table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Private Function doInsert(trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            If _haveData = False Then
                Try

                    ret = (DB.ExecuteNonQuery(SqlInsert, trans) > 0)
                    If ret = False Then
                        _error = DB.ErrorMessage
                    Else
                        _haveData = True
                    End If
                    _information = MessageResources.MSGIN001
                Catch ex As ApplicationException
                    ret = false
                    _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlInsert
                Catch ex As Exception
                    ret = False
                    _error = MessageResources.MSGEC101 & " Exception :" & ex.ToString() & "### SQL: " & SqlInsert
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEN002 & "### SQL: " & SqlInsert
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is updated to TB_REP_CUST_CATEGORY_DAY table successfully.
        '/// <param name=whText>The condition specify the updating record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Private Function doUpdate(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " Where " & whText
            If _haveData = True Then
                If whText.Trim() <> ""
                    Try

                        ret = (DB.ExecuteNonQuery(SqlUpdate & tmpWhere, trans) > 0)
                        If ret = False Then
                            _error = DB.ErrorMessage
                        End If
                        _information = MessageResources.MSGIU001
                    Catch ex As ApplicationException
                        ret = False
                        _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlUpdate & tmpWhere
                    Catch ex As Exception
                        ret = False
                        _error = MessageResources.MSGEC102 & " Exception :" & ex.ToString() & "### SQL: " & SqlUpdate & tmpWhere
                    End Try
                Else
                    ret = False
                    _error = MessageResources.MSGEU003 & "### SQL: " & SqlUpdate & tmpWhere
                End If
            Else
                ret = False
                _error = MessageResources.MSGEU002 & "### SQL: " & SqlUpdate & tmpWhere
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is deleted from TB_REP_CUST_CATEGORY_DAY table successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Private Function doDelete(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " Where " & whText
            If doChkData(whText, trans) = True Then
                If whText.Trim() <> ""
                    Try
                        ret = (DB.ExecuteNonQuery(SqlDelete & tmpWhere, trans) > 0)
                        If ret = False Then
                            _error = MessageResources.MSGED001
                        End If
                        _information = MessageResources.MSGID001
                    Catch ex As ApplicationException
                        ret = False
                        _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlDelete & tmpWhere
                    Catch ex As Exception
                        ret = False
                        _error = MessageResources.MSGEC103 & " Exception :" & ex.ToString() & "### SQL: " & SqlDelete & tmpWhere
                    End Try
                Else
                    ret = False
                    _error = MessageResources.MSGED003 & "### SQL: " & SqlDelete & tmpWhere
                End If
            Else
                ret = False
                _error = MessageResources.MSGEU002 & "### SQL: " & SqlDelete & tmpWhere
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of TB_REP_CUST_CATEGORY_DAY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doChkData(whText As String, trans As SQLTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " WHERE " & whText
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then _shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("show_date")) = False Then _show_date = Rdr("show_date").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then _service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then _service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("key_regis")) = False Then _key_regis = Convert.ToInt32(Rdr("key_regis"))
                        If Convert.IsDBNull(Rdr("key_serve")) = False Then _key_serve = Convert.ToInt32(Rdr("key_serve"))
                        If Convert.IsDBNull(Rdr("key_miss_call")) = False Then _key_miss_call = Convert.ToInt32(Rdr("key_miss_call"))
                        If Convert.IsDBNull(Rdr("key_cancel")) = False Then _key_cancel = Convert.ToInt32(Rdr("key_cancel"))
                        If Convert.IsDBNull(Rdr("key_not_call")) = False Then _key_not_call = Convert.ToInt32(Rdr("key_not_call"))
                        If Convert.IsDBNull(Rdr("key_not_con")) = False Then _key_not_con = Convert.ToInt32(Rdr("key_not_con"))
                        If Convert.IsDBNull(Rdr("key_not_end")) = False Then _key_not_end = Convert.ToInt32(Rdr("key_not_end"))
                        If Convert.IsDBNull(Rdr("sme_regis")) = False Then _sme_regis = Convert.ToInt32(Rdr("sme_regis"))
                        If Convert.IsDBNull(Rdr("sme_serve")) = False Then _sme_serve = Convert.ToInt32(Rdr("sme_serve"))
                        If Convert.IsDBNull(Rdr("sme_miss_call")) = False Then _sme_miss_call = Convert.ToInt32(Rdr("sme_miss_call"))
                        If Convert.IsDBNull(Rdr("sme_cancel")) = False Then _sme_cancel = Convert.ToInt32(Rdr("sme_cancel"))
                        If Convert.IsDBNull(Rdr("sme_not_call")) = False Then _sme_not_call = Convert.ToInt32(Rdr("sme_not_call"))
                        If Convert.IsDBNull(Rdr("sme_not_con")) = False Then _sme_not_con = Convert.ToInt32(Rdr("sme_not_con"))
                        If Convert.IsDBNull(Rdr("sme_not_end")) = False Then _sme_not_end = Convert.ToInt32(Rdr("sme_not_end"))
                        If Convert.IsDBNull(Rdr("roy_regis")) = False Then _roy_regis = Convert.ToInt32(Rdr("roy_regis"))
                        If Convert.IsDBNull(Rdr("roy_serve")) = False Then _roy_serve = Convert.ToInt32(Rdr("roy_serve"))
                        If Convert.IsDBNull(Rdr("roy_miss_call")) = False Then _roy_miss_call = Convert.ToInt32(Rdr("roy_miss_call"))
                        If Convert.IsDBNull(Rdr("roy_cancel")) = False Then _roy_cancel = Convert.ToInt32(Rdr("roy_cancel"))
                        If Convert.IsDBNull(Rdr("roy_not_call")) = False Then _roy_not_call = Convert.ToInt32(Rdr("roy_not_call"))
                        If Convert.IsDBNull(Rdr("roy_not_con")) = False Then _roy_not_con = Convert.ToInt32(Rdr("roy_not_con"))
                        If Convert.IsDBNull(Rdr("roy_not_end")) = False Then _roy_not_end = Convert.ToInt32(Rdr("roy_not_end"))
                        If Convert.IsDBNull(Rdr("tot_regis")) = False Then _tot_regis = Convert.ToInt32(Rdr("tot_regis"))
                        If Convert.IsDBNull(Rdr("tot_serve")) = False Then _tot_serve = Convert.ToInt32(Rdr("tot_serve"))
                        If Convert.IsDBNull(Rdr("tot_miss_call")) = False Then _tot_miss_call = Convert.ToInt32(Rdr("tot_miss_call"))
                        If Convert.IsDBNull(Rdr("tot_cancel")) = False Then _tot_cancel = Convert.ToInt32(Rdr("tot_cancel"))
                        If Convert.IsDBNull(Rdr("tot_not_call")) = False Then _tot_not_call = Convert.ToInt32(Rdr("tot_not_call"))
                        If Convert.IsDBNull(Rdr("tot_not_con")) = False Then _tot_not_con = Convert.ToInt32(Rdr("tot_not_con"))
                        If Convert.IsDBNull(Rdr("tot_not_end")) = False Then _tot_not_end = Convert.ToInt32(Rdr("tot_not_end"))
                        If Convert.IsDBNull(Rdr("gov_regis")) = False Then _gov_regis = Convert.ToInt32(Rdr("gov_regis"))
                        If Convert.IsDBNull(Rdr("gov_serve")) = False Then _gov_serve = Convert.ToInt32(Rdr("gov_serve"))
                        If Convert.IsDBNull(Rdr("gov_miss_call")) = False Then _gov_miss_call = Convert.ToInt32(Rdr("gov_miss_call"))
                        If Convert.IsDBNull(Rdr("gov_cancel")) = False Then _gov_cancel = Convert.ToInt32(Rdr("gov_cancel"))
                        If Convert.IsDBNull(Rdr("gov_not_call")) = False Then _gov_not_call = Convert.ToInt32(Rdr("gov_not_call"))
                        If Convert.IsDBNull(Rdr("gov_not_con")) = False Then _gov_not_con = Convert.ToInt32(Rdr("gov_not_con"))
                        If Convert.IsDBNull(Rdr("gov_not_end")) = False Then _gov_not_end = Convert.ToInt32(Rdr("gov_not_end"))
                        If Convert.IsDBNull(Rdr("emb_regis")) = False Then _emb_regis = Convert.ToInt32(Rdr("emb_regis"))
                        If Convert.IsDBNull(Rdr("emb_serve")) = False Then _emb_serve = Convert.ToInt32(Rdr("emb_serve"))
                        If Convert.IsDBNull(Rdr("emb_miss_call")) = False Then _emb_miss_call = Convert.ToInt32(Rdr("emb_miss_call"))
                        If Convert.IsDBNull(Rdr("emb_cancel")) = False Then _emb_cancel = Convert.ToInt32(Rdr("emb_cancel"))
                        If Convert.IsDBNull(Rdr("emb_not_call")) = False Then _emb_not_call = Convert.ToInt32(Rdr("emb_not_call"))
                        If Convert.IsDBNull(Rdr("emb_not_con")) = False Then _emb_not_con = Convert.ToInt32(Rdr("emb_not_con"))
                        If Convert.IsDBNull(Rdr("emb_not_end")) = False Then _emb_not_end = Convert.ToInt32(Rdr("emb_not_end"))
                        If Convert.IsDBNull(Rdr("non_regis")) = False Then _non_regis = Convert.ToInt32(Rdr("non_regis"))
                        If Convert.IsDBNull(Rdr("non_serve")) = False Then _non_serve = Convert.ToInt32(Rdr("non_serve"))
                        If Convert.IsDBNull(Rdr("non_miss_call")) = False Then _non_miss_call = Convert.ToInt32(Rdr("non_miss_call"))
                        If Convert.IsDBNull(Rdr("non_cancel")) = False Then _non_cancel = Convert.ToInt32(Rdr("non_cancel"))
                        If Convert.IsDBNull(Rdr("non_not_call")) = False Then _non_not_call = Convert.ToInt32(Rdr("non_not_call"))
                        If Convert.IsDBNull(Rdr("non_not_con")) = False Then _non_not_con = Convert.ToInt32(Rdr("non_not_con"))
                        If Convert.IsDBNull(Rdr("non_not_end")) = False Then _non_not_end = Convert.ToInt32(Rdr("non_not_end"))
                        If Convert.IsDBNull(Rdr("sta_regis")) = False Then _sta_regis = Convert.ToInt32(Rdr("sta_regis"))
                        If Convert.IsDBNull(Rdr("sta_serve")) = False Then _sta_serve = Convert.ToInt32(Rdr("sta_serve"))
                        If Convert.IsDBNull(Rdr("sta_miss_call")) = False Then _sta_miss_call = Convert.ToInt32(Rdr("sta_miss_call"))
                        If Convert.IsDBNull(Rdr("sta_cancel")) = False Then _sta_cancel = Convert.ToInt32(Rdr("sta_cancel"))
                        If Convert.IsDBNull(Rdr("sta_not_call")) = False Then _sta_not_call = Convert.ToInt32(Rdr("sta_not_call"))
                        If Convert.IsDBNull(Rdr("sta_not_con")) = False Then _sta_not_con = Convert.ToInt32(Rdr("sta_not_con"))
                        If Convert.IsDBNull(Rdr("sta_not_end")) = False Then _sta_not_end = Convert.ToInt32(Rdr("sta_not_end"))
                        If Convert.IsDBNull(Rdr("ais_regis")) = False Then _ais_regis = Convert.ToInt32(Rdr("ais_regis"))
                        If Convert.IsDBNull(Rdr("ais_serve")) = False Then _ais_serve = Convert.ToInt32(Rdr("ais_serve"))
                        If Convert.IsDBNull(Rdr("ais_miss_call")) = False Then _ais_miss_call = Convert.ToInt32(Rdr("ais_miss_call"))
                        If Convert.IsDBNull(Rdr("ais_cancel")) = False Then _ais_cancel = Convert.ToInt32(Rdr("ais_cancel"))
                        If Convert.IsDBNull(Rdr("ais_not_call")) = False Then _ais_not_call = Convert.ToInt32(Rdr("ais_not_call"))
                        If Convert.IsDBNull(Rdr("ais_not_con")) = False Then _ais_not_con = Convert.ToInt32(Rdr("ais_not_con"))
                        If Convert.IsDBNull(Rdr("ais_not_end")) = False Then _ais_not_end = Convert.ToInt32(Rdr("ais_not_end"))
                        If Convert.IsDBNull(Rdr("pre_regis")) = False Then _pre_regis = Convert.ToInt32(Rdr("pre_regis"))
                        If Convert.IsDBNull(Rdr("pre_serve")) = False Then _pre_serve = Convert.ToInt32(Rdr("pre_serve"))
                        If Convert.IsDBNull(Rdr("pre_miss_call")) = False Then _pre_miss_call = Convert.ToInt32(Rdr("pre_miss_call"))
                        If Convert.IsDBNull(Rdr("pre_cancel")) = False Then _pre_cancel = Convert.ToInt32(Rdr("pre_cancel"))
                        If Convert.IsDBNull(Rdr("pre_not_call")) = False Then _pre_not_call = Convert.ToInt32(Rdr("pre_not_call"))
                        If Convert.IsDBNull(Rdr("pre_not_con")) = False Then _pre_not_con = Convert.ToInt32(Rdr("pre_not_con"))
                        If Convert.IsDBNull(Rdr("pre_not_end")) = False Then _pre_not_end = Convert.ToInt32(Rdr("pre_not_end"))
                        If Convert.IsDBNull(Rdr("for_regis")) = False Then _for_regis = Convert.ToInt32(Rdr("for_regis"))
                        If Convert.IsDBNull(Rdr("for_serve")) = False Then _for_serve = Convert.ToInt32(Rdr("for_serve"))
                        If Convert.IsDBNull(Rdr("for_miss_call")) = False Then _for_miss_call = Convert.ToInt32(Rdr("for_miss_call"))
                        If Convert.IsDBNull(Rdr("for_cancel")) = False Then _for_cancel = Convert.ToInt32(Rdr("for_cancel"))
                        If Convert.IsDBNull(Rdr("for_not_call")) = False Then _for_not_call = Convert.ToInt32(Rdr("for_not_call"))
                        If Convert.IsDBNull(Rdr("for_not_con")) = False Then _for_not_con = Convert.ToInt32(Rdr("for_not_con"))
                        If Convert.IsDBNull(Rdr("for_not_end")) = False Then _for_not_end = Convert.ToInt32(Rdr("for_not_end"))
                        If Convert.IsDBNull(Rdr("tha_regis")) = False Then _tha_regis = Convert.ToInt32(Rdr("tha_regis"))
                        If Convert.IsDBNull(Rdr("tha_serve")) = False Then _tha_serve = Convert.ToInt32(Rdr("tha_serve"))
                        If Convert.IsDBNull(Rdr("tha_miss_call")) = False Then _tha_miss_call = Convert.ToInt32(Rdr("tha_miss_call"))
                        If Convert.IsDBNull(Rdr("tha_cancel")) = False Then _tha_cancel = Convert.ToInt32(Rdr("tha_cancel"))
                        If Convert.IsDBNull(Rdr("tha_not_call")) = False Then _tha_not_call = Convert.ToInt32(Rdr("tha_not_call"))
                        If Convert.IsDBNull(Rdr("tha_not_con")) = False Then _tha_not_con = Convert.ToInt32(Rdr("tha_not_con"))
                        If Convert.IsDBNull(Rdr("tha_not_end")) = False Then _tha_not_end = Convert.ToInt32(Rdr("tha_not_end"))
                        If Convert.IsDBNull(Rdr("no_regis")) = False Then _no_regis = Convert.ToInt32(Rdr("no_regis"))
                        If Convert.IsDBNull(Rdr("no_serve")) = False Then _no_serve = Convert.ToInt32(Rdr("no_serve"))
                        If Convert.IsDBNull(Rdr("no_miss_call")) = False Then _no_miss_call = Convert.ToInt32(Rdr("no_miss_call"))
                        If Convert.IsDBNull(Rdr("no_cancel")) = False Then _no_cancel = Convert.ToInt32(Rdr("no_cancel"))
                        If Convert.IsDBNull(Rdr("no_not_call")) = False Then _no_not_call = Convert.ToInt32(Rdr("no_not_call"))
                        If Convert.IsDBNull(Rdr("no_not_con")) = False Then _no_not_con = Convert.ToInt32(Rdr("no_not_con"))
                        If Convert.IsDBNull(Rdr("no_not_end")) = False Then _no_not_end = Convert.ToInt32(Rdr("no_not_end"))
                    Else
                        ret = False
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    ret = False
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEV001
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of TB_REP_CUST_CATEGORY_DAY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction) As TbRepCustCategoryDayCenLinqDB
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then _shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then _shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then _shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_date")) = False Then _service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("show_date")) = False Then _show_date = Rdr("show_date").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then _service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then _service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then _service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("key_regis")) = False Then _key_regis = Convert.ToInt32(Rdr("key_regis"))
                        If Convert.IsDBNull(Rdr("key_serve")) = False Then _key_serve = Convert.ToInt32(Rdr("key_serve"))
                        If Convert.IsDBNull(Rdr("key_miss_call")) = False Then _key_miss_call = Convert.ToInt32(Rdr("key_miss_call"))
                        If Convert.IsDBNull(Rdr("key_cancel")) = False Then _key_cancel = Convert.ToInt32(Rdr("key_cancel"))
                        If Convert.IsDBNull(Rdr("key_not_call")) = False Then _key_not_call = Convert.ToInt32(Rdr("key_not_call"))
                        If Convert.IsDBNull(Rdr("key_not_con")) = False Then _key_not_con = Convert.ToInt32(Rdr("key_not_con"))
                        If Convert.IsDBNull(Rdr("key_not_end")) = False Then _key_not_end = Convert.ToInt32(Rdr("key_not_end"))
                        If Convert.IsDBNull(Rdr("sme_regis")) = False Then _sme_regis = Convert.ToInt32(Rdr("sme_regis"))
                        If Convert.IsDBNull(Rdr("sme_serve")) = False Then _sme_serve = Convert.ToInt32(Rdr("sme_serve"))
                        If Convert.IsDBNull(Rdr("sme_miss_call")) = False Then _sme_miss_call = Convert.ToInt32(Rdr("sme_miss_call"))
                        If Convert.IsDBNull(Rdr("sme_cancel")) = False Then _sme_cancel = Convert.ToInt32(Rdr("sme_cancel"))
                        If Convert.IsDBNull(Rdr("sme_not_call")) = False Then _sme_not_call = Convert.ToInt32(Rdr("sme_not_call"))
                        If Convert.IsDBNull(Rdr("sme_not_con")) = False Then _sme_not_con = Convert.ToInt32(Rdr("sme_not_con"))
                        If Convert.IsDBNull(Rdr("sme_not_end")) = False Then _sme_not_end = Convert.ToInt32(Rdr("sme_not_end"))
                        If Convert.IsDBNull(Rdr("roy_regis")) = False Then _roy_regis = Convert.ToInt32(Rdr("roy_regis"))
                        If Convert.IsDBNull(Rdr("roy_serve")) = False Then _roy_serve = Convert.ToInt32(Rdr("roy_serve"))
                        If Convert.IsDBNull(Rdr("roy_miss_call")) = False Then _roy_miss_call = Convert.ToInt32(Rdr("roy_miss_call"))
                        If Convert.IsDBNull(Rdr("roy_cancel")) = False Then _roy_cancel = Convert.ToInt32(Rdr("roy_cancel"))
                        If Convert.IsDBNull(Rdr("roy_not_call")) = False Then _roy_not_call = Convert.ToInt32(Rdr("roy_not_call"))
                        If Convert.IsDBNull(Rdr("roy_not_con")) = False Then _roy_not_con = Convert.ToInt32(Rdr("roy_not_con"))
                        If Convert.IsDBNull(Rdr("roy_not_end")) = False Then _roy_not_end = Convert.ToInt32(Rdr("roy_not_end"))
                        If Convert.IsDBNull(Rdr("tot_regis")) = False Then _tot_regis = Convert.ToInt32(Rdr("tot_regis"))
                        If Convert.IsDBNull(Rdr("tot_serve")) = False Then _tot_serve = Convert.ToInt32(Rdr("tot_serve"))
                        If Convert.IsDBNull(Rdr("tot_miss_call")) = False Then _tot_miss_call = Convert.ToInt32(Rdr("tot_miss_call"))
                        If Convert.IsDBNull(Rdr("tot_cancel")) = False Then _tot_cancel = Convert.ToInt32(Rdr("tot_cancel"))
                        If Convert.IsDBNull(Rdr("tot_not_call")) = False Then _tot_not_call = Convert.ToInt32(Rdr("tot_not_call"))
                        If Convert.IsDBNull(Rdr("tot_not_con")) = False Then _tot_not_con = Convert.ToInt32(Rdr("tot_not_con"))
                        If Convert.IsDBNull(Rdr("tot_not_end")) = False Then _tot_not_end = Convert.ToInt32(Rdr("tot_not_end"))
                        If Convert.IsDBNull(Rdr("gov_regis")) = False Then _gov_regis = Convert.ToInt32(Rdr("gov_regis"))
                        If Convert.IsDBNull(Rdr("gov_serve")) = False Then _gov_serve = Convert.ToInt32(Rdr("gov_serve"))
                        If Convert.IsDBNull(Rdr("gov_miss_call")) = False Then _gov_miss_call = Convert.ToInt32(Rdr("gov_miss_call"))
                        If Convert.IsDBNull(Rdr("gov_cancel")) = False Then _gov_cancel = Convert.ToInt32(Rdr("gov_cancel"))
                        If Convert.IsDBNull(Rdr("gov_not_call")) = False Then _gov_not_call = Convert.ToInt32(Rdr("gov_not_call"))
                        If Convert.IsDBNull(Rdr("gov_not_con")) = False Then _gov_not_con = Convert.ToInt32(Rdr("gov_not_con"))
                        If Convert.IsDBNull(Rdr("gov_not_end")) = False Then _gov_not_end = Convert.ToInt32(Rdr("gov_not_end"))
                        If Convert.IsDBNull(Rdr("emb_regis")) = False Then _emb_regis = Convert.ToInt32(Rdr("emb_regis"))
                        If Convert.IsDBNull(Rdr("emb_serve")) = False Then _emb_serve = Convert.ToInt32(Rdr("emb_serve"))
                        If Convert.IsDBNull(Rdr("emb_miss_call")) = False Then _emb_miss_call = Convert.ToInt32(Rdr("emb_miss_call"))
                        If Convert.IsDBNull(Rdr("emb_cancel")) = False Then _emb_cancel = Convert.ToInt32(Rdr("emb_cancel"))
                        If Convert.IsDBNull(Rdr("emb_not_call")) = False Then _emb_not_call = Convert.ToInt32(Rdr("emb_not_call"))
                        If Convert.IsDBNull(Rdr("emb_not_con")) = False Then _emb_not_con = Convert.ToInt32(Rdr("emb_not_con"))
                        If Convert.IsDBNull(Rdr("emb_not_end")) = False Then _emb_not_end = Convert.ToInt32(Rdr("emb_not_end"))
                        If Convert.IsDBNull(Rdr("non_regis")) = False Then _non_regis = Convert.ToInt32(Rdr("non_regis"))
                        If Convert.IsDBNull(Rdr("non_serve")) = False Then _non_serve = Convert.ToInt32(Rdr("non_serve"))
                        If Convert.IsDBNull(Rdr("non_miss_call")) = False Then _non_miss_call = Convert.ToInt32(Rdr("non_miss_call"))
                        If Convert.IsDBNull(Rdr("non_cancel")) = False Then _non_cancel = Convert.ToInt32(Rdr("non_cancel"))
                        If Convert.IsDBNull(Rdr("non_not_call")) = False Then _non_not_call = Convert.ToInt32(Rdr("non_not_call"))
                        If Convert.IsDBNull(Rdr("non_not_con")) = False Then _non_not_con = Convert.ToInt32(Rdr("non_not_con"))
                        If Convert.IsDBNull(Rdr("non_not_end")) = False Then _non_not_end = Convert.ToInt32(Rdr("non_not_end"))
                        If Convert.IsDBNull(Rdr("sta_regis")) = False Then _sta_regis = Convert.ToInt32(Rdr("sta_regis"))
                        If Convert.IsDBNull(Rdr("sta_serve")) = False Then _sta_serve = Convert.ToInt32(Rdr("sta_serve"))
                        If Convert.IsDBNull(Rdr("sta_miss_call")) = False Then _sta_miss_call = Convert.ToInt32(Rdr("sta_miss_call"))
                        If Convert.IsDBNull(Rdr("sta_cancel")) = False Then _sta_cancel = Convert.ToInt32(Rdr("sta_cancel"))
                        If Convert.IsDBNull(Rdr("sta_not_call")) = False Then _sta_not_call = Convert.ToInt32(Rdr("sta_not_call"))
                        If Convert.IsDBNull(Rdr("sta_not_con")) = False Then _sta_not_con = Convert.ToInt32(Rdr("sta_not_con"))
                        If Convert.IsDBNull(Rdr("sta_not_end")) = False Then _sta_not_end = Convert.ToInt32(Rdr("sta_not_end"))
                        If Convert.IsDBNull(Rdr("ais_regis")) = False Then _ais_regis = Convert.ToInt32(Rdr("ais_regis"))
                        If Convert.IsDBNull(Rdr("ais_serve")) = False Then _ais_serve = Convert.ToInt32(Rdr("ais_serve"))
                        If Convert.IsDBNull(Rdr("ais_miss_call")) = False Then _ais_miss_call = Convert.ToInt32(Rdr("ais_miss_call"))
                        If Convert.IsDBNull(Rdr("ais_cancel")) = False Then _ais_cancel = Convert.ToInt32(Rdr("ais_cancel"))
                        If Convert.IsDBNull(Rdr("ais_not_call")) = False Then _ais_not_call = Convert.ToInt32(Rdr("ais_not_call"))
                        If Convert.IsDBNull(Rdr("ais_not_con")) = False Then _ais_not_con = Convert.ToInt32(Rdr("ais_not_con"))
                        If Convert.IsDBNull(Rdr("ais_not_end")) = False Then _ais_not_end = Convert.ToInt32(Rdr("ais_not_end"))
                        If Convert.IsDBNull(Rdr("pre_regis")) = False Then _pre_regis = Convert.ToInt32(Rdr("pre_regis"))
                        If Convert.IsDBNull(Rdr("pre_serve")) = False Then _pre_serve = Convert.ToInt32(Rdr("pre_serve"))
                        If Convert.IsDBNull(Rdr("pre_miss_call")) = False Then _pre_miss_call = Convert.ToInt32(Rdr("pre_miss_call"))
                        If Convert.IsDBNull(Rdr("pre_cancel")) = False Then _pre_cancel = Convert.ToInt32(Rdr("pre_cancel"))
                        If Convert.IsDBNull(Rdr("pre_not_call")) = False Then _pre_not_call = Convert.ToInt32(Rdr("pre_not_call"))
                        If Convert.IsDBNull(Rdr("pre_not_con")) = False Then _pre_not_con = Convert.ToInt32(Rdr("pre_not_con"))
                        If Convert.IsDBNull(Rdr("pre_not_end")) = False Then _pre_not_end = Convert.ToInt32(Rdr("pre_not_end"))
                        If Convert.IsDBNull(Rdr("for_regis")) = False Then _for_regis = Convert.ToInt32(Rdr("for_regis"))
                        If Convert.IsDBNull(Rdr("for_serve")) = False Then _for_serve = Convert.ToInt32(Rdr("for_serve"))
                        If Convert.IsDBNull(Rdr("for_miss_call")) = False Then _for_miss_call = Convert.ToInt32(Rdr("for_miss_call"))
                        If Convert.IsDBNull(Rdr("for_cancel")) = False Then _for_cancel = Convert.ToInt32(Rdr("for_cancel"))
                        If Convert.IsDBNull(Rdr("for_not_call")) = False Then _for_not_call = Convert.ToInt32(Rdr("for_not_call"))
                        If Convert.IsDBNull(Rdr("for_not_con")) = False Then _for_not_con = Convert.ToInt32(Rdr("for_not_con"))
                        If Convert.IsDBNull(Rdr("for_not_end")) = False Then _for_not_end = Convert.ToInt32(Rdr("for_not_end"))
                        If Convert.IsDBNull(Rdr("tha_regis")) = False Then _tha_regis = Convert.ToInt32(Rdr("tha_regis"))
                        If Convert.IsDBNull(Rdr("tha_serve")) = False Then _tha_serve = Convert.ToInt32(Rdr("tha_serve"))
                        If Convert.IsDBNull(Rdr("tha_miss_call")) = False Then _tha_miss_call = Convert.ToInt32(Rdr("tha_miss_call"))
                        If Convert.IsDBNull(Rdr("tha_cancel")) = False Then _tha_cancel = Convert.ToInt32(Rdr("tha_cancel"))
                        If Convert.IsDBNull(Rdr("tha_not_call")) = False Then _tha_not_call = Convert.ToInt32(Rdr("tha_not_call"))
                        If Convert.IsDBNull(Rdr("tha_not_con")) = False Then _tha_not_con = Convert.ToInt32(Rdr("tha_not_con"))
                        If Convert.IsDBNull(Rdr("tha_not_end")) = False Then _tha_not_end = Convert.ToInt32(Rdr("tha_not_end"))
                        If Convert.IsDBNull(Rdr("no_regis")) = False Then _no_regis = Convert.ToInt32(Rdr("no_regis"))
                        If Convert.IsDBNull(Rdr("no_serve")) = False Then _no_serve = Convert.ToInt32(Rdr("no_serve"))
                        If Convert.IsDBNull(Rdr("no_miss_call")) = False Then _no_miss_call = Convert.ToInt32(Rdr("no_miss_call"))
                        If Convert.IsDBNull(Rdr("no_cancel")) = False Then _no_cancel = Convert.ToInt32(Rdr("no_cancel"))
                        If Convert.IsDBNull(Rdr("no_not_call")) = False Then _no_not_call = Convert.ToInt32(Rdr("no_not_call"))
                        If Convert.IsDBNull(Rdr("no_not_con")) = False Then _no_not_con = Convert.ToInt32(Rdr("no_not_con"))
                        If Convert.IsDBNull(Rdr("no_not_end")) = False Then _no_not_end = Convert.ToInt32(Rdr("no_not_end"))
                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return Me
        End Function


        '/// Returns an indication whether the Class Data of TB_REP_CUST_CATEGORY_DAY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doMappingParameter(whText As String, trans As SQLTransaction) As TbRepCustCategoryDayCenParaDB
            ClearData()
            _haveData  = False
            Dim ret As New TbRepCustCategoryDayCenParaDB
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then ret.id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then ret.create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then ret.create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then ret.update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then ret.update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("shop_id")) = False Then ret.shop_id = Convert.ToInt32(Rdr("shop_id"))
                        If Convert.IsDBNull(Rdr("shop_name_th")) = False Then ret.shop_name_th = Rdr("shop_name_th").ToString()
                        If Convert.IsDBNull(Rdr("shop_name_en")) = False Then ret.shop_name_en = Rdr("shop_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_date")) = False Then ret.service_date = Convert.ToDateTime(Rdr("service_date"))
                        If Convert.IsDBNull(Rdr("show_date")) = False Then ret.show_date = Rdr("show_date").ToString()
                        If Convert.IsDBNull(Rdr("service_id")) = False Then ret.service_id = Convert.ToInt32(Rdr("service_id"))
                        If Convert.IsDBNull(Rdr("service_name_en")) = False Then ret.service_name_en = Rdr("service_name_en").ToString()
                        If Convert.IsDBNull(Rdr("service_name_th")) = False Then ret.service_name_th = Rdr("service_name_th").ToString()
                        If Convert.IsDBNull(Rdr("key_regis")) = False Then ret.key_regis = Convert.ToInt32(Rdr("key_regis"))
                        If Convert.IsDBNull(Rdr("key_serve")) = False Then ret.key_serve = Convert.ToInt32(Rdr("key_serve"))
                        If Convert.IsDBNull(Rdr("key_miss_call")) = False Then ret.key_miss_call = Convert.ToInt32(Rdr("key_miss_call"))
                        If Convert.IsDBNull(Rdr("key_cancel")) = False Then ret.key_cancel = Convert.ToInt32(Rdr("key_cancel"))
                        If Convert.IsDBNull(Rdr("key_not_call")) = False Then ret.key_not_call = Convert.ToInt32(Rdr("key_not_call"))
                        If Convert.IsDBNull(Rdr("key_not_con")) = False Then ret.key_not_con = Convert.ToInt32(Rdr("key_not_con"))
                        If Convert.IsDBNull(Rdr("key_not_end")) = False Then ret.key_not_end = Convert.ToInt32(Rdr("key_not_end"))
                        If Convert.IsDBNull(Rdr("sme_regis")) = False Then ret.sme_regis = Convert.ToInt32(Rdr("sme_regis"))
                        If Convert.IsDBNull(Rdr("sme_serve")) = False Then ret.sme_serve = Convert.ToInt32(Rdr("sme_serve"))
                        If Convert.IsDBNull(Rdr("sme_miss_call")) = False Then ret.sme_miss_call = Convert.ToInt32(Rdr("sme_miss_call"))
                        If Convert.IsDBNull(Rdr("sme_cancel")) = False Then ret.sme_cancel = Convert.ToInt32(Rdr("sme_cancel"))
                        If Convert.IsDBNull(Rdr("sme_not_call")) = False Then ret.sme_not_call = Convert.ToInt32(Rdr("sme_not_call"))
                        If Convert.IsDBNull(Rdr("sme_not_con")) = False Then ret.sme_not_con = Convert.ToInt32(Rdr("sme_not_con"))
                        If Convert.IsDBNull(Rdr("sme_not_end")) = False Then ret.sme_not_end = Convert.ToInt32(Rdr("sme_not_end"))
                        If Convert.IsDBNull(Rdr("roy_regis")) = False Then ret.roy_regis = Convert.ToInt32(Rdr("roy_regis"))
                        If Convert.IsDBNull(Rdr("roy_serve")) = False Then ret.roy_serve = Convert.ToInt32(Rdr("roy_serve"))
                        If Convert.IsDBNull(Rdr("roy_miss_call")) = False Then ret.roy_miss_call = Convert.ToInt32(Rdr("roy_miss_call"))
                        If Convert.IsDBNull(Rdr("roy_cancel")) = False Then ret.roy_cancel = Convert.ToInt32(Rdr("roy_cancel"))
                        If Convert.IsDBNull(Rdr("roy_not_call")) = False Then ret.roy_not_call = Convert.ToInt32(Rdr("roy_not_call"))
                        If Convert.IsDBNull(Rdr("roy_not_con")) = False Then ret.roy_not_con = Convert.ToInt32(Rdr("roy_not_con"))
                        If Convert.IsDBNull(Rdr("roy_not_end")) = False Then ret.roy_not_end = Convert.ToInt32(Rdr("roy_not_end"))
                        If Convert.IsDBNull(Rdr("tot_regis")) = False Then ret.tot_regis = Convert.ToInt32(Rdr("tot_regis"))
                        If Convert.IsDBNull(Rdr("tot_serve")) = False Then ret.tot_serve = Convert.ToInt32(Rdr("tot_serve"))
                        If Convert.IsDBNull(Rdr("tot_miss_call")) = False Then ret.tot_miss_call = Convert.ToInt32(Rdr("tot_miss_call"))
                        If Convert.IsDBNull(Rdr("tot_cancel")) = False Then ret.tot_cancel = Convert.ToInt32(Rdr("tot_cancel"))
                        If Convert.IsDBNull(Rdr("tot_not_call")) = False Then ret.tot_not_call = Convert.ToInt32(Rdr("tot_not_call"))
                        If Convert.IsDBNull(Rdr("tot_not_con")) = False Then ret.tot_not_con = Convert.ToInt32(Rdr("tot_not_con"))
                        If Convert.IsDBNull(Rdr("tot_not_end")) = False Then ret.tot_not_end = Convert.ToInt32(Rdr("tot_not_end"))
                        If Convert.IsDBNull(Rdr("gov_regis")) = False Then ret.gov_regis = Convert.ToInt32(Rdr("gov_regis"))
                        If Convert.IsDBNull(Rdr("gov_serve")) = False Then ret.gov_serve = Convert.ToInt32(Rdr("gov_serve"))
                        If Convert.IsDBNull(Rdr("gov_miss_call")) = False Then ret.gov_miss_call = Convert.ToInt32(Rdr("gov_miss_call"))
                        If Convert.IsDBNull(Rdr("gov_cancel")) = False Then ret.gov_cancel = Convert.ToInt32(Rdr("gov_cancel"))
                        If Convert.IsDBNull(Rdr("gov_not_call")) = False Then ret.gov_not_call = Convert.ToInt32(Rdr("gov_not_call"))
                        If Convert.IsDBNull(Rdr("gov_not_con")) = False Then ret.gov_not_con = Convert.ToInt32(Rdr("gov_not_con"))
                        If Convert.IsDBNull(Rdr("gov_not_end")) = False Then ret.gov_not_end = Convert.ToInt32(Rdr("gov_not_end"))
                        If Convert.IsDBNull(Rdr("emb_regis")) = False Then ret.emb_regis = Convert.ToInt32(Rdr("emb_regis"))
                        If Convert.IsDBNull(Rdr("emb_serve")) = False Then ret.emb_serve = Convert.ToInt32(Rdr("emb_serve"))
                        If Convert.IsDBNull(Rdr("emb_miss_call")) = False Then ret.emb_miss_call = Convert.ToInt32(Rdr("emb_miss_call"))
                        If Convert.IsDBNull(Rdr("emb_cancel")) = False Then ret.emb_cancel = Convert.ToInt32(Rdr("emb_cancel"))
                        If Convert.IsDBNull(Rdr("emb_not_call")) = False Then ret.emb_not_call = Convert.ToInt32(Rdr("emb_not_call"))
                        If Convert.IsDBNull(Rdr("emb_not_con")) = False Then ret.emb_not_con = Convert.ToInt32(Rdr("emb_not_con"))
                        If Convert.IsDBNull(Rdr("emb_not_end")) = False Then ret.emb_not_end = Convert.ToInt32(Rdr("emb_not_end"))
                        If Convert.IsDBNull(Rdr("non_regis")) = False Then ret.non_regis = Convert.ToInt32(Rdr("non_regis"))
                        If Convert.IsDBNull(Rdr("non_serve")) = False Then ret.non_serve = Convert.ToInt32(Rdr("non_serve"))
                        If Convert.IsDBNull(Rdr("non_miss_call")) = False Then ret.non_miss_call = Convert.ToInt32(Rdr("non_miss_call"))
                        If Convert.IsDBNull(Rdr("non_cancel")) = False Then ret.non_cancel = Convert.ToInt32(Rdr("non_cancel"))
                        If Convert.IsDBNull(Rdr("non_not_call")) = False Then ret.non_not_call = Convert.ToInt32(Rdr("non_not_call"))
                        If Convert.IsDBNull(Rdr("non_not_con")) = False Then ret.non_not_con = Convert.ToInt32(Rdr("non_not_con"))
                        If Convert.IsDBNull(Rdr("non_not_end")) = False Then ret.non_not_end = Convert.ToInt32(Rdr("non_not_end"))
                        If Convert.IsDBNull(Rdr("sta_regis")) = False Then ret.sta_regis = Convert.ToInt32(Rdr("sta_regis"))
                        If Convert.IsDBNull(Rdr("sta_serve")) = False Then ret.sta_serve = Convert.ToInt32(Rdr("sta_serve"))
                        If Convert.IsDBNull(Rdr("sta_miss_call")) = False Then ret.sta_miss_call = Convert.ToInt32(Rdr("sta_miss_call"))
                        If Convert.IsDBNull(Rdr("sta_cancel")) = False Then ret.sta_cancel = Convert.ToInt32(Rdr("sta_cancel"))
                        If Convert.IsDBNull(Rdr("sta_not_call")) = False Then ret.sta_not_call = Convert.ToInt32(Rdr("sta_not_call"))
                        If Convert.IsDBNull(Rdr("sta_not_con")) = False Then ret.sta_not_con = Convert.ToInt32(Rdr("sta_not_con"))
                        If Convert.IsDBNull(Rdr("sta_not_end")) = False Then ret.sta_not_end = Convert.ToInt32(Rdr("sta_not_end"))
                        If Convert.IsDBNull(Rdr("ais_regis")) = False Then ret.ais_regis = Convert.ToInt32(Rdr("ais_regis"))
                        If Convert.IsDBNull(Rdr("ais_serve")) = False Then ret.ais_serve = Convert.ToInt32(Rdr("ais_serve"))
                        If Convert.IsDBNull(Rdr("ais_miss_call")) = False Then ret.ais_miss_call = Convert.ToInt32(Rdr("ais_miss_call"))
                        If Convert.IsDBNull(Rdr("ais_cancel")) = False Then ret.ais_cancel = Convert.ToInt32(Rdr("ais_cancel"))
                        If Convert.IsDBNull(Rdr("ais_not_call")) = False Then ret.ais_not_call = Convert.ToInt32(Rdr("ais_not_call"))
                        If Convert.IsDBNull(Rdr("ais_not_con")) = False Then ret.ais_not_con = Convert.ToInt32(Rdr("ais_not_con"))
                        If Convert.IsDBNull(Rdr("ais_not_end")) = False Then ret.ais_not_end = Convert.ToInt32(Rdr("ais_not_end"))
                        If Convert.IsDBNull(Rdr("pre_regis")) = False Then ret.pre_regis = Convert.ToInt32(Rdr("pre_regis"))
                        If Convert.IsDBNull(Rdr("pre_serve")) = False Then ret.pre_serve = Convert.ToInt32(Rdr("pre_serve"))
                        If Convert.IsDBNull(Rdr("pre_miss_call")) = False Then ret.pre_miss_call = Convert.ToInt32(Rdr("pre_miss_call"))
                        If Convert.IsDBNull(Rdr("pre_cancel")) = False Then ret.pre_cancel = Convert.ToInt32(Rdr("pre_cancel"))
                        If Convert.IsDBNull(Rdr("pre_not_call")) = False Then ret.pre_not_call = Convert.ToInt32(Rdr("pre_not_call"))
                        If Convert.IsDBNull(Rdr("pre_not_con")) = False Then ret.pre_not_con = Convert.ToInt32(Rdr("pre_not_con"))
                        If Convert.IsDBNull(Rdr("pre_not_end")) = False Then ret.pre_not_end = Convert.ToInt32(Rdr("pre_not_end"))
                        If Convert.IsDBNull(Rdr("for_regis")) = False Then ret.for_regis = Convert.ToInt32(Rdr("for_regis"))
                        If Convert.IsDBNull(Rdr("for_serve")) = False Then ret.for_serve = Convert.ToInt32(Rdr("for_serve"))
                        If Convert.IsDBNull(Rdr("for_miss_call")) = False Then ret.for_miss_call = Convert.ToInt32(Rdr("for_miss_call"))
                        If Convert.IsDBNull(Rdr("for_cancel")) = False Then ret.for_cancel = Convert.ToInt32(Rdr("for_cancel"))
                        If Convert.IsDBNull(Rdr("for_not_call")) = False Then ret.for_not_call = Convert.ToInt32(Rdr("for_not_call"))
                        If Convert.IsDBNull(Rdr("for_not_con")) = False Then ret.for_not_con = Convert.ToInt32(Rdr("for_not_con"))
                        If Convert.IsDBNull(Rdr("for_not_end")) = False Then ret.for_not_end = Convert.ToInt32(Rdr("for_not_end"))
                        If Convert.IsDBNull(Rdr("tha_regis")) = False Then ret.tha_regis = Convert.ToInt32(Rdr("tha_regis"))
                        If Convert.IsDBNull(Rdr("tha_serve")) = False Then ret.tha_serve = Convert.ToInt32(Rdr("tha_serve"))
                        If Convert.IsDBNull(Rdr("tha_miss_call")) = False Then ret.tha_miss_call = Convert.ToInt32(Rdr("tha_miss_call"))
                        If Convert.IsDBNull(Rdr("tha_cancel")) = False Then ret.tha_cancel = Convert.ToInt32(Rdr("tha_cancel"))
                        If Convert.IsDBNull(Rdr("tha_not_call")) = False Then ret.tha_not_call = Convert.ToInt32(Rdr("tha_not_call"))
                        If Convert.IsDBNull(Rdr("tha_not_con")) = False Then ret.tha_not_con = Convert.ToInt32(Rdr("tha_not_con"))
                        If Convert.IsDBNull(Rdr("tha_not_end")) = False Then ret.tha_not_end = Convert.ToInt32(Rdr("tha_not_end"))
                        If Convert.IsDBNull(Rdr("no_regis")) = False Then ret.no_regis = Convert.ToInt32(Rdr("no_regis"))
                        If Convert.IsDBNull(Rdr("no_serve")) = False Then ret.no_serve = Convert.ToInt32(Rdr("no_serve"))
                        If Convert.IsDBNull(Rdr("no_miss_call")) = False Then ret.no_miss_call = Convert.ToInt32(Rdr("no_miss_call"))
                        If Convert.IsDBNull(Rdr("no_cancel")) = False Then ret.no_cancel = Convert.ToInt32(Rdr("no_cancel"))
                        If Convert.IsDBNull(Rdr("no_not_call")) = False Then ret.no_not_call = Convert.ToInt32(Rdr("no_not_call"))
                        If Convert.IsDBNull(Rdr("no_not_con")) = False Then ret.no_not_con = Convert.ToInt32(Rdr("no_not_con"))
                        If Convert.IsDBNull(Rdr("no_not_end")) = False Then ret.no_not_end = Convert.ToInt32(Rdr("no_not_end"))

                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return ret
        End Function

        ' SQL Statements


        'Get Insert Statement for table TB_REP_CUST_CATEGORY_DAY
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, SERVICE_DATE, SHOW_DATE, SERVICE_ID, SERVICE_NAME_EN, SERVICE_NAME_TH, KEY_REGIS, KEY_SERVE, KEY_MISS_CALL, KEY_CANCEL, KEY_NOT_CALL, KEY_NOT_CON, KEY_NOT_END, SME_REGIS, SME_SERVE, SME_MISS_CALL, SME_CANCEL, SME_NOT_CALL, SME_NOT_CON, SME_NOT_END, ROY_REGIS, ROY_SERVE, ROY_MISS_CALL, ROY_CANCEL, ROY_NOT_CALL, ROY_NOT_CON, ROY_NOT_END, TOT_REGIS, TOT_SERVE, TOT_MISS_CALL, TOT_CANCEL, TOT_NOT_CALL, TOT_NOT_CON, TOT_NOT_END, GOV_REGIS, GOV_SERVE, GOV_MISS_CALL, GOV_CANCEL, GOV_NOT_CALL, GOV_NOT_CON, GOV_NOT_END, EMB_REGIS, EMB_SERVE, EMB_MISS_CALL, EMB_CANCEL, EMB_NOT_CALL, EMB_NOT_CON, EMB_NOT_END, NON_REGIS, NON_SERVE, NON_MISS_CALL, NON_CANCEL, NON_NOT_CALL, NON_NOT_CON, NON_NOT_END, STA_REGIS, STA_SERVE, STA_MISS_CALL, STA_CANCEL, STA_NOT_CALL, STA_NOT_CON, STA_NOT_END, AIS_REGIS, AIS_SERVE, AIS_MISS_CALL, AIS_CANCEL, AIS_NOT_CALL, AIS_NOT_CON, AIS_NOT_END, PRE_REGIS, PRE_SERVE, PRE_MISS_CALL, PRE_CANCEL, PRE_NOT_CALL, PRE_NOT_CON, PRE_NOT_END, FOR_REGIS, FOR_SERVE, FOR_MISS_CALL, FOR_CANCEL, FOR_NOT_CALL, FOR_NOT_CON, FOR_NOT_END, THA_REGIS, THA_SERVE, THA_MISS_CALL, THA_CANCEL, THA_NOT_CALL, THA_NOT_CON, THA_NOT_END, NO_REGIS, NO_SERVE, NO_MISS_CALL, NO_CANCEL, NO_NOT_CALL, NO_NOT_CON, NO_NOT_END)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetDouble(_SHOP_ID) & ", "
                sql += DB.SetString(_SHOP_NAME_TH) & ", "
                sql += DB.SetString(_SHOP_NAME_EN) & ", "
                sql += DB.SetDateTime(_SERVICE_DATE) & ", "
                sql += DB.SetString(_SHOW_DATE) & ", "
                sql += DB.SetDouble(_SERVICE_ID) & ", "
                sql += DB.SetString(_SERVICE_NAME_EN) & ", "
                sql += DB.SetString(_SERVICE_NAME_TH) & ", "
                sql += DB.SetDouble(_KEY_REGIS) & ", "
                sql += DB.SetDouble(_KEY_SERVE) & ", "
                sql += DB.SetDouble(_KEY_MISS_CALL) & ", "
                sql += DB.SetDouble(_KEY_CANCEL) & ", "
                sql += DB.SetDouble(_KEY_NOT_CALL) & ", "
                sql += DB.SetDouble(_KEY_NOT_CON) & ", "
                sql += DB.SetDouble(_KEY_NOT_END) & ", "
                sql += DB.SetDouble(_SME_REGIS) & ", "
                sql += DB.SetDouble(_SME_SERVE) & ", "
                sql += DB.SetDouble(_SME_MISS_CALL) & ", "
                sql += DB.SetDouble(_SME_CANCEL) & ", "
                sql += DB.SetDouble(_SME_NOT_CALL) & ", "
                sql += DB.SetDouble(_SME_NOT_CON) & ", "
                sql += DB.SetDouble(_SME_NOT_END) & ", "
                sql += DB.SetDouble(_ROY_REGIS) & ", "
                sql += DB.SetDouble(_ROY_SERVE) & ", "
                sql += DB.SetDouble(_ROY_MISS_CALL) & ", "
                sql += DB.SetDouble(_ROY_CANCEL) & ", "
                sql += DB.SetDouble(_ROY_NOT_CALL) & ", "
                sql += DB.SetDouble(_ROY_NOT_CON) & ", "
                sql += DB.SetDouble(_ROY_NOT_END) & ", "
                sql += DB.SetDouble(_TOT_REGIS) & ", "
                sql += DB.SetDouble(_TOT_SERVE) & ", "
                sql += DB.SetDouble(_TOT_MISS_CALL) & ", "
                sql += DB.SetDouble(_TOT_CANCEL) & ", "
                sql += DB.SetDouble(_TOT_NOT_CALL) & ", "
                sql += DB.SetDouble(_TOT_NOT_CON) & ", "
                sql += DB.SetDouble(_TOT_NOT_END) & ", "
                sql += DB.SetDouble(_GOV_REGIS) & ", "
                sql += DB.SetDouble(_GOV_SERVE) & ", "
                sql += DB.SetDouble(_GOV_MISS_CALL) & ", "
                sql += DB.SetDouble(_GOV_CANCEL) & ", "
                sql += DB.SetDouble(_GOV_NOT_CALL) & ", "
                sql += DB.SetDouble(_GOV_NOT_CON) & ", "
                sql += DB.SetDouble(_GOV_NOT_END) & ", "
                sql += DB.SetDouble(_EMB_REGIS) & ", "
                sql += DB.SetDouble(_EMB_SERVE) & ", "
                sql += DB.SetDouble(_EMB_MISS_CALL) & ", "
                sql += DB.SetDouble(_EMB_CANCEL) & ", "
                sql += DB.SetDouble(_EMB_NOT_CALL) & ", "
                sql += DB.SetDouble(_EMB_NOT_CON) & ", "
                sql += DB.SetDouble(_EMB_NOT_END) & ", "
                sql += DB.SetDouble(_NON_REGIS) & ", "
                sql += DB.SetDouble(_NON_SERVE) & ", "
                sql += DB.SetDouble(_NON_MISS_CALL) & ", "
                sql += DB.SetDouble(_NON_CANCEL) & ", "
                sql += DB.SetDouble(_NON_NOT_CALL) & ", "
                sql += DB.SetDouble(_NON_NOT_CON) & ", "
                sql += DB.SetDouble(_NON_NOT_END) & ", "
                sql += DB.SetDouble(_STA_REGIS) & ", "
                sql += DB.SetDouble(_STA_SERVE) & ", "
                sql += DB.SetDouble(_STA_MISS_CALL) & ", "
                sql += DB.SetDouble(_STA_CANCEL) & ", "
                sql += DB.SetDouble(_STA_NOT_CALL) & ", "
                sql += DB.SetDouble(_STA_NOT_CON) & ", "
                sql += DB.SetDouble(_STA_NOT_END) & ", "
                sql += DB.SetDouble(_AIS_REGIS) & ", "
                sql += DB.SetDouble(_AIS_SERVE) & ", "
                sql += DB.SetDouble(_AIS_MISS_CALL) & ", "
                sql += DB.SetDouble(_AIS_CANCEL) & ", "
                sql += DB.SetDouble(_AIS_NOT_CALL) & ", "
                sql += DB.SetDouble(_AIS_NOT_CON) & ", "
                sql += DB.SetDouble(_AIS_NOT_END) & ", "
                sql += DB.SetDouble(_PRE_REGIS) & ", "
                sql += DB.SetDouble(_PRE_SERVE) & ", "
                sql += DB.SetDouble(_PRE_MISS_CALL) & ", "
                sql += DB.SetDouble(_PRE_CANCEL) & ", "
                sql += DB.SetDouble(_PRE_NOT_CALL) & ", "
                sql += DB.SetDouble(_PRE_NOT_CON) & ", "
                sql += DB.SetDouble(_PRE_NOT_END) & ", "
                sql += DB.SetDouble(_FOR_REGIS) & ", "
                sql += DB.SetDouble(_FOR_SERVE) & ", "
                sql += DB.SetDouble(_FOR_MISS_CALL) & ", "
                sql += DB.SetDouble(_FOR_CANCEL) & ", "
                sql += DB.SetDouble(_FOR_NOT_CALL) & ", "
                sql += DB.SetDouble(_FOR_NOT_CON) & ", "
                sql += DB.SetDouble(_FOR_NOT_END) & ", "
                sql += DB.SetDouble(_THA_REGIS) & ", "
                sql += DB.SetDouble(_THA_SERVE) & ", "
                sql += DB.SetDouble(_THA_MISS_CALL) & ", "
                sql += DB.SetDouble(_THA_CANCEL) & ", "
                sql += DB.SetDouble(_THA_NOT_CALL) & ", "
                sql += DB.SetDouble(_THA_NOT_CON) & ", "
                sql += DB.SetDouble(_THA_NOT_END) & ", "
                sql += DB.SetDouble(_NO_REGIS) & ", "
                sql += DB.SetDouble(_NO_SERVE) & ", "
                sql += DB.SetDouble(_NO_MISS_CALL) & ", "
                sql += DB.SetDouble(_NO_CANCEL) & ", "
                sql += DB.SetDouble(_NO_NOT_CALL) & ", "
                sql += DB.SetDouble(_NO_NOT_CON) & ", "
                sql += DB.SetDouble(_NO_NOT_END)
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_REP_CUST_CATEGORY_DAY
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "SHOP_ID = " & DB.SetDouble(_SHOP_ID) & ", "
                Sql += "SHOP_NAME_TH = " & DB.SetString(_SHOP_NAME_TH) & ", "
                Sql += "SHOP_NAME_EN = " & DB.SetString(_SHOP_NAME_EN) & ", "
                Sql += "SERVICE_DATE = " & DB.SetDateTime(_SERVICE_DATE) & ", "
                Sql += "SHOW_DATE = " & DB.SetString(_SHOW_DATE) & ", "
                Sql += "SERVICE_ID = " & DB.SetDouble(_SERVICE_ID) & ", "
                Sql += "SERVICE_NAME_EN = " & DB.SetString(_SERVICE_NAME_EN) & ", "
                Sql += "SERVICE_NAME_TH = " & DB.SetString(_SERVICE_NAME_TH) & ", "
                Sql += "KEY_REGIS = " & DB.SetDouble(_KEY_REGIS) & ", "
                Sql += "KEY_SERVE = " & DB.SetDouble(_KEY_SERVE) & ", "
                Sql += "KEY_MISS_CALL = " & DB.SetDouble(_KEY_MISS_CALL) & ", "
                Sql += "KEY_CANCEL = " & DB.SetDouble(_KEY_CANCEL) & ", "
                Sql += "KEY_NOT_CALL = " & DB.SetDouble(_KEY_NOT_CALL) & ", "
                Sql += "KEY_NOT_CON = " & DB.SetDouble(_KEY_NOT_CON) & ", "
                Sql += "KEY_NOT_END = " & DB.SetDouble(_KEY_NOT_END) & ", "
                Sql += "SME_REGIS = " & DB.SetDouble(_SME_REGIS) & ", "
                Sql += "SME_SERVE = " & DB.SetDouble(_SME_SERVE) & ", "
                Sql += "SME_MISS_CALL = " & DB.SetDouble(_SME_MISS_CALL) & ", "
                Sql += "SME_CANCEL = " & DB.SetDouble(_SME_CANCEL) & ", "
                Sql += "SME_NOT_CALL = " & DB.SetDouble(_SME_NOT_CALL) & ", "
                Sql += "SME_NOT_CON = " & DB.SetDouble(_SME_NOT_CON) & ", "
                Sql += "SME_NOT_END = " & DB.SetDouble(_SME_NOT_END) & ", "
                Sql += "ROY_REGIS = " & DB.SetDouble(_ROY_REGIS) & ", "
                Sql += "ROY_SERVE = " & DB.SetDouble(_ROY_SERVE) & ", "
                Sql += "ROY_MISS_CALL = " & DB.SetDouble(_ROY_MISS_CALL) & ", "
                Sql += "ROY_CANCEL = " & DB.SetDouble(_ROY_CANCEL) & ", "
                Sql += "ROY_NOT_CALL = " & DB.SetDouble(_ROY_NOT_CALL) & ", "
                Sql += "ROY_NOT_CON = " & DB.SetDouble(_ROY_NOT_CON) & ", "
                Sql += "ROY_NOT_END = " & DB.SetDouble(_ROY_NOT_END) & ", "
                Sql += "TOT_REGIS = " & DB.SetDouble(_TOT_REGIS) & ", "
                Sql += "TOT_SERVE = " & DB.SetDouble(_TOT_SERVE) & ", "
                Sql += "TOT_MISS_CALL = " & DB.SetDouble(_TOT_MISS_CALL) & ", "
                Sql += "TOT_CANCEL = " & DB.SetDouble(_TOT_CANCEL) & ", "
                Sql += "TOT_NOT_CALL = " & DB.SetDouble(_TOT_NOT_CALL) & ", "
                Sql += "TOT_NOT_CON = " & DB.SetDouble(_TOT_NOT_CON) & ", "
                Sql += "TOT_NOT_END = " & DB.SetDouble(_TOT_NOT_END) & ", "
                Sql += "GOV_REGIS = " & DB.SetDouble(_GOV_REGIS) & ", "
                Sql += "GOV_SERVE = " & DB.SetDouble(_GOV_SERVE) & ", "
                Sql += "GOV_MISS_CALL = " & DB.SetDouble(_GOV_MISS_CALL) & ", "
                Sql += "GOV_CANCEL = " & DB.SetDouble(_GOV_CANCEL) & ", "
                Sql += "GOV_NOT_CALL = " & DB.SetDouble(_GOV_NOT_CALL) & ", "
                Sql += "GOV_NOT_CON = " & DB.SetDouble(_GOV_NOT_CON) & ", "
                Sql += "GOV_NOT_END = " & DB.SetDouble(_GOV_NOT_END) & ", "
                Sql += "EMB_REGIS = " & DB.SetDouble(_EMB_REGIS) & ", "
                Sql += "EMB_SERVE = " & DB.SetDouble(_EMB_SERVE) & ", "
                Sql += "EMB_MISS_CALL = " & DB.SetDouble(_EMB_MISS_CALL) & ", "
                Sql += "EMB_CANCEL = " & DB.SetDouble(_EMB_CANCEL) & ", "
                Sql += "EMB_NOT_CALL = " & DB.SetDouble(_EMB_NOT_CALL) & ", "
                Sql += "EMB_NOT_CON = " & DB.SetDouble(_EMB_NOT_CON) & ", "
                Sql += "EMB_NOT_END = " & DB.SetDouble(_EMB_NOT_END) & ", "
                Sql += "NON_REGIS = " & DB.SetDouble(_NON_REGIS) & ", "
                Sql += "NON_SERVE = " & DB.SetDouble(_NON_SERVE) & ", "
                Sql += "NON_MISS_CALL = " & DB.SetDouble(_NON_MISS_CALL) & ", "
                Sql += "NON_CANCEL = " & DB.SetDouble(_NON_CANCEL) & ", "
                Sql += "NON_NOT_CALL = " & DB.SetDouble(_NON_NOT_CALL) & ", "
                Sql += "NON_NOT_CON = " & DB.SetDouble(_NON_NOT_CON) & ", "
                Sql += "NON_NOT_END = " & DB.SetDouble(_NON_NOT_END) & ", "
                Sql += "STA_REGIS = " & DB.SetDouble(_STA_REGIS) & ", "
                Sql += "STA_SERVE = " & DB.SetDouble(_STA_SERVE) & ", "
                Sql += "STA_MISS_CALL = " & DB.SetDouble(_STA_MISS_CALL) & ", "
                Sql += "STA_CANCEL = " & DB.SetDouble(_STA_CANCEL) & ", "
                Sql += "STA_NOT_CALL = " & DB.SetDouble(_STA_NOT_CALL) & ", "
                Sql += "STA_NOT_CON = " & DB.SetDouble(_STA_NOT_CON) & ", "
                Sql += "STA_NOT_END = " & DB.SetDouble(_STA_NOT_END) & ", "
                Sql += "AIS_REGIS = " & DB.SetDouble(_AIS_REGIS) & ", "
                Sql += "AIS_SERVE = " & DB.SetDouble(_AIS_SERVE) & ", "
                Sql += "AIS_MISS_CALL = " & DB.SetDouble(_AIS_MISS_CALL) & ", "
                Sql += "AIS_CANCEL = " & DB.SetDouble(_AIS_CANCEL) & ", "
                Sql += "AIS_NOT_CALL = " & DB.SetDouble(_AIS_NOT_CALL) & ", "
                Sql += "AIS_NOT_CON = " & DB.SetDouble(_AIS_NOT_CON) & ", "
                Sql += "AIS_NOT_END = " & DB.SetDouble(_AIS_NOT_END) & ", "
                Sql += "PRE_REGIS = " & DB.SetDouble(_PRE_REGIS) & ", "
                Sql += "PRE_SERVE = " & DB.SetDouble(_PRE_SERVE) & ", "
                Sql += "PRE_MISS_CALL = " & DB.SetDouble(_PRE_MISS_CALL) & ", "
                Sql += "PRE_CANCEL = " & DB.SetDouble(_PRE_CANCEL) & ", "
                Sql += "PRE_NOT_CALL = " & DB.SetDouble(_PRE_NOT_CALL) & ", "
                Sql += "PRE_NOT_CON = " & DB.SetDouble(_PRE_NOT_CON) & ", "
                Sql += "PRE_NOT_END = " & DB.SetDouble(_PRE_NOT_END) & ", "
                Sql += "FOR_REGIS = " & DB.SetDouble(_FOR_REGIS) & ", "
                Sql += "FOR_SERVE = " & DB.SetDouble(_FOR_SERVE) & ", "
                Sql += "FOR_MISS_CALL = " & DB.SetDouble(_FOR_MISS_CALL) & ", "
                Sql += "FOR_CANCEL = " & DB.SetDouble(_FOR_CANCEL) & ", "
                Sql += "FOR_NOT_CALL = " & DB.SetDouble(_FOR_NOT_CALL) & ", "
                Sql += "FOR_NOT_CON = " & DB.SetDouble(_FOR_NOT_CON) & ", "
                Sql += "FOR_NOT_END = " & DB.SetDouble(_FOR_NOT_END) & ", "
                Sql += "THA_REGIS = " & DB.SetDouble(_THA_REGIS) & ", "
                Sql += "THA_SERVE = " & DB.SetDouble(_THA_SERVE) & ", "
                Sql += "THA_MISS_CALL = " & DB.SetDouble(_THA_MISS_CALL) & ", "
                Sql += "THA_CANCEL = " & DB.SetDouble(_THA_CANCEL) & ", "
                Sql += "THA_NOT_CALL = " & DB.SetDouble(_THA_NOT_CALL) & ", "
                Sql += "THA_NOT_CON = " & DB.SetDouble(_THA_NOT_CON) & ", "
                Sql += "THA_NOT_END = " & DB.SetDouble(_THA_NOT_END) & ", "
                Sql += "NO_REGIS = " & DB.SetDouble(_NO_REGIS) & ", "
                Sql += "NO_SERVE = " & DB.SetDouble(_NO_SERVE) & ", "
                Sql += "NO_MISS_CALL = " & DB.SetDouble(_NO_MISS_CALL) & ", "
                Sql += "NO_CANCEL = " & DB.SetDouble(_NO_CANCEL) & ", "
                Sql += "NO_NOT_CALL = " & DB.SetDouble(_NO_NOT_CALL) & ", "
                Sql += "NO_NOT_CON = " & DB.SetDouble(_NO_NOT_CON) & ", "
                Sql += "NO_NOT_END = " & DB.SetDouble(_NO_NOT_END) + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_REP_CUST_CATEGORY_DAY
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_REP_CUST_CATEGORY_DAY
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, SHOP_ID, SHOP_NAME_TH, SHOP_NAME_EN, SERVICE_DATE, SHOW_DATE, SERVICE_ID, SERVICE_NAME_EN, SERVICE_NAME_TH, KEY_REGIS, KEY_SERVE, KEY_MISS_CALL, KEY_CANCEL, KEY_NOT_CALL, KEY_NOT_CON, KEY_NOT_END, SME_REGIS, SME_SERVE, SME_MISS_CALL, SME_CANCEL, SME_NOT_CALL, SME_NOT_CON, SME_NOT_END, ROY_REGIS, ROY_SERVE, ROY_MISS_CALL, ROY_CANCEL, ROY_NOT_CALL, ROY_NOT_CON, ROY_NOT_END, TOT_REGIS, TOT_SERVE, TOT_MISS_CALL, TOT_CANCEL, TOT_NOT_CALL, TOT_NOT_CON, TOT_NOT_END, GOV_REGIS, GOV_SERVE, GOV_MISS_CALL, GOV_CANCEL, GOV_NOT_CALL, GOV_NOT_CON, GOV_NOT_END, EMB_REGIS, EMB_SERVE, EMB_MISS_CALL, EMB_CANCEL, EMB_NOT_CALL, EMB_NOT_CON, EMB_NOT_END, NON_REGIS, NON_SERVE, NON_MISS_CALL, NON_CANCEL, NON_NOT_CALL, NON_NOT_CON, NON_NOT_END, STA_REGIS, STA_SERVE, STA_MISS_CALL, STA_CANCEL, STA_NOT_CALL, STA_NOT_CON, STA_NOT_END, AIS_REGIS, AIS_SERVE, AIS_MISS_CALL, AIS_CANCEL, AIS_NOT_CALL, AIS_NOT_CON, AIS_NOT_END, PRE_REGIS, PRE_SERVE, PRE_MISS_CALL, PRE_CANCEL, PRE_NOT_CALL, PRE_NOT_CON, PRE_NOT_END, FOR_REGIS, FOR_SERVE, FOR_MISS_CALL, FOR_CANCEL, FOR_NOT_CALL, FOR_NOT_CON, FOR_NOT_END, THA_REGIS, THA_SERVE, THA_MISS_CALL, THA_CANCEL, THA_NOT_CALL, THA_NOT_CON, THA_NOT_END, NO_REGIS, NO_SERVE, NO_MISS_CALL, NO_CANCEL, NO_NOT_CALL, NO_NOT_CON, NO_NOT_END FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
