using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Core.Common
{
    public class SqlDynamicParameters : SqlMapper.IDynamicParameters
    {
        private readonly DynamicParameters dynamicParameters = new DynamicParameters();

        private readonly List<SqlParameter> sqlParameters = new List<SqlParameter>();

        public void Add(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null)
        {
            dynamicParameters.Add(name, value, dbType, direction, size);
        }

        public void Add(string name, SqlDbType oracleDbType, int size)//, ParameterDirection direction)
        {
            var oracleParameter = new SqlParameter(name, oracleDbType, size);//,direction);

            sqlParameters.Add(oracleParameter);
        }
        public void Add(string name, SqlDbType oracleDbType)//, ParameterDirection direction)
        {
            var oracleParameter = new SqlParameter(name, oracleDbType);//,direction);

            sqlParameters.Add(oracleParameter);
        }
        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            ((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);

            var oracleCommand = command as SqlCommand;

            if (oracleCommand != null)
            {
                oracleCommand.Parameters.AddRange(sqlParameters.ToArray());
            }
        }
    }
}
