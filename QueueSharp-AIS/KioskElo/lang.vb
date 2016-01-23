Imports Engine.Kiosk.KioskModule

Module lang
    'Public Language As String = "TH"
    'Public msgWarning As String = "ผิดพลาด"
    'Public msgVerifyYourNumber As String = "กรุณาตรวจสอบหมายเลขโทรศัพท์"
    'Public msgWrongNumberFormat As String = "รูปแบบเบอร์โทรศัพท์ไม่ถูกต้อง"
    'Public msgQCancelled As String = "คิวนี้ได้ยกเลิกแล้ว" & vbCrLf & "กรุณากดเลือกทำรายการ" & vbCrLf & "เพื่อรับบริการอีกครั้ง"
    'Public msgWantToBeServedNow As String = "ต้องการรับบริการทันทีหรือไม่?"
    'Public itemname_field As String = "item_name_th"
    'Public msgCancelAppointment As String = "ต้องการยกเลิก" & vbCrLf & "การจองคิวใช่หรือไม่ ?"
    'Public msgCancelAppointmentCompleted1 As String = "ระบบได้ยกเลิกการจอง" & vbCrLf & "หมายเลข "
    'Public msgCancelAppointmentCompleted2 As String = vbCrLf & vbCrLf & "เรียบร้อยแล้ว" & vbCrLf & "ขอบคุณค่ะ"
    'Public msgGetTicketAndWaitForAppointment As String = "กรุณารับบัตรคิว" & vbCrLf & "และรอรับบริการประมาณ"
    'Public msgAppointmentNotSupported As String = "ระบบไม่รองรับประเภทลูกค้านัด" & vbCrLf & "กรุณาติดต่อเจ้าหน้าที่"
    'Public msgMaximum As String = "เลือกได้สูงสุด "
    'Public msgService As String = " บริการ"
    'Public msgHasAppointment As String = "มีนัดรับบริการ"
    'Public msgDateon As String = "วันที่: "
    'Public msgTimeAt As String = " เวลา: "
    'Public msgAppTimeAt As String = " เวลา: "
    'Public msgAtclock As String = " น."
    'Public msgBefore As String = "ก่อน"
    'Public msgCannotAppoinment As String = "ไม่สามารถทำรายการจองได้ "
    'Public msgPleasmakeanew As String = " จะสามารถทำการจองได้อีกครั้ง "
    'Public msgYouChooseTheService As String = "คุณเลือกบริการ"
    'Public msgBookingNotAllowed As String = "ซึ่งระบบไม่ได้เปิดการจองไว้ในวันนี้"
    'Public msgAppointmentAgain As String = " ในวันที่ "
    'Public msgPleaseChooseOneService = "กรุณากดเลือกบริการ"
    'Public msgOnward As String = " onwards"
    'Public msgCannotBBooked As String = "ไม่สามารถทำการจองได้ "
    'Public msgPlsWaitForAbout As String = "ประมาณ "
    'Public msgMinute As String = " นาที"
    'Public msgPlsChkQSetting As String = "กรุณาเช็คการตั้งค่าของหมายเลขคิว "
    'Public msgOfService As String = "ของบริการ "
    'Public msgAvailBookTime As String = "เวลาที่สามารถจองได้คือ "
    'Public msgConfirm As String = "ยืนยัน"
    'Public msgSureToBook As String = "ต้องการทำการจอง ใช่หรือไม่?"
    'Public msgChooseAppTime As String = "กรุณาเลือกเวลาการจอง"
    'Public msgAppTime As String = "ช่วงเวลาที่เลือกรับบริการ" & vbCrLf & "ไม่เพียงพอต่อบริการที่เลือก" & vbCrLf & vbCrLf & "กรุณาเลือกบริการใหม่"
    'Public msgComeB4 As String = "กรุณามาก่อนเวลานัด "
    'Public msgIfLate As String = "หากมาเกินเวลานัด" & vbCrLf & "คิวจะยกเลิกโดยอัติโนมัติ"
    'Public msgBook As String = ""
    'Public msgPleastaketicket As String = "กรุณารับบัตรคิว "
    ''----
    'Public msgYouwillservewith As String = ""
    'Public msgYouwillbeservefirstwith As String = ""
    'Public msgWithinabout As String = ""
    'Public msgAbout As String = ""
    'Public msgNotSufficientTime As String = ""
    'Public msgPleaseSelectAppAgain As String = ""
    'Public msgMaxServiceApp As String = "เลือกได้สูงสุด "
    'Public msgHaveAppointment As String = "มีนัดรับบริการ"
    'Public msgPleasArrive As String = "กรุณามาก่อนเวลานัด "
    'Public msgMinuteBeforeApptime As String = " นาที"
    'Public msgLateArriveThan As String = "Late arrival than the scheduled time"
    'Public msgAutomaticCancelApp As String = "หากมาเกินเวลานัด คิวจะยกเลิกโดยอัตโนมัติ"
    'Public msgMobile As String = ""
    'Public msgDialogBtnOK As String = "ตกลง"

    'Public msgDialogCaptureNotComplete As String = "ไม่สามารถบันทึกภาพได้!"


    Public Sub ChangeLanguage()
        Select Case Language
            Case "EN"
                'frmRegister.pb.BackgroundImage = My.Resources.GB_1366_768
                frmRegister.pb.BackColor = Color.White
                frmRegister.btnOK.BackgroundImage = My.Resources.btnOK_Eng
                frmRegister.btnEdit.BackgroundImage = My.Resources.btnEdit_Eng
                frmRegister.btnMain.BackgroundImage = My.Resources.btnMain_Eng
                'frmRegister.PictureBox1.BackgroundImage = My.Resources.Logo3G_Eng
                frmRegister.PictureBox2.BackgroundImage = My.Resources.txtTitle_Eng
                'frmChooseService.pb.Image = My.Resources.GB_1366_768
                frmChooseService.pb.BackColor = Color.White
                frmChooseService.btnOK.BackgroundImage = My.Resources.btnOK_Eng
                frmChooseService.btnMain.BackgroundImage = My.Resources.Resources.btnBack_Eng
                frmChooseService.btnAppointment.BackgroundImage = My.Resources.Resources.btnAppointment_Eng
                frmChooseService.pbChoseService.BackgroundImage = My.Resources.txtServices_Eng
                frmChooseService.pbServQ.BackgroundImage = My.Resources.txtReserveQ_Eng
                frmChooseService.pbTotalQ.BackgroundImage = My.Resources.txtNoQ_Eng
                frmChooseService.pbWaitingTime.BackgroundImage = My.Resources.txtWaitingTime_Eng
                frmChooseService.pbWaitService.BackgroundImage = My.Resources.txtWaitingQ_Eng
                'frmChooseService.PictureBox6.BackgroundImage = My.Resources.Logo3G_Eng

                'frmAppointment.pb.Image = My.Resources.GB_1366_768
                frmAppointment.pb.BackColor = Color.White
                frmAppointment.btnOK.BackgroundImage = My.Resources.btnOK_Eng
                frmAppointment.btnMain.BackgroundImage = My.Resources.Resources.btmMain_EN
                frmAppointment.btnP.BackgroundImage = My.Resources.Resources.btnBack_EN

                frmRegister.btnAppNew.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
                frmRegister.btnAppOK.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
                frmRegister.btnAppCancel.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
                frmRegister.btnAppBack.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
                frmRegister.btnAppNew.Text = "New Queue"
                frmRegister.btnAppOK.Text = "OK"
                frmRegister.btnAppCancel.Text = "Cancel Appointment"
                frmRegister.btnAppBack.Text = "Main Menu"

                frmKiosCapture.BackgroundImage = My.Resources.Resources.EN_template_capture
                frmKiosCapture.pbReCapture.BackgroundImage = My.Resources.Resources.back_en
                frmKiosCapture.pbSave.BackgroundImage = My.Resources.Resources.ok_en
                msgDialogCaptureNotComplete = "Can't capture image."
                frmKiosCapture.lblImage.Text = "Picture :"
                frmKiosCapture.lblLastDate.Text = "Last Capture Date :"
                frmKiosCapture.lblMobileNo.Text = "Mobile No :"

                frmDialogMsg.btnCancel.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
                frmDialogMsg.btnConfirm.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
                frmDialogMsg.btnOK.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)

                frmDialogMsg.btnCancel.Text = "Cancel"
                frmDialogMsg.btnConfirm.Text = "Confirm"
                frmDialogMsg.btnOK.Text = "OK"

                frmDialogMsg.btnMain.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
                frmDialogMsg.btnPrevious.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)

                frmDialogMsg.btnMain.Text = "Main menu"
                frmDialogMsg.btnPrevious.Text = "Previous"

                '-----------------
                msgNotSufficientTime = "Your selected time is not sufficient " & vbCrLf & "for your requested services "
                msgPleaseSelectAppAgain = " Please selected appointment time again "


                itemname_field = "item_name"
                msgWarning = "Attention"
                msgVerifyYourNumber = "Please Correct The Mobile Number."
                msgWrongNumberFormat = "Invalid Mobile Number"
                msgQCancelled = "This appointment has been cancelled." & vbCrLf & "Please make a new queue request."

                msgWantToBeServedNow = "Take the queue number " & vbCrLf & " to be served now?"
                msgCannotAppoinment = "cannot make an appointment at the moment"
                msgHasAppointment = "has an appointment for:"
                msgPleastaketicket = "Please take your queue ticket"
                msgCancelAppointment = "Are you sure you want" & vbCrLf & "to cancel the appointment?"
                msgCancelAppointmentCompleted1 = "The appointment for" & vbCrLf
                msgCancelAppointmentCompleted2 = vbCrLf & "has been cancelled." & vbCrLf & vbCrLf & "Thank You."
                msgGetTicketAndWaitForAppointment = "Please take your queue ticket" & vbCrLf & "You will be served within" & vbCrLf & "about "
                msgAppointmentNotSupported = "Appointment not Supported" & vbCrLf & "Please Contact Our Staff"
                msgMaximum = "Maximum "
                msgService = " services can be selected"
                msgOnward = " onwards"
                msgAppointmentAgain = " appointment again from "
                msgYouChooseTheService = "You choose the service"
                msgBookingNotAllowed = "Which is not be able to Book Today"
                msgPleaseChooseOneService = "Please select the services"
                msgCannotBBooked = "Cannot be Booked "
                msgPleasmakeanew = "Please make a new "
                msgYouwillservewith = "You will be served within"
                msgYouwillbeservefirstwith = "You will be served first with"
                msgWithinabout = "within about "
                msgAbout = "about "
                msgDateon = " on "
                msgTimeAt = " at "
                msgAppTimeAt = " at: "
                msgPleasArrive = "Please arrive "
                'msgAtclock = " น."
                msgMinute = " minutes"
                msgAvailBookTime = "Our soonest availability for your appointment is "
                msgConfirm = "Confirm"
                msgSureToBook = "Do you want to make the appointment with us?"
                msgChooseAppTime = "Choose Appointment Time"
                msgAppTime = "Your selected time is not sufficient" & vbCrLf & "for your requested services" & vbCrLf & vbCrLf & "Please select your appointment time again"
                msgComeB4 = "Please Do Not Late Over "
                msgIfLate = "If you are late" & vbCrLf & "Your Appointment will be Cancelled"
                msgBook = "Book"
                msgMaxServiceApp = "Max Services Appointment per Time "
                msgHaveAppointment = "You have an appointment for"
                msgMinuteBeforeApptime = " minutes before the appointment time."
                msgLateArriveThan = "Late arrival than the scheduled time"
                msgAutomaticCancelApp = "will automatically cancel the appointment."
                msgDialogBtnOK = "OK"
                '-----------------
            Case "TH"
                'frmRegister.pb.BackgroundImage = My.Resources.GB_1366_768
                frmRegister.pb.BackColor = Color.White
                frmRegister.btnOK.BackgroundImage = My.Resources.Untitled_1
                frmRegister.btnEdit.BackgroundImage = My.Resources._27
                frmRegister.btnMain.BackgroundImage = My.Resources._32
                'frmRegister.PictureBox1.BackgroundImage = My.Resources.Logo3G_Tha
                frmRegister.PictureBox2.BackgroundImage = My.Resources._16

                'frmChooseService.pb.Image = My.Resources.GB_1366_768
                frmChooseService.pb.BackColor = Color.White
                frmChooseService.btnOK.BackgroundImage = My.Resources.Untitled_1
                frmChooseService.btnMain.BackgroundImage = My.Resources.Resources._32
                frmChooseService.btnAppointment.BackgroundImage = My.Resources.Resources.Untitled_3
                frmChooseService.pbChoseService.BackgroundImage = My.Resources._111
                frmChooseService.pbServQ.BackgroundImage = My.Resources._15
                frmChooseService.pbTotalQ.BackgroundImage = My.Resources._131
                frmChooseService.pbWaitingTime.BackgroundImage = My.Resources._141
                frmChooseService.pbWaitService.BackgroundImage = My.Resources._121
                'frmChooseService.PictureBox6.BackgroundImage = My.Resources.Logo3G_Tha

                'frmAppointment.pb.Image = My.Resources.GB_1366_768
                frmAppointment.pb.BackColor = Color.White
                frmAppointment.btnOK.BackgroundImage = My.Resources.Untitled_1
                frmAppointment.btnMain.BackgroundImage = My.Resources.Resources._32
                frmAppointment.btnP.BackgroundImage = My.Resources.Resources.previous_01

                frmRegister.btnAppNew.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                frmRegister.btnAppOK.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                frmRegister.btnAppCancel.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                frmRegister.btnAppBack.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                frmRegister.btnAppNew.Text = "รับคิวใหม่"
                frmRegister.btnAppOK.Text = "ตกลง"
                frmRegister.btnAppCancel.Text = "ยกเลิกจอง"
                frmRegister.btnAppBack.Text = "หน้าหลัก"

                frmDialogMsg.btnCancel.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                frmDialogMsg.btnConfirm.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                frmDialogMsg.btnOK.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Bold)

                frmDialogMsg.btnCancel.Text = "ยกเลิก"
                frmDialogMsg.btnConfirm.Text = "ตกลง"
                frmDialogMsg.btnOK.Text = "ตกลง"

                frmDialogMsg.btnMain.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                frmDialogMsg.btnPrevious.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Bold)

                frmDialogMsg.btnMain.Text = "หน้าหลัก"
                frmDialogMsg.btnPrevious.Text = "ก่อนหน้า"

                frmKiosCapture.BackgroundImage = My.Resources.Resources.TH_template_capture
                frmKiosCapture.pbReCapture.BackgroundImage = My.Resources.Resources.re_capture
                frmKiosCapture.pbSave.BackgroundImage = My.Resources.Resources.ok_en
                msgDialogCaptureNotComplete = "ไม่สามารถบันทึกภาพได้!"
                frmKiosCapture.lblImage.Text = "Picture :"
                frmKiosCapture.lblLastDate.Text = "Last Capture Date :"
                frmKiosCapture.lblMobileNo.Text = "Mobile No :"

                '-----------------
                itemname_field = "item_name_th"
                msgWarning = "ผิดพลาด"
                msgVerifyYourNumber = "กรุณาตรวจสอบหมายเลขโทรศัพท์"
                msgWrongNumberFormat = "รูปแบบเบอร์โทรศัพท์ไม่ถูกต้อง"
                msgQCancelled = "คิวนี้ได้ยกเลิกแล้ว" & vbCrLf & "กรุณากดเลือกทำรายการ" & vbCrLf & "เพื่อรับบริการอีกครั้ง"
                msgWantToBeServedNow = "ต้องการรับบริการทันทีหรือไม่ ?"
                msgCancelAppointment = "ต้องการยกเลิก" & vbCrLf & "การจองคิว ใช่หรือไม่ ?"
                msgCancelAppointmentCompleted1 = "ระบบได้ยกเลิกการจอง" & vbCrLf & "หมายเลข "
                msgCancelAppointmentCompleted2 = vbCrLf & vbCrLf & "เรียบร้อยแล้ว" & vbCrLf & "ขอบคุณค่ะ"
                msgGetTicketAndWaitForAppointment = "กรุณารับบัตรคิว" & vbCrLf & "และรอรับบริการประมาณ "
                msgAppointmentNotSupported = "ระบบไม่รองรับประเภทลูกค้านัด" & vbCrLf & "กรุณาติดต่อเจ้าหน้าที่"
                msgMaximum = "เลือกได้สูงสุด "
                msgService = " บริการ"
                'msgOnward = " onwards"
                msgAppointmentAgain = " ในวันที่ "
                msgYouwillbeservefirstwith = "และรอรับบริการ"
                msgYouChooseTheService = "คุณเลือกบริการ"
                msgBookingNotAllowed = "ซึ่งระบบไม่ได้เปิดการจองไว้ในวันนี้"
                msgCannotAppoinment = "ไม่สามารถทำรายการจองได้ "
                msgHasAppointment = "มีนัดรับบริการ"
                msgPleastaketicket = "กรุณารับบัตรคิว "
                msgPleasmakeanew = " จะสามารถทำการจองได้อีกครั้ง "
                msgPleaseChooseOneService = "กรุณากดเลือกบริการ"
                msgCannotBBooked = "ไม่สามารถทำการจองได้ "
                msgPlsWaitForAbout = "และรอรับบริการประมาณ "
                msgMinute = " นาที"
                msgTimeAt = " เวลา: "
                msgAppTimeAt = " เวลา: "
                msgDateon = "วันที่: "
                msgAtclock = " น."
                msgBefore = "ก่อน"
                msgPleasArrive = "กรุณามาก่อนเวลานัด "
                msgPlsChkQSetting = "กรุณาเช็คการตั้งค่าของหมายเลขคิว !!!"
                msgAvailBookTime = "เวลาที่สามารถจองได้คือ "
                msgSureToBook = "ต้องการทำการจอง ใช่หรือไม่?"
                msgConfirm = "ยืนยัน"
                msgChooseAppTime = "กรุณาเลือกเวลาการจอง"
                msgAppTime = "ช่วงเวลาที่เลือกรับบริการ" & vbCrLf & "ไม่เพียงพอต่อบริการที่เลือก" & vbCrLf & vbCrLf & "กรุณาเลือกบริการใหม่"
                msgComeB4 = "กรุณามาก่อนเวลานัด "
                msgIfLate = "หากมาเกินเวลานัด" & vbCrLf & "คิวจะยกเลิกโดยอัติโนมัติ"
                msgBook = ""
                msgMaxServiceApp = "เลือกได้สูงสุด "
                msgHaveAppointment = "มีนัดรับบริการ"
                msgMinuteBeforeApptime = " นาที"
                msgAutomaticCancelApp = "หากมาเกินเวลานัด คิวจะยกเลิกโดยอัตโนมัติ"
                msgDialogBtnOK = "ตกลง"
                '-----------------
        End Select
    End Sub
End Module

