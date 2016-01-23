Imports CenParaDB.TABLE
Imports CenLinqDB.TABLE
Imports CenLinqDB.Common.Utilities
Imports System.Data.SqlClient
Imports Engine.Common
Imports ShLinqDB.TABLE
Namespace Configuration
    Public Class ShopHoldReasonENG
        Dim _err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property

        Public Function SaveShHoldReason(ByVal UserName As String, ByVal p As TbHoldReasonCenParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim shDT As New DataTable
            shDT = FunctionEng.GetActiveShopDDL
            If shDT.Rows.Count > 0 Then
                For i As Integer = 0 To shDT.Rows.Count - 1
                    Dim _shopid As String = shDT.Rows(i).Item("ID").ToString
                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(_shopid)
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = Engine.Common.FunctionEng.GetShTransction(_shopid, "ShopHoldReasonENG.vb.SaveShHoldReason")
                    If shTrans.Trans IsNot Nothing Then
                        Dim shItemLnq As New TbHoldReasonShLinqDB
                        shItemLnq.ChkDataByWhere("MASTER_HOLDREASONID='" & p.ID & "'", shTrans.Trans)
                        If shItemLnq.ID <> 0 Then
                            shItemLnq.MASTER_HOLDREASONID = p.ID
                            shItemLnq.NAME = p.NAME
                            shItemLnq.PRODUCTIVE = p.PRODUCTIVE
                            shItemLnq.ACTIVE_STATUS = p.ACTIVE_STATUS
                            ret = shItemLnq.UpdateByPK(UserName, shTrans.Trans)
                        End If

                        If ret = False Then
                            shTrans.RollbackTransaction()
                            _err = "ShopHoldReasonENG.vb.SaveShHoldReason :" & shLnq.SHOP_ABB & " ### " & shItemLnq.ErrorMessage
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
            End If
            Return ret
        End Function
    End Class
End Namespace