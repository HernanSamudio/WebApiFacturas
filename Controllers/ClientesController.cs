using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApiFacturas.Models;
using WebApiFacturas.Services;

namespace WebApiFacturas.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                var clientes = await _clienteService.ListarClientes();

                if (!clientes.Any())
                {
                    return NoContent();
                }

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            try
            {
                var cliente = await _clienteService.ObtenerClientePorId(id);

                if (cliente == null)
                    return NoContent();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] Cliente cliente)
        {
            try
            {
                if (_clienteService.ValidarCliente(cliente))
                {
                    await _clienteService.CrearCliente(cliente);
                    return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
                }
                return BadRequest("Validación fallida para el cliente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            try
            {
                if (_clienteService.ValidarCliente(cliente))
                {
                    await _clienteService.ActualizarCliente(cliente);
                    return NoContent();
                }
                return BadRequest("Validación fallida para el cliente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                var cliente = _clienteService.ObtenerClientePorId(id);
                if (cliente == null)
                    return NotFound();

                await _clienteService.EliminarCliente(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}