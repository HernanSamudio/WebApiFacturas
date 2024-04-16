using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

public class FacturaRepository
{
    private readonly ApplicationDbContext _context;

    public FacturaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Factura>> GetAllFacturasAsync()
    {
        return await _context.Facturas.ToListAsync();
    }

    public async Task<Factura> GetFacturaByIdAsync(int id)
    {
        return await _context.Facturas.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Factura> AddFacturaAsync(Factura factura)
    {
        _context.Facturas.Add(factura);
        await _context.SaveChangesAsync();
        return factura;
    }

    public async Task UpdateFacturaAsync(Factura factura)
    {
        _context.Entry(factura).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFacturaAsync(int id)
    {
        var factura = await _context.Facturas.FindAsync(id);
        if (factura != null)
        {
            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
        }
    }
}