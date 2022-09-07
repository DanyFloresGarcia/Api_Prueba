using System;

namespace Api_Prueba.Domain
{
    public class Contacto
    {
        public const string FotoPorDefecto = "Images/avatars/no-photo.jpg";
        public long IdContacto { get; set; }
        public string Codigo { get; set; }

		public Empresa Empresa { get; set; }
		public string Nombre { get; set; }
		public string Celular { get; set; }
		public string Direccion { get; set; }
		public string Correo { get; set; }
		public int IdUsuarioCreador { get; set; }
		public bool Activo { get; set; }
		public DateTime FechaCreacion { get; set; }
	}
}