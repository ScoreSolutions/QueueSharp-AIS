using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Text;

namespace GenerateC.Utilities
{
    public class OracleDB : BaseDB
    {
        /// <summary>
        /// Converts the System.Data.OracleClient.OracleException to its equivalent string representation using message meaning.
        /// </summary>
        /// <param name="ex">The System.Data.OracleClient.OracleException object.</param>
        /// <returns>A string representation of meaning of this System.Data.OracleClient.OracleException instant as equivalent message.</returns>
        public static string GetExceptionMessage(OracleException ex)
        {
            string ret = "";
            switch (ex.Code.ToString())
            {
                case "2292" :
                    ret = "ไม่สามารถทำรายการได้ เนื่องจากมีการอ้างอิงจากตารางอื่น";
                    break;
                default :
                    //ret = string.Format(ErrorDatabaseOther, ex.Code.ToString(), ex.Message);
                    ret = "ไม่สามารถทำรายการได้ เนื่องจากเกิดความผิดพลาดจากฐานข้อมูล : " + ex.Message;
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Get the connection string for the current application's default configuration.
        /// </summary>
        protected static string ConnectionString
        {
            get
            {
                try
                {
                    //return ConfigurationManager.ConnectionStrings["ORACLECONNECTION"].ConnectionString;
                    //return "Data Source=orcl;Persist Security Info=True;User ID=excise_law;Password=password;Unicode=True";
                    //return "Data Source=illegal;Persist Security Info=True;User ID=excise_law;Password=password;Unicode=True";
                    return "Data Source=scoredev;Persist Security Info=True;User ID=excise_law;Password=password;Unicode=True";
                    //return "Data Source=T;Persist Security Info=True;User ID=excise_law;Password=password;Unicode=True";
                }
                catch (Exception ex)
                {
                    throw new DatabaseException(ErrorConnectionString, ex);
                }
            }
        }

        #region Prepare data for insert or update to database
        /// <summary>
        /// Converts the current date to its equivalent string representation against using the database format.
        /// </summary>
        /// <returns>A string representation of value of this current date as database format.</returns>
        public static string SetDate() { return SetDateTime(DateTime.Today); }
        /// <summary>
        /// Converts the System.DateTime instant to its equivalent string representation against using the database format.
        /// </summary>
        /// <param name="DateIn">The System.DateTime object.</param>
        /// <returns>A string representation of value of this System.DateTime instant as database format.</returns>
        public static string SetDate(DateTime DateIn)
        {
            return SetDateTime(DateIn.Date);
        }
        public static DateTime SetTxtDate(Object DateIn)
        { 
            DateTime ret;
            if (!Convert.IsDBNull(DateIn))
                ret = Convert.ToDateTime(DateIn);
            else
                ret = new DateTime();

            return ret;
        }

        /// <summary>
        /// Converts the current date and time to its equivalent string representation against using the database format.
        /// </summary>
        /// <returns>A string representation of value of this current date as database format.</returns>
        public static string SetDateTime() { return SetDateTime(DateTime.Now); }
        /// <summary>
        /// Converts the System.DateTime instant to its equivalent string representation against using the database format.
        /// </summary>
        /// <param name="DateIn">The System.DateTime object.</param>
        /// <returns>A string representation of value of this System.DateTime instant as database format.</returns>
        public static string SetDateTime(DateTime DateIn)
        {
            return (DateIn.Year == 1 ? "null" : "TO_DATE('" + DateIn.Year.ToString("0000") + DateIn.ToString("-MM-dd HH:mm:ss") + "', 'YYYY-MM-DD HH24:MI:SS')");
        }

        public static string SetDateToStringValue(DateTime DateIn)
        {
            return "'" + DateIn.Year.ToString("0000") + DateIn.ToString("MMdd") + "'";
        }
        public static string SetDateToStringField(string fieldName)
        {
            return "TO_CHAR(" + fieldName + ", 'YYYYMMDD')";
        }
        #endregion

        #region GetConnection
        /// <summary>
        /// Initializes a new opened instance of the System.Data.OracleClient.OracleConnection class with the current application's default connection string.
        /// </summary>
        /// <returns>An opened System.Data.OracleClient.OracleConnection object.</returns>
        public static OracleConnection GetConnection()
        {
            OracleConnection connection;
            try
            {
                connection = new OracleConnection(ConnectionString);
                connection.Open();
                return connection;
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(GetExceptionMessage(ex), ex);
            }
            catch (DatabaseException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ErrorConnection, ex);
            }
        }

        /// <summary>
        /// Initializes a new opened instance of the System.Data.OracleClient.OracleConnection class with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>An opened System.Data.OracleClient.OracleConnection object.</returns>
        public static OracleConnection GetConnection(string connectionString)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(GetExceptionMessage(ex), ex);
            }
            catch (DatabaseException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ErrorConnection, ex);
            }
        }
        #endregion

        #region BuildCommand
        /// <summary>
        /// Initializes the specified System.Data.OracleClient.OracleCommand with the specified System.Data.OracleClient.OracleConnection,
        /// System.Data.OracleClient.OracleTransaction, System.Data.CommandType, Oracle statement and collection of the 
        /// System.Data.OracleClient.OracleParameter.
        /// </summary>
        /// <param name="cmd">The System.Data.OracleClient.OracleCommand object.</param>
        /// <param name="conn">The System.Data.OracleClient.OracleConnection used by the System.Data.OracleClient.OracleCommand.</param>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction within which the System.Data.OracleClient.OracleCommand executes.</param>
        /// <param name="cmdType">The value indicating how the System.Data.OracleClient.OracleCommand.CommandText property is interpreted.</param>
        /// <param name="cmdText">The Oracle statement or stored procedure to execute against the database.</param>
        /// <param name="cmdParms">The collection of System.Data.OracleClient.OracleParameter to the System.Data.OracleClient.OracleParameterCollection.</param>
        private static void BuildCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (OracleException ex)
                {
                    throw new DatabaseException(GetExceptionMessage(ex), ex);
                }
                catch (DatabaseException ex)
                {
                    throw (ex);
                }
                catch (Exception ex)
                {
                    throw new DatabaseException(ErrorConnection, ex);
                }
            }
            try
            {
                cmd.Connection = conn;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ErrorSetCommandConnection, ex);
            }
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            try
            {
                cmd.CommandType = cmdType;
            }
            catch (ArgumentException ex)
            {
                throw new DatabaseException(ErrorInvalidCommandType, ex);
            }

            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    try
                    {
                        cmd.Parameters.Add(parm);
                    }
                    catch (ArgumentNullException ex)
                    {
                        throw new DatabaseException(ErrorNullParameter, ex);
                    }
                    catch (ArgumentException ex)
                    {
                        throw new DatabaseException(ErrorDuplicateParameter, ex);
                    }
                }
            }
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// Executes an Oracle statement against the System.Data.OracleClient.OracleCommand.Connection
        /// and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">Oracle statement to execut against the database.</param>
        /// <returns>
        /// For UPDATE, INSERT, and DELETE statements, the return value is the number 
        /// of rows affected by the command. For CREATE TABLE and DROP TABLE statements, 
        /// the return value is 0. For all other types of statements, the return value is -1.
        /// </returns>
        public static int ExecuteNonQuery(string sql) { return ExecuteNonQuery(sql, null, null); }
        /// <summary>
        /// Executes an SQL statement against the System.Data.OracleClent.OracleCommand.Connection
        /// and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.OracleClent.OracleConnection object.</param>
        /// <returns>
        /// For UPDATE, INSERT, and DELETE statements, the return value is the number 
        /// of rows affected by the command. For CREATE TABLE and DROP TABLE statements, 
        /// the return value is 0. For all other types of statements, the return value is -1.
        /// </returns>
        public static int ExecuteNonQuery(string sql, OracleConnection conn) { return ExecuteNonQuery(sql, conn, null); }
        /// <summary>
        /// Executes an SQL statement against the System.Data.OracleClent.OracleCommand.Connection
        /// and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="trans">The System.Data.OracleClent.OracleTransaction used to execute System.Data.OracleClent.OracleCommand.</param>
        /// <returns>
        /// For UPDATE, INSERT, and DELETE statements, the return value is the number 
        /// of rows affected by the command. For CREATE TABLE and DROP TABLE statements, 
        /// the return value is 0. For all other types of statements, the return value is -1.
        /// </returns>
        public static int ExecuteNonQuery(string sql, OracleTransaction trans) { return ExecuteNonQuery(sql, null, trans); }
        /// <summary>
        /// Executes an SQL statement against the System.Data.OracleClent.OracleCommand.Connection
        /// and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.OracleClent.OracleConnection object.</param>
        /// <param name="trans">The System.Data.OracleClent.OracleTransaction used to execute System.Data.OracleClent.OracleCommand.</param>
        /// <returns>
        /// For UPDATE, INSERT, and DELETE statements, the return value is the number 
        /// of rows affected by the command. For CREATE TABLE and DROP TABLE statements, 
        /// the return value is 0. For all other types of statements, the return value is -1.
        /// </returns>
        public static int ExecuteNonQuery(string sql, OracleConnection conn, OracleTransaction trans)
        {
            int retval;
            OracleCommand command = new OracleCommand();

            try
            {
                if (trans == null)
                {
                    conn = new OracleConnection(ConnectionString);
                    using (conn)
                    {
                        BuildCommand(command, conn, trans, CommandType.Text, sql, null);
                        retval = command.ExecuteNonQuery();
                    }
                }
                else
                {
                    if (trans != null && conn == null)
                        conn = trans.Connection;

                    BuildCommand(command, trans.Connection, trans, CommandType.Text, sql, null);
                    retval = command.ExecuteNonQuery();
                }
            }
            catch (DatabaseException ex)
            {
                throw ex;
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(GetExceptionMessage(ex), ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ErrorExecuteNonQuery, ex);
            }

            return retval;
        }

        public static int ExecuteSuspect(string sql, OracleTransaction trans, object objPhoto)
        {
            int retval;

            OracleCommand command = new OracleCommand();
            OracleParameter pPic = new OracleParameter();
            pPic.ParameterName = ":LINK_PHOTO";
            pPic.OracleType = OracleType.Blob;
            pPic.Value = objPhoto;

            command.Parameters.Add(pPic);

            try
            {
                BuildCommand(command, trans.Connection, trans, CommandType.Text, sql,null);
                retval = command.ExecuteNonQuery();
            }
            catch (DatabaseException ex)
            {
                throw ex;
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(GetExceptionMessage(ex), ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ErrorExecuteNonQuery, ex);
            }

            return retval;
        }

        #endregion

        #region ExecuteReader
        /// <summary>
        /// Sends the System.Data.OracleClent.OracleCommand.CommandText to the System.Data.OracleClent.OracleCommand.Connection
        /// and builds an System.Data.OracleClent.OracleDataReader.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <returns>An System.Data.OracleClent.OracleDataReader object.</returns>
        public static OracleDataReader ExecuteReader(string sql) { return ExecuteReader(sql, null, null); }
        /// <summary>
        /// Sends the System.Data.OracleClent.OracleCommand.CommandText to the System.Data.OracleClent.OracleCommand.Connection
        /// and builds an System.Data.OracleClent.OracleDataReader.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="trans">The System.Data.OracleClent.OracleTransaction object.</param>
        /// <returns>An System.Data.OracleClent.OracleDataReader object.</returns>
        public static OracleDataReader ExecuteReader(string sql, OracleTransaction trans) { return ExecuteReader(sql, null, trans); }
        /// <summary>
        /// Sends the System.Data.OracleClent.OracleCommand.CommandText to the System.Data.OracleClent.OracleCommand.Connection
        /// and builds an System.Data.OracleClent.OracleDataReader.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.OracleClent.OracleConnection object.</param>
        /// <returns>An System.Data.OracleClent.OracleDataReader object.</returns>
        public static OracleDataReader ExecuteReader(string sql, OracleConnection conn) { return ExecuteReader(sql, conn, null); }
        /// <summary>
        /// Sends the System.Data.OracleClent.OracleCommand.CommandText to the System.Data.OracleClent.OracleCommand.Connection
        /// and builds an System.Data.OracleClent.OracleDataReader.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.OracleClent.OracleConnection object.</param>
        /// <param name="trans">The System.Data.OracleClent.OracleTransaction object.</param>
        /// <returns>An System.Data.OracleClent.OracleDataReader object.</returns>
        public static OracleDataReader ExecuteReader(string sql, OracleConnection conn, OracleTransaction trans)
        {
            OracleCommand command = new OracleCommand();
            OracleDataReader reader;
            bool LetClose = false;

            try
            {
                if (trans != null && conn == null)
                    conn = trans.Connection;
                else if (conn == null)
                {
                    conn = GetConnection();
                    LetClose = true;
                }

                BuildCommand(command, conn, trans, CommandType.Text, sql, null);
                reader = (LetClose ? command.ExecuteReader(CommandBehavior.CloseConnection) : command.ExecuteReader());
            }
            catch (DatabaseException ex)
            {
                throw ex;
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(GetExceptionMessage(ex), ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ErrorExecuteReader, ex);
            }

            return reader;
        }
        #endregion

        #region ExecuteTable
        /// <summary>
        /// Sends the System.Data.OracleClent.OracleCommand.CommandText to the System.Data.OracleClent.OracleCommand.Connection
        /// and builds an System.Data.DataTable.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <returns>The System.Data.DataTable object.</returns>
        public static DataTable ExecuteTable(string sql) { return ExecuteTable(sql, null, null); }
        /// <summary>
        /// Sends the System.Data.OracleClent.OracleCommand.CommandText to the System.Data.OracleClent.OracleCommand.Connection
        /// and builds an System.Data.DataTable.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.OracleClent.OracleConnection object.</param>
        /// <returns>The System.Data.DataTable object.</returns>
        public static DataTable ExecuteTable(string sql, OracleConnection conn) { return ExecuteTable(sql, conn, null); }
        /// <summary>
        /// Sends the System.Data.OracleClent.OracleCommand.CommandText to the System.Data.OracleClent.OracleCommand.Connection
        /// and builds an System.Data.DataTable.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="trans">The System.Data.OracleClent.OracleTransaction object.</param>
        /// <returns>The System.Data.DataTable object.</returns>
        public static DataTable ExecuteTable(string sql, OracleTransaction trans) { return ExecuteTable(sql, null, trans); }
        /// <summary>
        /// Sends the System.Data.OracleClent.OracleCommand.CommandText to the System.Data.OracleClent.OracleCommand.Connection
        /// and builds an System.Data.DataTable.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.OracleClent.OracleConnection object.</param>
        /// <param name="trans">The System.Data.OracleClent.OracleTransaction object.</param>
        /// <returns>The System.Data.DataTable object.</returns>
        public static DataTable ExecuteTable(string sql, OracleConnection conn, OracleTransaction trans)
        {
            OracleCommand command = new OracleCommand();
            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = command;
            DataTable dt = new DataTable();

            try
            {
                if (trans == null && conn != null)
                {
                    //conn = new OracleConnection(ConnectionString);
                    using (conn)
                    {
                        BuildCommand(command, conn, trans, CommandType.Text, sql, null);

                        adapter.Fill(dt);
                        adapter.Dispose();
                    }
                }
                else if (trans != null && conn == null)
                {
                    conn = trans.Connection;
                    BuildCommand(command, conn, trans, CommandType.Text, sql, null);

                    adapter.Fill(dt);
                    adapter.Dispose();
                }
                else
                {
                    conn = new OracleConnection(ConnectionString);
                    using (conn)
                    {
                        BuildCommand(command, conn, trans, CommandType.Text, sql, null);

                        adapter.Fill(dt);
                        adapter.Dispose();
                    }
                }
            
            }
            catch (DatabaseException ex)
            {
                adapter.Dispose();
                throw ex;
            }
            catch (OracleException ex)
            {
                adapter.Dispose();
                throw new DatabaseException(GetExceptionMessage(ex), ex);
            }
            catch (Exception ex)
            {
                adapter.Dispose();
                throw new DatabaseException(ErrorExecuteTable, ex);
            }

            return dt;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query as a .NET Framework data type. Extra columns
        /// or rows are ignored.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <returns>
        /// The first column of the first row in the result set as a .NET Framework data 
        /// type, or a null reference if the result set is empty or the result is a REFCURSOR.
        /// </returns>
        public static object ExecuteScalar(string sql) { return ExecuteScalar(sql, null, null); }
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query as a .NET Framework data type. Extra columns
        /// or rows are ignored.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.OracleClent.OracleConnection object.</param>
        /// <returns>
        /// The first column of the first row in the result set as a .NET Framework data 
        /// type, or a null reference if the result set is empty or the result is a REFCURSOR.
        /// </returns>
        public static object ExecuteScalar(string sql, OracleConnection conn) { return ExecuteScalar(sql, conn, null); }
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query as a .NET Framework data type. Extra columns
        /// or rows are ignored.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="trans">The System.Data.OracleClent.OracleTransaction object.</param>
        /// <returns>
        /// The first column of the first row in the result set as a .NET Framework data 
        /// type, or a null reference if the result set is empty or the result is a REFCURSOR.
        /// </returns>
        public static object ExecuteScalar(string sql, OracleTransaction trans) { return ExecuteScalar(sql, null, trans); }
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query as a .NET Framework data type. Extra columns
        /// or rows are ignored.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.OracleClent.OracleConnection object.</param>
        /// <param name="trans">The System.Data.OracleClent.OracleTransaction object.</param>
        /// <returns>
        /// The first column of the first row in the result set as a .NET Framework data 
        /// type, or a null reference if the result set is empty or the result is a REFCURSOR.
        /// </returns>
        public static object ExecuteScalar(string sql, OracleConnection conn, OracleTransaction trans)
        {
            OracleCommand command = new OracleCommand();
            object retval;
            try
            {
                if (trans == null)
                {
                    conn = new OracleConnection(ConnectionString);
                    using (conn)
                    {
                        BuildCommand(command, conn, trans, CommandType.Text, sql, null);
                        retval = command.ExecuteScalar();
                    }
                }
                else
                {
                    if (trans != null && conn == null)
                        conn = trans.Connection;

                    BuildCommand(command, trans.Connection, trans, CommandType.Text, sql, null);
                    retval = command.ExecuteScalar();
                }
            }
            catch (DatabaseException ex)
            {
                throw ex;
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(GetExceptionMessage(ex), ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ErrorExecuteScalar, ex);
            }

            return retval;
        }
        #endregion

        #region Get ID

        /// <summary>
        /// Executes the query, and returns the next ID of table.
        /// </summary>
        /// <param name="tableName">Table Name.</param>
        /// <returns>
        /// the next ID of table.
        /// </returns>
        public static double GetNextID(string tableName) { return GetNextID(tableName, null, null); }
        /// <summary>
        /// Executes the query, and returns the next ID of table.
        /// </summary>
        /// <param name="tableName">Table Name.</param>
        /// <param name="conn">The System.Data.OracleClent.OracleConnection object.</param>
        /// <returns>
        /// the next ID of table.
        /// </returns>
        public static double GetNextID(string seqName, OracleConnection conn) { return GetNextID(seqName, conn, null); }
        /// <summary>
        /// Executes the query, and returns the next ID of table.
        /// </summary>
        /// <param name="tableName">Table Name.</param>
        /// <param name="trans">The System.Data.OracleClent.OracleTransaction object.</param>
        /// <returns>
        /// the next ID of table.
        /// </returns>
        public static double GetNextID(string seqName, OracleTransaction trans) { return GetNextID(seqName, null, trans); }
        /// <summary>
        /// Executes the query, and returns the next ID of table.
        /// </summary>
        /// <param name="tableName">Table Name.</param>
        /// <param name="conn">The System.Data.OracleClent.OracleConnection object.</param>
        /// <param name="trans">The System.Data.OracleClent.OracleTransaction object.</param>
        /// <returns>
        /// the next ID of table.
        /// </returns>
        public static double GetNextID(string seqName, OracleConnection conn, OracleTransaction trans)
        {
            //string sql = "SELECT GETLASTID(" + SetString(tableName) + ") FROM DUAL ";
            string sql = "SELECT " + seqName + ".NEXTVAL FROM DUAL ";
            OracleCommand command = new OracleCommand();
            double retval;
            try
            {
                if (trans == null && conn == null)
                {
                    using (conn = new OracleConnection(ConnectionString))
                    {
                        BuildCommand(command, conn, trans, CommandType.Text, sql, null);
                        retval = Convert.ToDouble(command.ExecuteScalar());
                    }
                }
                else
                {
                    if (trans != null && conn == null)
                        conn = trans.Connection;

                    BuildCommand(command, trans.Connection, trans, CommandType.Text, sql, null);
                    retval = Convert.ToDouble(command.ExecuteScalar());
                }
            }
            catch (DatabaseException ex)
            {
                throw ex;
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(GetExceptionMessage(ex), ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ErrorExecuteScalar, ex);
            }

            return retval;
        }

        #endregion

        /// <summary>
        /// Set order clause corresponding to Thai dictionary.
        /// </summary>
        /// <param name="orderBy">The fields for sort data.</param>
        /// <returns>The order clause.</returns>
        public static string SetSortString(string orderBy)
        {
            string _orderBy = "";
            if (orderBy.Trim() != "")
            {
                string[] field = orderBy.Split(',');
                for (int i = 0; i < field.Length; ++i)
                {
                    string[] sort = field[i].Trim().Split(' ');
                    string fieldName = sort[0].Trim().ToUpper();
                    if (fieldName == "AMOUNT" || fieldName == "LOID" || fieldName == "RANK" || fieldName.Contains("DATE") || fieldName.Contains("QTY") 
                        || fieldName == "HOLIDAY" || fieldName == "ENERGY" || fieldName == "PORTION")
                        _orderBy += (_orderBy == "" ? "" : ", ") + fieldName + " " + (sort.Length == 2 ? sort[1] : "");
                    else
                        _orderBy += (_orderBy == "" ? "" : ", ") + "NLSSORT(UPPER(" + fieldName + "), 'NLS_SORT = THAI_DICTIONARY') " + (sort.Length == 2 ? sort[1] : "");
                }
            }
            else
                _orderBy = orderBy;

            return _orderBy;
        }

        /// <summary>
        /// Get running code with transaction
        /// </summary>
        /// <param name="RunningName"></param>
        /// <param name="RunningItem"></param>
        /// <returns></returns>
        public static string GetRunningCode(string RunningName, string RunningItem) { return GetRunningCode(RunningName, RunningItem, null); }
        public static string GetRunningCode(string RunningName, string RunningItem, OracleTransaction zTrans)
        {
            string tablename = "RUNNING";
            bool LetClose = false;
            OracleConnection zConn = null;
            if (zTrans == null)
            {
                LetClose = true;
                zConn = GetConnection();
                zTrans = zConn.BeginTransaction(IsolationLevel.ReadCommitted);
            }

            string loid = "";
            string code = "";
            string lastValue = "";
            string year = "";
            string month = "";
            string sqlz = "SELECT LOID, CODE, YEAR, MONTH, VALUE FROM " + tablename + " WHERE RUNNING = '" + RunningName + "' AND ITEM = '" + RunningItem + "' ";
            OracleDataReader zRd = ExecuteReader(sqlz, zTrans);
            if (zRd.Read())
            {
                int length = 0;
                loid = zRd["LOID"].ToString();
                if (!Convert.IsDBNull(zRd["YEAR"])) year = zRd["YEAR"].ToString();
                if (!Convert.IsDBNull(zRd["MONTH"])) month = zRd["MONTH"].ToString();
                lastValue = zRd["VALUE"].ToString();
                length = lastValue.Length;

                if (month != "" && year != "")
                {
                    if ((DateTime.Now.Year + 543).ToString().Substring(4 - year.Length) != year || DateTime.Now.Month.ToString("00") != month)
                    {
                        year = (DateTime.Now.Year + 543).ToString().Substring(4 - year.Length);
                        month = DateTime.Now.Month.ToString("00");
                        lastValue = "0";
                    }
                }
                else if (year != "")
                {
                    if (year != (DateTime.Now.Year + 543).ToString().Substring(4 - year.Length) && year != "")
                    {
                        year = (DateTime.Now.Year + 543).ToString().Substring(4 - year.Length);
                        lastValue = "0";
                    }
                }

                lastValue = "00000000000000000000" + (Convert.ToDouble(lastValue) + 1).ToString();
                lastValue = lastValue.Substring(lastValue.Length - length);
                code = zRd["CODE"].ToString() + year + month + lastValue;
                zRd.Close();

                sqlz = "UPDATE " + tablename + " SET MONTH = '" + month + "', YEAR = '" + year + "', VALUE = '" + lastValue + "' WHERE LOID = " + loid;
                ExecuteNonQuery(sqlz, zTrans);
            }
            else
            {
                if (LetClose)
                {
                    zTrans.Commit();
                    zConn.Close();
                }
                throw new ApplicationException("ไม่สามารถอ่านค่า running ได้");
            }

            if (LetClose)
            {
                zTrans.Commit();
                zConn.Close();
            }
            return code;
        }

    }
}
