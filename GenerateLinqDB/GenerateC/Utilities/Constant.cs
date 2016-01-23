using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace GenerateC.Utilities
{
    public class Constant
    {
        public const string CultureSessionID = "SHNDCulture";
        public const string ApplicationErrorSessionID = "ErrorMessage";
        public const string IntFormat = "#,##0";
        public const string DoubleFormat = "#,##0.00";
        public const string DateFormat = "dd/MM/yyyy";

        public static string HomeFolder
        {
            get { return System.Web.HttpContext.Current.Request.ApplicationPath + "/"; }
        }

        public static string ImageFolder
        {
            get { return HomeFolder + "Images/"; }
        }

        public partial class CultureName
        {
            public const string Default = "th-TH";
            public const string English = "en-US";
            public const string Thai = "th-TH";
        }

        public partial class FieldName
        {
            public const string CREATE_BY = "CREATE_BY";
            public const string CREATE_ON = "CREATE_ON";
            public const string UPDATE_BY = "UPDATE_BY";
            public const string UPDATE_ON = "UPDATE_ON";
        }
        public partial class DatabaseType
        {
            public const string SQL = "SQL";
            public const string Oracle = "Oracle";
        }
        public static string GetFullDate()
        {
            string month = "";
            switch (DateTime.Now.Month)
            {
                case 1:
                    month = "January";
                    break;
                case 2:
                    month = "Febuary";
                    break;
                case 3:
                    month = "March";
                    break;
                case 4:
                    month = "April";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "June";
                    break;
                case 7:
                    month = "July";
                    break;
                case 8:
                    month = "August";
                    break;
                case 9:
                    month = "September";
                    break;
                case 10:
                    month = "October";
                    break;
                case 11:
                    month = "November";
                    break;
                case 12:
                    month = "December";
                    break;
            }
            return month + "," + DateTime.Now.Day.ToString() + " " + DateTime.Now.Year.ToString();
        }
    }
}
