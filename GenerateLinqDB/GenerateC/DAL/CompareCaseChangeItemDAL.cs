using System;
using System.Data;
using System.Data.OracleClient;
using DB = GenerateC.Utilities.OracleDB;
using GenerateC.Utilities;

namespace DAL.Illegal
{
    /// <summary>
    /// Represents a transaction for COMPARE_CASE_CHANGE_ITEM table.
    /// [Created by 127.0.0.1 on June,21 2010]
    /// </summary>
    public class CompareCaseChangeItemDAL
    {
        public CompareCaseChangeItemDAL()
        {

        }

        /// <summary>COMPARE_CASE_CHANGE_ITEM</summary>
        private const string tableName = "COMPARE_CASE_CHANGE_ITEM";
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
        double _APPLICATION_ARREST_LAWBREAKER = 0;
        string _AUTO_INV_NO = "";
        string _BOOK_NO = "";
        string _CANCEL_BY = "";
        DateTime _CANCEL_DATE = new DateTime(1, 1, 1);
        double _COMPARE_CASE_CHANGE_ID = 0;
        double _CREATE_BY = 0;
        DateTime _CREATE_ON = new DateTime(1, 1, 1);
        double _ID = 0;
        string _INV_NO = "";
        string _ISCANCEL = "";
        string _PAPER_NO = "";
        double _UPDATE_BY = 0;
        DateTime _UPDATE_ON = new DateTime(1, 1, 1);

        //Generate Field Property 
        public double APPLICATION_ARREST_LAWBREAKER
        {
            get { return _APPLICATION_ARREST_LAWBREAKER; }
            set { _APPLICATION_ARREST_LAWBREAKER = value; }
        }
        public string AUTO_INV_NO
        {
            get { return _AUTO_INV_NO; }
            set { _AUTO_INV_NO = value; }
        }
        public string BOOK_NO
        {
            get { return _BOOK_NO; }
            set { _BOOK_NO = value; }
        }
        public string CANCEL_BY
        {
            get { return _CANCEL_BY; }
            set { _CANCEL_BY = value; }
        }
        public DateTime CANCEL_DATE
        {
            get { return _CANCEL_DATE; }
            set { _CANCEL_DATE = value; }
        }
        public double COMPARE_CASE_CHANGE_ID
        {
            get { return _COMPARE_CASE_CHANGE_ID; }
            set { _COMPARE_CASE_CHANGE_ID = value; }
        }
        public double CREATE_BY
        {
            get { return _CREATE_BY; }
            set { _CREATE_BY = value; }
        }
        public DateTime CREATE_ON
        {
            get { return _CREATE_ON; }
            set { _CREATE_ON = value; }
        }
        public double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string INV_NO
        {
            get { return _INV_NO; }
            set { _INV_NO = value; }
        }
        public string ISCANCEL
        {
            get { return _ISCANCEL; }
            set { _ISCANCEL = value; }
        }
        public string PAPER_NO
        {
            get { return _PAPER_NO; }
            set { _PAPER_NO = value; }
        }
        public double UPDATE_BY
        {
            get { return _UPDATE_BY; }
            set { _UPDATE_BY = value; }
        }
        public DateTime UPDATE_ON
        {
            get { return _UPDATE_ON; }
            set { _UPDATE_ON = value; }
        }
        #endregion

