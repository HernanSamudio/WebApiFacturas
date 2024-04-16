using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class FacturasController : ControllerBase
{
    private readonly FacturaService _facturaService;

    public FacturasController(FacturaService facturaService)
    {
        _facturaService = facturaService;
    }

    // GET: api/Facturas
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
            // Consider logging the exception details
            return StatusCode(500, "Error del servidor: " + ex.Message);
        }
    }

    // GET: api/Facturas/5
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
            // Consider logging the exception details
            return StatusCode(500, "Error del servidor: " + ex.Message);
        }
    }

    // POST: api/Facturas
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
            // Consider logging the exception details
            return StatusCode(500, "Error del servidor: " + ex.Message);
        }
    }

    // PUT: api/Facturas/5
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

            return NoContent();
        }
        catch (Exception ex)
        {
            // Consider logging the exception details
            return StatusCode(500, "Error del servidor: " + ex.Message);
        }
    }

    // DELETE: api/Facturas/5
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

            return NoContent();
        }
        catch (Exception ex)
        {
            // Consider logging the exception details
            return StatusCode(500, "Error del servidor: " + ex.Message);
        }
    }
}