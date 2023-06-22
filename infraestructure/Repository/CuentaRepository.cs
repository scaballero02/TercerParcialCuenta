using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using infraestructure.Models;

namespace Infraestructure.Repository
{
    public class CuentaRepository
    {
        private string _connectionString;
        private Npgsql.NpgsqlConnection connection;
        public CuentaRepository(string connectionString)
        {
            this._connectionString = connectionString;
            this.connection = new Npgsql.NpgsqlConnection(this._connectionString);
        }

        public string InsertarCuenta(CuentaModel cuenta)
        {
            try 
            {
                String query = "insert into cuenta(id_persona, nombre_cuenta, numero_cuenta, saldo, limite_saldo, limite_transferencia, estado) " +
                    " values(@id_persona, @nombre_cuenta, @numero_cuenta, @saldo, @limite_saldo, @limite_transferencia, @estado)";
                connection.Open();
                connection.Execute(query, cuenta);
                connection.Close();
                return "Se inserto correctamente...";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string ModificarCuenta(CuentaModel cuenta, int id)
        {
            try
            {
                connection.Execute($"UPDATE cuenta SET " +
                    "nombre_cuenta = @nombre_cuenta, " +
                    "numero_cuenta = @numero_cuenta, " +
                    "saldo = @saldo, " +
                    "limite_saldo = @limite_saldo, " +
                    "limite_transferencia = @limite_transferencia, " +
                    "estado = @estado " +

                    $"WHERE id = {id}", cuenta);
                return "Se modificaron los datos correctamente...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string EliminarCuenta(int id)
        {
            try
            {
                connection.Execute($" DELETE FROM cuenta WHERE id = {id}");
                return "Se eliminó correctamente el registro...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CuentaModel ConsultarCuenta(int id)
        {
            try
            {
                return connection.QueryFirst<CuentaModel>($"SELECT * FROM cuenta WHERE id = {id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CuentaModel> ListarCuenta()
        {
            try
            {
                return connection.Query<CuentaModel>($"SELECT * FROM cuenta order by id asc");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}