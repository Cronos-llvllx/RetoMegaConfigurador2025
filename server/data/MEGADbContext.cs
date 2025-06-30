using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.data;

public class MEGADbContext : DbContext
{
    public MEGADbContext(DbContextOptions<MEGADbContext> options) : base(options)
    {
    }

    public DbSet<Ciudad> Ciudades { get; set; }
    public DbSet<Colonia> Colonias { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<ContratoPaquete> ContratoPaquetes { get; set; }
    public DbSet<Paquete> Paquetes { get; set; }
    public DbSet<PaqueteServicio> PaqueteServicios { get; set; }
    public DbSet<PromoPersonalizada> PromoPersonalizadas { get; set; }
    public DbSet<Promocion> Promociones { get; set; }
    public DbSet<PromocionCiudad> PromocionCiudades { get; set; }
    public DbSet<PromocionColonia> PromocionColonias { get; set; }
    public DbSet<PromocionContrato> PromocionContratos { get; set; }
    public DbSet<PromocionPaquete> PromocionPaquetes { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<Suscriptor> Suscriptores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=megadb;User Id=sa;Password=Hg0mv1ll79_;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // --- CÓDIGO AÑADIDO: Mapeo explícito a los nombres de tabla de tu script SQL ---
        modelBuilder.Entity<Ciudad>().ToTable("Ciudad");
        modelBuilder.Entity<Colonia>().ToTable("Colonia");
        modelBuilder.Entity<Contrato>().ToTable("Contrato");
        modelBuilder.Entity<Paquete>().ToTable("Paquete");
        modelBuilder.Entity<PromoPersonalizada>().ToTable("PromoPersonalizada");
        modelBuilder.Entity<Promocion>().ToTable("Promocion");
        modelBuilder.Entity<Servicio>().ToTable("Servicio");
        modelBuilder.Entity<Suscriptor>().ToTable("Suscriptor");
        modelBuilder.Entity<ContratoPaquete>().ToTable("ContratoPaquete");
        modelBuilder.Entity<PaqueteServicio>().ToTable("PaqueteServicio");
        modelBuilder.Entity<PromocionPaquete>().ToTable("PromocionPaquete");
        modelBuilder.Entity<PromocionCiudad>().ToTable("PromocionCiudad");
        modelBuilder.Entity<PromocionColonia>().ToTable("PromocionColonia");
        modelBuilder.Entity<PromocionContrato>().ToTable("PromocionContrato");
        // -------------------------------------------------------------------------

        // Definición de llaves primarias
        modelBuilder.Entity<Ciudad>().HasKey(e => e.Idciudad);
        modelBuilder.Entity<Colonia>().HasKey(e => e.IdColonia);
        modelBuilder.Entity<Contrato>().HasKey(e => e.Idcontrato);
        modelBuilder.Entity<Paquete>().HasKey(e => e.Idpaquete);
        modelBuilder.Entity<PromoPersonalizada>().HasKey(e => e.Idpromopersonalizada);
        modelBuilder.Entity<Promocion>().HasKey(e => e.Idpromocion);
        modelBuilder.Entity<Servicio>().HasKey(e => e.Idservicio);
        modelBuilder.Entity<Suscriptor>().HasKey(e => e.Idsuscriptor);

        // Definición de precisión para decimales
        modelBuilder.Entity<Contrato>().Property(p => p.PrecioBase).HasPrecision(6, 2);
        modelBuilder.Entity<Paquete>().Property(p => p.PrecioBase).HasPrecision(6, 2);
        modelBuilder.Entity<PromoPersonalizada>().Property(p => p.PrecioPorcen).HasPrecision(6, 2);
        modelBuilder.Entity<Promocion>().Property(p => p.PrecioBase).HasPrecision(6, 2);
        modelBuilder.Entity<Servicio>().Property(p => p.PrecioBase).HasPrecision(6, 2);

        // Definición de relaciones (llaves foráneas)
        modelBuilder.Entity<Colonia>()
            .HasOne(col => col.Ciudad)
            .WithMany(ciu => ciu.Colonias)
            .HasForeignKey(col => col.Idciudad);

        modelBuilder.Entity<Suscriptor>()
            .HasOne(sus => sus.Colonia)
            .WithMany(col => col.Suscriptores)
            .HasForeignKey(sus => sus.Idcolonia);
            
        modelBuilder.Entity<Contrato>()
            .HasOne(c => c.Suscriptor)
            .WithOne(s => s.Contrato)
            .HasForeignKey<Contrato>(c => c.Idsuscriptor);

        modelBuilder.Entity<PromoPersonalizada>()
            .HasOne(pp => pp.Contrato)
            .WithMany(con => con.PromosPersonalizadas)
            .HasForeignKey(pp => pp.Idcontrato);
        
        // Configuración de llaves primarias para tablas de unión (muchos a muchos)
        modelBuilder.Entity<ContratoPaquete>().HasKey(cp => new { cp.Idcontrato, cp.Idpaquete });
        modelBuilder.Entity<PaqueteServicio>().HasKey(ps => new { ps.Idpaquete, ps.Idservicio });
        modelBuilder.Entity<PromocionPaquete>().HasKey(pp => new { pp.Idpromocion, pp.Idpaquete });
        modelBuilder.Entity<PromocionCiudad>().HasKey(pc => new { pc.Idpromocion, pc.Idciudad });
        modelBuilder.Entity<PromocionColonia>().HasKey(pc => new { pc.Idpromocion, pc.Idcolonia });
        modelBuilder.Entity<PromocionContrato>().HasKey(pc => new { pc.Idpromocion, pc.Idcontrato });
    }
}