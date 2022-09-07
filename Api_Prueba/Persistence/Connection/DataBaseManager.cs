using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Api_Prueba.Persistence.Connection
{
    public class DataBaseManager : DataBaseConnection
    {
        private readonly DataBase _database;

        protected const bool AllowNull = true;

        protected DataBaseManager(ConnectionOptions options) : base(options)
        {
            _database = options.DataBase;
        }

        protected void SetQuery(DbCommand command, string query)
        {
            command.CommandText = query;
        }

        protected DbCommand CreateCommand(DbConnection connection)
        {
            var command = GetCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        protected DbCommand CreateCommandText(DbConnection connection)
        {
            var command = GetCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            return command;
        }

        protected void AssignTransaction(DbCommand command, DbTransaction transaction)
        {
            command.Transaction = transaction;
        }

        private DbCommand GetCommand()
        {
            return _database switch
            {
                DataBase.SqlServer => new SqlCommand(),
                _ => null
            };
        }

        private DbParameter GetParam()
        {
            DbParameter parameter = _database switch
            {
                DataBase.SqlServer => new SqlParameter(),
                _ => throw new ArgumentOutOfRangeException()
            };
            return parameter;
        }

        protected Task<DbDataReader> ExecuteReaderAsync(DbCommand command)
        {
            if (command != null)
            {
                return command.ExecuteReaderAsync();
            }
            throw new Exception("No se ha inicializado el comando a ejecutar.");
        }

        protected async Task<DbDataReader> ExecuteReaderAsync(DbCommand command, int timeOut)
        {
            if (command == null) throw new Exception("No se ha inicializado el comando a ejecutar.");
            command.CommandTimeout = timeOut;
            return await command.ExecuteReaderAsync();
        }

        protected Task<int> ExecuteQueryAsync(DbCommand command)
        {
            if (command != null)
            {
                return command.ExecuteNonQueryAsync();
            }
            throw new Exception("No se ha inicializado el comando a ejecutar.");
        }

        protected Task<int> ExecuteQueryAsync(DbCommand command, int timeOut)
        {
            if (command == null) throw new Exception("No se ha inicializado el comando a ejecutar.");
            command.CommandTimeout = timeOut;
            return command.ExecuteNonQueryAsync();
        }

        protected Task<object> ExecuteScalarAsync(DbCommand command)
        {
            if (command == null) throw new Exception("No se ha inicializado el comando a ejecutar.");
            return command.ExecuteScalarAsync();
        }

        protected Task<object> ExecuteScalarAsync(DbCommand command, int timeOut)
        {
            if (command == null) throw new Exception("No se inicializado el comando a ejecutar.");
            command.CommandTimeout = timeOut;
            return command.ExecuteScalarAsync();
        }

        protected void AddInParameter(DbCommand command, string name, object value)
        {
            if (command == null) throw new Exception("No se ha inicializado el comando a ejecutar.");
            var parameter = GetParam();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        protected void AddInParameter(DbCommand command, string name, object value, DbType dbType)
        {
            if (command == null) throw new Exception("No se ha inicializado el comando a ejecutar.");
            var parameter = GetParam();
            parameter.DbType = dbType;
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        protected void AddInParameter(DbCommand command, string name, object value, bool nullable)
        {
            if (command == null) throw new Exception("No se ha inicializado el comando a ejecutar.");
            var parameter = GetParam();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = name;
            parameter.Value = nullable ? value ?? DBNull.Value : value;
            command.Parameters.Add(parameter);
        }

        protected void AddInParameter(DbCommand command, string name, object value, bool nullable, DbType dbType)
        {
            if (command == null) throw new Exception("No se ha inicializado el comando a ejecutar.");
            var parameter = GetParam();
            parameter.DbType = dbType;
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = name;
            parameter.Value = nullable ? value ?? DBNull.Value : value;
            command.Parameters.Add(parameter);
        }

        protected void AddOutParameter(DbCommand command, string name, DbType type)
        {
            if (command == null) throw new Exception("No se ha inicializado el comando a ejecutar.");
            var parameter = GetParam();
            parameter.Direction = ParameterDirection.Output;
            parameter.ParameterName = name;
            parameter.Size = int.MaxValue;
            parameter.DbType = type;
            command.Parameters.Add(parameter);
        }

        protected object GetOutput(DbCommand command, string name)
        {
            foreach (DbParameter parameter in command.Parameters)
            {
                if (parameter.ParameterName.Equals(name))
                {
                    return parameter.Value;
                }
            }
            throw new Exception($"No se ha encontrado el parámetro de salida {name}.");
        }
    }
}