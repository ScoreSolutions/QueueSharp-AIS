Namespace Kiosk
    Public Class KioskAppointmentENG : Inherits KioskBaseENG
        Function InsertAppointment(ByVal Capacity As Integer, ByVal ChooseService As String, ByVal Time As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As Boolean
            Dim sql As String = ""
            Dim item As String() = ChooseService.Split(",")
            For i As Int32 = 0 To item.Length - 1
                Dim Id As Int32 = FindID("TB_APPOINTMENT_CUSTOMER", INIFileName, SoftwareName, FunctionName)
                sql = "INSERT INTO TB_APPOINTMENT_CUSTOMER (id, app_date,capacity,item_id, customer_id,start_slot,active_status,create_by,create_date) "
                sql += " VALUES(" & Id & ", getdate()," & Capacity & ", " & item(i) & ",'" & Mobile & "', '" & FixDate(FindDateNow(INIFileName, SoftwareName, FunctionName)) & " " & Time & "',1,0,getdate())"
                executeSQL(sql, INIFileName, SoftwareName, FunctionName)
            Next
            Return True
        End Function

        Public Sub UpdateUseAppointmentSlot(ByVal InUse As Integer, ByVal ItemID As Integer, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String)
            Dim Sql As String = "update TB_APPOINTMENT_SLOT set in_use = " & InUse & " where id = " & ItemID
            executeSQL(Sql, INIFileName, SoftwareName, FunctionName)
        End Sub

        Public Function GetAppointmentSlotInUse(ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String, ByVal StartSlotTime As String, ByVal Slot As Integer, ByVal CountService As Integer) As DataTable
            Dim Sql As String = "select id,in_use "
            Sql += " from TB_APPOINTMENT_SLOT "
            Sql += " where DATEDIFF(d,app_date,GETDATE()) = 0 and in_use < capacity "
            Sql += " and CONVERT(varchar(5),slot_time,114) between '" & StartSlotTime & "' and '" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, CDate(StartSlotTime)).Hour.ToString.PadLeft(2, "0") & ":" & DateAdd(DateInterval.Minute, (CountService - 1) * Slot, CDate(StartSlotTime)).Minute.ToString.PadLeft(2, "0") & "'"
            Dim dt As New DataTable
            dt = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function

        Public Function GetSelectedItem(ByVal ChooseService As String, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim dt_temp As New DataTable
            Dim Sql As String = "select item_name,item_name_th,item_wait,item_time,item_order from tb_item where id in (" & ChooseService & ") order by item_wait,item_time,item_order"
            dt_temp = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt_temp
        End Function

        Public Function GetTimeSlot(ByVal k_before_close As Integer, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim Sql As String = "declare @EndSlot as datetime; "
            Sql += " select @EndSlot = (select max(slot_time) from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,GETDATE()) = 0);"
            Sql += " select top 1 start_time,  case when @EndSlot < dateadd(minute,(" & k_before_close & ") * -1 ,end_slot) then @EndSlot else dateadd(minute,(" & k_before_close & ") * -1 ,end_slot) end as end_time ,slot "
            Sql += " from TB_APPOINTMENT_SCHEDULE where DATEDIFF(D,GETDATE(),app_date)=0"
            Dim dt As New DataTable
            dt = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dt
        End Function

        Public Function GetSlotStatus(ByVal k_before_app As Integer, ByVal INIFileName As String, ByVal SoftwareName As String, ByVal FunctionName As String) As DataTable
            Dim dtT As New DataTable
            Dim Sql As String = "select convert(varchar(5),slot_time,114) as time,case when in_use = capacity then 1 else 0 end as status from TB_APPOINTMENT_SLOT where DATEDIFF(d,app_date,GETDATE()) = 0 and slot_time > DATEADD(MINUTE," & k_before_app & ",GETDATE())"
            dtT = getDataTable(Sql, INIFileName, SoftwareName, FunctionName)
            Return dtT
        End Function
    End Class
End Namespace

