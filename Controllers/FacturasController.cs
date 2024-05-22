using Microsoft.AspNetCore.Mvc;
using WebApiFacturas.Models;
using WebApiFacturas.Services;

namespace WebApiFacturas.Controllers
{
    [Route("api/facturas")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly FacturaService _facturaService;

        public FacturasController(FacturaService facturaService)
        {
            _facturaService = facturaService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            try
            {
                var facturas = await _facturaService.ListarFacturas();
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Error del servidor: " + ex.Message);
            }
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            try
            {
                var factura = await _facturaService.ObtenerFacturaPorId(id);
                if (factura == null)
                {
                    return NotFound();
                }
                return Ok(factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error del servidor: " + ex.Message);
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura([FromBody] Factura factura)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _facturaService.CrearFactura(factura);
                return CreatedAtAction("GetFactura", new { id = factura.Id }, factura);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Error del servidor: " + ex.Message);
            }
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, [FromBody] Factura factura)
        {
            if (id != factura.Id)
            {
                return BadRequest("El ID de la factura no coincide con el ID en la ruta.");
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool updateResult = await _facturaService.ActualizarFactura(factura);
                if (!updateResult)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error del servidor: " + ex.Message);
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            try
            {
                bool deleteResult = await _facturaService.EliminarFactura(id);
                if (!deleteResult)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error del servidor: " + ex.Message);
            }
        }
    }
}
