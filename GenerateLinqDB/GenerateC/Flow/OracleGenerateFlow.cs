using System;
using System.Collections.Generic;
using System.Text;
using GenerateC.Utilities;
using GenerateC.Data;
using System.Data;

namespace GenerateC.Flow
{
    public class OracleGenerateFlow : BaseGenerateFlow
    {
        OracleGenerateDAL _dal;

        private OracleGenerateDAL DALObj
        {
            get { if (_dal == null) { _dal = new OracleGenerateDAL(); } return _dal; }
        }

        private void SetData(GenerateData data)
        {
            DALObj.DataSource = data.DataSource;
            DALObj.DatabaseName = data.DatabaseName;
            DALObj.UserID = data.UserID;
            DALObj.Password = data.Password;
            DALObj.TableName = data.TableName;
            _tableName = data.TableName;
            _databaseType = DALObj.DatabaseType;
            _isView = DALObj.IsView();
            _columnTable = DALObj.GetTableColumn();
            
            if (!_isView)
            {
                _uniqueColumnTable = DALObj.GetUniqueColumn();
                _pkColumnTable = DALObj.GetPKColumn();
            }
        }
        private void SetConnDesc(GenerateC.Data.GenerateData data)
        {
            DALObj.DataSource = data.DataSource;
            DALObj.DatabaseName = data.DatabaseName;
            DALObj.UserID = data.UserID;
            DALObj.Password = data.Password;
            DALObj.DatabaseType = data.DatabaseType;
            _databaseType = DALObj.DatabaseType;
        }

        public string GenerateLinq(GenerateData data)
        {
            SetData(data);
            return GenerateLinqCode(data);
        }

        public string GeneratePara(GenerateData data)
        {
            SetData(data);
            return GenerateParaCode(data);
        }

        public DataTable GetTableList(Data.GenerateData Data)
        {
            SetConnDesc(Data);
            return DALObj.GetTableList();
        }
    }
}
