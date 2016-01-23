using System;
using System.Data;
using System.Data.OracleClient;
using DB = GenerateC.Utilities.OracleDB;
using GenerateC.Utilities;

namespace DAL.Illegal
{
    /// <summary>
    /// Represents a transaction for DISTRICT table.
    /// [Created by 127.0.0.1 on June,21 2010]
    /// </summary>
    public class DistrictDAL
    {
        public DistrictDAL()
        {

        }

        /// <summary>DISTRICT</summary>
        private const string tableName = "DISTRICT";
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
        double _CREATE_BY = 0;
        DateTime _CREATE_ON = new DateTime(1, 1, 1);
        string _DISTRICT_CODE = "";
        string _DISTRICT_NAME = "";
        double _ID = 0;
        string _OFFCODE = "";
        string _PROVINCE_CODE = "";
        double _UPDATE_BY = 0;
        DateTime _UPDATE_ON = new DateTime(1, 1, 1);

        //Generate Field Property 
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
        public string DISTRICT_CODE
        {
            get { return _DISTRICT_CODE; }
            set { _DISTRICT_CODE = value; }
        }
        public string DISTRICT_NAME
        {
            get { return _DISTRICT_NAME; }
            set { _DISTRICT_NAME = value; }
        }
        public double ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string OFFCODE
        {
            get { return _OFFCODE; }
            set { _OFFCODE = value; }
        }
        public string PROVINCE_CODE
        {
            get { return _PROVINCE_CODE; }
            set { _PROVINCE_CODE = value; }
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
            _CREATE_BY = 0;
            _CREATE_ON = new DateTime(1, 1, 1);
            _DISTRICT_CODE = "";
            _DISTRICT_NAME = "";
            _ID = 0;
            _OFFCODE = "";
            _PROVINCE_CODE = "";
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
                        if (!Convert.IsDBNull(Rdr["CREATE_BY"])) _CREATE_BY = Convert.ToDouble(Rdr["CREATE_BY"]);
                        if (!Convert.IsDBNull(Rdr["CREATE_ON"])) _CREATE_ON = Convert.ToDateTime(Rdr["CREATE_ON"]);
                        if (!Convert.IsDBNull(Rdr["DISTRICT_CODE"])) _DISTRICT_CODE = Rdr["DISTRICT_CODE"].ToString();
                        if (!Convert.IsDBNull(Rdr["DISTRICT_NAME"])) _DISTRICT_NAME = Rdr["DISTRICT_NAME"].ToString();
                        if (!Convert.IsDBNull(Rdr["ID"])) _ID = Convert.ToDouble(Rdr["ID"]);
                        if (!Convert.IsDBNull(Rdr["OFFCODE"])) _OFFCODE = Rdr["OFFCODE"].ToString();
                        if (!Convert.IsDBNull(Rdr["PROVINCE_CODE"])) _PROVINCE_CODE = Rdr["PROVINCE_CODE"].ToString();
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
        public bool InsertData(string userID, OracleTransaction trans)
        {
            _ID = DB.GetNextID("SQ" + tableName, trans);
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
        public bool UpdateByDISTRICT_CODE(string cDISTRICT_CODE, string userID, OracleTransaction trans)
        {
            //_UPDATE_BY = userID;
            //_UPDATE_ON = DateTime.Now;
            return doUpdate("DISTRICT_CODE = " + DB.SetString(cDISTRICT_CODE) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is deleted from  table successfully.
        /// </summary>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if delete data successfully; otherwise, false.</returns>
        public bool DeleteByDISTRICT_CODE(string cDISTRICT_CODE, OracleTransaction trans)
        {
            return doDelete("DISTRICT_CODE = " + DB.SetString(cDISTRICT_CODE) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the record of  by specified ID key is retrieved successfully.
        /// </summary>
        /// <param name="cID">The ID key.</param>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        public bool ChkDataByDISTRICT_CODE(string cDISTRICT_CODE, OracleTransaction trans)
        {
            return doChkData("DISTRICT_CODE = " + DB.SetString(cDISTRICT_CODE) + " ", trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is updated to  table successfully.
        /// </summary>
        /// <param name="userID">The current user.</param>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if update data successfully; otherwise, false.</returns>
        public bool UpdateByAK_KEY_3_DISTRICTD(string cDISTRICT_NAME, string cPROVINCE_CODE, string userID, OracleTransaction trans)
        {
            //_UPDATE_BY = userID;
            //_UPDATE_ON = DateTime.Now;
            return doUpdate("DISTRICT_NAME = " + DB.SetString(cDISTRICT_NAME) + " AND PROVINCE_CODE = " + DB.SetString(cPROVINCE_CODE), trans);
        }

        /// <summary>
        /// Returns an indication whether the current data is deleted from  table successfully.
        /// </summary>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if delete data successfully; otherwise, false.</returns>
        public bool DeleteByAK_KEY_3_DISTRICTD(string cDISTRICT_NAME, string cPROVINCE_CODE, OracleTransaction trans)
        {
            return doDelete("DISTRICT_NAME = " + DB.SetString(cDISTRICT_NAME) + " AND PROVINCE_CODE = " + DB.SetString(cPROVINCE_CODE), trans);
        }

        /// <summary>
        /// Returns an indication whether the record of  by specified ID key is retrieved successfully.
        /// </summary>
        /// <param name="cID">The ID key.</param>
        /// <param name="trans">The System.Data.OracleClient.OracleTransaction used by this System.Data.OracleClient.OracleCommand.</param>
        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        public bool ChkDataByAK_KEY_3_DISTRICTD(string cDISTRICT_NAME, string cPROVINCE_CODE, OracleTransaction trans)
        {
            return doChkData("DISTRICT_NAME = " + DB.SetString(cDISTRICT_NAME) + " AND PROVINCE_CODE = " + DB.SetString(cPROVINCE_CODE), trans);
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
                string sql = "INSERT INTO " + tableName + "(CREATE_BY, CREATE_ON, DISTRICT_CODE, DISTRICT_NAME, ID, OFFCODE, PROVINCE_CODE) ";
                sql += "VALUES (";
                sql += DB.SetDouble(_CREATE_BY) + ", ";
                sql += DB.SetDateTime(_CREATE_ON) + ", ";
                sql += DB.SetString(_DISTRICT_CODE) + ", ";
                sql += DB.SetString(_DISTRICT_NAME) + ", ";
                sql += DB.SetDouble(_ID) + ", ";
                sql += DB.SetString(_OFFCODE) + ", ";
                sql += DB.SetString(_PROVINCE_CODE) + " ";
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
                sql += "DISTRICT_CODE = " + DB.SetString(_DISTRICT_CODE) + ", ";
                sql += "DISTRICT_NAME = " + DB.SetString(_DISTRICT_NAME) + ", ";
                sql += "OFFCODE = " + DB.SetString(_OFFCODE) + ", ";
                sql += "PROVINCE_CODE = " + DB.SetString(_PROVINCE_CODE) + ", ";
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