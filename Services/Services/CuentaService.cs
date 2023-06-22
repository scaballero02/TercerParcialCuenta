using Infraestructure.Repository;
using infraestructure.Models;
namespace Services.Services
{
    public class CuentaService
    {
        private CuentaRepository repositoryCuenta;

        public CuentaService(string connectionString)
        {
            this.repositoryCuenta = new CuentaRepository(connectionString);
        }

        public string InsertarCuenta(CuentaModel Cuenta)
        {
            return validarDatosCuenta(Cuenta) ? repositoryCuenta.InsertarCuenta(Cuenta) : throw new Exception("Error en la validacion");
        }

        public string ModificarCuenta(CuentaModel Cuenta, int id)
        {
            return validarDatosCuenta(Cuenta) ? repositoryCuenta.ModificarCuenta(Cuenta, id) : throw new Exception("Error en la validacion");
        }

        public string EliminarCuenta(int id)
        {
            return repositoryCuenta.EliminarCuenta(id);
        }

        public CuentaModel ConsultarCuenta(int id)
        {
            return repositoryCuenta.ConsultarCuenta(id);
        }

        public IEnumerable<CuentaModel> ListarCuenta()
        {
            return repositoryCuenta.ListarCuenta();
        }

        private bool validarDatosCuenta(CuentaModel Cuenta)
        {
            /*if (Cuenta.Nombre.Trim().Length > 2)
            {
                return false;
            }*/

            return true;
        }
    }
}