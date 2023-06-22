using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class OperacionService
    {
        private OperacionService operacionService;

        private CuentaRepository cuentaRepository;

        public OperacionService(CuentaRepository CuentaRepository)
        {
            this.cuentaRepository = CuentaRepository;
        }

        public string Transferencia(int origenCuentaId, int destinoCuentaId, double cantidad)
        {
            var origenCuenta = cuentaRepository.ConsultarCuenta(origenCuentaId);
            var destinoCuenta = cuentaRepository.ConsultarCuenta(destinoCuentaId);

            if (origenCuenta.Id == destinoCuenta.Id)
            {
                throw new ArgumentException("No se puede transferenciair en la misma cuenta");
            }

            if (origenCuenta.estado == false || destinoCuenta.estado == false)
            {
                throw new ArgumentException("Cuenta invalIda o inhabilitada");
            }

            if (origenCuenta.limite_transferencia < cantidad || destinoCuenta.limite_saldo < (destinoCuenta.saldo + cantidad))
            {
                throw new ArgumentException("Desbordamiento de limite de transferencia");
            }

            if (origenCuenta.saldo < cantidad)
            {
                throw new InvalidOperationException("Saldo insuficiente");
            }

            origenCuenta.saldo -= cantidad;
            destinoCuenta.saldo += cantidad;

            cuentaRepository.ModificarCuenta(origenCuenta, origenCuentaId);
            cuentaRepository.ModificarCuenta(destinoCuenta, destinoCuentaId);
            return "transferencia exitosa!";
        }

        public string Deposito(double cantidad, int CuentaId)
        {
            var Cuenta = cuentaRepository.ConsultarCuenta(CuentaId);
            if (Cuenta.estado == false)
            {
                throw new ArgumentException("Cuenta invalIda o inhabilitada");
            }
            if (Cuenta.limite_saldo < (Cuenta.saldo + cantidad))
            {
                throw new ArgumentException("limite de saldo superado");
            }
            Cuenta.saldo += cantidad;

            cuentaRepository.ModificarCuenta(Cuenta, CuentaId);

            return "Deposito exitoso!";
        }

        public string Devolucion(double cantidad, int CuentaId)
        {
            var Cuenta = cuentaRepository.ConsultarCuenta(CuentaId);
            if (Cuenta.estado == false)
            {
                throw new ArgumentException("Cuenta invalida o inhabilitada");
            }
            if (Cuenta.saldo < cantidad)
            {
                throw new InvalidOperationException("Saldo insuficiente");
            }
            Cuenta.saldo -= cantidad;
            cuentaRepository.ModificarCuenta(Cuenta, CuentaId);
            return "Devolucion exitosa!";
        }

        public string Extracto(double cantidad, int CuentaId)
        {
            var Cuenta = cuentaRepository.ConsultarCuenta(CuentaId);
            if (Cuenta.estado == false)
            {
                throw new ArgumentException("Cuenta invalIda o inhabilitada");
            }
            if (Cuenta.saldo < cantidad)
            {
                throw new InvalidOperationException("Saldo insuficiente");
            }
            Cuenta.saldo -= cantidad;
            cuentaRepository.ModificarCuenta(Cuenta, CuentaId);

            var updatedCuenta = cuentaRepository.ConsultarCuenta(CuentaId);

            return "Extraccion exitosa! Su saldo restante es: " + updatedCuenta.saldo;

        }

        public string Bloquear(int CuentaId)
        {
            var Cuenta = cuentaRepository.ConsultarCuenta(CuentaId);
            if (Cuenta == null)
            {
                throw new ArgumentException("InvalId Cuenta Id");
            }

            Cuenta.estado = false;

            cuentaRepository.ModificarCuenta(Cuenta, CuentaId);

            var updatedCuenta = cuentaRepository.ConsultarCuenta(CuentaId);

            return "Cuenta bloqueada exitosamente";

        }
    }
}
