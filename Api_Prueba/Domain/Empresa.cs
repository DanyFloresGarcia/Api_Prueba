using System;
using System.Collections.Generic;

namespace Api_Prueba.Domain
{
    public class Empresa
    {
        public int idEmpresa { get; set; }
        public string Nombre { get; set; }
        public string RUC { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

}