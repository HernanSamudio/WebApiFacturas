using System.Text.RegularExpressions;
using WebApiFacturas.Models;
using WebApiFacturas.Repository;

namespace WebApiFacturas.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly FacturaRepository _facturaRepository;

        public ClienteService(ClienteRepository clienteRepository, FacturaRepository facturaRepository)
        {
            _clienteRepository = clienteRepository;
            _facturaRepository = facturaRepository;
        }

        public async Task<IEnumerable<Cliente>> ListarClientes()
        {
            return await _clienteRepository.GetAllClientes();
        }

        public async Task<Cliente> ObtenerClientePorId(int id)
        {
            return await _clienteRepository.GetClienteById(id);
        }

        public async Task<Cliente> CrearCliente(Cliente cliente)
        {
            return await _clienteRepository.AddCliente(cliente);
        }

        public async Task ActualizarCliente(int id, Cliente cliente)
        {
            await _clienteRepository.UpdateCliente(id, cliente);
        }

        public async Task EliminarCliente(int id)
        {
            var eliminarFactura = await _facturaRepository.DeleteFacturaPorCliente(id);
            if(eliminarFactura) await _clienteRepository.DeleteCliente(id);
        }

        public bool ValidarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre) || cliente.Nombre.Length < 3)
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Apellido) || cliente.Apellido.Length < 3)
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Documento) || cliente.Documento.Length < 3)
                return false;

            if (!Regex.IsMatch(cliente.Celular, @"^\d{10}$"))
                return false;

            return true;
        }
    }
}
