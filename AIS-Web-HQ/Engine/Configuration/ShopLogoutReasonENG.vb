Imports CenParaDB.TABLE
Imports CenLinqDB.TABLE
Imports CenLinqDB.Common.Utilities
Imports System.Data.SqlClient
Imports Engine.Common
Imports ShLinqDB.TABLE
Namespace Configuration
    Public Class ShopLogoutReasonENG
        Dim _err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property

        Public Function SaveShLogoutReason(ByVal UserName As String, ByVal p As TbLogoutReasonCenParaDB) As Boolean
            _err = ""
            Dim ret As Boolean = False
            Dim shDT As New DataTable
            shDT = FunctionEng.GetActiveShopDDL
            If shDT.Rows.Count > 0 Then
                For i As Integer = 0 To shDT.Rows.Count - 1
                    Dim _shopid As String = shDT.Rows(i).Item("ID").ToString
                    Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(_shopid)
                    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                    shTrans = Engine.Common.FunctionEng.GetShTransction(_shopid, "SaveShLogoutReason.SaveShLogoutReason")
                    If shTrans.Trans IsNot Nothing Then
                        Dim shItemLnq As New TbLogoutReasonShLinqDB
                        shItemLnq.ChkDataByWhere("MASTER_LOGOUTREASONID='" & p.ID & "'", shTrans.Trans)
                        If shItemLnq.ID <> 0 Then
                            shItemLnq.MASTER_LOGOUTREASONID = p.ID
                            shItemLnq.NAME = p.NAME
                            shItemLnq.PRODUCTIVE = p.PRODUCTIVE
                            shItemLnq.ACTIVE_STATUS = p.ACTIVE_STATUS
                            ret = shItemLnq.UpdateByPK(UserName, shTrans.Trans)
                        End If

                        If ret = False Then
                            shTrans.RollbackTransaction()
                            _err = "SaveShLogoutReason.SaveShLogoutReason :" & shLnq.SHOP_ABB & " ### " & shItemLnq.ErrorMessage
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

