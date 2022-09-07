using Api_Prueba.Persistence.Connection;
using Api_Prueba.Persistence.Sql.Data;
using Microsoft.Extensions.Options;

namespace Api_Prueba.Persistence.Sql
{
    public class SqlDataContext : ISqlDataContext
    {
        public ContactoData Contacto { get; set; }

        public SqlDataContext(IOptions<ConnectionsSettings> connections)
        {
            var options = new ConnectionOptions
            {
                DataBase = DataBase.SqlServer,
                ConnectionString = connections.Value.SqlServerConnection
            };

            Contacto = new ContactoData(options);
        }
    }
}