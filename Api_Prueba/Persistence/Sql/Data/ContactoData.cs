using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Api_Prueba.Domain;
using Api_Prueba.Persistence.Connection;
using System;

namespace Api_Prueba.Persistence.Sql.Data
{
    public class ContactoData : DataBaseManager
    {
        public ContactoData(ConnectionOptions options) : base(options)
        {
        }


        public async Task<List<Contacto>> Buscar(bool? Activo)
        {
            var contactos = new List<Contacto>();

            await using var connection = GetConnection();
            await connection.OpenAsync();
            await using var command = CreateCommand(connection);
            SetQuery(command, "Usp_Listar_Contacto");
            //AddInParameter(command, "@bActivo", Activo, AllowNull);
            await using var reader = await ExecuteReaderAsync(command, 72000);

            while (await reader.ReadAsync())
            {
                var contacto = new Contacto
                {
                    Codigo = reader["Codigo"].ToString(),
                    Nombre = reader["NombreContacto"].ToString(),
                    Celular = reader["Celular"].ToString(),
                    Empresa = new Empresa
                    {
                        Nombre = reader["NombreEmpresa"].ToString()
                    },
                    FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString())
                };

                contactos.Add(contacto);
            }

            return contactos;
        }

        public async Task<string> Registrar(Contacto contacto)
        {
            await using var connection = GetConnection();
            await connection.OpenAsync();
            await using var command = CreateCommand(connection);
            SetQuery(command, "[Usp_Registrar_Contacto]");
            AddInParameter(command, "@iIdEmpresa", contacto.Empresa?.idEmpresa);
            AddInParameter(command, "@vNombre", contacto.Nombre, AllowNull);
            AddInParameter(command, "@vCelular", contacto.Celular, AllowNull);
            AddInParameter(command, "@vDireccion", contacto.Direccion, AllowNull);
            AddInParameter(command, "@vCorreo", contacto.Correo, AllowNull);
            AddOutParameter(command, "@vMensaje", DbType.String);
            await ExecuteQueryAsync(command, 72000);
            var mensaje = GetOutput(command, "@vMensaje").ToString();

            return mensaje;
        }

        public async Task<string> Actualizar(Contacto contacto)
        {
            await using var connection = GetConnection();
            await connection.OpenAsync();
            await using var command = CreateCommand(connection);
            SetQuery(command, "[Usp_Actualizar_Contacto]");
            AddInParameter(command, "@iIdContacto", contacto.IdContacto);
            AddInParameter(command, "@iIdEmpresa", contacto.Empresa?.idEmpresa);
            AddInParameter(command, "@vNombre", contacto.Nombre, AllowNull);
            AddInParameter(command, "@vCelular", contacto.Celular);
            AddInParameter(command, "@vDireccion", contacto.Direccion);
            AddInParameter(command, "@vCorreo", contacto.Correo, AllowNull);
            AddOutParameter(command, "@vMensaje", DbType.String);
            await ExecuteQueryAsync(command, 72000);
            var mensaje = GetOutput(command, "@vMensaje").ToString();

            return mensaje;
        }
        public async Task<string> Eliminar(Contacto contacto)
        {
            await using var connection = GetConnection();
            await connection.OpenAsync();
            await using var command = CreateCommand(connection);
            SetQuery(command, "[Usp_Eliminar_Contacto]");
            AddInParameter(command, "@iIdContacto", contacto.IdContacto);
            AddInParameter(command, "@iIdUsuario", contacto.IdUsuarioCreador);
            AddOutParameter(command, "@vMensaje", DbType.String);
            await ExecuteQueryAsync(command, 72000);
            var mensaje = GetOutput(command, "@vMensaje").ToString();

            return mensaje;
        }
    }
}