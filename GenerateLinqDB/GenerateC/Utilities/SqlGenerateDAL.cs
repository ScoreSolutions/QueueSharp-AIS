using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using GenerateC.Utilities;
using Message = GenerateC.Utilities.MessageResources;

namespace GenerateC.Utilities
{
    public class SqlGenerateDAL
    {
        private string _DataSource = "";
        private string _DatabaseName = "";
        private string _UserID = "";
        private string _Password = "";
        private string _TableName = "";
        private string _DatabaseType = "";
        private string _ErrorTableNotFound = Message.MSGEC014;

        public SqlGenerateDAL()
        {
        }

        public string DatabaseType
        {
            set { _DatabaseType = value; }
            get { return _DatabaseType; }
        }

        public string DataSource
        {
            set { _DataSource = value; }
        }

        public string DatabaseName
        {
            set { _DatabaseName = value; }
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
                return "Data Source=" + _DataSource + ";Initial Catalog=" + _DatabaseName + ";Persist Security Info=True;User ID=" + _UserID + ";password=" + _Password;
            }
        }

        public DataTable GetUQColumn()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("constraint_keys");
            dt.Columns.Add("constraint_type");
            dt.Columns.Add("constraint_name");

            SqlConnection conn = SqlDB.GetConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("EXEC SP_HELPCONSTRAINT " + SqlDB.SetString(_TableName), conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable tmpTable = ds.Tables[1];
            foreach (DataRow dRow in tmpTable.Rows)
            {
                DataRow dr = dt.NewRow();
                if (dRow["constraint_type"].ToString() == "UNIQUE (non-clustered)")
                { 
                    dr["constraint_keys"] = dRow["constraint_keys"].ToString();
                    dr["constraint_type"] = "U";
                    dr["constraint_name"] = dRow["constraint_name"].ToString();

                    dt.Rows.Add(dr);
                }
            }

            conn.Close();
            conn.Dispose();

            return dt;
        }

        public DataTable GetPKColumn()
        {
            DataTable uniqueTable = new DataTable();
            uniqueTable.Columns.Add("COLUMN_NAME");
            uniqueTable.Columns.Add("CONSTRAINT_TYPE");
            uniqueTable.Columns.Add("TYPE_NAME");
            uniqueTable.Columns.Add("PK_NAME");

            try
            {
                using (SqlConnection conn = SqlDB.GetConnection(ConnectionString))
                {
                    DataTable tmpTable = SqlDB.ExecuteTable("EXEC SP_PKEYS " + SqlDB.SetString(_TableName), conn);
                    foreach (DataRow dRow in tmpTable.Rows)
                    {
                        DataRow newRow = uniqueTable.NewRow();
                        newRow["COLUMN_NAME"] = dRow["COLUMN_NAME"].ToString();
                        newRow["CONSTRAINT_TYPE"] = "P";
                        newRow["PK_NAME"] = dRow["PK_NAME"].ToString();

                        DataTable typeTable = SqlDB.ExecuteTable("EXEC SP_COLUMNS " + SqlDB.SetString(_TableName), conn);
                        foreach (DataRow tRow in typeTable.Rows)
                        {
                            if (tRow["COLUMN_NAME"].ToString() == dRow["COLUMN_NAME"].ToString()) newRow["TYPE_NAME"] = tRow["TYPE_NAME"].ToString();
                        }
                        uniqueTable.Rows.Add(newRow);
                    }

                    conn.Close();
                    conn.Dispose();
                }
            }
            catch
            {
            }
            return uniqueTable;
        }

        public DataTable GetTableColumn()
        {
            string sql = "EXEC SP_COLUMNS " + SqlDB.SetString(_TableName);
            DataTable dt = SqlDB.ExecuteTable(sql, SqlDB.GetConnection(ConnectionString));
            dt.DefaultView.Sort = "COLUMN_NAME";
            if (dt.Rows.Count == 0)
                throw new ApplicationException(String.Format(_ErrorTableNotFound, _TableName));
            else
                return dt;
        }

        public bool IsView()
        {
            string Sql = "EXEC sp_tables \'" + _TableName + "\' ";
            DataTable dt = SqlDB.ExecuteTable(Sql, SqlDB.GetConnection(ConnectionString));
            if (dt.Rows.Count == 0)
                return false;
            else
                return dt.Rows[0]["TABLE_TYPE"].ToString() == "VIEW";
        }
        public DataTable GetTableList()
        {
            string Sql = "EXEC SP_TABLES null,null,'" + _DatabaseName + "'";
            DataTable dt = SqlDB.ExecuteTable(Sql,SqlDB.GetConnection(ConnectionString));
            DataTable dtList = new DataTable();
            dtList.Columns.Add("TABLE_NAME");
            dtList.Columns.Add("TABLE_VALUE");

            dt.DefaultView.Sort = "TABLE_NAME";

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["TABLE_OWNER"].ToString() == "dbo")
                {
                    DataRow drL = dtList.NewRow();
                    drL["TABLE_NAME"] = dr["TABLE_NAME"].ToString() + " # " + dr["TABLE_TYPE"].ToString();
                    drL["TABLE_VALUE"] = dr["TABLE_NAME"].ToString();
                    dtList.Rows.Add(drL);
                }
            }
            return dtList;

        }
    }
}
