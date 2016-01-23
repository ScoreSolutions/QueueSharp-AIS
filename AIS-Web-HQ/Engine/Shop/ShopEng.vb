Imports System.Data
Imports Cen = CenLinqDB.TABLE
Imports Sh = ShLinqDB.TABLE
Imports pSh = ShParaDB.TABLE
Imports CenLinqDB.Common.Utilities
Imports uSh = ShLinqDB.Common.Utilities

Namespace Shop
    Public Class ShopEng
        Public Function GetShopAppointmentConfig(ByVal ShopID As Long) As pSh.TbAppointmentShParaDB
            Dim lnq As Cen.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(ShopID)
            Dim para As New pSh.TbAppointmentShParaDB
            Dim shTrans As New uSh.TransactionDB
            shTrans.CreateTransaction(lnq.SHOP_DB_SERVER, lnq.SHOP_DB_USERID, lnq.SHOP_DB_PWD, lnq.SHOP_DB_NAME)

            Dim shLnq As New Sh.TbAppointmentShLinqDB
            Dim shPara As New pSh.TbAppointmentShParaDB
            shPara = shLnq.GetParameter(1, shTrans.Trans)

            Return shPara
        End Function
    End Class
End Namespace

