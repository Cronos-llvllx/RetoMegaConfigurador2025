using megaapi.models;
using megaapi.models.views;
using Microsoft.EntityFrameworkCore;

namespace megaapi.data;

/// <summary>
/// El contexto de la API a la base de datos.
/// Esta versión está configurada para mapear EXACTAMENTE todos los modelos y relaciones.
/// </summary>
public class MEGADbContext : DbContext
{
  public MEGADbContext(DbContextOptions<MEGADbContext> options) : base(options)
  {
  }

  // DbSet para cada una de tus entidades
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

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // --- CONFIGURACIÓN COMPLETA DEL MODELO ---
    // 1. Mapeo explícito a los nombres de tabla del script SQL (singular)
    modelBuilder.Entity<Ciudad>().ToTable("Ciudad");
    modelBuilder.Entity<Colonia>().ToTable("Colonia");
    modelBuilder.Entity<Contrato>().ToTable("Contrato");
    modelBuilder.Entity<Paquete>().ToTable("Paquete");
    modelBuilder.Entity<PromoPersonalizada>().ToTable("PromoPersonalizada");
    modelBuilder.Entity<Promocion>().ToTable("Promocion");
    modelBuilder.Entity<Servicio>().ToTable("Servicio");
    modelBuilder.Entity<Suscriptor>().ToTable("Suscriptor");

    // Tablas de Unión
    modelBuilder.Entity<ContratoPaquete>().ToTable("ContratoPaquete");
    modelBuilder.Entity<PaqueteServicio>().ToTable("PaqueteServicio");
    modelBuilder.Entity<PromocionPaquete>().ToTable("PromocionPaquete");
    modelBuilder.Entity<PromocionCiudad>().ToTable("PromocionCiudad");
    modelBuilder.Entity<PromocionColonia>().ToTable("PromocionColonia");
    modelBuilder.Entity<PromocionContrato>().ToTable("PromocionContrato");

    // 2. Precisión para Decimales
    // --- Definición de precisión para Decimales (CORREGIDO) ---
    modelBuilder.Entity<Contrato>().Property(p => p.PrecioBase).HasColumnType("decimal(6, 2)");
    modelBuilder.Entity<Paquete>().Property(p => p.PrecioBase).HasColumnType("decimal(6, 2)");
    modelBuilder.Entity<PromoPersonalizada>().Property(p => p.PrecioPorcen).HasColumnType("decimal(6, 2)");
    // CORRECCIÓN CLAVE: Se usa 'PrecioPorcen' para la entidad Promocion
    modelBuilder.Entity<Promocion>().Property(p => p.PrecioPorcen).HasColumnType("decimal(6, 2)");
    modelBuilder.Entity<Servicio>().Property(p => p.PrecioBase).HasColumnType("decimal(6, 2)");

    // Definición de valores por defecto.
    modelBuilder.Entity<Promocion>()
      .Property(p => p.FechaRegistro)
      .HasDefaultValueSql("CAST(GETDATE() AS DATE)");

    // 3. Configuración de Llaves Primarias
    modelBuilder.Entity<Ciudad>().HasKey(c => c.Idciudad);
    modelBuilder.Entity<Colonia>().HasKey(c => c.Idcolonia);
    modelBuilder.Entity<Contrato>().HasKey(c => c.Idcontrato);
    modelBuilder.Entity<Paquete>().HasKey(p => p.Idpaquete);
    modelBuilder.Entity<PromoPersonalizada>().HasKey(p => p.Idpromopersonalizada);
    modelBuilder.Entity<Promocion>().HasKey(p => p.Idpromocion);
    modelBuilder.Entity<Servicio>().HasKey(s => s.Idservicio);
    modelBuilder.Entity<Suscriptor>().HasKey(s => s.Idsuscriptor);

    // 4. Relaciones (Llaves Foráneas) y Llaves Primarias
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

    // 4. Tablas de Unión (Muchos a Muchos)
    modelBuilder.Entity<ContratoPaquete>(entity =>
    {
      entity.HasKey(e => new { e.Idcontrato, e.Idpaquete });
      entity.HasOne(d => d.Contrato).WithMany(p => p.Paquetes).HasForeignKey(d => d.Idcontrato);
      entity.HasOne(d => d.Paquete).WithMany(p => p.Contratos).HasForeignKey(d => d.Idpaquete);
    });

    modelBuilder.Entity<PaqueteServicio>(entity =>
    {
      entity.HasKey(e => new { e.Idpaquete, e.Idservicio });
      entity.HasOne(d => d.Paquete).WithMany(p => p.Servicios).HasForeignKey(d => d.Idpaquete);
      entity.HasOne(d => d.Servicio).WithMany(p => p.Paquetes).HasForeignKey(d => d.Idservicio);
    });

    modelBuilder.Entity<PromocionCiudad>(entity =>
    {
      entity.HasKey(e => new { e.Idpromocion, e.Idciudad });
      entity.HasOne(d => d.Promocion).WithMany(p => p.Ciudades).HasForeignKey(d => d.Idpromocion);
      entity.HasOne(d => d.Ciudad).WithMany(p => p.Promociones).HasForeignKey(d => d.Idciudad);
    });

    modelBuilder.Entity<PromocionColonia>(entity =>
    {
      entity.HasKey(e => new { e.Idpromocion, e.Idcolonia });
      entity.HasOne(d => d.Promocion).WithMany(p => p.Colonias).HasForeignKey(d => d.Idpromocion);
      entity.HasOne(d => d.Colonia).WithMany(p => p.Promociones).HasForeignKey(d => d.Idcolonia);
    });

    modelBuilder.Entity<PromocionContrato>(entity =>
    {
      entity.HasKey(e => new { e.Idpromocion, e.Idcontrato });
      entity.HasOne(d => d.Promocion).WithMany(p => p.Contratos).HasForeignKey(d => d.Idpromocion);
      entity.HasOne(d => d.Contrato).WithMany(p => p.Promociones).HasForeignKey(d => d.Idcontrato);
    });

    modelBuilder.Entity<PromocionPaquete>(entity =>
    {
      entity.HasKey(e => new { e.Idpromocion, e.Idpaquete });
      entity.HasOne(d => d.Promocion).WithMany(p => p.Paquetes).HasForeignKey(d => d.Idpromocion);
      entity.HasOne(d => d.Paquete).WithMany(p => p.Promociones).HasForeignKey(d => d.Idpaquete);
    });
  }
}
