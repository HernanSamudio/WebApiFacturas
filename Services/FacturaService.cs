using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class FacturaService
{
    private readonly FacturaRepository _facturaRepository;

    public FacturaService(FacturaRepository facturaRepository)
    {
        _facturaRepository = facturaRepository;
    }

    public async Task<IEnumerable<Factura>> ListarFacturasAsync()
    {
        // Lógica para obtener todas las facturas
        return await _facturaRepository.GetAllFacturasAsync();
    }

    public async Task<Factura> ObtenerFacturaPorIdAsync(int id)
    {
        // Lógica para obtener una factura específica por ID
        return await _facturaRepository.GetFacturaByIdAsync(id);
    }

    public async Task<Factura> CrearFacturaAsync(Factura factura)
    {
        ValidarFactura(factura);
        return await _facturaRepository.AddFacturaAsync(factura);
    }

    public async Task ActualizarFacturaAsync(Factura factura)
    {
        ValidarFactura(factura);
        await _facturaRepository.UpdateFacturaAsync(factura);
    }

    public async Task EliminarFacturaAsync(int id)
    {
        await _facturaRepository.DeleteFacturaAsync(id);
    }

    private void ValidarFactura(Factura factura)
    {
        var nroFacturaPattern = @"^\d{3}-\d{3}-\d{6}$";
        if (!Regex.IsMatch(factura.Nro_Factura, nroFacturaPattern))
            throw new ArgumentException("El número de factura no cumple con el patrón requerido.");

        if (factura.Total <= 0 || factura.Total_iva5 < 0 || factura.Total_iva10 < 0 || factura.Total_iva < 0)
            throw new ArgumentException("Los totales de la factura deben ser números positivos y el total debe ser mayor que cero.");

        if (string.IsNullOrWhiteSpace(factura.Total_letras) || factura.Total_letras.Length < 6)
            throw new ArgumentException("El total en letras es obligatorio y debe tener al menos 6 caracteres.");
    }
}