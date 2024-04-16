using Microsoft.EntityFrameworkCore;
using WebApiFacturas.Models;

namespace WebApiFacturas.Repository
{
    public class ClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> AddCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task UpdateCliente(int id, Cliente cliente)
        {
            var resultado = await _context.Clientes.Where(x => x.Id == id).SingleOrDefaultAsync();

            if (resultado != null)
            {
                resultado.Nombre = cliente.Nombre;
                resultado.Apellido = cliente.Apellido;
                resultado.Documento = cliente.Documento;
                resultado.Direccion = cliente.Direccion;
                resultado.Mail = cliente.Mail;
                resultado.Celular = cliente.Celular;
                resultado.Estado = cliente.Estado;
                resultado.IdBanco = cliente.IdBanco;

                _context.Entry(resultado).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Cliente no encontrado");
            }
        }

        public async Task DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}