        #region Common Variables
        /// <summary>
        /// Initialize data.
        /// </summary>
        private void ClearData()
        {
            _APPLICATION_ARREST_LAWBREAKER = 0;
            _AUTO_INV_NO = "";
            _BOOK_NO = "";
            _CANCEL_BY = "";
            _CANCEL_DATE = new DateTime(1, 1, 1);
            _COMPARE_CASE_CHANGE_ID = 0;
            _CREATE_BY = 0;
            _CREATE_ON = new DateTime(1, 1, 1);
            _ID = 0;
            _INV_NO = "";
            _ISCANCEL = "";
            _PAPER_NO = "";
            _UPDATE_BY = 0;
            _UPDATE_ON = new DateTime(1, 1, 1);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Returns an indication whether the current data is inserted into  table successfully.
        /// </summary>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if insert data successfully; otherwise, false.</returns>
        private bool doInsert(OracleTransaction trans)
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
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if update data successfully; otherwise, false.</returns>
        private bool doUpdate(string whText, OracleTransaction trans)
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
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if delete data successfully; otherwise, false.</returns>
        private bool doDelete(string whText, OracleTransaction trans)
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
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        private bool doChkData(string whText, OracleTransaction trans)
        {
            bool ret = true;
            ClearData();
            _haveData = false;
            if (whText.Trim() != "")
            {
                string tmpWhere = " WHERE " + whText;
                OracleDataReader Rdr = null;
                try
                {
                    Rdr = DB.ExecuteReader(sqlSelect + tmpWhere, trans);
                    if (Rdr.Read())
                    {
                        _haveData = true;
                        if (!Convert.IsDBNull(Rdr["APPLICATION_ARREST_LAWBREAKER"])) _APPLICATION_ARREST_LAWBREAKER = Convert.ToDouble(Rdr["APPLICATION_ARREST_LAWBREAKER"]);
                        if (!Convert.IsDBNull(Rdr["AUTO_INV_NO"])) _AUTO_INV_NO = Rdr["AUTO_INV_NO"].ToString();
                        if (!Convert.IsDBNull(Rdr["BOOK_NO"])) _BOOK_NO = Rdr["BOOK_NO"].ToString();
                        if (!Convert.IsDBNull(Rdr["CANCEL_BY"])) _CANCEL_BY = Rdr["CANCEL_BY"].ToString();
                        if (!Convert.IsDBNull(Rdr["CANCEL_DATE"])) _CANCEL_DATE = Convert.ToDateTime(Rdr["CANCEL_DATE"]);
                        if (!Convert.IsDBNull(Rdr["COMPARE_CASE_CHANGE_ID"])) _COMPARE_CASE_CHANGE_ID = Convert.ToDouble(Rdr["COMPARE_CASE_CHANGE_ID"]);
                        if (!Convert.IsDBNull(Rdr["CREATE_BY"])) _CREATE_BY = Convert.ToDouble(Rdr["CREATE_BY"]);
                        if (!Convert.IsDBNull(Rdr["CREATE_ON"])) _CREATE_ON = Convert.ToDateTime(Rdr["CREATE_ON"]);
                        if (!Convert.IsDBNull(Rdr["ID"])) _ID = Convert.ToDouble(Rdr["ID"]);
                        if (!Convert.IsDBNull(Rdr["INV_NO"])) _INV_NO = Rdr["INV_NO"].ToString();
                        if (!Convert.IsDBNull(Rdr["ISCANCEL"])) _ISCANCEL = Rdr["ISCANCEL"].ToString();
                        if (!Convert.IsDBNull(Rdr["PAPER_NO"])) _PAPER_NO = Rdr["PAPER_NO"].ToString();
                        if (!Convert.IsDBNull(Rdr["UPDATE_BY"])) _UPDATE_BY = Convert.ToDouble(Rdr["UPDATE_BY"]);
                        if (!Convert.IsDBNull(Rdr["UPDATE_ON"])) _UPDATE_ON = Convert.ToDateTime(Rdr["UPDATE_ON"]);
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
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>The System.Data.DataTable object for specified condition.</returns>
        public DataTable GetDataList(string whereClause, string orderBy, OracleTransaction trans)
        {
            return DB.ExecuteTable(sqlSelect + (whereClause == "" ? "" : "WHERE " + whereClause + " ") + (orderBy == "" ? "" : "ORDER BY " + DB.SetSortString(orderBy)), trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is inserted into  table successfully.
        /// </summary>
        /// <param name="userID">The current user.</param>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if insert data successfully; otherwise, false.</returns>
        public bool InsertCurrentData(string userID, OracleTransaction trans)
        {
            _ID = DB.GetNextID("SQ" + TableName, trans);
            //_CREATE_BY = userID;
            //_CREATE_ON = DateTime.Now;
            return doInsert(trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is updated to  table successfully.
        /// </summary>
        /// <param name="userID">The current user.</param>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if update data successfully; otherwise, false.</returns>
        public bool UpdateByID(string userID, OracleTransaction trans)
        {
            //_UPDATE_BY = userID;
            //_UPDATE_ON = DateTime.Now;
            return doUpdate("ID = " + DB.SetDouble(_ID) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is deleted from  table successfully.
        /// </summary>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if delete data successfully; otherwise, false.</returns>
        public bool DeleteByID(OracleTransaction trans)
        {
            return doDelete("ID = " + DB.SetDouble(_ID) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the record of  by specified ID key is retrieved successfully.
        /// </summary>
        /// <param name="cID">The ID key.</param>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        public bool ChkDataByID(double cID, OracleTransaction trans)
        {
            return doChkData("ID = " + DB.SetDouble(cID) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is updated to  table successfully.
        /// </summary>
        /// <param name="userID">The current user.</param>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if update data successfully; otherwise, false.</returns>
        public bool UpdateByAK_KEY_2_COMPAR43_(double cAPPLICATION_ARREST_LAWBREAKER, double cCOMPARE_CASE_CHANGE_ID, string userID, OracleTransaction trans)
        {
            //_UPDATE_BY = userID;
            //_UPDATE_ON = DateTime.Now;
            return doUpdate("APPLICATION_ARREST_LAWBREAKER = " + DB.SetDouble(cAPPLICATION_ARREST_LAWBREAKER) + " AND COMPARE_CASE_CHANGE_ID = " + DB.SetDouble(cCOMPARE_CASE_CHANGE_ID), trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is deleted from  table successfully.
        /// </summary>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if delete data successfully; otherwise, false.</returns>
        public bool DeleteByAK_KEY_2_COMPAR43_(double cAPPLICATION_ARREST_LAWBREAKER, double cCOMPARE_CASE_CHANGE_ID, OracleTransaction trans)
        {
            return doDelete("APPLICATION_ARREST_LAWBREAKER = " + DB.SetDouble(cAPPLICATION_ARREST_LAWBREAKER) + " AND COMPARE_CASE_CHANGE_ID = " + DB.SetDouble(cCOMPARE_CASE_CHANGE_ID), trans);
        }

        /// <summary>
        /// Returns an indication whether the record of  by specified ID key is retrieved successfully.
        /// </summary>
        /// <param name="cID">The ID key.</param>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        public bool ChkDataByAK_KEY_2_COMPAR43_(double cAPPLICATION_ARREST_LAWBREAKER, double cCOMPARE_CASE_CHANGE_ID, OracleTransaction trans)
        {
            return doChkData("APPLICATION_ARREST_LAWBREAKER = " + DB.SetDouble(cAPPLICATION_ARREST_LAWBREAKER) + " AND COMPARE_CASE_CHANGE_ID = " + DB.SetDouble(cCOMPARE_CASE_CHANGE_ID), trans);
        }

        /// <summary>
        /// Returns an indication whether the record of  by specified ID key is retrieved successfully.
        /// </summary>
        /// <param name="cID">The ID key.</param>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        public bool ChkDataByWhere(string whText, OracleTransaction trans)
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
                string sql = "INSERT INTO " + tableName + "(APPLICATION_ARREST_LAWBREAKER, AUTO_INV_NO, BOOK_NO, CANCEL_BY, CANCEL_DATE, COMPARE_CASE_CHANGE_ID, CREATE_BY, CREATE_ON, ID, INV_NO, ISCANCEL, PAPER_NO) ";
                sql += "VALUES (";
                sql += DB.SetDouble(_APPLICATION_ARREST_LAWBREAKER) + ", ";
                sql += DB.SetString(_AUTO_INV_NO) + ", ";
                sql += DB.SetString(_BOOK_NO) + ", ";
                sql += DB.SetString(_CANCEL_BY) + ", ";
                sql += DB.SetDateTime(_CANCEL_DATE) + ", ";
                sql += DB.SetDouble(_COMPARE_CASE_CHANGE_ID) + ", ";
                sql += DB.SetDouble(_CREATE_BY) + ", ";
                sql += DB.SetDateTime(_CREATE_ON) + ", ";
                sql += DB.SetDouble(_ID) + ", ";
                sql += DB.SetString(_INV_NO) + ", ";
                sql += DB.SetString(_ISCANCEL) + ", ";
                sql += DB.SetString(_PAPER_NO) + " ";
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
                sql += "APPLICATION_ARREST_LAWBREAKER = " + DB.SetDouble(_APPLICATION_ARREST_LAWBREAKER) + ", ";
                sql += "AUTO_INV_NO = " + DB.SetString(_AUTO_INV_NO) + ", ";
                sql += "BOOK_NO = " + DB.SetString(_BOOK_NO) + ", ";
                sql += "CANCEL_BY = " + DB.SetString(_CANCEL_BY) + ", ";
                sql += "CANCEL_DATE = " + DB.SetDateTime(_CANCEL_DATE) + ", ";
                sql += "COMPARE_CASE_CHANGE_ID = " + DB.SetDouble(_COMPARE_CASE_CHANGE_ID) + ", ";
                sql += "INV_NO = " + DB.SetString(_INV_NO) + ", ";
                sql += "ISCANCEL = " + DB.SetString(_ISCANCEL) + ", ";
                sql += "PAPER_NO = " + DB.SetString(_PAPER_NO) + ", ";
                sql += "UPDATE_BY = " + DB.SetDouble(_UPDATE_BY) + ", ";
                sql += "UPDATE_ON = " + DB.SetDateTime(_UPDATE_ON) + " ";
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