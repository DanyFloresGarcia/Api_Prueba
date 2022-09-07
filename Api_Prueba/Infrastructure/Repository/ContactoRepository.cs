using System.Collections.Generic;
using System.Threading.Tasks;
using Api_Prueba.Application.Abstract;
using Api_Prueba.Domain;
using Api_Prueba.Persistence.Sql;
//using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;

namespace Api_Prueba.Infrastructure.Repository
{
    public class ContactoRepository : IContactoRepository
    {
        private readonly ISqlDataContext _sqlDataContext;

        public ContactoRepository(ISqlDataContext sqlDataSqlDataContext)
        {
            _sqlDataContext = sqlDataSqlDataContext;
        }


        public async Task<List<Contacto>> Buscar(bool? activo)
        {
            return await _sqlDataContext.Contacto.Buscar(activo);
        }

        public async Task<string> Registrar(Contacto contacto)
        {
            return await _sqlDataContext.Contacto.Registrar(contacto);
        }

        public async Task<string> Actualizar(Contacto contacto)
        {
            return await _sqlDataContext.Contacto.Actualizar(contacto);
        }

        public async Task<string> Eliminar(Contacto contacto)
        {
            return await _sqlDataContext.Contacto.Eliminar(contacto);
        }
    }
}