using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Text;
using Message = GenerateC.Utilities.MessageResources;


namespace GenerateC.Utilities
{
    /// <summary>
    /// Represents a common object in Oracle transaction management.
    /// </summary>
    public class OracleTransactionDB
    {
        #region Variables

        private string _error = "";
        private OracleConnection _conn;
        private OracleTransaction _trans;

        private string errorCommitTransaction = Message.MSGEC008;
        private string errorRollbackTransaction = Message.MSGEC008;
        private string errorCreateTransaction = Message.MSGEC008;

        /// <summary>
        /// Gets error message when operation in progress.
        /// </summary>
        public string ErrorMessage
        {
            get { return _error; }
        }
        /// <summary>
        /// Gets the current System.Data.OracleClient.OracleConnection.
        /// </summary>
        public OracleConnection Conn
        {
            get { return _conn; }
        }
        /// <summary>
        /// Gets the current System.Data.OracleClient.OracleTransaction.
        /// </summary>
        public OracleTransaction Trans
        {
            get { return _trans; }
        }

        #endregion

        /// <summary>
        /// Commit the SQL database transaction and close the connection to the database.
        /// </summary>
        /// <returns>Return true when process operation successfully, otherwise false.</returns>
        public bool CommitTransaction()
        {
            bool ret = true;
            try
            {
                if (_trans != null) { _trans.Commit(); }
                if (_conn != null) { _conn.Close(); }
            }
            catch (OracleException ex)
            {
                ret = false;
                _error = OracleDB.GetExceptionMessage(ex);
            }
            catch
            {
                ret = false;
                _error = errorCommitTransaction;
            }
            return ret;
        }

        /// <summary>
        /// Rolls back a transaction from a pending state and close the connection to the database.
        /// </summary>
        /// <returns>Return true when process operation successfully, otherwise false.</returns>
        public bool RollbackTransaction()
        {
            bool ret = true;
            try
            {
                if (_trans != null) { _trans.Rollback(); }
                if (_conn != null) { _conn.Close(); }
            }
            catch (OracleException ex)
            {
                ret = false;
                _error = OracleDB.GetExceptionMessage(ex);
            }
            catch
            {
                ret = false;
                _error = errorRollbackTransaction;
            }
            return ret;
        }

        /// <summary>
        /// Initializes a new opened instance of the System.Data.OracleClient.OracleConnection class and begins a transaction at a database with System.Data.IsolationLevel.ReadCommitted.
        /// </summary>
        /// <returns>Return yes when transaction is begined, otherwise no.</returns>
        public bool CreateTransaction()
        {
            bool ret = true;
            try
            {
                if (_conn == null) { _conn = OracleDB.GetConnection(); }
                _trans = _conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            }
            catch (OracleException ex)
            {
                ret = false;
                _error = OracleDB.GetExceptionMessage(ex);
            }
            catch
            {
                ret = false;
                _error = errorCreateTransaction;
            }
            return ret;
        }

        /// <summary>
        /// Initializes a new opened instance of the System.Data.OracleClient.OracleConnection class and begins a transaction at a database with System.Data.IsolationLevel.ReadCommitted.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Return yes when transaction is begined, otherwise no.</returns>
        public bool CreateTransaction(string connectionString)
        {
            bool ret = true;
            try
            {
                if (_conn == null) { _conn = OracleDB.GetConnection(connectionString); }
                _trans = _conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            }
            catch (OracleException ex)
            {
                ret = false;
                _error = OracleDB.GetExceptionMessage(ex);
            }
            catch
            {
                ret = false;
                _error = errorCreateTransaction;
            }
            return ret;
        }

    }
}
