Imports ShLinqDB.TABLE
Imports ShParaDB.TABLE
Imports Engine.Common
Namespace Configuration
    Public Class ShopCounterENG
        Dim _Err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _Err
            End Get
        End Property

        Public Function GetCustomerType(ByVal ShopID As Long) As DataTable
            Dim dt As New DataTable
            Dim lnq As New TbCustomertypeShLinqDB
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB
            shTrans = FunctionEng.GetShTransction(ShopID, "ShopCounterENG.GetCustomerType")
            If shTrans.Trans IsNot Nothing Then
                dt = lnq.GetDataList("active_status = 1", "customertype_name", shTrans.Trans)
                lnq = Nothing
            End If
            
            Return dt
        End Function

        Public Function SaveShopCounter(ByVal p As TbCounterShParaDB, ByVal LoginName As String, ByVal ShopID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Long
            Dim ret As Long = 0
            Dim lnq As New TbCounterShLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, shTrans.Trans)
            End If
            lnq.COUNTER_CODE = p.COUNTER_CODE
            lnq.COUNTER_NAME = p.COUNTER_NAME
            lnq.COUNTER_STATUS = p.COUNTER_STATUS
            lnq.QUICKSERVICE = p.QUICKSERVICE
            lnq.BEEP = p.BEEP
            lnq.RETURN_CASE = p.RETURN_CASE
            lnq.AUTO_SWAP = p.AUTO_SWAP
            lnq.SPEED_LANE = p.SPEED_LANE
            lnq.UNITDISPLAY = p.UNITDISPLAY
            lnq.AVAILABLE = p.AVAILABLE
            lnq.BACK_OFFICE = p.BACK_OFFICE
            lnq.COUNTER_MANAGER = p.COUNTER_MANAGER
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS
            lnq.COUNTER_MANAGER = p.COUNTER_MANAGER

            Dim r As Boolean = False
            If lnq.ID <> 0 Then
                r = lnq.UpdateByPK(LoginName, shTrans.Trans)
            Else
                r = lnq.InsertData(LoginName, shTrans.Trans)
            End If

            If r = True Then
                ret = lnq.ID
            Else
                _Err = "ShopCounterENG.SaveShopCounter" & lnq.ErrorMessage
            End If

            Return ret
        End Function

        Public Function SaveCounterCustomerType(ByVal p As TbCounterCustomertypeShParaDB, ByVal LoginName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New TbCounterCustomertypeShLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, shTrans.Trans)
            End If
            lnq.COUNTER_ID = p.COUNTER_ID
            lnq.CUSTOMERTYPE_ID = p.CUSTOMERTYPE_ID

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(LoginName, shTrans.Trans)
            Else
                ret = lnq.InsertData(LoginName, shTrans.Trans)
            End If

            If ret = False Then
                _Err = lnq.ErrorMessage
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Function SaveCounterCustomerTypeAllow(ByVal p As TbCounterCustomertypeAllowShParaDB, ByVal LoginName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New TbCounterCustomertypeAllowShLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, shTrans.Trans)
            End If
            lnq.COUNTER_ID = p.COUNTER_ID
            lnq.CUSTOMERTYPE_ID = p.CUSTOMERTYPE_ID

            If lnq.ID <> 0 Then
                ret = lnq.UpdateByPK(LoginName, shTrans.Trans)
            Else
                ret = lnq.InsertData(LoginName, shTrans.Trans)
            End If

            If ret = False Then
                _Err = lnq.ErrorMessage
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Sub DeleteCounterCustomerType(ByVal CounterID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB)
            Dim sql As String = "delete from TB_COUNTER_CUSTOMERTYPE where counter_id='" & CounterID & "'"
            ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, shTrans.Trans)
        End Sub

        Public Sub DeleteCounterCustomerTypeAllow(ByVal CounterID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB)
            Dim sql As String = "delete from TB_COUNTER_CUSTOMERTYPE_ALLOW where counter_id='" & CounterID & "'"
            ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, shTrans.Trans)
        End Sub

        Public Function CheckDupByCounterCode(ByVal CounterCode As String, ByVal CounterID As Long, ByVal ShopID As Long) As Boolean
            Dim ret As Boolean = False
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(ShopID, "ShopCounterENG.CheckDupByCounterCode")
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New TbCounterShLinqDB
                ret = lnq.ChkDataByWhere("counter_code = '" & CounterCode & "' and id<>'" & CounterID & "'", shTrans.Trans)
                lnq = Nothing
            End If
            Return ret
        End Function

        Public Function CheckDupByCounterName(ByVal CounterName As String, ByVal CounterID As Long, ByVal ShopID As Long) As Boolean
            Dim ret As Boolean = False
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(ShopID, "ShopCounterENG.CheckDupbyCounterName")
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New TbCounterShLinqDB
                ret = lnq.ChkDataByWhere("counter_name = '" & CounterName & "' and id<>'" & CounterID & "'", shTrans.Trans)
                lnq = Nothing
            End If
            
            Return ret
        End Function

        Public Function CheckDupByCounterUnitDisplayID(ByVal UnitDisplayID As String, ByVal CounterID As Long, ByVal ShopID As Long) As Boolean
            Dim ret As Boolean = False
            Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
            shTrans = Engine.Common.FunctionEng.GetShTransction(ShopID, "ShopCounterENG.CheckDupByCounterUnitDisplayID")
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New TbCounterShLinqDB
                ret = lnq.ChkDataByWhere("unitdisplay = '" & UnitDisplayID & "' and id<>'" & CounterID & "'", shTrans.Trans)
                lnq = Nothing
            End If

            Return ret
        End Function

        Public Function GetShopCounterPara(ByVal CounterID As Long, ByVal ShopID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As TbCounterShParaDB
            Dim ret As New ShParaDB.TABLE.TbCounterShParaDB
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New ShLinqDB.TABLE.TbCounterShLinqDB
                ret = lnq.GetParameter(CounterID, shTrans.Trans)
                lnq = Nothing
            End If

            Return ret
        End Function

        Public Function GetShopCounterCustomerType(ByVal CounterID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim cDt As New DataTable
            Dim lnq As New ShLinqDB.TABLE.TbCounterCustomertypeShLinqDB
            cDt = lnq.GetDataList("counter_id='" & CounterID & "'", "", shTrans.Trans)
            lnq = Nothing
            Return cDt
        End Function

        Public Function GetShopCounterCustomerTypeAllow(ByVal CounterID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim cDt As New DataTable
            Dim lnq As New ShLinqDB.TABLE.TbCounterCustomertypeAllowShLinqDB
            cDt = lnq.GetDataList("counter_id='" & CounterID & "'", "", shTrans.Trans)
            lnq = Nothing
            Return cDt
        End Function

        Public Sub New()
            _Err = ""
        End Sub
    End Class
End Namespace