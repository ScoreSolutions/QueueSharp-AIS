using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GenerateC.Utilities
{
    internal class SqlDB : BaseDB
    {

        /// <summary>
        /// Converts the System.Data.SqlClient.SqlException to its equivalent string representation using message meaning.
        /// </summary>
        /// <param name="ex">The System.Data.SqlClient.SqlException object.</param>
        /// <returns>A string representation of meaning of this System.Data.SqlClient.SqlException instant as equivalent message.</returns>
        public static string GetExceptionMessage(SqlException ex)
        {
            string ret = "";
            ret = string.Format(ErrorDatabaseOther, ex.ErrorCode.ToString(), ex.Message);
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
                    return "Data Source=localhost;Initial Catalog=DBName;User ID=UserID;Password=pwd";
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
            return (DateIn.Year == 1 ? "null" : "'" + DateIn.Year.ToString("0000") + DateIn.ToString("-MM-dd HH:mm:ss") + "'");
        }
        #endregion

        #region GetConnection
        /// <summary>
        /// Initializes a new opened instance of the System.Data.SqlClient.SqlConnection class with the current application's default connection string.
        /// </summary>
        /// <returns>An opened System.Data.SqlClient.SqlConnection object.</returns>
        public static SqlConnection GetConnection()
        {
            SqlConnection connection;
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
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
        /// Initializes a new opened instance of the System.Data.SqlClient.SqlConnection class with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>An opened System.Data.SqlClient.SqlConnection object.</returns>
        public static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
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
        /// Initializes the specified System.Data.SqlClient.SqlCommand with the specified System.Data.SqlClient.SqlConnection,
        /// System.Data.SqlClient.SqlTransaction, System.Data.CommandType, Sql statement and collection of the 
        /// System.Data.SqlClient.SqlParameter.
        /// </summary>
        /// <param name="cmd">The System.Data.SqlClient.SqlCommand object.</param>
        /// <param name="conn">The System.Data.SqlClient.SqlConnection used by the System.Data.SqlClient.SqlCommand.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction within which the System.Data.SqlClient.SqlCommand executes.</param>
        /// <param name="cmdType">The value indicating how the System.Data.SqlClient.SqlCommand.CommandText property is interpreted.</param>
        /// <param name="cmdText">The Sql statement or stored procedure to execute against the database.</param>
        /// <param name="cmdParms">The collection of System.Data.SqlClient.SqlParameter to the System.Data.SqlClient.SqlParameterCollection.</param>
        private static void BuildCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException ex)
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
                cmd.CommandTimeout = 120;
            }
            catch (ArgumentException ex)
            {
                throw new DatabaseException(ErrorInvalidCommandType, ex);
            }

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
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
        /// Executes an Sql statement against the System.Data.SqlClient.SqlCommand.Connection
        /// and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">Sql statement to execut against the database.</param>
        /// <returns>
        /// For UPDATE, INSERT, and DELETE statements, the return value is the number 
        /// of rows affected by the command. For CREATE TABLE and DROP TABLE statements, 
        /// the return value is 0. For all other types of statements, the return value is -1.
        /// </returns>
        public static int ExecuteNonQuery(string sql) { return ExecuteNonQuery(sql, null, null); }
        /// <summary>
        /// Executes an SQL statement against the System.Data.SqlClent.SqlCommand.Connection
        /// and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.SqlClent.SqlConnection object.</param>
        /// <returns>
        /// For UPDATE, INSERT, and DELETE statements, the return value is the number 
        /// of rows affected by the command. For CREATE TABLE and DROP TABLE statements, 
        /// the return value is 0. For all other types of statements, the return value is -1.
        /// </returns>
        public static int ExecuteNonQuery(string sql, SqlConnection conn) { return ExecuteNonQuery(sql, conn, null); }
        /// <summary>
        /// Executes an SQL statement against the System.Data.SqlClent.SqlCommand.Connection
        /// and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="trans">The System.Data.SqlClent.SqlTransaction used to execute System.Data.SqlClent.SqlCommand.</param>
        /// <returns>
        /// For UPDATE, INSERT, and DELETE statements, the return value is the number 
        /// of rows affected by the command. For CREATE TABLE and DROP TABLE statements, 
        /// the return value is 0. For all other types of statements, the return value is -1.
        /// </returns>
        public static int ExecuteNonQuery(string sql, SqlTransaction trans) { return ExecuteNonQuery(sql, null, trans); }
        /// <summary>
        /// Executes an SQL statement against the System.Data.SqlClent.SqlCommand.Connection
        /// and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.SqlClent.SqlConnection object.</param>
        /// <param name="trans">The System.Data.SqlClent.SqlTransaction used to execute System.Data.SqlClent.SqlCommand.</param>
        /// <returns>
        /// For UPDATE, INSERT, and DELETE statements, the return value is the number 
        /// of rows affected by the command. For CREATE TABLE and DROP TABLE statements, 
        /// the return value is 0. For all other types of statements, the return value is -1.
        /// </returns>
        public static int ExecuteNonQuery(string sql, SqlConnection conn, SqlTransaction trans)
        {
            int retval;
            SqlCommand command = new SqlCommand();

            try
            {
                if (trans == null)
                {
                    conn = new SqlConnection(ConnectionString);
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
            catch (SqlException ex)
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
        /// Sends the System.Data.SqlClent.SqlCommand.CommandText to the System.Data.SqlClent.SqlCommand.Connection
        /// and builds an System.Data.SqlClent.SqlDataReader.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <returns>An System.Data.SqlClent.SqlDataReader object.</returns>
        public static SqlDataReader ExecuteReader(string sql) { return ExecuteReader(sql, null, null); }
        /// <summary>
        /// Sends the System.Data.SqlClent.SqlCommand.CommandText to the System.Data.SqlClent.SqlCommand.Connection
        /// and builds an System.Data.SqlClent.SqlDataReader.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="trans">The System.Data.SqlClent.SqlTransaction object.</param>
        /// <returns>An System.Data.SqlClent.SqlDataReader object.</returns>
        public static SqlDataReader ExecuteReader(string sql, SqlTransaction trans) { return ExecuteReader(sql, null, trans); }
        /// <summary>
        /// Sends the System.Data.SqlClent.SqlCommand.CommandText to the System.Data.SqlClent.SqlCommand.Connection
        /// and builds an System.Data.SqlClent.SqlDataReader.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.SqlClent.SqlConnection object.</param>
        /// <returns>An System.Data.SqlClent.SqlDataReader object.</returns>
        public static SqlDataReader ExecuteReader(string sql, SqlConnection conn) { return ExecuteReader(sql, conn, null); }
        /// <summary>
        /// Sends the System.Data.SqlClent.SqlCommand.CommandText to the System.Data.SqlClent.SqlCommand.Connection
        /// and builds an System.Data.SqlClent.SqlDataReader.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.SqlClent.SqlConnection object.</param>
        /// <param name="trans">The System.Data.SqlClent.SqlTransaction object.</param>
        /// <returns>An System.Data.SqlClent.SqlDataReader object.</returns>
        public static SqlDataReader ExecuteReader(string sql, SqlConnection conn, SqlTransaction trans)
        {
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
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
            catch (SqlException ex)
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
        /// Sends the System.Data.SqlClent.SqlCommand.CommandText to the System.Data.SqlClent.SqlCommand.Connection
        /// and builds an System.Data.DataTable.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <returns>The System.Data.DataTable object.</returns>
        public static DataTable ExecuteTable(string sql) { return ExecuteTable(sql, null, null); }
        /// <summary>
        /// Sends the System.Data.SqlClent.SqlCommand.CommandText to the System.Data.SqlClent.SqlCommand.Connection
        /// and builds an System.Data.DataTable.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.SqlClent.SqlConnection object.</param>
        /// <returns>The System.Data.DataTable object.</returns>
        public static DataTable ExecuteTable(string sql, SqlConnection conn) { return ExecuteTable(sql, conn, null); }
        /// <summary>
        /// Sends the System.Data.SqlClent.SqlCommand.CommandText to the System.Data.SqlClent.SqlCommand.Connection
        /// and builds an System.Data.DataTable.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="trans">The System.Data.SqlClent.SqlTransaction object.</param>
        /// <returns>The System.Data.DataTable object.</returns>
        public static DataTable ExecuteTable(string sql, SqlTransaction trans) { return ExecuteTable(sql, null, trans); }
        /// <summary>
        /// Sends the System.Data.SqlClent.SqlCommand.CommandText to the System.Data.SqlClent.SqlCommand.Connection
        /// and builds an System.Data.DataTable.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.SqlClent.SqlConnection object.</param>
        /// <param name="trans">The System.Data.SqlClent.SqlTransaction object.</param>
        /// <returns>The System.Data.DataTable object.</returns>
        public static DataTable ExecuteTable(string sql, SqlConnection conn, SqlTransaction trans)
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataTable dt = new DataTable();

            try
            {
                //If conn Is Nothing And trans Is Nothing Then
                //    conn = New SqlConnection(ConnectionString)
                //ElseIf trans IsNot Nothing And conn Is Nothing Then
                //    conn = trans.Connection
                //End If
                if (conn == null && trans == null)
                {
                    conn = new SqlConnection(ConnectionString);
                    //using (conn)
                    //{
                    //    BuildCommand(command, conn, trans, CommandType.Text, sql, null);

                    //    adapter.Fill(dt);
                    //    adapter.Dispose();
                    //}
                }
                else if(trans != null && conn == null)
                    conn = trans.Connection;

                BuildCommand(command, conn, trans, CommandType.Text, sql, null);
                adapter.Fill(dt);
                adapter.Dispose();
            }
            catch (DatabaseException ex)
            {
                adapter.Dispose();
                throw ex;
            }
            catch (SqlException ex)
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
        /// <param name="conn">The System.Data.SqlClent.SqlConnection object.</param>
        /// <returns>
        /// The first column of the first row in the result set as a .NET Framework data 
        /// type, or a null reference if the result set is empty or the result is a REFCURSOR.
        /// </returns>
        public static object ExecuteScalar(string sql, SqlConnection conn) { return ExecuteScalar(sql, conn, null); }
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query as a .NET Framework data type. Extra columns
        /// or rows are ignored.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="trans">The System.Data.SqlClent.SqlTransaction object.</param>
        /// <returns>
        /// The first column of the first row in the result set as a .NET Framework data 
        /// type, or a null reference if the result set is empty or the result is a REFCURSOR.
        /// </returns>
        public static object ExecuteScalar(string sql, SqlTransaction trans) { return ExecuteScalar(sql, null, trans); }
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query as a .NET Framework data type. Extra columns
        /// or rows are ignored.
        /// </summary>
        /// <param name="sql">SQL statement to execut against the database.</param>
        /// <param name="conn">The System.Data.SqlClent.SqlConnection object.</param>
        /// <param name="trans">The System.Data.SqlClent.SqlTransaction object.</param>
        /// <returns>
        /// The first column of the first row in the result set as a .NET Framework data 
        /// type, or a null reference if the result set is empty or the result is a REFCURSOR.
        /// </returns>
        public static object ExecuteScalar(string sql, SqlConnection conn, SqlTransaction trans)
        {
            SqlCommand command = new SqlCommand();
            object retval;
            try
            {
                if (trans == null)
                {
                    conn = new SqlConnection(ConnectionString);
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
            catch (SqlException ex)
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
        public static double GetNextID(string fldName, string tableName) { return GetNextID(fldName,tableName, null, null); }
        /// <summary>
        /// Executes the query, and returns the next ID of table.
        /// </summary>
        /// <param name="tableName">Table Name.</param>
        /// <param name="conn">The System.Data.SqlClent.SqlConnection object.</param>
        /// <returns>
        /// the next ID of table.
        /// </returns>
        public static double GetNextID(string fldName, string tableName, SqlConnection conn) { return GetNextID(fldName, tableName, conn, null); }
        /// <summary>
        /// Executes the query, and returns the next ID of table.
        /// </summary>
        /// <param name="tableName">Table Name.</param>
        /// <param name="trans">The System.Data.SqlClent.SqlTransaction object.</param>
        /// <returns>
        /// the next ID of table.
        /// </returns>
        public static double GetNextID(string fldName, string tableName, SqlTransaction trans) { return GetNextID(fldName,tableName, null, trans); }
        /// <summary>
        /// Executes the query, and returns the next ID of table.
        /// </summary>
        /// <param name="tableName">Table Name.</param>
        /// <param name="conn">The System.Data.SqlClent.SqlConnection object.</param>
        /// <param name="trans">The System.Data.SqlClent.SqlTransaction object.</param>
        /// <returns>
        /// the next ID of table.
        /// </returns>
        public static double GetNextID(string fldName,string tableName, SqlConnection conn, SqlTransaction trans)
        {
            double retval;
            string sql = "";

            SqlDataReader dataReader = ExecuteReader("SELECT MAX(" + fldName + ") MAXID FROM " + tableName, conn, trans);
            if (dataReader.Read())
                retval = Convert.ToInt64(dataReader["MAXID"]);
            else
                retval = 1;
            
            dataReader.Close();
            ExecuteNonQuery(sql, conn, trans);
            return retval;
        }

        public static string GetJDate() { return GetJDate(DateTime.Now); }
        public static string GetJDate(DateTime zdate)
        {
            return "1" + zdate.Year.ToString().Substring(2, 2) + zdate.DayOfYear.ToString("000");
        }

        #endregion

        public static string GetRunning(string tableName, string item, SqlTransaction trans)
        {
            double retVal = 1;
            DateTime runDate = DateTime.Today;
            string sql = "SELECT LASTID, RUNDATE FROM RUNNING WHERE TABLENAME = '" + tableName + "'";
            DataTable dt = SqlDB.ExecuteTable(sql, trans);
            if (dt.Rows.Count > 0)
            {
                retVal = Convert.ToDouble(dt.Rows[0]["LASTID"]);
                runDate = Convert.ToDateTime(dt.Rows[0]["RUNDATE"]);
                if (DateTime.Today.CompareTo(runDate.Date) != 0)
                {
                    retVal = 1;
                    runDate = DateTime.Today;
                }
                else
                    retVal += 1;
                sql = "UPDATE RUNNING SET LASTID = " + SqlDB.SetDouble(retVal) + ", " +
                    "RUNDATE = " + SqlDB.SetDate(runDate) +
                    "WHERE TABLENAME = '" + tableName + "'";
                SqlDB.ExecuteNonQuery(sql, trans);
            }
            else
            {
                sql = "INSERT INTO RUNNING(TABLENAME, LASTID, ITEM, RUNDATE) " +
                    "VALUES(" + SqlDB.SetString(tableName) + ", " +
                    "" + SqlDB.SetDouble(retVal) + ", " +
                    "" + SqlDB.SetString(item) + ", " +
                    "" + SqlDB.SetDate(runDate) + ") ";
                SqlDB.ExecuteNonQuery(sql, trans);
            }
            return item.ToUpper() + (runDate.Year + 543).ToString().Substring(2, 2) + runDate.ToString("MMdd") + retVal.ToString("0000");
        }

    }
}
