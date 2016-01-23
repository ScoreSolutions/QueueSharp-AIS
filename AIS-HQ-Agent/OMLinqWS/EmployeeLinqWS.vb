Imports CenParaDB.OMWS
Public Class EmployeeLinqWS
    Public Function GetEmployeeProfileByUsername(ByVal ProfileUserName As String, ByVal WsURL As String, ByVal WsUserName As String, ByVal WsPassword As String, ByVal WsDomain As String) As GetEmployeeProfileByUsernamePara
        Dim ret As New GetEmployeeProfileByUsernamePara
        Dim ds As New DataSet
        Dim ws As New OMServices.WS_OM_OMEHRServices
        ws.Url = WsURL
        ws.Credentials = New System.Net.NetworkCredential(WsUserName, WsPassword, WsDomain)
        ds = ws.GetEmployeeProfileByUsername(ProfileUserName)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim dt As DataTable = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                If Convert.IsDBNull(dr("PIN")) = False Then ret.PIN = dr("PIN").ToString
                If Convert.IsDBNull(dr("USERID")) = False Then ret.USERID = dr("USERID").ToString
                If Convert.IsDBNull(dr("THPREFIX")) = False Then ret.TH_PREFIX = dr("THPREFIX").ToString
                If Convert.IsDBNull(dr("THFIRSTNAME")) = False Then ret.TH_FIRST_NAME = dr("THFIRSTNAME").ToString
                If Convert.IsDBNull(dr("THLASTNAME")) = False Then ret.TH_LAST_NAME = dr("THLASTNAME").ToString
                If Convert.IsDBNull(dr("ENPREFIX")) = False Then ret.EN_PREFIX = dr("ENPREFIX").ToString
                If Convert.IsDBNull(dr("ENFIRSTNAME")) = False Then ret.EN_FIRST_NAME = dr("ENFIRSTNAME").ToString
                If Convert.IsDBNull(dr("ENLASTNAME")) = False Then ret.EN_LAST_NAME = dr("ENLASTNAME").ToString
                If Convert.IsDBNull(dr("EMAIL")) = False Then ret.EMAIL = dr("EMAIL").ToString
                If Convert.IsDBNull(dr("EMPLOYEETYPE")) = False Then ret.EMPLOYEE_TYPE = dr("EMPLOYEETYPE").ToString
                If Convert.IsDBNull(dr("EMPLOYEEGROUP")) = False Then ret.EMPLOYEE_GROUP = dr("EMPLOYEEGROUP").ToString
                If Convert.IsDBNull(dr("POSITIONID")) = False Then ret.POSITION_ID = dr("POSITIONID").ToString
                If Convert.IsDBNull(dr("POSITIONDESC")) = False Then ret.POSITION_DESC = dr("POSITIONDESC").ToString
                If Convert.IsDBNull(dr("TELNO")) = False Then ret.TEL_NO = dr("TELNO").ToString
                If Convert.IsDBNull(dr("ORGCODE")) = False Then ret.ORG_CODE = dr("ORGCODE").ToString
                If Convert.IsDBNull(dr("ORGNAME")) = False Then ret.ORG_NAME = dr("ORGNAME").ToString
                If Convert.IsDBNull(dr("ORGDESC")) = False Then ret.ORG_DESC = dr("ORGDESC").ToString
                If Convert.IsDBNull(dr("COMPANYCODE")) = False Then ret.COMPANY_CODE = dr("COMPANYCODE").ToString
                If Convert.IsDBNull(dr("CONAME")) = False Then ret.CONAME = dr("CONAME").ToString
                If Convert.IsDBNull(dr("NICKNAME")) = False Then ret.NICKNAME = dr("NICKNAME").ToString
                If Convert.IsDBNull(dr("DPCODE")) = False Then ret.DPCODE = dr("DPCODE").ToString
                If Convert.IsDBNull(dr("DPNAME")) = False Then ret.DPNAME = dr("DPNAME").ToString
                If Convert.IsDBNull(dr("DPDESC")) = False Then ret.DPDESC = dr("DPDESC").ToString
                If Convert.IsDBNull(dr("SCCODE")) = False Then ret.SCCODE = dr("SCCODE").ToString
                If Convert.IsDBNull(dr("SCNAME")) = False Then ret.SCNAME = dr("SCNAME").ToString
                If Convert.IsDBNull(dr("SCDESC")) = False Then ret.SCDESC = dr("SCDESC").ToString
                If Convert.IsDBNull(dr("MOBILENO")) = False Then ret.MOBILE_NO = dr("MOBILENO").ToString
            End If
            dt = Nothing
        End If
        ds = Nothing
        ws = Nothing

        Return ret
    End Function
End Class
