using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GenerateC.Utilities;
using GenerateC.Data;

namespace GenerateC.Flow
{
    public class BaseGenerateFlow
    {
        private string primaryKeyField = "";
        private string primaryKeyType = "";
        protected bool _isView = false;
        protected string _databaseType = "Sql";
        protected DataTable _columnTable = new DataTable();
        protected DataTable _pkColumnTable = new DataTable();
        protected DataTable _uniqueColumnTable = new DataTable();
        protected string _tableName = "";

        
        protected string GenerateLinqCode(GenerateData data)
        {
            string ret = "";
            ret = "using System;";
            ret += "\nusing System.Data;";
            ret += "\nusing System.Data." + _databaseType + "Client;";
            ret += "\nusing DB = " + data.ProjectName + ".DAL.Utilities." + _databaseType + "DB;";
            ret += "\nusing " + data.ProjectName + ".Data.Common.Utilities;";
            ret += "\n";
            ret += "\nnamespace " + data.NameSpaces;
            ret += "\n{";
            ret += "\n    /// <summary>";
            ret += "\n    /// Represents a transaction for " + data.TableName + " " + (_isView ? "view" : "table") + ".";
            ret += "\n    /// [Created by " + data.UserHostName + " on " + Constant.GetFullDate() + "]";
            ret += "\n    /// </summary>";
            ret += "\n    public class " + data.ClassName + "DAL";
            ret += "\n    {";
            ret += "\n        public " + data.ClassName + "DAL()";
            ret += "\n        {";
            ret += "\n";
            ret += "\n        }";
            ret += "\n";
            ret += "\n        /// <summary>" + data.TableName + "</summary>";
            ret += "\n        private const string " + (_isView ? "view" : "table") + "Name = \"" + data.TableName + "\";";
            if (!_isView) ret += "\n        int _deletedRow = 0;";
            ret += "\n";
            ret += GenerateCommonVariables();
            ret += "\n";
            ret += GenerateFieldVariables();
            ret += "\n";
            ret += GenerateClearVariables();
            ret += "\n";
            ret += GeneratePrivateMethods();
            ret += "\n";
            ret += GenerateAppendDataMethod();
            ret += "\n";
            ret += GenerateSQL();
            ret += "\n";
            
            ret += "\n    }";
            ret += "\n}";
            return ret;
        }

        protected string GenerateParaCode(GenerateData data)
        {
            string ret = "using System;";
            ret += "\nusing System.Data.Linq;";
            ret += "\nusing System.Data.Linq.Mapping;";
            ret += "\nusing System.ComponentModel;";
            ret += "\n";
            ret += "\nnamespace " + data.NameSpaces;
            ret += "\n{";
            ret += "\n    /// <summary>";
            ret += "\n    /// Represents a " + data.TableName + " Parameter.";
            ret += "\n    /// [Created by " + data.UserHostName + " on " + Constant.GetFullDate() + "]";
            ret += "\n    /// </summary>";
            ret += "\n    [Table(Name = \"" + data.TableName + "\")]";
            ret += "\n    public class " + data.ClassName + "Para";
            ret += "\n    {";
            ret += GenerateFieldVariables();
            ret += "\n    }";
            ret += "\n}";
            return ret;
        }

        private string GetStatement(string dataType, string columnName)
        {
            string ret = "";

            switch (dataType.ToUpper())
            {
                case "DATE":
                    ret = "DB.SetDateTime(" + columnName + ")";
                    break;
                case "DATETIME":
                    ret = "DB.SetDateTime(" + columnName + ")";
                    break;
                case "DATETIME2":
                    ret = "DB.SetDateTime(" + columnName + ")";
                    break;
                case "SMALLDATETIME":
                    ret = "DB.SetDateTime(" + columnName + ")";
                    break;
                case "BIGINT":
                    ret = "DB.SetDouble(" + columnName + ")";
                    break;
                case "DOUBLE":
                    ret = "DB.SetDouble(" + columnName + ")";
                    break;
                case "FLOAT":
                    ret = "DB.SetDouble(" + columnName + ")";
                    break;
                case "INT":
                    ret = "DB.SetDouble(" + columnName + ")";
                    break;
                case "MONEY":
                    ret = "DB.SetDouble(" + columnName + ")";
                    break;
                case "NUMBER":
                    ret = "DB.SetDouble(" + columnName + ")";
                    break;
                case "NUMERIC":
                    ret = "DB.SetDouble(" + columnName + ")";
                    break;
                case "SMALLINT":
                    ret = "DB.SetDouble(" + columnName + ")";
                    break;
                case "BIT":
                    ret = "DB.SetBoolean(" + columnName + ")";
                    break;
                case "DECIMAL":
                    ret = "DB.SetDouble(" + columnName + ")";
                    break;
                default:
                    ret = "DB.SetString(" + columnName + ")";
                    break;
            }

            return ret;
        }

        #region Public Method generate

