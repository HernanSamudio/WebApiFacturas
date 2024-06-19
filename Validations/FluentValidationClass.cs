using FluentValidation;
using WebApiFacturas.Models;
using WebApiFacturas.Services;

namespace WebApiFacturas.Validations
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        private readonly ClienteService _clienteService;

        public ClienteValidator(ClienteService clienteService)
        {
            _clienteService = clienteService;

            RuleFor(cliente => cliente.Nombre).NotEmpty().MinimumLength(3);
            RuleFor(cliente => cliente.Apellido).NotEmpty().MinimumLength(3);
            RuleFor(cliente => cliente.Celular).NotEmpty().Matches(@"^\d{10}$");
            RuleFor(cliente => cliente.Documento).NotEmpty().MinimumLength(7)
                .Must(DebeSerUnico).WithMessage("El documento ya está registrado.");
            RuleFor(cliente => cliente.Mail).NotEmpty().EmailAddress();
            RuleFor(cliente => cliente.Estado).NotEmpty();
        }

        private bool DebeSerUnico(string documento)
        {
            if (_clienteService == null)
            {
                throw new InvalidOperationException("_clienteService no ha sido inicializado correctamente.");
            }

            return !_clienteService.ExisteDocumento(documento);
        }
    }

    public class FacturaValidator : AbstractValidator<Factura>
    {
        public FacturaValidator()
        {
            RuleFor(factura => factura.Nro_Factura)
                .NotEmpty().WithMessage("El número de factura es obligatorio.")
                .Matches(@"^\d{3}-\d{3}-\d{6}$").WithMessage("El número de factura debe seguir el patrón 000-000-000000.");

            RuleFor(factura => factura.Fecha_Hora)
                .NotEmpty().WithMessage("La fecha y hora son obligatorias.");

            RuleFor(factura => factura.Total)
                .NotEmpty().WithMessage("El campo Total es obligatorio.")
                .Must(value => EsDecimal(value)).WithMessage("El campo Total debe ser numérico y mayor que cero.");

            RuleFor(factura => factura.Total_iva5)
                .NotEmpty().WithMessage("El campo Total_iva5 es obligatorio.")
                .Must(value => EsDecimal(value)).WithMessage("El campo Total_iva5 debe ser numérico y mayor que cero.");

            RuleFor(factura => factura.Total_iva10)
                .NotEmpty().WithMessage("El campo Total_iva10 es obligatorio.")
                .Must(value => EsDecimal(value)).WithMessage("El campo Total_iva10 debe ser numérico y mayor que cero.");

            RuleFor(factura => factura.Total_iva)
                .NotEmpty().WithMessage("El campo Total_iva es obligatorio.")
                .Must(value => EsDecimal(value)).WithMessage("El campo Total_iva debe ser numérico y mayor que cero.");

            RuleFor(factura => factura.Total_letras)
                .NotEmpty().WithMessage("El campo Total_letras es obligatorio.")
                .MinimumLength(6).WithMessage("El campo Total_letras debe tener al menos 6 caracteres.");

            RuleFor(factura => factura.Sucursal)
                .NotEmpty().WithMessage("El campo Sucursal es obligatorio.");
        }

        private bool EsDecimal(decimal value)
        {
            return value > 0;
        }
    }



}