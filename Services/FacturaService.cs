using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApiFacturas.Models;
using WebApiFacturas.Repository;

namespace WebApiFacturas.Services
{
    public class FacturaService
    {
        private readonly FacturaRepository _facturaRepository;

        public FacturaService(FacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<IEnumerable<Factura>> ListarFacturas()
        {
            try
            {
                return await _facturaRepository.GetAllFacturas();
            }
            catch (Exception ex)
            {
                // Log the exception, adjust the return value or rethrow the exception as needed
                throw new Exception("Error al listar facturas", ex);
            }
        }

        public async Task<Factura> ObtenerFacturaPorId(int id)
        {
            try
            {
                return await _facturaRepository.GetFacturaById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la factura con ID {id}", ex);
            }
        }

        public async Task<Factura> CrearFactura(Factura factura)
        {
            try
            {
                if (!ValidarFactura(factura))
                {
                    throw new ArgumentException("Validación de factura fallida.");
                }
                return await _facturaRepository.AddFactura(factura);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la factura", ex);
            }
        }

        public async Task<bool> ActualizarFactura(Factura factura)
        {
            try
            {
                if (!ValidarFactura(factura))
                {
                    return false;
                }
                await _facturaRepository.UpdateFactura(factura);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la factura", ex);
            }
        }

        public async Task<bool> EliminarFactura(int id)
        {
            try
            {
                await _facturaRepository.DeleteFactura(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la factura", ex);
            }
        }

        public bool ValidarFactura(Factura factura)
        {
            var nroFacturaPattern = @"^\d{3}-\d{3}-\d{6}$";
            if (!Regex.IsMatch(factura.Nro_Factura, nroFacturaPattern))
                return false;

            if (factura.Total <= 0 || factura.Total_iva5 < 0 || factura.Total_iva10 < 0 || factura.Total_iva < 0)
                return false;

            if (string.IsNullOrEmpty(factura.Total_letras) || factura.Total_letras.Length < 6)
                return false;

            return true;
        }
    }
}
