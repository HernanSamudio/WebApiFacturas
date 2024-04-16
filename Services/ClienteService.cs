using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class ClienteService
{
    private readonly ClienteRepository _clienteRepository;

    public ClienteService(ClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
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
        ValidarCliente(cliente);
        return await _clienteRepository.AddCliente(cliente);
    }

    public async Task ActualizarCliente(Cliente cliente)
    {
        ValidarCliente(cliente);
        await _clienteRepository.UpdateCliente(cliente);
    }

    public async Task EliminarCliente(int id)
    {
        await _clienteRepository.DeleteCliente(id);
    }

    private void ValidarCliente(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nombre) || cliente.Nombre.Length < 3)
            throw new ArgumentException("El nombre del cliente es obligatorio y debe tener al menos 3 caracteres.");

        if (string.IsNullOrWhiteSpace(cliente.Apellido) || cliente.Apellido.Length < 3)
            throw new ArgumentException("El apellido del cliente es obligatorio y debe tener al menos 3 caracteres.");

        if (string.IsNullOrWhiteSpace(cliente.Documento))
            throw new ArgumentException("La cédula del cliente es obligatoria.");

        // if (!Regex.IsMatch(cliente.Celular, @"^\d{10}$"))
        //     throw new ArgumentException("El celular del cliente debe ser numérico y tener 10 dígitos.");
    }
}