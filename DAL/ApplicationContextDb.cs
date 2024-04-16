using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Factura> Facturas { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Clientes");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Documento).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Mail).HasMaxLength(100);
            entity.Property(e => e.Celular).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Estado).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.ToTable("Facturas");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.Property(e => e.Id_cliente).IsRequired().HasColumnName("IdCliente");
            entity.HasOne<Cliente>().WithMany().HasForeignKey(e => e.Id_cliente); // Asume relación uno a muchos

            entity.Property(e => e.Nro_Factura).IsRequired().HasMaxLength(15).HasColumnName("NumeroFactura");
            entity.Property(e => e.Fecha_Hora).IsRequired().HasColumnName("FechaHora");
            entity.Property(e => e.Total).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Total_iva5).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Total_iva10).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Total_iva).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Total_letras).IsRequired().HasMaxLength(500).HasColumnName("TotalEnLetras");
            entity.Property(e => e.Sucursal).HasMaxLength(100);
        });
    }
}