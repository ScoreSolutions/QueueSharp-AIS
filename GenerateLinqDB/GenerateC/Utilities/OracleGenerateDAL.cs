using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GenerateC.Utilities;
using Message = GenerateC.Utilities.MessageResources;

namespace GenerateC.Utilities
{
    public class OracleGenerateDAL
    {
        private string _DatabaseName = "";
        private string _DataSource = "";
        private string _UserID = "";
        private string _Password = "";
        private string _TableName = "";
        private string _DatabaseType = "Oracle";
        private string _ErrorTableNotFound = Message.MSGEC014;

        public string DatabaseType
        {
            get { return _DatabaseType; }
            set { _DatabaseType = value; }
        }

        public string DatabaseName
        {
            set { _DatabaseName = value; }
        }

        public string DataSource
        {
            set { _DataSource = value; }
        }

        public string UserID
        {
            set { _UserID = value; }
        }

        public string Password
        {
            set { _Password = value; }
        }

        public string TableName
        {
            set { _TableName = value.ToUpper(); }
        }

        /// <summary>
        /// Get the connection string for the current application's default configuration.
        /// </summary>
        private string ConnectionString
        {
            get
            {
                return "Data Source=" + _DataSource + ";Persist Security Info=True;User ID=" + _UserID + ";Password=" + _Password + ";Unicode=True";
            }
        }

        public DataTable GetUniqueColumn()
        {
            string sql = "SELECT A.COLUMN_NAME, B.CONSTRAINT_TYPE, C.DATA_TYPE TYPE_NAME, A.CONSTRAINT_NAME ";
            sql += "FROM USER_CONS_COLUMNS A INNER JOIN USER_CONSTRAINTS B ON A.CONSTRAINT_NAME = B.CONSTRAINT_NAME ";
            sql += "INNER JOIN USER_TAB_COLUMNS C ON C.TABLE_NAME = A.TABLE_NAME AND C.COLUMN_NAME = A.COLUMN_NAME ";
            sql += "WHERE A.TABLE_NAME = " + OracleDB.SetString(_TableName) + " AND B.CONSTRAINT_TYPE = 'U' ";
            sql += "ORDER BY B.CONSTRAINT_TYPE, A.COLUMN_NAME";

            DataTable dt = OracleDB.ExecuteTable(sql, OracleDB.GetConnection(ConnectionString));
            DataTable ret = new DataTable();
            ret.Columns.Add("constraint_keys");
            ret.Columns.Add("constraint_type");
            ret.Columns.Add("constraint_name");

            if (dt.Rows.Count > 0)
            {
                string constraintName = "";
                foreach (DataRow dRow in dt.Rows)
                {
                    if (constraintName != dRow["CONSTRAINT_NAME"].ToString().Trim().ToUpper())
                    {
                        constraintName = dRow["CONSTRAINT_NAME"].ToString().Trim().ToUpper();
                        dt.DefaultView.RowFilter = "CONSTRAINT_NAME = '" + constraintName + "'";
                        DataRow dr = ret.NewRow();
                        if (dt.DefaultView.Count > 1) //ถ้า Constraints นั้นมีฟิลด์ที่เกี่ยวข้องมากกว่า 1 ฟิลด์ ให้อ้างอิงจากชื่อ Constraints
                        {
                            string constraintKeys = "";
                            for (int i = 0; i < dt.DefaultView.Count; i++)
                            {
                                if (constraintKeys == "")
                                    constraintKeys = dt.DefaultView[i]["COLUMN_NAME"].ToString();
                                else
                                    constraintKeys += "," + dt.DefaultView[i]["COLUMN_NAME"].ToString();
                            }
                            dr["constraint_keys"] = constraintKeys;
                            dr["constraint_type"] = "U";
                            dr["constraint_name"] = dRow["CONSTRAINT_NAME"].ToString().Trim().ToUpper();
                        }
                        else
                        {
                            dr["constraint_keys"] = dRow["COLUMN_NAME"].ToString();
                            dr["constraint_type"] = "U";
                            dr["constraint_name"] = dRow["COLUMN_NAME"].ToString();
                        }
                        ret.Rows.Add(dr);
                    }
                }
            }
            
            return ret;
        }

        public DataTable GetPKColumn()
        {
            string sql = "SELECT A.COLUMN_NAME, B.CONSTRAINT_TYPE, C.DATA_TYPE TYPE_NAME, A.CONSTRAINT_NAME ";
            sql += "FROM USER_CONS_COLUMNS A INNER JOIN USER_CONSTRAINTS B ON A.CONSTRAINT_NAME = B.CONSTRAINT_NAME ";
            sql += "INNER JOIN USER_TAB_COLUMNS C ON C.TABLE_NAME = A.TABLE_NAME AND C.COLUMN_NAME = A.COLUMN_NAME ";
            sql += "WHERE A.TABLE_NAME = " + OracleDB.SetString(_TableName) + " AND B.CONSTRAINT_TYPE = 'P' ";
            sql += "ORDER BY B.CONSTRAINT_TYPE, A.COLUMN_NAME";

            return OracleDB.ExecuteTable(sql, OracleDB.GetConnection(ConnectionString));
        }

        public DataTable GetTableColumn()
        {
            string sql = "SELECT COLUMN_NAME, DATA_TYPE TYPE_NAME ";
            sql += "FROM USER_TAB_COLUMNS WHERE TABLE_NAME = " + OracleDB.SetString(_TableName) + " ";
            sql += "ORDER BY COLUMN_NAME";
            DataTable dt = OracleDB.ExecuteTable(sql, OracleDB.GetConnection(ConnectionString));
            if (dt.Rows.Count == 0)
                throw new ApplicationException(String.Format(_ErrorTableNotFound, _TableName));
            else
                return dt;
        }

        public bool IsView()
        {
            string sql = "SELECT VIEW_NAME FROM USER_VIEWS WHERE VIEW_NAME = " + OracleDB.SetString(_TableName) + " ";
            return (OracleDB.ExecuteTable(sql, OracleDB.GetConnection(ConnectionString)).Rows.Count > 0);
        }
        public DataTable GetTableList()
        { 
            string Sql ="SELECT OBJECT_NAME TABLE_NAME, OBJECT_TYPE TABLE_TYPE";
            Sql += " FROM USER_OBJECTS ";
            Sql += " WHERE OBJECT_TYPE IN ('TABLE','VIEW')";
            Sql += " ORDER BY OBJECT_NAME";

            DataTable dt  = OracleDB.ExecuteTable(Sql, OracleDB.GetConnection(ConnectionString));
            DataTable dtList = new DataTable();
            dtList.Columns.Add("TABLE_NAME");
            dtList.Columns.Add("TABLE_VALUE");

            foreach (DataRow  dr  in dt.Rows)
            {
                DataRow drL  = dtList.NewRow();
                drL["TABLE_NAME"] = dr["TABLE_NAME"].ToString() + " # " + dr["TABLE_TYPE"].ToString();
                drL["TABLE_VALUE"] = dr["TABLE_NAME"].ToString();
                dtList.Rows.Add(drL);
            }
            return  dtList;
        }
    }
}
