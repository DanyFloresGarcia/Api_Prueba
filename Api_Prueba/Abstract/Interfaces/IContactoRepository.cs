using System.Collections.Generic;
using System.Threading.Tasks;
using Api_Prueba.Domain;

namespace Api_Prueba.Application.Abstract
{
    public interface IContactoRepository
    {
        Task<string> Registrar(Contacto contacto);
        Task<string> Actualizar(Contacto contacto);

        Task<string> Eliminar(Contacto contacto);

        Task<List<Contacto>> Buscar(bool? activo);
    }
}