        private string GetDataType(string dataType)
        {
            string ret = "";
            switch (dataType.ToUpper())
            {
                case "DATE":
                case "DATETIME":
                case "SMALLDATETIME":
                    ret = "DateTime";
                    break;

                case "BIGINT":
                case "DOUBLE":
                case "FLOAT":
                case "INT":
                case "MONEY":
                case "NUMBER":
                case "NUMERIC":
                case "REAL":
                case "SMALLINT":
                    ret = "double";
                    break;

                case "BIT":
                    ret = "bool";
                    break;

                case "DECIMAL":
                    ret = "decimal";
                    break;

                default:
                    ret = "string";
                    break;
            }
            return ret;
        }

        private string GenerateAppendDataMethod()
        {
            string primaryField = "";
            string ret = "\n        #region Public Methods";
            ret += "\n";
            ret += "\n        /// <summary>";
            ret += "\n        /// Executes the select statement with the specified condition and return a System.Data.DataTable.";
            ret += "\n        /// </summary>";
            ret += "\n        /// <param name=\"whereClause\">The condition for execute select statement.</param>";
            ret += "\n        /// <param name=\"orderBy\">The fields for sort data.</param>";
            ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
            ret += "\n        /// <returns>The System.Data.DataTable object for specified condition.</returns>";
            ret += "\n        public DataTable GetDataList(string whereClause, string orderBy, " + _databaseType + "Transaction trans)";
            ret += "\n        {";
            ret += "\n            return DB.ExecuteTable(sqlSelect + (whereClause == \"\" ? \"\" : \"WHERE \" + whereClause + \" \") + (orderBy == \"\" ? \"\" : \"ORDER BY \" + " + (_databaseType == "Oracle" ? "DB.SetSortString(orderBy)" : "orderBy") + "), trans);";
            ret += "\n        }";
            ret += "\n";
            ret += "\n";
            ret += "\n        /// <summary>";
            ret += "\n        /// Executes the select statement with the specified condition and return a System.Data.DataTable.";
            ret += "\n        /// </summary>";
            ret += "\n        /// <param name=\"whereClause\">The condition for execute select statement.</param>";
            ret += "\n        /// <param name=\"orderBy\">The fields for sort data.</param>";
            ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
            ret += "\n        /// <returns>The System.Data.DataTable object for specified condition.</returns>";
            ret += "\n        public DataTable GetListBySql(string Sql, string orderBy, " + _databaseType + "Transaction trans)";
            ret += "\n        {";
            ret += "\n            return DB.ExecuteTable(Sql + (orderBy == \"\" ? \"\" : \"ORDER BY \" + " + (_databaseType == "Oracle" ? "DB.SetSortString(orderBy)" : "orderBy") + "), trans);";
            ret += "\n        }";
            ret += "\n";
            ret += "\n";

            if (!_isView)
            {
                DataRow dRow = _pkColumnTable.Rows[0];
                primaryField = dRow["COLUMN_NAME"].ToString();

                primaryKeyField = primaryField;
                primaryKeyType = dRow["TYPE_NAME"].ToString();
                ret += "\n        /// <summary>";
                ret += "\n        /// Returns an indication whether the current data is inserted into " + _tableName + " " + (_isView ? "view" : "table") + " successfully.";
                ret += "\n        /// </summary>";
                ret += "\n        /// <param name=\"userID\">The current user.</param>";
                ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
                ret += "\n        /// <returns>true if insert data successfully; otherwise, false.</returns>";
                ret += "\n        public bool InsertData(string userID, " + _databaseType + "Transaction trans)";
                ret += "\n        {";
                if(_databaseType.ToUpper() == Constant.DatabaseType.SQL)
                    ret += "\n            _" + primaryField + " = DB.GetNextID(\"" + primaryField + "\",tableName, trans);";
                else
                    ret += "\n            _" + primaryField + " = DB.GetNextID(\"SQ\" + tableName, trans);";
                
                ret += "\n            _" + Constant.FieldName.CREATE_BY + " = userID;";
                ret += "\n            _" + Constant.FieldName.CREATE_ON + " = DateTime.Now;";
                ret += "\n            return doInsert(trans);";
                ret += "\n        }";
                ret += "\n";
                ret += "\n        /// <summary>";
                ret += "\n        /// Returns an indication whether the current data is updated to " + _tableName + " " + (_isView ? "view" : "table") + " successfully.";
                ret += "\n        /// </summary>";
                ret += "\n        /// <param name=\"userID\">The current user.</param>";
                ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
                ret += "\n        /// <returns>true if update data successfully; otherwise, false.</returns>";
                ret += "\n        public bool UpdateByPK(string userID, " + _databaseType + "Transaction trans)";
                ret += "\n        {";
                ret += "\n            _" + Constant.FieldName.UPDATE_BY + " = userID;";
                ret += "\n            _" + Constant.FieldName.UPDATE_ON + " = DateTime.Now;";
                ret += "\n            return doUpdate(\"" + primaryField + " = \" + " + GetStatement(dRow["TYPE_NAME"].ToString(), "_" + primaryField) + " + \" \", trans);";
                ret += "\n        }";
                ret += "\n";
                ret += "\n        /// <summary>";
                ret += "\n        /// Returns an indication whether the current data is deleted from " + _tableName + " " + (_isView ? "view" : "table") + " successfully.";
                ret += "\n        /// </summary>";
                ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
                ret += "\n        /// <returns>true if delete data successfully; otherwise, false.</returns>";
                ret += "\n        public bool DeleteByPK(" + _databaseType + "Transaction trans)";
                ret += "\n        {";
                ret += "\n            return doDelete(\"" + primaryField + " = \" + " + GetStatement(dRow["TYPE_NAME"].ToString(), "_" + primaryField) + " + \" \", trans);";
                ret += "\n        }";
                ret += "\n";
                ret += "\n        /// <summary>";
                ret += "\n        /// Returns an indication whether the record of " + _tableName + " by specified " + primaryField + " key is retrieved successfully.";
                ret += "\n        /// </summary>";
                ret += "\n        /// <param name=\"c" + primaryField + "\">The " + primaryField + " key.</param>";
                ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
                ret += "\n        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>";
                ret += "\n        public bool ChkDataByPK(" + GetDataType(dRow["TYPE_NAME"].ToString()) + " c" + primaryField + ", " + _databaseType + "Transaction trans)";
                ret += "\n        {";
                ret += "\n            return doChkData(\"" + primaryField + " = \" + " + GetStatement(dRow["TYPE_NAME"].ToString(), "c" + primaryField) + " + \" \", trans);";
                ret += "\n        }";
                ret += "\n";

                foreach (DataRow rUQ in _uniqueColumnTable.Rows)
                {
                    if (rUQ["CONSTRAINT_TYPE"].ToString() == "U")
                    {
                        //string constraintName = rUQ["CONSTRAINT_NAME"].ToString();
                        string constraintName = GetConstraintName(rUQ["CONSTRAINT_KEYS"].ToString());
                        string paramType = GetParamType(rUQ["CONSTRAINT_KEYS"].ToString());
                        string assignType = GetAssignType(rUQ["CONSTRAINT_KEYS"].ToString());

                        ret += "\n        /// <summary>";
                        ret += "\n        /// Returns an indication whether the current data is updated to " + _tableName + " " + (_isView ? "view" : "table") + " successfully.";
                        ret += "\n        /// </summary>";
                        ret += "\n        /// <param name=\"userID\">The current user.</param>";
                        ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
                        ret += "\n        /// <returns>true if update data successfully; otherwise, false.</returns>";
                        ret += "\n        public bool UpdateBy" + constraintName + "(" + paramType + ",string userID, " + _databaseType + "Transaction trans)";
                        ret += "\n        {";
                        ret += "\n            _" + Constant.FieldName.UPDATE_BY + " = userID;";
                        ret += "\n            _" + Constant.FieldName.UPDATE_ON + " = DateTime.Now;";
                        ret += "\n            return doUpdate(" + assignType + ", trans);";
                        ret += "\n        }";
                        ret += "\n";
                        ret += "\n        /// <summary>";
                        ret += "\n        /// Returns an indication whether the current data is deleted from " + _tableName + " " + (_isView ? "view" : "table") + " successfully.";
                        ret += "\n        /// </summary>";
                        ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
                        ret += "\n        /// <returns>true if delete data successfully; otherwise, false.</returns>";
                        ret += "\n        public bool DeleteBy" + constraintName + "(" + paramType + ", " + _databaseType + "Transaction trans)";
                        ret += "\n        {";
                        ret += "\n            return doDelete(" + assignType + ", trans);";
                        ret += "\n        }";
                        ret += "\n";
                        ret += "\n        /// <summary>";
                        ret += "\n        /// Returns an indication whether the record of " + _tableName + " by specified " + primaryField + " key is retrieved successfully.";
                        ret += "\n        /// </summary>";
                        ret += "\n        /// <param name=\"c" + primaryField + "\">The " + primaryField + " key.</param>";
                        ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
                        ret += "\n        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>";
                        ret += "\n        public bool ChkDataBy" + constraintName + "(" + paramType + ", " + _databaseType + "Transaction trans)";
                        ret += "\n        {";
                        ret += "\n            return doChkData(" + assignType + ", trans);";
                        ret += "\n        }";
                        ret += "\n";
                    }
                }
            }

            ret += "\n        /// <summary>";
            ret += "\n        /// Returns an indication whether the record of " + _tableName + " by specified " + primaryField + " key is retrieved successfully.";
            ret += "\n        /// </summary>";
            ret += "\n        /// <param name=\"c" + primaryField + "\">The " + primaryField + " key.</param>";
            ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
            ret += "\n        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>";
            ret += "\n        public bool ChkDataByWhere(string whText, " + _databaseType + "Transaction trans)";
            ret += "\n        {";
            ret += "\n            return doChkData(whText, trans);";
            ret += "\n        }";
            ret += "\n        #endregion";

            return ret;
        }

