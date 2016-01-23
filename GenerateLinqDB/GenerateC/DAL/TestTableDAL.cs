using System;
using System.Data;
using System.Data.SqlClient;
using DB = GenerateC.Utilities.SqlDB;
using GenerateC.Utilities;

namespace DAL.DST
{
    /// <summary>
    /// Represents a transaction for TEST_TABLE table.
    /// [Created by 127.0.0.1 on June,21 2010]
    /// </summary>
    public class TestTableDAL
    {
        public TestTableDAL()
        {

        }

        /// <summary>TEST_TABLE</summary>
        private const string tableName = "TEST_TABLE";
        int _deletedRow = 0;

        #region Common Variables

        string _error = "";
        string _information = "";
        bool _haveData = false;

        public string TableName
        {
            get { return tableName; }
        }
        public string ErrorMessage
        {
            get { return _error; }
        }
        public string InformationMessage
        {
            get { return _information; }
        }
        public bool HaveData
        {
            get { return _haveData; }
        }

        #endregion

        #region GenerateFieldVariables
        //Generate Field List
        double _ID = 0;
        string _STAFF_CODE = "";
        string _STAFF_NAME = "";
        string _LAST_NAME = "";
        double _DEPT_ID = 0;
        double _POS_ID = 0;

        //Generate Field Property 
        public double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string STAFF_CODE
        {
            get { return _STAFF_CODE; }
            set { _STAFF_CODE = value; }
        }
        public string STAFF_NAME
        {
            get { return _STAFF_NAME; }
            set { _STAFF_NAME = value; }
        }
        public string LAST_NAME
        {
            get { return _LAST_NAME; }
            set { _LAST_NAME = value; }
        }
        public double DEPT_ID
        {
            get { return _DEPT_ID; }
            set { _DEPT_ID = value; }
        }
        public double POS_ID
        {
            get { return _POS_ID; }
            set { _POS_ID = value; }
        }
        #endregion

