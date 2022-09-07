using System.Data.Common;
using System.Data.SqlClient;

namespace Api_Prueba.Persistence.Connection
{
    public class DataBaseConnection
    {
        private readonly ConnectionOptions _options;

        protected DataBaseConnection(ConnectionOptions options)
        {
            _options = options;
        }
        
        protected DbConnection GetConnection()
        {
            return _options.DataBase switch
            {
                DataBase.SqlServer => new SqlConnection(_options.ConnectionString)
            };
        }
    }
}