        #endregion

        #region Common

        private string GenerateCommonVariables()
        {
            string ret = "\n        #region Common Variables";
            ret += "\n";
            ret += "\n        string _error = \"\";";
            ret += "\n        string _information = \"\";";
            ret += "\n        bool _haveData = false;";
            ret += "\n";
            ret += "\n        public string " + (_isView ? "View" : "Table") + "Name";
            ret += "\n        {";
            ret += "\n            get { return " + (_isView ? "view" : "table") + "Name; }";
            ret += "\n        }";
            ret += "\n        public string ErrorMessage";
            ret += "\n        {";
            ret += "\n            get { return _error; }";
            ret += "\n        }";
            ret += "\n        public string InformationMessage";
            ret += "\n        {";
            ret += "\n            get { return _information; }";
            ret += "\n        }";
            ret += "\n        public bool HaveData";
            ret += "\n        {";
            ret += "\n            get { return _haveData; }";
            ret += "\n        }";
            ret += "\n";
            ret += "\n        #endregion";
            return ret;
        }

        #endregion

        private string GenerateClearVariables()
        {
            string ret = "\n        #region Common Variables";
            ret += "\n        /// <summary>";
            ret += "\n        /// Initialize data.";
            ret += "\n        /// </summary>";
            ret += "\n        private void ClearData()";
            ret += "\n        {";
            foreach (DataRow dRow in _columnTable.Rows)
            {
                string colName = "_" + dRow["COLUMN_NAME"].ToString().ToUpper();

                switch (dRow["TYPE_NAME"].ToString().ToUpper())
                {
                    case "DATE":
                        ret += "\n            " + colName + " = new DateTime(1,1,1);";
                        break;
                    case "DATETIME":
                        ret += "\n            " + colName + " = new DateTime(1,1,1);";
                        break;
                    case "DATETIME2":
                        ret += "\n            " + colName + " = new DateTime(1,1,1);";
                        break;
                    case "SMALLDATETIME":
                        ret += "\n            " + colName + " = new DateTime(1,1,1);";
                        break;
                    case "BIGINT":
                        ret += "\n            " + colName + " = 0;";
                        break;
                    case "DOUBLE":
                        ret += "\n            " + colName + " = 0;";
                        break;
                    case "FLOAT":
                        ret += "\n            " + colName + " = 0;";
                        break;
                    case "INT":
                        ret += "\n            " + colName + " = 0;";
                        break;
                    case "MONEY":
                        ret += "\n            " + colName + " = 0;";
                        break;
                    case "NUMBER":
                        ret += "\n            " + colName + " = 0;";
                        break;
                    case "NUMERIC":
                        ret += "\n            " + colName + " = 0;";
                        break;
                    case "SMALLINT":
                        ret += "\n            " + colName + " = 0;";
                        break;
                    case "BIT":
                        ret += "\n            " + colName + " = false;";
                        break;
                    case "DECIMAL":
                        ret += "\n            " + colName + " = 0;";
                        break;
                    default:
                        ret += "\n            " + colName + " = \"\";";
                        break;
                }
            }

            ret += "\n        }";
            ret += "\n        #endregion";

            return ret;
        }

