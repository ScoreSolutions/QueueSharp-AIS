Imports ShLinqDB.TABLE
Imports ShParaDB.TABLE
Imports System.Web.UI.WebControls

Namespace Configuration
    Public Class ShopServiceENG
        Dim _err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err.Trim
            End Get
        End Property

        Public Function SaveShopItem(ByVal p As CenParaDB.TABLE.TbItemCenParaDB, ByVal UserName As String, ByVal ShopList As TreeNodeCollection) As Boolean
            Dim ret As Boolean = False
            For Each c As TreeNode In ShopList
                Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(c.Value)
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = Engine.Common.FunctionEng.GetShTransction(c.Value, "ShopServiceENG.SaveShopItem")
                If shTrans.Trans IsNot Nothing Then
                    Dim lnq As New TbItemShLinqDB
                    lnq.ChkDataByWhere("master_itemid='" & p.ID & "'", shTrans.Trans)
                    If lnq.ID = 0 Then
                        lnq.ChkDataByWhere("item_code='" & p.ITEM_CODE & "'", shTrans.Trans)
                    End If
                    If lnq.ID = 0 Then
                        lnq.ChkDataByWhere("item_name='" & p.ITEM_NAME & "'", shTrans.Trans)
                    End If
                    If lnq.ID = 0 Then
                        lnq.ChkDataByWhere("item_name_th='" & p.ITEM_NAME_TH & "'", shTrans.Trans)
                    End If

                    lnq.MASTER_ITEMID = p.ID
                    lnq.ITEM_CODE = p.ITEM_CODE
                    lnq.ITEM_NAME = p.ITEM_NAME
                    lnq.ITEM_NAME_TH = p.ITEM_NAME_TH
                    lnq.ITEM_TIME = p.ITEM_TIME
                    lnq.ITEM_WAIT = p.ITEM_WAIT
                    lnq.TXT_QUEUE = p.TXT_QUEUE
                    lnq.COLOR = p.COLOR
                    lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
                    lnq.APP_MIN_QUEUE = p.APP_MIN_QUEUE
                    lnq.APP_MAX_QUEUE = p.APP_MAX_QUEUE
                    lnq.TB_ITEM_GROUP_ID = p.TB_ITEM_GROUP_ID
                    lnq.BRAND_NAME = p.BRAND_NAME

                    If lnq.ID <> 0 Then
                        ret = lnq.UpdateByPK(UserName, shTrans.Trans)
                        lnq.UpdateBySql("update TB_ITEM set item_order = " & p.ITEM_ORDER & " where id ='" & lnq.ID & "'", shTrans.Trans)

                        If p.ITEM_ORDER > lnq.ITEM_ORDER Then
                            Dim Sql As String = "select *  from TB_ITEM  where item_order <= " & p.ITEM_ORDER & "  order by item_order asc"
                            Dim dt As New DataTable
                            dt = lnq.GetListBySql(Sql, shTrans.Trans)
                            If dt.Rows.Count > 0 Then
                                For i As Int32 = 0 To dt.Rows.Count - 1
                                    Sql = "update TB_ITEM set item_order = " & i + 1 & " where id = " & dt.Rows(i).Item("id")
                                    lnq.UpdateBySql(Sql, shTrans.Trans)
                                Next
                            End If
                        Else
                            Dim Sql As String = "select *  from TB_ITEM  where item_order < " & p.ITEM_ORDER & "  order by item_order asc"
                            Dim dt As New DataTable
                            dt = lnq.GetListBySql(Sql, shTrans.Trans)
                            For i As Int32 = 0 To dt.Rows.Count - 1
                                Sql = "update TB_ITEM set item_order = " & i + 1 & " where id = " & dt.Rows(i).Item("id")
                                lnq.UpdateBySql(Sql, shTrans.Trans)
                            Next

                            Sql = "select *  from TB_ITEM  where item_order >= " & p.ITEM_ORDER & " and item_code <> '" & p.ITEM_CODE & "' order by item_order asc"
                            dt = New DataTable
                            dt = lnq.GetListBySql(Sql, shTrans.Trans)
                            For i As Int32 = 0 To dt.Rows.Count - 1
                                Sql = "update TB_ITEM set item_order = " & p.ITEM_ORDER + i + 1 & " where id = " & dt.Rows(i).Item("id")
                                lnq.UpdateBySql(Sql, shTrans.Trans)
                            Next
                        End If

                    Else
                        Dim dt As New DataTable
                        dt = lnq.GetListBySql("Select Count(Item_Order) + 1 ItemOrder From TB_ITEM ", shTrans.Trans)
                        Dim shItemOrder As Integer = 1
                        If dt.Rows.Count > 0 Then
                            shItemOrder = CInt(dt.Rows(0).Item("ItemOrder"))
                        End If

                        lnq.ITEM_ORDER = shItemOrder
                        ret = lnq.InsertData(UserName, shTrans.Trans)
                    End If


                    If ret = False Then
                        shTrans.RollbackTransaction()
                        _err = "ShopServiceENG.SaveShopItem :" & shLnq.SHOP_ABB & " ### " & lnq.ErrorMessage
                        Exit For
                    Else
                        shTrans.CommitTransaction()
                    End If
                Else
                    _err = shLnq.SHOP_ABB & " ### " & shTrans.ErrorMessage
                    ret = False
                    Exit For
                End If
            Next

            Return ret
        End Function

        Public Function GetShopItemPara(ByVal ShopID As Long, ByVal ItemID As Integer) As TbItemShParaDB
            Dim p As New TbItemShParaDB
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(ShopID, "ShopServiceENG.GetShopItemPara")
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New TbItemShLinqDB
                p = lnq.GetParameter(ItemID, shTrans.Trans)
                shTrans.CommitTransaction()
                lnq = Nothing
            End If

            Return p
        End Function

        Public Sub New()
            _err = ""
        End Sub

        Public Function SaveShopSegment(ByVal p As ShParaDB.TABLE.TbSegmentShParaDB, ByVal UserName As String, ByVal ShopList As TreeNodeCollection) As Boolean
            Dim ret As Boolean = False
            For Each c As TreeNode In ShopList
                Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(c.Value)

                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = Engine.Common.FunctionEng.GetShTransction(c.Value, "ShopServiceENG.SaveShopItem")
                If shTrans.Trans IsNot Nothing Then
                    Dim lnq As New TbSegmentShLinqDB
                    lnq.ChkDataByWhere("id='" & p.ID & "'", shTrans.Trans)
                    lnq.SEGMENT = p.SEGMENT
                    lnq.CUSTOMERTYPE_ID = p.CUSTOMERTYPE_ID
                    'lnq.CREATE_BY = p.CREATE_BY
                    'lnq.CREATE_DATE = p.CREATE_DATE
                    'lnq.UPDATE_BY = p.UPDATE_BY
                    'lnq.UPDATE_DATE = p.UPDATE_DATE
                    lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
  

                    If lnq.ID <> 0 Then
                        ret = lnq.UpdateByPK(UserName, shTrans.Trans)
                    Else
                        ret = lnq.InsertData(UserName, shTrans.Trans)
                    End If
                    If ret = False Then
                        shTrans.RollbackTransaction()
                        _err = "ShopSegmentENG.SaveSegmentItem :" & shLnq.SHOP_ABB & " ### " & lnq.ErrorMessage
                        Exit For
                    Else
                        shTrans.CommitTransaction()
                    End If
                Else
                    _err = shLnq.SHOP_ABB & " ### " & shTrans.ErrorMessage
                    ret = False
                    Exit For
                End If
            Next

            Return ret
        End Function

        Public Function GetSegmentItemPara(ByVal ShopID As Long, ByVal ItemID As Integer) As TbSegmentShParaDB
            Dim p As New TbSegmentShParaDB
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(ShopID, "ShopServiceENG.GetSegmenttemPara")
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New TbSegmentShLinqDB
                p = lnq.GetParameter(ItemID, shTrans.Trans)
                shTrans.CommitTransaction()
                lnq = Nothing
            End If
            Return p
        End Function

    End Class
End Namespace

