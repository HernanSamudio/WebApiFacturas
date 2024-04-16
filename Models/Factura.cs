namespace WebApiFacturas.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public int Id_cliente { get; set; }
        public string Nro_Factura { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public decimal Total { get; set; }
        public decimal Total_iva5 { get; set; }
        public decimal Total_iva10 { get; set; }
        public decimal Total_iva { get; set; }
        public string Total_letras { get; set; }
        public string Sucursal { get; set; }
    }
}