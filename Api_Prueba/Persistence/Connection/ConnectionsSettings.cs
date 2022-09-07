namespace Api_Prueba.Persistence.Connection
{
    public class ConnectionsSettings
    {
        public const string ConnectionStrings = "ConnectionStrings";
        public string SqlServerConnection { get; set; }
        public string AuditoriaSqlServerConnection { get; set; }
        public string MySqlConnection { get; set; }
    }
}