Imports ShLinqDB.TABLE
Imports ShParaDB.TABLE
Imports System.Web.UI.WebControls
Namespace Configuration
    Public Class ShopCustomerTypeENG
        Dim _err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err.Trim
            End Get
        End Property

        Public Sub New()
            _err = ""
        End Sub

        Public Function SaveCustomerType(ByVal p As CenParaDB.TABLE.TbCustomertypeCenParaDB, ByVal UserName As String, ByVal ShopList As TreeNodeCollection) As Boolean
            Dim ret As Boolean = False
            For Each c As TreeNode In ShopList
                Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(c.Value)
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans = Engine.Common.FunctionEng.GetShTransction(c.Value, "ShopServiceENG.SaveCustomerType")
                If shTrans.Trans IsNot Nothing Then
                    Dim lnq As New TbCustomertypeShLinqDB
                    lnq.ChkDataByWhere("master_customertypeid='" & p.ID & "'", shTrans.Trans)
                    If lnq.ID = 0 Then
                        lnq.ChkDataByWhere("customertype_code='" & p.CUSTOMERTYPE_CODE & "'", shTrans.Trans)
                    End If
                    If lnq.ID = 0 Then
                        lnq.ChkDataByWhere("customertype_name='" & p.CUSTOMERTYPE_NAME & "'", shTrans.Trans)
                    End If

                    lnq.MASTER_CUSTOMERTYPEID = p.ID
                    lnq.CUSTOMERTYPE_CODE = p.CUSTOMERTYPE_CODE
                    lnq.CUSTOMERTYPE_NAME = p.CUSTOMERTYPE_NAME
                    lnq.DEF = p.DEF
                    lnq.APP = p.APP
                    lnq.TXT_QUEUE = p.TXT_QUEUE
                    lnq.COLOR = p.COLOR
                    lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
                    lnq.MIN_QUEUE = p.MIN_QUEUE
                    lnq.MAX_QUEUE = p.MAX_QUEUE
                    lnq.PRIORITY_VALUE = "1"

                    If lnq.ID <> 0 Then
                        ret = lnq.UpdateByPK(UserName, shTrans.Trans)
                    Else
                        ret = lnq.InsertData(UserName, shTrans.Trans)
                    End If
                    If ret = False Then
                        shTrans.RollbackTransaction()
                        _err = "ShopServiceENG.SaveCustomerType :" & shLnq.SHOP_ABB & " ### " & lnq.ErrorMessage
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

        Public Function GetShopCustomerTypePara(ByVal ShopID As Long, ByVal CustomerTypeID As Integer) As TbCustomertypeShParaDB
            Dim p As New TbCustomertypeShParaDB
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(ShopID, "ShopCustomerTypeENG.GetShopItemPara")
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New TbCustomertypeShLinqDB
                p = lnq.GetParameter(CustomerTypeID, shTrans.Trans)
                shTrans.CommitTransaction()
                lnq = Nothing
            End If

            Return p
        End Function

        Public Function CheckDuplicateCustomerType(ByVal ShopID As Long, ByVal CustomerTypeID As String, ByVal CustomerTypeName As String, ByVal CustomerTypeCode As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(shTrans, trans, shLnq)
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New TbCustomertypeShLinqDB

                ret = lnq.ChkDataByWhere("customertype_code='" & CustomerTypeCode & "' and id<>'" & CustomerTypeID & "'", shTrans.Trans)
                If ret = True Then
                    _err = "Duplicate Customer Type Code at " & shLnq.SHOP_NAME_EN
                End If

                If ret = False Then
                    ret = lnq.ChkDataByWhere("customertype_name='" & CustomerTypeName & "' and id<>'" & CustomerTypeID & "'", shTrans.Trans)
                    If ret = True Then
                        _err = "Duplicate Customer Type Name at " & shLnq.SHOP_NAME_EN
                    End If
                End If

                shTrans.CommitTransaction()
                lnq = Nothing
            End If
            trans.CommitTransaction()

            Return ret
        End Function

    End Class
End Namespace

