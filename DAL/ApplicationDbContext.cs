using Microsoft.EntityFrameworkCore;
using WebApiFacturas.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Factura> Facturas { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("examen");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("clientes", "examen");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdBanco).HasColumnName("idbanco");
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100).HasColumnName("nombre");
            entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100).HasColumnName("apellido");
            entity.Property(e => e.Documento).IsRequired().HasMaxLength(20).HasColumnName("documento");
            entity.Property(e => e.Direccion).HasMaxLength(200).HasColumnName("direccion");
            entity.Property(e => e.Mail).HasMaxLength(100).HasColumnName("mail");
            entity.Property(e => e.Celular).IsRequired().HasMaxLength(10).HasColumnName("celular");
            entity.Property(e => e.Estado).IsRequired().HasMaxLength(50).HasColumnName("estado");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.ToTable("facturas", "examen");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Id_cliente).IsRequired().HasColumnName("idcliente");
            entity.Property(e => e.Nro_Factura).IsRequired().HasMaxLength(15).HasColumnName("numerofactura");
            entity.Property(e => e.Fecha_Hora).IsRequired().HasColumnName("fechahora");
            entity.Property(e => e.Total).IsRequired().HasColumnType("decimal(18,2)").HasColumnName("total");
            entity.Property(e => e.Total_iva5).IsRequired().HasColumnType("decimal(18,2)").HasColumnName("totaliva5");
            entity.Property(e => e.Total_iva10).IsRequired().HasColumnType("decimal(18,2)").HasColumnName("totaliva10");
            entity.Property(e => e.Total_iva).IsRequired().HasColumnType("decimal(18,2)").HasColumnName("totaliva");
            entity.Property(e => e.Total_letras).IsRequired().HasMaxLength(500).HasColumnName("totalenletras");
            entity.Property(e => e.Sucursal).HasMaxLength(100).HasColumnName("sucursal");
        });
    }
}