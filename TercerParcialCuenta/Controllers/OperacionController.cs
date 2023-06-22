using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcialCuenta.Controllers
{
    public class OperacionController : Controller
    {
        private OperacionService operacionService;

        public OperacionController(OperacionService operacionService)
        {
            this.operacionService = operacionService;
        }

        [Authorize]
        [HttpPut("{origenCuentaId}/Transferir/{destinoCuentaId}")]
        public ActionResult Transfer(int origenCuentaId, int destinoCuentaId, double cantidad)
        {
            try
            {
                var result = operacionService.Transferencia(origenCuentaId, destinoCuentaId, cantidad);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred" + ex);
            }
        }

        [Authorize]
        [HttpPut("Depositar/{destinoCuentaId}")]
        public ActionResult Deposito(double cantidad, int destinoCuentaId)
        {
            try
            {
                var result = operacionService.Deposito(cantidad, destinoCuentaId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [Authorize]
        [HttpPut("Devolución/{destinoCuentaId}")]
        public ActionResult Devolucion(double cantidad, int destinoCuentaId)
        {
            try
            {
                var result = operacionService.Devolucion(cantidad, destinoCuentaId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [Authorize]
        [HttpPut("Extracción/{destinoCuentaId}")]
        public ActionResult Extracto(double cantidad, int destinoCuentaId)
        {
            try
            {
                var result = operacionService.Extracto(cantidad, destinoCuentaId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [Authorize]
        [HttpDelete("Bloquear/{destinoCuentaId}")]
        public ActionResult Bloquear(int destinoCuentaId)
        {
            try
            {
                var result = operacionService.Bloquear(destinoCuentaId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}
