Imports Engine.Utilities
Imports System.Text

Partial Public Class ImportCustomerProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request("FileName") IsNot Nothing Then
            Dim ret As New ImportCustomerResponsePara
            ret = SetFlagToStartDaily(Request("FileName"), Request("FileSize"))
            If ret.RESPONSE = True Then
                Response.Write("SUCCESS")
            Else
                Response.Write(ret.ErrorMessage)
                FunctionENG.SaveErrorLog("ImportCustomerProfile.Page_Load", ret.ErrorMessage)
            End If
        End If
    End Sub

    Private Function SetFlagToStartDaily(ByVal fleName As String, ByVal fleSize As String) As ImportCustomerResponsePara
        Dim res As New ImportCustomerResponsePara
        Dim trans As New TransactionDB(True)

        Dim fleType As String = ""
        If fleName.IndexOf("CAMP") > -1 Then
            fleType = "3"
        ElseIf fleName.IndexOf("CUSINFO_REPLACE") > -1 Then
            fleType = "2"
        Else
            fleType = "1"
        End If

        If CheckDupFileName(fleName, trans) = False Then
            Dim NewID As Long = SqlDB.GetNextID("id", "TB_LOG_CUSTOMER_DAILY", trans.Trans)
            Dim sql As String = "insert into TB_LOG_CUSTOMER_DAILY (id, create_by, create_date, file_size, file_name, file_type,  import_msg)"
            sql += " values(" & NewID & ", 'ImportWS',getdate(),'" & fleSize & "', '" & fleName & "','" & fleType & "','Waiting')"
            If SqlDB.ExecuteNonQuery(sql, trans.Trans) > 0 Then
                trans.CommitTransaction()
                res.RESPONSE = True
            Else
                trans.RollbackTransaction()
                res.RESPONSE = False
                res.ErrorMessage = SqlDB.ErrorMessage
                FunctionENG.SaveErrorLog("ImportCustomerProfile.SetFlagToStartDaily", SqlDB.ErrorMessage)
            End If
        Else
            res.RESPONSE = False
            res.ErrorMessage = "Duplicate File Name"
        End If
        Return res
    End Function

    Private Function CheckDupFileName(ByVal fleName As String, ByVal trans As TransactionDB) As Boolean

        If trans.Trans IsNot Nothing Then
            Dim sql As String = "select top 1 id from TB_LOG_CUSTOMER_DAILY where file_name = '" & fleName & "'"
            Dim dt As DataTable = SqlDB.ExecuteTable(sql, trans.Trans)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function


    Private Function NewImportCustomerProfile(ByVal Request As HttpRequest) As ImportCustomerResponsePara
        Dim res As New ImportCustomerResponsePara

    End Function
    Private Function ImportCustomerProfile(ByVal Request As HttpRequest) As ImportCustomerResponsePara
        Dim res As New ImportCustomerResponsePara
        Dim MobileNo As String = request("mNo")
        Dim TitleName As String = request("TiN")
        Dim FirstName As String = request("FN")
        Dim LastName As String = request("LN")
        Dim EMail As String = request("Mail")
        Dim PreferLang As String = Request("Lang")
        Dim Segment As String = request("SG")
        Dim BirthDate As String = request("BD")
        Dim Status As String = request("STA")
        Dim Category As String = request("CAT")
        Dim AccountBalance As String = request("AR")
        Dim ContactClass As String = request("Cont")
        Dim ServiceYear As String = request("SY")
        Dim ChumScore As String = request("SC")
        Dim NetworkType As String = request("NT")
        'Dim CamCode As String = request("CC")
        'Dim CamName As String = request("CN")
        'Dim CamNameEng As String = request("CNE")
        'Dim CamDesc As String = request("CD")

        Dim ret As Boolean = False
        Dim trans As New TransactionDB
        Dim lnq As New TbCustomerCenLinqDB
        lnq.MOBILE_NO = MobileNo
        lnq.TITLE_NAME = TitleName
        lnq.F_NAME = FirstName
        lnq.L_NAME = LastName
        lnq.EMAIL = EMail
        lnq.PREFER_LANG = PreferLang
        lnq.SEGMENT_LEVEL = Segment
        lnq.BIRTH_DATE = BirthDate
        lnq.MOBILE_STATUS = Status
        lnq.CATEGORY = Category
        lnq.ACCOUNT_BALANCE = AccountBalance
        lnq.CONTACT_CLASS = ContactClass
        lnq.SERVICE_YEAR = ServiceYear
        lnq.CHUM_SCORE = ChumScore
        lnq.NETWORK_TYPE = NetworkType
        ret = lnq.InsertData("DailyInsert", trans.Trans)

        If ret = True Then
            trans.CommitTransaction()
            res.RESPONSE = True
        Else
            trans.RollbackTransaction()
            res.RESPONSE = False
            res.ErrorMessage = lnq.ErrorMessage
        End If
        Return res
    End Function

    Private Function UpdateCampaign(ByVal Request As HttpRequest) As ImportCustomerResponsePara
        Dim res As New ImportCustomerResponsePara
        Dim MobileNo As String = Request("mNo")
        Dim CamCode As String = Request("CmCode")
        Dim CamName As String = Request("CmName")
        Dim CamNameEN As String = Request("CmNameEn")
        Dim CamDesc As String = Request("CmDesc")

        Dim uSql As String = "UPDATE TB_CUSTOMER SET "
        uSql += " UPDATE_BY = 'DailyUpdate', UPDATE_DATE = getdate(), "
        uSql += " CAMPAIGN_CODE = '" & CamCode & "', CAMPAIGN_NAME = '" & CamName & "', "
        uSql += " CAMPAIGN_NAME_EN = '" & CamNameEN & "', CAMPAIGN_DESC = '" & CamDesc & "' "
        uSql += " Where MOBILE_NO = '" & MobileNo & "' "

        Dim ret As Boolean = False
        Dim trans As New TransactionDB
        Dim lnq As New TbCustomerCenLinqDB
        ret = lnq.UpdateBySql(uSql, trans.Trans)

        If ret = True Then
            trans.CommitTransaction()
            res.RESPONSE = True
        Else
            trans.RollbackTransaction()
            res.RESPONSE = False
            res.ErrorMessage = lnq.ErrorMessage
        End If
        Return res
    End Function
    

    
End Class