        #region Common Variables
        /// <summary>
        /// Initialize data.
        /// </summary>
        private void ClearData()
        {
            _ID = 0;
            _STAFF_CODE = "";
            _STAFF_NAME = "";
            _LAST_NAME = "";
            _DEPT_ID = 0;
            _POS_ID = 0;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Returns an indication whether the current data is inserted into  table successfully.
        /// </summary>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if insert data successfully; otherwise, false.</returns>
        private bool doInsert(SqlTransaction trans)
        {
            bool ret = true;
            if (!_haveData)
            {
                try
                {
                    ret = (DB.ExecuteNonQuery(sqlInsert, trans) > 0);
                    if (!ret)
                        _error = MessageResources.MSGEN001;
                    else
                        _haveData = true;
                    _information = MessageResources.MSGIN001;
                }
                catch (ApplicationException ex)
                {
                    ret = false;
                    _error = ex.Message;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    ret = false;
                    _error = MessageResources.MSGEC101;
                }
            }
            else
            {
                ret = false;
                _error = MessageResources.MSGEN002;
            }
            return ret;
        }

        /// <summary>
        /// Returns an indication whether the current data is updated to  table successfully.
        /// </summary>
        /// <param name="whText">The condition specify the updating record(s).</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if update data successfully; otherwise, false.</returns>
        private bool doUpdate(string whText, SqlTransaction trans)
        {
            bool ret = true;
            if (_haveData)
            {
                if (whText.Trim() != "")
                {
                    string tmpWhere = " WHERE " + whText;
                    try
                    {
                        ret = (DB.ExecuteNonQuery(sqlUpdate + tmpWhere, trans) > 0);
                        if (!ret) _error = MessageResources.MSGEU001;
                        _information = MessageResources.MSGIU001;
                    }
                    catch (ApplicationException ex)
                    {
                        ret = false;
                        _error = ex.Message;
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        ret = false;
                        _error = MessageResources.MSGEC102;
                    }
                }
                else
                {
                    ret = false;
                    _error = MessageResources.MSGEU003;
                }
            }
            else
            {
                ret = false;
                _error = MessageResources.MSGEU002;
            }
            return ret;
        }

        /// <summary>
        /// Returns an indication whether the current data is deleted from  table successfully.
        /// </summary>
        /// <param name="whText">The condition specify the deleting record(s).</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if delete data successfully; otherwise, false.</returns>
        private bool doDelete(string whText, SqlTransaction trans)
        {
            bool ret = true;
            if (whText.Trim() != "")
            {
                string tmpWhere = " WHERE " + whText;
                try
                {
                    ret = (DB.ExecuteNonQuery(sqlDelete + tmpWhere, trans) > 0);
                    if (!ret) _error = MessageResources.MSGED001;
                    _information = MessageResources.MSGID001;
                }
                catch (ApplicationException ex)
                {
                    ret = false;
                    _error = ex.Message;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    ret = false;
                    _error = MessageResources.MSGEC103;
                }
            }
            else
            {
                ret = false;
                _error = MessageResources.MSGED003;
            }
            return ret;
        }

        /// <summary>
        /// Returns an indication whether the record of  by specified condition is retrieved successfully.
        /// </summary>
        /// <param name="whText">The condition specify the deleting record(s).</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        private bool doChkData(string whText, SqlTransaction trans)
        {
            bool ret = true;
            ClearData();
            _haveData = false;
            if (whText.Trim() != "")
            {
                string tmpWhere = " WHERE " + whText;
                SqlDataReader Rdr = null;
                try
                {
                    Rdr = DB.ExecuteReader(sqlSelect + tmpWhere, trans);
                    if (Rdr.Read())
                    {
                        _haveData = true;
                        if (!Convert.IsDBNull(Rdr["ID"])) _ID = Convert.ToDouble(Rdr["ID"]);
                        if (!Convert.IsDBNull(Rdr["STAFF_CODE"])) _STAFF_CODE = Rdr["STAFF_CODE"].ToString();
                        if (!Convert.IsDBNull(Rdr["STAFF_NAME"])) _STAFF_NAME = Rdr["STAFF_NAME"].ToString();
                        if (!Convert.IsDBNull(Rdr["LAST_NAME"])) _LAST_NAME = Rdr["LAST_NAME"].ToString();
                        if (!Convert.IsDBNull(Rdr["DEPT_ID"])) _DEPT_ID = Convert.ToDouble(Rdr["DEPT_ID"]);
                        if (!Convert.IsDBNull(Rdr["POS_ID"])) _POS_ID = Convert.ToDouble(Rdr["POS_ID"]);
                    }
                    else
                    {
                        ret = false;
                        _error = MessageResources.MSGEV002;
                    }
                    Rdr.Close();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    ret = false;
                    _error = MessageResources.MSGEC104;
                    if (Rdr != null && !Rdr.IsClosed)
                        Rdr.Close();
                }
            }
            else
            {
                ret = false;
                _error = MessageResources.MSGEV001;
            }
            return ret;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the select statement with the specified condition and return a System.Data.DataTable.
        /// </summary>
        /// <param name="whereClause">The condition for execute select statement.</param>
        /// <param name="orderBy">The fields for sort data.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>The System.Data.DataTable object for specified condition.</returns>
        public DataTable GetDataList(string whereClause, string orderBy, SqlTransaction trans)
        {
            return DB.ExecuteTable(sqlSelect + (whereClause == "" ? "" : "WHERE " + whereClause + " ") + (orderBy == "" ? "" : "ORDER BY " + orderBy), trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is inserted into  table successfully.
        /// </summary>
        /// <param name="userID">The current user.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if insert data successfully; otherwise, false.</returns>
        public bool InsertCurrentData(string userID, SqlTransaction trans)
        {
            _ID = DB.GetNextID("ID", TableName, trans);
            //_CREATE_BY = userID;
            //_CREATE_ON = DateTime.Now;
            return doInsert(trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is updated to  table successfully.
        /// </summary>
        /// <param name="userID">The current user.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if update data successfully; otherwise, false.</returns>
        public bool UpdateByID(string userID, SqlTransaction trans)
        {
            //_UPDATE_BY = userID;
            //_UPDATE_ON = DateTime.Now;
            return doUpdate("ID = " + DB.SetDouble(_ID) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is deleted from  table successfully.
        /// </summary>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if delete data successfully; otherwise, false.</returns>
        public bool DeleteByID(SqlTransaction trans)
        {
            return doDelete("ID = " + DB.SetDouble(_ID) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the record of  by specified ID key is retrieved successfully.
        /// </summary>
        /// <param name="cID">The ID key.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        public bool ChkDataByID(double cID, SqlTransaction trans)
        {
            return doChkData("ID = " + DB.SetDouble(cID) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is updated to  table successfully.
        /// </summary>
        /// <param name="userID">The current user.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if update data successfully; otherwise, false.</returns>
        public bool UpdateBySTAFF_CODE(string cSTAFF_CODE, string userID, SqlTransaction trans)
        {
            //_UPDATE_BY = userID;
            //_UPDATE_ON = DateTime.Now;
            return doUpdate("STAFF_CODE = " + DB.SetString(cSTAFF_CODE) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is deleted from  table successfully.
        /// </summary>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if delete data successfully; otherwise, false.</returns>
        public bool DeleteBySTAFF_CODE(string cSTAFF_CODE, SqlTransaction trans)
        {
            return doDelete("STAFF_CODE = " + DB.SetString(cSTAFF_CODE) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the record of  by specified ID key is retrieved successfully.
        /// </summary>
        /// <param name="cID">The ID key.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        public bool ChkDataBySTAFF_CODE(string cSTAFF_CODE, SqlTransaction trans)
        {
            return doChkData("STAFF_CODE = " + DB.SetString(cSTAFF_CODE) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is updated to  table successfully.
        /// </summary>
        /// <param name="userID">The current user.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if update data successfully; otherwise, false.</returns>
        public bool UpdateBySTAFF_NAME(string cSTAFF_NAME, string userID, SqlTransaction trans)
        {
            //_UPDATE_BY = userID;
            //_UPDATE_ON = DateTime.Now;
            return doUpdate("STAFF_NAME = " + DB.SetString(cSTAFF_NAME) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is deleted from  table successfully.
        /// </summary>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if delete data successfully; otherwise, false.</returns>
        public bool DeleteBySTAFF_NAME(string cSTAFF_NAME, SqlTransaction trans)
        {
            return doDelete("STAFF_NAME = " + DB.SetString(cSTAFF_NAME) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the record of  by specified ID key is retrieved successfully.
        /// </summary>
        /// <param name="cID">The ID key.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        public bool ChkDataBySTAFF_NAME(string cSTAFF_NAME, SqlTransaction trans)
        {
            return doChkData("STAFF_NAME = " + DB.SetString(cSTAFF_NAME) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is updated to  table successfully.
        /// </summary>
        /// <param name="userID">The current user.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if update data successfully; otherwise, false.</returns>
        public bool UpdateByIX_TEST_TABLE_2(double cDEPT_ID, double cPOS_ID, string userID, SqlTransaction trans)
        {
            //_UPDATE_BY = userID;
            //_UPDATE_ON = DateTime.Now;
            return doUpdate("DEPT_ID = " + DB.SetDouble(cDEPT_ID) + " AND POS_ID = " + DB.SetDouble(cPOS_ID), trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is deleted from  table successfully.
        /// </summary>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if delete data successfully; otherwise, false.</returns>
        public bool DeleteByIX_TEST_TABLE_2(double cDEPT_ID, double cPOS_ID, SqlTransaction trans)
        {
            return doDelete("DEPT_ID = " + DB.SetDouble(cDEPT_ID) + " AND POS_ID = " + DB.SetDouble(cPOS_ID), trans);
        }

        /// <summary>
        /// Returns an indication whether the record of  by specified ID key is retrieved successfully.
        /// </summary>
        /// <param name="cID">The ID key.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        public bool ChkDataByIX_TEST_TABLE_2(double cDEPT_ID, double cPOS_ID, SqlTransaction trans)
        {
            return doChkData("DEPT_ID = " + DB.SetDouble(cDEPT_ID) + " AND POS_ID = " + DB.SetDouble(cPOS_ID), trans);
        }

        /// <summary>
        /// Returns an indication whether the record of  by specified ID key is retrieved successfully.
        /// </summary>
        /// <param name="cID">The ID key.</param>
        /// <param name="trans">The System.Data.SqlClient.SqlTransaction used by this System.Data.SqlClient.SqlCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        public bool ChkDataByWhere(string whText, SqlTransaction trans)
        {
            return doChkData(whText, trans);
        }
        #endregion

        #region SQL Statements

        /// <summary>
        /// Gets the insert statement for  table.
        /// </summary>
        private string sqlInsert
        {
            get
            {
                string sql = "INSERT INTO " + tableName + "(ID, STAFF_CODE, STAFF_NAME, LAST_NAME, DEPT_ID, POS_ID) ";
                sql += "VALUES (";
                sql += DB.SetDouble(_ID) + ", ";
                sql += DB.SetString(_STAFF_CODE) + ", ";
                sql += DB.SetString(_STAFF_NAME) + ", ";
                sql += DB.SetString(_LAST_NAME) + ", ";
                sql += DB.SetDouble(_DEPT_ID) + ", ";
                sql += DB.SetDouble(_POS_ID) + " ";
                sql += ")";
                return sql;
            }
        }

        /// <summary>
        /// Gets the update statement for  table.
        /// </summary>
        private string sqlUpdate
        {
            get
            {
                string sql = "UPDATE " + tableName + " SET ";
                sql += "STAFF_CODE = " + DB.SetString(_STAFF_CODE) + ", ";
                sql += "STAFF_NAME = " + DB.SetString(_STAFF_NAME) + ", ";
                sql += "LAST_NAME = " + DB.SetString(_LAST_NAME) + ", ";
                sql += "DEPT_ID = " + DB.SetDouble(_DEPT_ID) + ", ";
                sql += "POS_ID = " + DB.SetDouble(_POS_ID) + " ";
                return sql;
            }
        }

        /// <summary>
        /// Gets the delete statement for  table.
        /// </summary>
        private string sqlDelete
        {
            get
            {
                string sql = "DELETE FROM " + tableName + " ";
                return sql;
            }
        }

        /// <summary>
        /// Gets the select statement for  table.
        /// </summary>
        private string sqlSelect
        {
            get
            {
                string sql = "SELECT * FROM " + tableName + " ";
                return sql;
            }
        }

        #endregion

    }
}