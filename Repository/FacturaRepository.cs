using Microsoft.EntityFrameworkCore;
using WebApiFacturas.Models;

namespace WebApiFacturas.Repository
{
    public class FacturaRepository
    {
        private readonly ApplicationDbContext _context;

        public FacturaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Factura>> GetAllFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }

        public async Task<Factura> GetFacturaById(int id)
        {
            return await _context.Facturas.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Factura> AddFactura(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
            return factura;
        }

        public async Task UpdateFactura(Factura factura)
        {
            _context.Entry(factura).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
                await _context.SaveChangesAsync();
            }
        }
    }
}