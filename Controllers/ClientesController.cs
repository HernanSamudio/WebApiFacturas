using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ProyectoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async IActionResult GetClientes()
        {
            try
            {
                var clientes = await _clienteService.ListarClientes();

                if(!clientes.Any()) {
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
        public IActionResult GetCliente(int id)
        {
            try
            {
                var cliente = _clienteService.ObtenerCliente(id);
                if (cliente == null)
                    return NotFound();
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateCliente([FromBody] Cliente cliente)
        {
            try
            {
                if (_clienteService.ValidarCliente(cliente))
                {
                    _clienteService.CrearCliente(cliente);
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
        public IActionResult UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            try
            {
                if (id != cliente.Id)
                    return BadRequest("El ID del cliente no coincide.");

                if (_clienteService.ValidarCliente(cliente))
                {
                    _clienteService.ActualizarCliente(cliente);
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
        public IActionResult DeleteCliente(int id)
        {
            try
            {
                var cliente = _clienteService.ObtenerCliente(id);
                if (cliente == null)
                    return NotFound();

                _clienteService.EliminarCliente(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}