        #region Field Variables

        private string GenerateFieldVariables()
        {
            string ret = "";
            string retField = "";
            string retProp = "";

            retField += "\n         #region GenerateFieldVariables";
            retField += "\n         //Generate Field List";

            foreach (DataRow dRow in _columnTable.Rows)
            {
                string vTypeName = dRow["TYPE_NAME"].ToString().ToUpper();
                string retFieldType = "";
                string fieldDesc = "";
                string colName = "_" + dRow["COLUMN_NAME"].ToString().ToUpper();
                string isNullable = dRow["NULLABLE"].ToString();

                switch (dRow["TYPE_NAME"].ToString().ToUpper())
                {
                    case "DATE":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "DateTime");
                        fieldDesc = "DateTime";
                        retField += "\n         " + retFieldType + " " + colName + " = new DateTime(1,1,1);";
                        break;
                    case "DATETIME":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "DateTime");
                        fieldDesc = "DateTime";
                        retField += "\n         " + retFieldType + " " + colName + " = new DateTime(1,1,1);";
                        break;
                    case "DATETIME2":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "DateTime");
                        fieldDesc = "DateTime";
                        retField += "\n         " + retFieldType + " " + colName + " = new DateTime(1,1,1);";
                        break;
                    case "SMALLDATETIME":

                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "DateTime");
                        fieldDesc = "DateTime";
                        retField += "\n         " + retFieldType + " " + colName + " = new DateTime(1,1,1);";
                        break;
                    case "BIGINT":

                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "double");
                        fieldDesc = "BigInt";
                        retField += "\n         " + retFieldType + " " + colName + " = 0;";
                        break;
                    case "DOUBLE":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "double");
                        fieldDesc = "Double";
                        retField += "\n         " + retFieldType + " " + colName + " = 0;";
                        break;
                    case "FLOAT":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "double");
                        fieldDesc = "Double";
                        retField += "\n         " + retFieldType + " " + colName + " = 0;";
                        break;
                    case "INT":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "long");
                        fieldDesc = "Long";
                        retField += "\n         " + retFieldType + " " + colName + " = 0;";
                        break;
                    case "MONEY":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "double");
                        fieldDesc = "Double";
                        retField += "\n         " + retFieldType + " " + colName + " = 0;";
                        break;
                    case "NUMBER":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "double");
                        fieldDesc = "Double";
                        retField += "\n         " + retFieldType + " " + colName + " = 0;";
                        break;
                    case "NUMERIC":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "double");
                        fieldDesc = "Double";
                        retField += "\n         " + retFieldType + " " + colName + " = 0;";
                        break;
                    case "SMALLINT":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "long");
                        fieldDesc = "Long";
                        retField += "\n         " + retFieldType + " " + colName + " = 0;";
                        break;
                    case "BIT":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "bool");
                        fieldDesc = "bool";
                        retField += "\n         " + retFieldType + " " + colName + " = false;";
                        break;
                    case "DECIMAL":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "double");
                        fieldDesc = "Double";
                        retField += "\n         " + retFieldType + " " + colName + " = 0;";
                        break;
                    case "CHAR":
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "string");
                        fieldDesc = "Char(" + dRow["LENGTH"].ToString() + ")";
                        retField += "\n         " + retFieldType + " " + colName + " = \"\";";
                        break;
                    default:
                        retFieldType = (isNullable == "1" ? GetNullable(dRow) : "string");
                        fieldDesc = "VarChar(" + dRow["LENGTH"].ToString() + ")";
                        retField += "\n         " + retFieldType + " " + colName + " = \"\";";
                        break;
                }

                //retFieldType = (isNullable=="1" ? GetNullable(dRow) : retFieldType);
                retProp += "\n        [Column(Name = \"" + dRow["COLUMN_NAME"].ToString().ToUpper() + "\"" + ", DbType=\"" + fieldDesc + (isNullable == "0" ? " NOT NULL\" , CanBeNull = false " : "\"") + GetIsPrimaryKey(dRow["COLUMN_NAME"].ToString().ToUpper()) + ")]";
                retProp += "\n        public " + retFieldType + " " + dRow["COLUMN_NAME"].ToString().ToUpper();
                retProp += "\n        {";
                retProp += "\n            get { return " + colName + "; }";
                if(!_isView)
                    retProp += "\n            set { " + colName + " = value; }";

                retProp += "\n        }";
                retProp += "\n";
            }

            ret = retField;
            ret += "\n";
            ret += "\n         //Generate Field Property ";
            ret += retProp;
            ret += "\n         #endregion";

            return ret;
        }
        #endregion

        #region SQL statement generate

        private string GenerateSQL()
        {
            string ret = "";
            string column = "";
            string field = "";
            ret += "\n        #region SQL Statements";
            ret += "\n";
            if (!_isView)
            {
                foreach (DataRow dRow in _columnTable.Rows)
                {
                    if (dRow["COLUMN_NAME"].ToString() != Constant.FieldName.UPDATE_ON && dRow["COLUMN_NAME"].ToString() != Constant.FieldName.UPDATE_BY)
                    {
                        column += (column == "" ? "" : ", ") + dRow["COLUMN_NAME"].ToString().ToUpper();
                        field += (field == "" ? "" : " + \", \";") + "\n                sql += " + GetStatement(dRow["TYPE_NAME"].ToString(), "_" + dRow["COLUMN_NAME"].ToString().ToUpper());
                    }
                }
                field += (field == "" ? "" : " + \" \";");

                ret += "\n        /// <summary>";
                ret += "\n        /// Gets the insert statement for " + _tableName + " table.";
                ret += "\n        /// </summary>";
                ret += "\n        private string sqlInsert";
                ret += "\n        {";
                ret += "\n            get";
                ret += "\n            {";
                ret += "\n                string sql = \"INSERT INTO \" + " + (_isView ? "view" : "table") + "Name + \"(" + column + ") \";";
                ret += "\n                sql += \"VALUES (\";";
                ret += field;
                ret += "\n                sql += \")\";";
                ret += "\n                return sql;";
                ret += "\n            }";
                ret += "\n        }";
                ret += "\n";

                field = "";
                foreach (DataRow dRow in _columnTable.Rows)
                {
                    if (dRow["COLUMN_NAME"].ToString() != Constant.FieldName.CREATE_ON && dRow["COLUMN_NAME"].ToString() != Constant.FieldName.CREATE_BY && dRow["COLUMN_NAME"].ToString() != primaryKeyField)
                    {
                        field += (field == "" ? "" : " + \", \";") + "\n                sql += \"" + dRow["COLUMN_NAME"].ToString().ToUpper() + " = \" + " + GetStatement(dRow["TYPE_NAME"].ToString(), "_" + dRow["COLUMN_NAME"].ToString().ToUpper());
                    }
                }
                field += (field == "" ? "" : " + \" \";");
                ret += "\n        /// <summary>";
                ret += "\n        /// Gets the update statement for " + _tableName + " table.";
                ret += "\n        /// </summary>";
                ret += "\n        private string sqlUpdate";
                ret += "\n        {";
                ret += "\n            get";
                ret += "\n            {";
                ret += "\n                string sql = \"UPDATE \" + " + (_isView ? "view" : "table") + "Name + \" SET \";";
                ret += field;
                ret += "\n                return sql;";
                ret += "\n            }";
                ret += "\n        }";
                ret += "\n";

                ret += "\n        /// <summary>";
                ret += "\n        /// Gets the delete statement for " + _tableName + " table.";
                ret += "\n        /// </summary>";
                ret += "\n        private string sqlDelete";
                ret += "\n        {";
                ret += "\n            get";
                ret += "\n            {";
                ret += "\n                string sql = \"DELETE FROM \" + " + (_isView ? "view" : "table") + "Name + \" \";";
                ret += "\n                return sql;";
                ret += "\n            }";
                ret += "\n        }";
                ret += "\n";
            }
            ret += "\n        /// <summary>";
            ret += "\n        /// Gets the select statement for " + _tableName + " table.";
            ret += "\n        /// </summary>";
            ret += "\n        private string sqlSelect";
            ret += "\n        {";
            ret += "\n            get";
            ret += "\n            {";
            ret += "\n                string sql = \"SELECT * FROM \" + " + (_isView ? "view" : "table") + "Name + \" \";";
            ret += "\n                return sql;";
            ret += "\n            }";
            ret += "\n        }";
            ret += "\n";
            ret += "\n        #endregion";
            return ret;
        }

        #endregion

        #region Private Methods generate

        private string GeneratePrivateMethods()
        {
            string ret = "";
            ret += "\n        #region Private Methods";
            ret += "\n";
            if (!_isView)
            {
                ret += "\n        /// <summary>";
                ret += "\n        /// Returns an indication whether the current data is inserted into " + _tableName + " " + (_isView ? "view" : "table") + " successfully.";
                ret += "\n        /// </summary>";
                ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
                ret += "\n        /// <returns>true if insert data successfully; otherwise, false.</returns>";
                ret += "\n        private bool doInsert(" + _databaseType + "Transaction trans)";
                ret += "\n        {";
                ret += "\n            bool ret = true;";
                ret += "\n            if (!_haveData)";
                ret += "\n            {";
                ret += "\n                try";
                ret += "\n                {";
                ret += "\n                    ret = (DB.ExecuteNonQuery(sqlInsert, trans) > 0);";
                ret += "\n                    if (!ret)";
                ret += "\n                        _error = MessageResources.MSGEN001;";
                ret += "\n                    else";
                ret += "\n                        _haveData = true;";
                ret += "\n                    _information = MessageResources.MSGIN001;";
                ret += "\n                }";
                ret += "\n                catch (ApplicationException ex)";
                ret += "\n                {";
                ret += "\n                    ret = false;";
                ret += "\n                    _error = ex.Message;";
                ret += "\n                }";
                ret += "\n                catch (Exception ex)";
                ret += "\n                {";
                ret += "\n                    ex.ToString();";
                ret += "\n                    ret = false;";
                ret += "\n                    _error = MessageResources.MSGEC101;";
                ret += "\n                }";
                ret += "\n            }";
                ret += "\n            else";
                ret += "\n            {";
                ret += "\n                ret = false;";
                ret += "\n                _error = MessageResources.MSGEN002;";
                ret += "\n            }";
                ret += "\n            return ret;";
                ret += "\n        }";
                ret += "\n";
                ret += "\n        /// <summary>";
                ret += "\n        /// Returns an indication whether the current data is updated to " + _tableName + " " + (_isView ? "view" : "table") + " successfully.";
                ret += "\n        /// </summary>";
                ret += "\n        /// <param name=\"whText\">The condition specify the updating record(s).</param>";
                ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
                ret += "\n        /// <returns>true if update data successfully; otherwise, false.</returns>";
                ret += "\n        private bool doUpdate(string whText, " + _databaseType + "Transaction trans)";
                ret += "\n        {";
                ret += "\n            bool ret = true;";
                ret += "\n            if (_haveData)";
                ret += "\n            {";
                ret += "\n                if (whText.Trim() != \"\")";
                ret += "\n                {";
                ret += "\n                    string tmpWhere = \" WHERE \" + whText;";
                ret += "\n                    try";
                ret += "\n                    {";
                ret += "\n                        ret = (DB.ExecuteNonQuery(sqlUpdate + tmpWhere, trans) > 0);";
                ret += "\n                        if (!ret) _error = MessageResources.MSGEU001;";
                ret += "\n                        _information = MessageResources.MSGIU001;";
                ret += "\n                    }";
                ret += "\n                    catch (ApplicationException ex)";
                ret += "\n                    {";
                ret += "\n                        ret = false;";
                ret += "\n                        _error = ex.Message;";
                ret += "\n                    }";
                ret += "\n                    catch (Exception ex)";
                ret += "\n                    {";
                ret += "\n                        ex.ToString();";
                ret += "\n                        ret = false;";
                ret += "\n                        _error = MessageResources.MSGEC102;";
                ret += "\n                    }";
                ret += "\n                }";
                ret += "\n                else";
                ret += "\n                {";
                ret += "\n                    ret = false;";
                ret += "\n                    _error = MessageResources.MSGEU003;";
                ret += "\n                }";
                ret += "\n            }";
                ret += "\n            else";
                ret += "\n            {";
                ret += "\n                ret = false;";
                ret += "\n                _error = MessageResources.MSGEU002;";
                ret += "\n            }";
                ret += "\n            return ret;";
                ret += "\n        }";
                ret += "\n";
                ret += "\n        /// <summary>";
                ret += "\n        /// Returns an indication whether the current data is deleted from " + _tableName + " " + (_isView ? "view" : "table") + " successfully.";
                ret += "\n        /// </summary>";
                ret += "\n        /// <param name=\"whText\">The condition specify the deleting record(s).</param>";
                ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
                ret += "\n        /// <returns>true if delete data successfully; otherwise, false.</returns>";
                ret += "\n        private bool doDelete(string whText, " + _databaseType + "Transaction trans)";
                ret += "\n        {";
                ret += "\n            bool ret = true;";
                ret += "\n            if (whText.Trim() != \"\")";
                ret += "\n            {";
                ret += "\n               if(doChkData(whText.Trim(), trans) == true){";
                ret += "\n                  string tmpWhere = \" WHERE \" + whText;";
                ret += "\n                  try";
                ret += "\n                  {";
                ret += "\n                      ret = (DB.ExecuteNonQuery(sqlDelete + tmpWhere, trans) > 0);";
                ret += "\n                      if (!ret) _error = MessageResources.MSGED001;";
                ret += "\n                      _information = MessageResources.MSGID001;";
                ret += "\n                  }";
                ret += "\n                  catch (ApplicationException ex)";
                ret += "\n                  {";
                ret += "\n                      ret = false;";
                ret += "\n                      _error = ex.Message;";
                ret += "\n                  }";
                ret += "\n                  catch (Exception ex)";
                ret += "\n                  {";
                ret += "\n                      ex.ToString();";
                ret += "\n                      ret = false;";
                ret += "\n                      _error = MessageResources.MSGEC103;";
                ret += "\n                  }";
                ret += "\n               }";
                ret += "\n            }";
                ret += "\n            else";
                ret += "\n            {";
                ret += "\n                ret = false;";
                ret += "\n                _error = MessageResources.MSGED003;";
                ret += "\n            }";
                ret += "\n            return ret;";
                ret += "\n        }";
                ret += "\n";
            }
            ret += "\n        /// <summary>";
            ret += "\n        /// Returns an indication whether the record of " + _tableName + " by specified condition is retrieved successfully.";
            ret += "\n        /// </summary>";
            ret += "\n        /// <param name=\"whText\">The condition specify the deleting record(s).</param>";
            ret += "\n        /// <param name=\"trans\">The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>";
            ret += "\n        /// <returns>true if data is retrieved successfully; otherwise, false.</returns>";
            ret += "\n        private bool doChkData(string whText, " + _databaseType + "Transaction trans)";
            ret += "\n        {";
            ret += "\n            bool ret = true;";
            ret += "\n            ClearData();";
            ret += "\n            _haveData = false;";
            ret += "\n            if (whText.Trim() != \"\")";
            ret += "\n            {";
            ret += "\n                string tmpWhere = \" WHERE \" + whText;";
            ret += "\n                " + _databaseType + "DataReader Rdr = null;";
            ret += "\n                try";
            ret += "\n                {";
            ret += "\n                    Rdr = DB.ExecuteReader(sqlSelect + tmpWhere, trans);";
            ret += "\n                    if (Rdr.Read())";
            ret += "\n                    {";
            ret += "\n                            _haveData = true;";
            foreach (DataRow dRow in _columnTable.Rows)
            {
                string colName = dRow["COLUMN_NAME"].ToString().ToUpper();
                switch (dRow["TYPE_NAME"].ToString().ToUpper())
                {
                    case "DATE":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDateTime(Rdr[\"" + colName + "\"]);";
                        break;
                    case "DATETIME":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDateTime(Rdr[\"" + colName + "\"]);";
                        break;
                    case "DATETIME2":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDateTime(Rdr[\"" + colName + "\"]);";
                        break;
                    case "SMALLDATETIME":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDateTime(Rdr[\"" + colName + "\"]);";
                        break;
                    case "BIGINT":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDouble(Rdr[\"" + colName + "\"]);";
                        break;
                    case "DOUBLE":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDouble(Rdr[\"" + colName + "\"]);";
                        break;
                    case "FLOAT":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDouble(Rdr[\"" + colName + "\"]);";
                        break;
                    case "INT":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDouble(Rdr[\"" + colName + "\"]);";
                        break;
                    case "MONEY":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDouble(Rdr[\"" + colName + "\"]);";
                        break;
                    case "NUMBER":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDouble(Rdr[\"" + colName + "\"]);";
                        break;
                    case "NUMERIC":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDouble(Rdr[\"" + colName + "\"]);";
                        break;
                    case "SMALLINT":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDouble(Rdr[\"" + colName + "\"]);";
                        break;
                    case "BIT":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToBoolean(Rdr[\"" + colName + "\"]);";
                        break;
                    case "DECIMAL":
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Convert.ToDouble(Rdr[\"" + colName + "\"]);";
                        break;
                    default:
                        ret += "\n                            if (!Convert.IsDBNull(Rdr[\"" + colName + "\"])) _" + colName + " = Rdr[\"" + colName + "\"].ToString();";
                        break;
                }
            }
            ret += "\n                    }";
            ret += "\n                    else";
            ret += "\n                    {";
            ret += "\n                        ret = false;";
            ret += "\n                        _error = MessageResources.MSGEV002;";
            ret += "\n                    }";
            ret += "\n                    Rdr.Close();";
            ret += "\n                }";
            ret += "\n                catch (Exception ex)";
            ret += "\n                {";
            ret += "\n                    ex.ToString();";
            ret += "\n                    ret = false;";
            ret += "\n                    _error = MessageResources.MSGEC104;";
            ret += "\n                    if (Rdr != null && !Rdr.IsClosed)";
            ret += "\n                        Rdr.Close();";
            ret += "\n                }";
            ret += "\n            }";
            ret += "\n            else";
            ret += "\n            {";
            ret += "\n                ret = false;";
            ret += "\n                _error = MessageResources.MSGEV001;";
            ret += "\n            }";
            ret += "\n            return ret;";
            ret += "\n        }";
            ret += "\n";
            ret += "\n        #endregion";
            return ret;
        }

        #endregion

        private string GetIsPrimaryKey(string columnName) {
            string ret = "";
            DataRow dRow = _pkColumnTable.Rows[0];
            if (columnName.ToUpper() == dRow["COLUMN_NAME"].ToString().ToUpper())
                ret = ", IsPrimaryKey = true";
            
            return ret;
        }

        private string GetNullable(DataRow dRow) {
            string ret = "";

            if (dRow["NULLABLE"].ToString() == "1"){   //ถ้าฟิลด์สามารถรับค่าเป็น Null ได้
                switch (dRow["TYPE_NAME"].ToString().ToUpper()){
                    case "DATE":
                        ret = " System.Nullable<System.DateTime> ";
                        break;
                    case "DATETIME":
                        ret = " System.Nullable<System.DateTime> ";
                        break;
                    case "DATETIME2":
                        ret = " System.Nullable<System.DateTime> ";
                        break;
                    case "SMALLDATETIME":
                        ret = " System.Nullable<System.DateTime> ";
                        break;
                    case "BIGINT":
                        ret = " System.Nullable<long> ";
                        break;
                    case "DOUBLE":
                        ret = " System.Nullable<double> ";
                        break;
                    case "FLOAT":
                        ret = " System.Nullable<double> ";
                        break;
                    case "INT":
                        ret = " System.Nullable<long> ";
                        break;
                    case "MONEY":
                        ret = " System.Nullable<double> ";
                        break;
                    case "NUMBER":
                        ret = " System.Nullable<double> ";
                        break;
                    case "NUMERIC":
                        ret = " System.Nullable<double> ";
                        break;
                    case "SMALLINT":
                        ret = " System.Nullable<long> ";
                        break;
                    case "BIT":
                        ret = " System.Nullable<bool> ";
                        break;
                    case "DECIMAL":
                        ret = " System.Nullable<double> ";
                        break;
                    case "CHAR":
                        ret = " string ";
                        break;
                    default:
                        ret = " string ";
                        break;
                }
            }

            return ret;
        }
        
        private string GetParamType(string constraintKeys)
        {
            string ret = "";
            if (constraintKeys.IndexOf(",") > -1)
            {
                string[] paramName = constraintKeys.Split(',');
                foreach (string parName in paramName)
                {
                    string constraintField = parName.Trim().ToUpper();
                    _columnTable.DefaultView.RowFilter = "COLUMN_NAME='" + constraintField + "'";
                    if (ret == "")
                        ret = GetDataType(_columnTable.DefaultView[0]["TYPE_NAME"].ToString()) + " c" + constraintField;
                    else
                        ret += "," + GetDataType(_columnTable.DefaultView[0]["TYPE_NAME"].ToString()) + " c" + constraintField;
                }
            }
            else
            {
                _columnTable.DefaultView.RowFilter = "COLUMN_NAME='" + constraintKeys.Trim().ToUpper() + "'";
                ret = GetDataType(_columnTable.DefaultView[0]["TYPE_NAME"].ToString()) + " c" + constraintKeys.ToUpper();
            }

            return ret;
        }

        private string GetAssignType(string constraintKeys)
        {
            string ret = "";
            if (constraintKeys.IndexOf(",") > -1)
            {
                string[] paramName = constraintKeys.Split(',');
                foreach(string parName in paramName)
                {
                    string constraintField = parName.Trim().ToUpper();
                    _columnTable.DefaultView.RowFilter = "COLUMN_NAME='" + constraintField + "'";

                    if (_columnTable.DefaultView.Count > 0)
                    {
                        string assType = constraintField + " = " + "\"" + " + " + GetStatement(_columnTable.DefaultView[0]["TYPE_NAME"].ToString(), "c" + constraintField.Trim());
                        if (ret == "")
                            ret = "\"" + assType;
                        else
                            ret += " + " + "\"" + " AND " + assType;
                    }
                }
            }
            else
            {
                _columnTable.DefaultView.RowFilter = "COLUMN_NAME='" + constraintKeys.Trim() + "'";
                ret = "\"" + constraintKeys.ToUpper() + " = " + "\"" + " + " + GetStatement(_columnTable.DefaultView[0]["TYPE_NAME"].ToString(), "c" + constraintKeys.Trim().ToUpper()) + " + " + "\"" + " " + "\"" + "";
            }
            return ret;
        }

        private string GetConstraintName(string constraintKeys)
        {
            string ret = "";
            if (constraintKeys.IndexOf(",") > -1)
            {
                _uniqueColumnTable.DefaultView.RowFilter = "constraint_keys='" + constraintKeys.Trim().ToUpper() + "'";
                //ret = _uniqueColumnTable.DefaultView[0]["CONSTRAINT_NAME"].ToString().ToUpper();
                ret = constraintKeys.Replace(",", "_").ToUpper();
            }
            else
                ret = constraintKeys.ToUpper();

            return ret;
        }

    }
}
