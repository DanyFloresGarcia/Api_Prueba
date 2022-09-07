using Api_Prueba.Application.Abstract;
using Api_Prueba.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Prueba.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactoController : Controller
    {
        private readonly IContactoRepository _contactoRepository;

        /// <summary>
        /// Constructor del controlador de OrdenVenta.
        /// </summary>
        /// <param name="accesoUsuarioService"></param>
        /// <param name="OrdenVentaRepository"></param>
        public ContactoController(IContactoRepository contactoRepository)
        {
            _contactoRepository = contactoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Buscar(bool activo)
        {
            var OrdenVentas = await _contactoRepository.Buscar(activo);

            return Ok(OrdenVentas);
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult> Registrar(Contacto contacto)
        {
            contacto.IdUsuarioCreador = 1;

            var resultado = await _contactoRepository.Registrar(contacto);
            var mensaje = resultado;
            return Ok(mensaje);
        }


        [HttpPost("Actualizar")]
        public async Task<ActionResult> Actualizar(Contacto contacto)
        {
            contacto.IdUsuarioCreador = 1;

            var resultado = await _contactoRepository.Actualizar(contacto);
            var mensaje = resultado;
            return Ok(mensaje);
        }

        [HttpDelete("{idContacto:int}")]
        public async Task<ActionResult> Eliminar(int idContacto)
        {
            var contacto = new Contacto();
            contacto.IdContacto = idContacto;
            contacto.IdUsuarioCreador = 1;

            var resultado = await _contactoRepository.Eliminar(contacto);
            var mensaje = resultado;
            return Ok(mensaje);
        }

    }
}
