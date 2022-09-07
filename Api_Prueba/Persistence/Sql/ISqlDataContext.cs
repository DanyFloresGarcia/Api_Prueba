using Api_Prueba.Persistence.Sql.Data;

namespace Api_Prueba.Persistence.Sql
{
    public interface ISqlDataContext
    {
        ContactoData Contacto { get; }
    }
}