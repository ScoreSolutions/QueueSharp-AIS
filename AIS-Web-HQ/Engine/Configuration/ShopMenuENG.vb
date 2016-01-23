Namespace Configuration
    Public Class ShopMenuENG
        Dim _err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property

        Public Function SaveGroupUser(ByVal p As ShParaDB.TABLE.TbGroupuserShParaDB, ByVal dt As DataTable, ByVal uPara As CenParaDB.Common.UserProfilePara, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Long
            Dim ret As Long = 0

            Dim lnq As New ShLinqDB.TABLE.TbGroupuserShLinqDB
            If p.ID <> 0 Then
                lnq.GetDataByPK(p.ID, shTrans.Trans)
            End If

            lnq.GROUP_CODE = p.GROUP_CODE
            lnq.GROUP_NAME = p.GROUP_NAME
            lnq.ACTIVE_STATUS = p.ACTIVE_STATUS

            If lnq.ID <> 0 Then
                lnq.UpdateByPK(uPara.USER_PARA.ID, shTrans.Trans)
            Else
                lnq.InsertData(uPara.USER_PARA.ID, shTrans.Trans)
            End If

            ret = lnq.ID
            If ret > 0 Then
                Dim sql As String = "delete from TB_GROUPUSER_MENU where group_id='" & lnq.ID & "'"
                ShLinqDB.Common.Utilities.SqlDB.ExecuteNonQuery(sql, shTrans.Trans)

                For Each dr As DataRow In dt.Rows
                    Dim mLnq As New ShLinqDB.TABLE.TbGroupuserMenuShLinqDB
                    mLnq.GROUP_ID = lnq.ID
                    mLnq.MENU_ID = Convert.ToInt32(dr("MenuID"))

                    If mLnq.InsertData(uPara.USER_PARA.ID, shTrans.Trans) = False Then
                        ret = 0
                        _err = mLnq.ErrorMessage
                        Exit For
                    End If
                Next
            End If

            Return ret
        End Function
    End Class
End Namespace

