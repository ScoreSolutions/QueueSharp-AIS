using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Message = GenerateC.Utilities.MessageResources;

namespace GenerateC.Utilities
{
    public class BaseDB
    {
        protected static string ErrorConnectionString = Message.MSGEC001;
        protected static string ErrorConnection = Message.MSGEC002;
        protected static string ErrorSetCommandConnection = Message.MSGEC003;
        protected static string ErrorInvalidCommandType = Message.MSGEC004;
        protected static string ErrorDuplicateParameter = Message.MSGEC006;
        protected static string ErrorNullParameter = Message.MSGEC005;
        protected static string ErrorExecuteNonQuery = Message.MSGEC010;
        protected static string ErrorExecuteReader = Message.MSGEC011;
        protected static string ErrorExecuteTable = Message.MSGEC012;
        protected static string ErrorExecuteScalar = Message.MSGEC013;
        protected static string ErrorDatabaseOther = Message.MSGEC901;
        protected static string ErrorUndefined = Message.MSGEC902;

        internal partial class DatabaseException : ApplicationException
        {
            public DatabaseException(string message)
                : base(message)
            {
            }
            public DatabaseException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }

        #region Prepare data for insert or update to database
        /// <summary>
        /// Get a double-precision floating point number as string.
        /// </summary>
        /// <param name="number">A double value</param>
        /// <returns>The string representation of the value of this instance</returns>
        public static string SetDouble(double number) { return number.ToString(); }
        /// <summary>
        /// Get a decimal object as string.
        /// </summary>
        /// <param name="number">A decimal value</param>
        /// <returns>The string representation of the value of this instance</returns>
        public static string SetDecimal(decimal number) { return number.ToString(); }
        /// <summary>
        /// Get a boolean as string.
        /// </summary>
        /// <param name="number">A boolean value</param>
        /// <returns>The string representation of the value of this instance</returns>
        public static string SetBoolean(bool boolean) { return (boolean ? "1" : "0"); }
        /// <summary>
        /// Removes all occurrences of white space characters from the beginning and end of this instance. 
        /// And replaces all (') in this instant with valid string.
        /// </summary>
        /// <param name="strinput">A string value.</param>
        /// <returns>A new System.String equivalent to this instance.</returns>
        public static string SetString(string strinput) { return (strinput.Trim() == "" ? "NULL" : ("'" + strinput.Trim().Replace("'", "''") + "'")); }

        #endregion

    }
}
