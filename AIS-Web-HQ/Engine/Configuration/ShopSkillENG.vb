Imports System.Web.UI.WebControls
Imports ShLinqDB.TABLE
Imports ShParaDB.TABLE

Namespace Configuration
    Public Class ShopSkillENG
        Dim _err As String = ""
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err.Trim
            End Get
        End Property

        Public Function SaveShopSkill(ByVal p As CenParaDB.TABLE.TbSkillCenParaDB, ByVal UserName As String, ByVal ShopID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Long
            Dim ret As Long = 0

            Dim shLnq As CenLinqDB.TABLE.TbShopCenLinqDB = Engine.Common.FunctionEng.GetTbShopCenLinqDB(ShopID)
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New TbSkillShLinqDB
                lnq.ChkDataByWhere("master_skillid='" & p.ID & "'", shTrans.Trans)
                If lnq.ID = 0 Then
                    lnq.ChkDataByWhere("skill='" & p.SKILL & "'", shTrans.Trans)
                End If

                lnq.MASTER_SKILLID = p.ID
                lnq.SKILL = p.SKILL
                lnq.APPOINTMENT = p.APPOINTMENT
                lnq.ACTIVE_STATUS = p.ACTIVE_STATUS

                Dim re As Boolean = False
                If lnq.ID <> 0 Then
                    re = lnq.UpdateByPK(UserName, shTrans.Trans)
                Else
                    re = lnq.InsertData(UserName, shTrans.Trans)
                End If
                If re = False Then
                    _err = "ShopSkillENG.SaveShopSkill :" & shLnq.SHOP_ABB & " ### " & lnq.ErrorMessage
                Else
                    ret = lnq.ID
                End If
                lnq = Nothing
            Else
                _err = shLnq.SHOP_ABB & " ### " & shTrans.ErrorMessage
                ret = False
            End If

            Return ret
        End Function

        Public Function SaveShopSkillItem(ByVal SkillID As Long, ByVal p As TbSkillItemShParaDB, ByVal UserName As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As Boolean
            Dim ret As Boolean = False
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New TbSkillItemShLinqDB
                lnq.SKILL_ID = p.SKILL_ID
                lnq.ITEM_ID = p.ITEM_ID

                ret = lnq.InsertData(UserName, shTrans.Trans)
                If ret = False Then
                    _err = lnq.ErrorMessage
                End If
            End If

            Return ret
        End Function

        Public Function GetShopSkillPara(ByVal ShopID As Long, ByVal SkillID As Integer) As TbSkillShParaDB
            Dim p As New TbSkillShParaDB
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(ShopID, "ShopSkillENG.GetShopSkillPara")
            If shTrans.Trans IsNot Nothing Then
                Dim lnq As New TbSkillShLinqDB
                p = lnq.GetParameter(SkillID, shTrans.Trans)
                shTrans.CommitTransaction()
                lnq = Nothing
            End If

            Return p
        End Function

        Public Function GetSkillItem(ByVal ShopID As Long, ByVal SkillID As Long) As DataTable
            _err = ""
            Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(ShopID, "ShopSkillENG.GetSkillItem")
            Dim lnq As New CenLinqDB.TABLE.TbShopCenLinqDB
            Dim sql As String = "select s.id,s.skill_id,s.item_id,t.item_name, t.item_code,t.master_itemid"
            sql += " from TB_ITEM t "
            sql += " left join TB_SKILL_ITEM s on t.id=s.item_id"
            sql += " where skill_id='" & SkillID & "' or t.id is null "
            Dim dt As New DataTable
            dt = lnq.GetListBySql(sql, shTrans.Trans)
            shTrans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        Public Sub New()
            _err = ""
        End Sub
    End Class
End Namespace

