using System;
using System.Collections.Generic;
using System.Text;

namespace GenerateC.Utilities
{
    public class MessageResources
    {
        public static string CultureName
        {
            get
            {
                try
                {
                    if (System.Web.HttpContext.Current.Session[Constant.CultureSessionID] == null) System.Web.HttpContext.Current.Session[Constant.CultureSessionID] = Constant.CultureName.Default;
                    return System.Web.HttpContext.Current.Session[Constant.CultureSessionID].ToString();
                }
                catch
                {
                    return Constant.CultureName.Default;
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session[Constant.CultureSessionID] = value;
            }
        }

        public static string GetCultureText(string EnglishName, string ThaiName)
        {
            return (CultureName == Constant.CultureName.English ? EnglishName : ThaiName);
        }

        #region Message

        private static string GenerateMessage(string code, string message)
        {
            //return "MSG" + code + (message == "" ? "" : " :: ") + message;
            return message;
        }

        #region Error

        #region Critical Error
        /// <summary>Cannot not retrieve a System.Configuration.ConnectionStringSettingsCollection object.</summary>
        public static string MSGEC001
        {
            get { return GenerateMessage("EC001", GetCultureText("Cannot not retrieve a System.Configuration.ConnectionStringSettingsCollection object.", "ไม่พบค่าการเชื่อมต่อฐานข้อมูลใน Configuration")); }
        }
        /// <summary>The connection does not exist.-or- The connection is not open.</summary>
        public static string MSGEC002
        {
            get { return GenerateMessage("EC002", GetCultureText("The connection does not exist.-or- The connection is not open.", "ไม่สามารถเชื่อมต่อฐานข้อมูลได้")); }
        }
        /// <summary>Cannot set the connection used by the instance of the command.</summary>
        public static string MSGEC003
        {
            get { return GenerateMessage("EC003", GetCultureText("Cannot set the connection used by the instance of the command.", "ไม่สามารถกำหนดการเชื่อมต่อสำหรับคำสั่งทำรายการได้")); }
        }
        /// <summary>The value was not a valid System.Data.CommandType.</summary>
        public static string MSGEC004
        {
            get { return GenerateMessage("EC004", GetCultureText("The value was not a valid System.Data.CommandType.", "ค่าของ System.Data.CommandType ไม่ถูกต้อง")); }
        }
        /// <summary>The value of parameter is null.</summary>
        public static string MSGEC005
        {
            get { return GenerateMessage("EC005", GetCultureText("The value of parameter is null.", "ไม่ได้กำหนดพารามิเตอร์")); }
        }
        /// <summary>The parameter specified is already added to this or another parameter collection.</summary>
        public static string MSGEC006
        {
            get { return GenerateMessage("EC006", GetCultureText("The parameter specified is already added to this or another parameter collection.", "พารามิเตอร์ที่ระบุซ้ำกับที่มีอยู่")); }
        }
        /// <summary>No column with the {0} name was found in {1}.</summary>
        public static string MSGEC007
        {
            get { return GenerateMessage("EC007", GetCultureText("No column with the {0} name was found in {1}.", "ไม่พบคอลัมน์ {0} ในตาราง {1}")); }
        }
        /// <summary>The transaction has already been committed or rolled back.-or- The connection is broken.</summary>
        public static string MSGEC008
        {
            get { return GenerateMessage("EC008", GetCultureText("The transaction has already been committed or rolled back.-or- The connection is broken.", "มีการยืนยันหรือยกเลิกข้อมูลระหว่างทำรายการ หรือ การเชื่อมต่อฐานข้อมูลขัดข้อง")); }
        }
        /// <summary>Parallel transactions are not supported.</summary>
        public static string MSGEC009
        {
            get { return GenerateMessage("EC009", GetCultureText("Parallel transactions are not supported.", "ไม่สนับสนุนการทำรายการคู่ขนาน")); }
        }
        /// <summary>Error when execute sql statement.</summary>
        public static string MSGEC010
        {
            get { return GenerateMessage("EC010", GetCultureText("Error when execute sql statement.", "เกิดความผิดพลาดขณะรันคำสั่ง sql")); }
        }
        /// <summary>Error when build DataReader object.</summary>
        public static string MSGEC011
        {
            get { return GenerateMessage("EC011", GetCultureText("Error when build DataReader object.", "เกิดความผิดพลาดขณะสร้าง DataReader")); }
        }
        /// <summary>Error when build DataTable object.</summary>
        public static string MSGEC012
        {
            get { return GenerateMessage("EC012", GetCultureText("Error when build DataTable object.", "เกิดความผิดพลาดขณะสร้าง DataTable")); }
        }
        /// <summary>Error in ExecuteScalar method.</summary>
        public static string MSGEC013
        {
            get { return GenerateMessage("EC013", GetCultureText("Error in ExecuteScalar method.", "เกิดความผิดพลาดขณะรันคำสั่ง ExecuteScalar")); }
        }
        /// <summary>{0} does not exist in database.</summary>
        public static string MSGEC014
        {
            get { return GenerateMessage("EC014", GetCultureText("{0} does not exist in database.", "ไม่พบตารางหรือวิว {0} ในฐานข้อมูล")); }
        }
        /// <summary>Cannot download the resource as aSystem.Uri.</summary>
        public static string MSGEC015
        {
            get { return GenerateMessage("EC015", GetCultureText("Cannot download the resource as a System.Uri.", "ไม่สามารถดาวน์โหลดข้อมูลจากปลายทางได้")); }
        }

        /// <summary>There are errors occur while insert data.</summary>
        public static string MSGEC101
        {
            get { return GenerateMessage("EC101", GetCultureText("There are errors occur while insert data.", "เกิดความผิดพลาดขณะเพิ่มข้อมูลใหม่")); }
        }
        /// <summary>There are errors occur while update data.</summary>
        public static string MSGEC102
        {
            get { return GenerateMessage("EC102", GetCultureText("There are errors occur while update data.", "เกิดความผิดพลาดขณะบันทึกแก้ไขข้อมูล")); }
        }
        /// <summary>There are errors occur while delete data.</summary>
        public static string MSGEC103
        {
            get { return GenerateMessage("EC103", GetCultureText("There are errors occur while delete data.", "เกิดความผิดพลาดขณะลบข้อมูล")); }
        }
        /// <summary>There are errors occur while select data.</summary>
        public static string MSGEC104
        {
            get { return GenerateMessage("EC104", GetCultureText("There are errors occur while select data.", "เกิดความผิดพลาดขณะเรียกดูข้อมูล")); }
        }
        /// <summary>There are errors occur while importing excel data.</summary>
        public static string MSGEC105
        {
            get { return GenerateMessage("EC105", GetCultureText("There are errors occur while importing excel data.", "เกิดความผิดพลาดขณะนำเข้าข้อมูล จาก Excel")); }
        }
        /// <summary>There are errors occur while initial process.</summary>
        public static string MSGEC106
        {
            get { return GenerateMessage("EC106", GetCultureText("There are errors occur while initial process.", "เกิดความผิดพลาดในการกำหนดค่าเริ่มต้นเพื่อเข้าสู่กระบวนการ")); }
        }
        /// <summary>There are errors occur while loging in.</summary>
        public static string MSGES001
        {
            get { return GenerateMessage("ES001", GetCultureText("There are errors occur while loging in", "เกิดความผิดพลาดขณะเข้าสู่ระบบ")); }
        }
        /// <summary>There are errors occur while changing password.</summary>
        public static string MSGES002
        {
            get { return GenerateMessage("ES002", GetCultureText("There are errors occur while changing password", "เกิดความผิดพลาดขณะเปลี่ยนรหัสผ่าน")); }
        }

        /// <summary>Database error occur {0} -> {1}</summary>
        public static string MSGEC901
        {
            get { return GenerateMessage("EC901", GetCultureText("Database error occur {0} -> {1}", "เกิดความผิดพลาดในการติดต่อฐานข้อมูล {0} -> {1}")); }
        }
        /// <summary>Undefined error occur {0}</summary>
        public static string MSGEC902
        {
            get { return GenerateMessage("EC902", GetCultureText("Undefined error occur {0}", "เกิดความผิดพลาดขณะทำรายการ {0}")); }
        }
        #endregion

        #region Business Error
        /// <summary>Your user name is incorrect. Please try again.</summary>
        public static string MSGEA001
        {
            get { return GenerateMessage("EA001", GetCultureText("Your user name is incorrect. Please try again.", "ชื่อเข้าระบบไม่ถูกต้อง กรุณาลองอีกครั้ง")); }
        }
        /// <summary>Your password is incorrect. Please try again.</summary>
        public static string MSGEA002
        {
            get { return GenerateMessage("EA002", GetCultureText("Your password is incorrect. Please try again.", "รหัสผ่านไม่ถูกต้อง กรุณาลองอีกครั้ง")); }
        }
        /// <summary>You are not allowed entering to this system. Please contact your administrator.</summary>
        public static string MSGEA003
        {
            get { return GenerateMessage("EA003", GetCultureText("You are not allowed entering to this system. Please contact your administrator.", "คุณไม่มีสิทธิเข้าใช้ระบบนี้ กรุณาติดต่อเจ้าหน้าที่ผู้ดูแลระบบ")); }
        }
        /// <summary>You are not allowed temporarily. Please contact your administrator.</summary>
        public static string MSGEA004
        {
            get { return GenerateMessage("EA004", GetCultureText("You are not allowed temporarily. Please contact your administrator.", "คุณไม่มีสิทธิเข้าใช้ระบบในขณะนี้ กรุณาติดต่อเจ้าหน้าที่ผู้ดูแลระบบ")); }
        }
        /// <summary>There are no any records be deleted.</summary>
        public static string MSGED001
        {
            get { return GenerateMessage("ED001", GetCultureText("There are no any records be deleted.", "ไม่พบรายการที่ถูกลบ")); }
        }
        /// <summary>Can not delete non-exist data.</summary>
        public static string MSGED002
        {
            get { return GenerateMessage("ED002", GetCultureText("Can not delete non-exist data.", "ไม่สามารถลบข้อมูลได้ เนื่องจากไม่พบข้อมูล")); }
        }
        /// <summary>Can not delete data without conditions</summary>
        public static string MSGED003
        {
            get { return GenerateMessage("ED003", GetCultureText("Can not delete data without conditions", "ไม่สามารถลบข้อมูลได้ เนื่องจากไม่ได้ระบุเงื่อนไข")); }
        }
        /// <summary>{0} is required. (for textbox)</summary>
        public static string MSGEI001
        {
            get { return GenerateMessage("EI001", GetCultureText("{0} is required.", "กรุณาระบุ {0}")); }
        }
        /// <summary>{0} is required. (for dropdownlist, calendar, control list)</summary>
        public static string MSGEI002
        {
            get { return GenerateMessage("EI002", GetCultureText("{0} is required.", "กรุณาเลือก {0}")); }
        }
        /// <summary>{0} must be equal to {1}.</summary>
        public static string MSGEI003
        {
            get { return GenerateMessage("EI003", GetCultureText("{0} must be equal to {1}.", "{0} ต้องเท่ากับ {1}")); }
        }
        /// <summary>{0} must be not equal to {1}.</summary>
        public static string MSGEI004
        {
            get { return GenerateMessage("EI004", GetCultureText("{0} must be not equal to {1}.", "{0} ต้องไม่เท่ากับ {1}")); }
        }
        /// <summary>{0} must be more than {1}.</summary>
        public static string MSGEI005
        {
            get { return GenerateMessage("EI005", GetCultureText("{0} must be more than {1}.", "{0} ต้องมากกว่า {1}")); }
        }
        /// <summary>{0} must be more than or equal to {1}.</summary>
        public static string MSGEI006
        {
            get { return GenerateMessage("EI006", GetCultureText("{0} must be more than or equal to {1}.", "{0} ต้องมากกว่าหรือเท่ากับ {1}")); }
        }
        /// <summary>{0} must be less than {1}.</summary>
        public static string MSGEI007
        {
            get { return GenerateMessage("EI007", GetCultureText("{0} must be less than {1}.", "{0} ต้องน้อยกว่า {1}")); }
        }
        /// <summary>{0} must be less than or equal to {1}.</summary>
        public static string MSGEI008
        {
            get { return GenerateMessage("EI008", GetCultureText("{0} must be less than or equal to {1}.", "{0} ต้องน้อยกว่าหรือเท่ากับ {1}")); }
        }
        /// <summary>The length of {0} must be equal to {1}.</summary>
        public static string MSGEI009
        {
            get { return GenerateMessage("EI009", GetCultureText("The length of {0} must be equal to {1}.", "{0} ต้องมีความยาว {1} ตัวอักษร")); }
        }
        /// <summary>The length of {0} must be more than {1}.</summary>
        public static string MSGEI010
        {
            get { return GenerateMessage("EI010", GetCultureText("The length of {0} must be more than {1}.", "{0} ต้องมีความยาวมากกว่า {1} ตัวอักษร")); }
        }
        /// <summary>The length of {0} must be more than or equal to {1}.</summary>
        public static string MSGEI011
        {
            get { return GenerateMessage("EI011", GetCultureText("The length of {0} must be more than or equal to {1}.", "{0} ต้องมีความยาวมากกว่าหรือเท่ากับ {1} ตัวอักษร")); }
        }
        /// <summary>The length of {0} must be less than {1}.</summary>
        public static string MSGEI012
        {
            get { return GenerateMessage("EI012", GetCultureText("The length of {0} must be less than {1}.", "{0} ต้องมีความยาวน้อยกว่า {1} ตัวอักษร")); }
        }
        /// <summary>The length of {0} must be less than or equal to {1}.</summary>
        public static string MSGEI013
        {
            get { return GenerateMessage("EI013", GetCultureText("The length of {0} must be less than or equal to {1}.", "{0} ต้องมีความยาวน้อยกว่าหรือเท่ากับ {1} ตัวอักษร")); }
        }
        /// <summary>{0} must be same as {1}.</summary>
        public static string MSGEI014
        {
            get { return GenerateMessage("EI014", GetCultureText("{0} must be same as {1}.", "{0} ต้องตรงกับ {1}")); }
        }
        /// <summary>{0} = {1} has already existed in the system.</summary>
        public static string MSGEI015
        {
            get { return GenerateMessage("EI015", GetCultureText("{0} \"{1}\" has already existed in system.", "{0} \"{1}\" ซ้ำกับที่มีอยู่ในระบบ")); }
        }
        /// <summary>{0} = {1} and {2} = {3} has already existed in the system}.</summary>
        public static string MSGEI016
        {
            get { return GenerateMessage("EI016", GetCultureText("{0} \"{1}\" and {2} \"{3}\" has already existed in the system", "{0} \"{1}\" และ {2} \"{3}\" ซ้ำกับที่มีอยู่ในระบบ")); }
        }
        /// <summary>{0} = {1}, {2} = {3} and {4} = {5} has already existed in the system}.</summary>
        public static string MSGEI017
        {
            get { return GenerateMessage("EI017", GetCultureText("{0} \"{1}\", {2} \"{3}\" and {4} \"{5}\" has already existed in the system", "{0} \"{1}\", {2} \"{3}\" และ {4} \"{5}\" ซ้ำกับที่มีอยู่ในระบบ")); }
        }
        /// <summary>{0} = {1}, {2} = {3}, {4} = {5} and {6} = {7} has already existed in the system}.</summary>
        public static string MSGEI018
        {
            get { return GenerateMessage("EI018", GetCultureText("{0} \"{1}\", {2} \"{3}\", {4} \"{5}\" and {6} \"{7}\" has already existed in the system", "{0} \"{1}\", {2} \"{3}\", {4} \"{5}\" และ {6} \"{7}\" ซ้ำกับที่มีอยู่ในระบบ")); }
        }
        /// <summary>There are no any records be inserted.</summary>
        public static string MSGEN001
        {
            get { return GenerateMessage("EN001", GetCultureText("There are no any records be inserted.", "ไม่พบรายการที่เพิ่มใหม่")); }
        }
        /// <summary>Can not insert data with duplicate key.</summary>
        public static string MSGEN002
        {
            get { return GenerateMessage("EN002", GetCultureText("Can not insert data with duplicate key.", "ไม่สามารถเพิ่มข้อมูลซ้ำกับที่มีอยู่")); }
        }
        /// <summary>There are no any records be updated.</summary>
        public static string MSGEU001
        {
            get { return GenerateMessage("EU001", GetCultureText("There are no any records be updated.", "ไม่พบรายการที่แก้ไข")); }
        }
        /// <summary>Can not update non-exist data.</summary>
        public static string MSGEU002
        {
            get { return GenerateMessage("EU002", GetCultureText("Can not update non-exist data.", "ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากไม่พบข้อมูล")); }
        }
        /// <summary>Can not update single record data without primary key conditions.</summary>
        public static string MSGEU003
        {
            get { return GenerateMessage("EU003", GetCultureText("Can not update single record data without primary key conditions.", "ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากไม่ได้ระบุคีย์หลัก")); }
        }
        /// <summary>There are no primary key condition for single record select statement.</summary>
        public static string MSGEV001
        {
            get { return GenerateMessage("EV001", GetCultureText("There are no primary key condition for single record select statement.", "ข้อมูลจากการค้นหาอาจมีมากกว่า 1 รายการ กรุณาระบุคีย์หลัก")); }
        }
        /// <summary>Data not found.</summary>
        public static string MSGEV002
        {
            get { return GenerateMessage("EV002", GetCultureText("Data not found.", "ไม่พบข้อมูลตามเงื่อนไขที่กำหนด")); }
        }
        /// <summary>The search result has more one rows. Please select other primary key.</summary>
        public static string MSGEV003
        {
            get { return GenerateMessage("EV003", GetCultureText("The search result has more one rows. Please select other primary key.", "ข้อมูลจากการค้นหามีมากกว่า 1 รายการ กรุณาเลือกคีย์หลักใหม่")); }
        }
        #endregion

        #endregion

        #region Confirmation

        /// <summary>Are you sure you want to insert new data?</summary>
        public static string MSGCN001
        {
            get { return GenerateMessage("CN001", GetCultureText("Are you sure you want to insert new data?", "ต้องการเพิ่มรายการใช่หรือไม่?")); }
        }
        /// <summary>Are you sure you want to delete this record?</summary>
        public static string MSGCD001
        {
            get { return GenerateMessage("CD001", GetCultureText("Are you sure you want to delete this record?", "ต้องการลบรายการใช่หรือไม่?")); }
        }
        /// <summary>Are you sure you want to delete all records?</summary>
        public static string MSGCD002
        {
            get { return GenerateMessage("CD002", GetCultureText("Are you sure you want to delete all records?", "ต้องการลบรายการทั้งหมดใช่หรือไม่?")); }
        }
        /// <summary>Are you sure you want to delete selected records?</summary>
        public static string MSGCD003
        {
            get { return GenerateMessage("CD003", GetCultureText("Are you sure you want to delete selected records?", "ต้องการลบรายการที่เลือกใช่หรือไม่?")); }
        }
        /// <summary>Are you sure you want to delete the record {0}={1}?</summary>
        public static string MSGCD004
        {
            get { return GenerateMessage("CD004", GetCultureText("Are you sure you want to delete the record {0} \"{1}\"?", "ต้องการลบรายการ {0} \"{1}\" ใช่หรือไม่?")); }
        }
        /// <summary>Are you sure you want to copy the record {0} "{1}"?</summary>
        public static string MSGCC001
        {
            get { return GenerateMessage("CC001", GetCultureText("Are you sure you want to copy the record {0} \"{1}\"?", "ต้องการคัดลอกรายการ {0} \"{1}\" ใช่หรือไม่?")); }
        }
        /// <summary>Are you sure you want to log out?</summary>
        public static string MSGCS001
        {
            get { return GenerateMessage("CS001", GetCultureText("Are you sure you want to log out?", "ต้องการออกจากระบบใช่หรือไม่?")); }
        }

        #endregion

        #region Information
        /// <summary>Inserting data is completed successfully.</summary>
        public static string MSGIN001
        {
            get { return GenerateMessage("IN001", GetCultureText("Inserting data is completed successfully.", "เพิ่มข้อมูลเรียบร้อยแล้ว")); }
        }
        /// <summary>Updating data is completed successfully.</summary>
        public static string MSGIU001
        {
            get { return GenerateMessage("IU001", GetCultureText("Updating data is completed successfully.", "แก้ไขข้อมูลเรียบร้อยแล้ว")); }
        }
        /// <summary>Updating data is completed successfully.</summary>
        public static string MSGIR001
        {
            get { return GenerateMessage("IR001", GetCultureText("Updating data is completed successfully.", "ส่งเรื่องเรียบร้อยแล้ว")); }
        }
        /// <summary>Deleting data is completed successfully.</summary>
        public static string MSGID001
        {
            get { return GenerateMessage("ID001", GetCultureText("Deleting data is completed successfully.", "ลบข้อมูลเรียบร้อยแล้ว")); }
        }
        /// <summary>Changing password is completed successfully.</summary>
        public static string MSGIS001
        {
            get { return GenerateMessage("ID001", GetCultureText("Changing password is completed successfully.", "เปลี่ยนรหัสผ่านเรียบร้อยแล้ว")); }
        }

        #endregion

        #endregion

        #region Constant
        public static string Anonymous
        {
            get { return GetCultureText("Anonymous", "บุคคลทั่วไป"); }
        }
        public static string Cancel
        {
            get { return GetCultureText("Cancel", "ยกเลิก"); }
        }
        public static string ChangePassword
        {
            get { return GetCultureText("Change Password", "เปลียนรหัสผ่าน"); }
        }
        public static string CopyRight
        {
            get { return GetCultureText("Copyright &copy; All Rights Reserved.<br>Dept. of Intellectual Property, Ministry of Commerce<br>44/100 Nonthaburi 1 Rd., Bangkrasor, Muang, Nonthaburi, Thailand 11000.", "สงวนลิขสิทธิ์ &copy; โดย กรมทรัพย์สินทางปัญญา<br>44/100 ถนนนนทบุรี 1 ต.บางกระสอ อ.เมือง จ.นนทบุรี 11000"); }
        }
        public static string EditProfile
        {
            get { return GetCultureText("Edit Profile", "แก้ไขข้อมูลส่วนตัว"); }
        }
        public static string EmptyDataText
        {
            get { return GetCultureText("<center>***Data Not Found***</center>", "<center>***ไม่พบข้อมูล***</center>"); }
        }
        public static string Home
        {
            get { return GetCultureText("Home", "กลับหน้าหลัก"); }
        }
        public static string LogIn
        {
            get { return GetCultureText("Log In", "ลงชื่อเข้าใช้"); }
        }
        public static string LoginRequire
        {
            get { return GetCultureText("You are not logged in.", "กรุณาลงชื่อเข้าใช้ระบบ"); }
        }
        public static string OK
        {
            get { return GetCultureText("OK", "ตกลง"); }
        }
        public static string AutherizeRequire
        {
            get { return GetCultureText("You are not allowed to access this page.", "คุณไม่มีสิทธิ์เข้าใช้งานในส่วนนี้"); }
        }
        public static string Login_OfficerCheckBox
        {
            get { return GetCultureText("For officer only", "สำหรับเจ้าหน้าที่"); }
        }
        public static string Login_Password
        {
            get { return GetCultureText("Password  :&nbsp;&nbsp;", "รหัสผ่าน  :&nbsp;&nbsp;"); }
        }
        public static string Login_UserID
        {
            get { return GetCultureText("User ID  :&nbsp;&nbsp;", "ชื่อผู้ใช้  :&nbsp;&nbsp;"); }
        }
        public static string LogOut
        {
            get { return GetCultureText("Log Out", "ออกจากระบบ"); }
        }
        public static string LoggedOut
        {
            get { return GetCultureText("You are logged out.", "ออกจากระบบเรียบร้อยแล้ว"); }
        }
        public static string Register
        {
            get { return GetCultureText("Register", "สมัครสมาชิก"); }
        }
        public static string SearchHeader
        {
            get { return GetCultureText("Search Criteria", "ค้นหา"); }
        }
        public static string Thai
        {
            get { return "ไทย"; }
        }
        /// <summary>Please wait while processing ...</summary>
        public static string Wait
        {
            get { return GetCultureText("Please wait while processing ...", "กรุณารอสักครู่..."); }
        }
        public static string Welcome
        {
            get { return GetCultureText("Welcome", "ยินดีต้อนรับ"); }
        }
        public static string DownloadManual
        {
            get { return GetCultureText("Download User Manual", "ดาวน์โหลดคู่มือการใช้งาน"); }
        }
        public static string ContactAdmin
        {
            get { return GetCultureText("Contact System Administrator", "ติดต่อผู้ดูแลระบบ"); }
        }
        #endregion
    }
}
