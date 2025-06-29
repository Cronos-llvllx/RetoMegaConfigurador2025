using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.data;

/// <summary>
/// El contexto de la API a la base de datos.
/// </summary>
/// <param name="options"></param>
public class MEGADbContext(DbContextOptions<MEGADbContext> options) : DbContext(options)
{
  /// <summary>Entidad Ciudad.</summary>
  public DbSet<Ciudad> Ciudad => Set<Ciudad>();
  /// <summary>Entidad Colonia.</summary>
  public DbSet<Colonia> Colonia => Set<Colonia>();
  /// <summary>Entidad Contrato.</summary>
  public DbSet<Contrato> Contrato => Set<Contrato>();
  /// <summary>Entidad PromoPersonalizada</summary>
  public DbSet<PromoPersonalizada> PromoPersonalizada => Set<PromoPersonalizada>();
  /// <summary>Relación Contrato-Paquete.</summary>
  public DbSet<ContratoPaquete> ContratoPaquete => Set<ContratoPaquete>();
  /// <summary>Entidad Paquete.</summary>
  public DbSet<Paquete> Paquetes => Set<Paquete>();
  /// <summary>Relación Paquete-Servicio.</summary>
  public DbSet<PaqueteServicio> PaqueteServicio => Set<PaqueteServicio>();
  /// <summary>Entidad Promocion.</summary>
  public DbSet<Promocion> Promocion => Set<Promocion>();
  /// <summary>Relación Promocion-Ciudad</summary>
  public DbSet<PromocionCiudad> PromocionCiudad => Set<PromocionCiudad>();
  /// <summary>Relación Promocion-Colonia.</summary>
  public DbSet<PromocionColonia> PromocionColonia => Set<PromocionColonia>();
  /// <summary>Relación Promocion-Contrato.</summary>
  public DbSet<PromocionContrato> PromocionContrato => Set<PromocionContrato>();
  /// <summary>Relación Promocion-Paquete.</summary>
  public DbSet<PromocionPaquete> PromocionPaquete => Set<PromocionPaquete>();
  /// <summary>Entidad Servicio.</summary>
  public DbSet<Servicio> Servicio => Set<Servicio>();
  /// <summary>Entidad Suscriptor.</summary>
  public DbSet<Suscriptor> Suscriptor => Set<Suscriptor>();

  /// <summary>Se ejecuta cuando se crea el modelo de la BD.</summary>
  /// <param name="modelBuilder"></param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Define las llaves primarias de entidades.
    modelBuilder.Entity<Ciudad>().HasKey(e => e.Idciudad);
    modelBuilder.Entity<Colonia>().HasKey(e => e.IdColonia);
    modelBuilder.Entity<Contrato>().HasKey(e => e.Idcontrato);
    modelBuilder.Entity<PromoPersonalizada>().HasKey(e => e.Idpromopersonalizada);
    modelBuilder.Entity<Paquete>().HasKey(e => e.Idpaquete);
    modelBuilder.Entity<Promocion>().HasKey(e => e.Idpromocion);
    modelBuilder.Entity<Servicio>().HasKey(e => e.Idservicio);
    modelBuilder.Entity<Suscriptor>().HasKey(e => e.Idsuscriptor);

    // Asigna la precisión para tipos de dato decimal.
    modelBuilder.Entity<Contrato>()
      .Property(c => c.PrecioBase)
      .HasPrecision(6, 2);
    modelBuilder.Entity<Paquete>()
      .Property(c => c.PrecioBase)
      .HasPrecision(6, 2);
    modelBuilder.Entity<Promocion>()
      .Property(c => c.PrecioBase)
      .HasPrecision(6, 2);
    modelBuilder.Entity<PromoPersonalizada>()
      .Property(c => c.PrecioPorcen)
      .HasPrecision(6, 2);
    modelBuilder.Entity<Servicio>()
      .Property(c => c.PrecioBase)
      .HasPrecision(6, 2);

    // Define las llaves foráneas de las entidades.
    modelBuilder.Entity<Colonia>()
      .HasOne(col => col.Ciudad)
      .WithMany(ciu => ciu.Colonias)
      .HasForeignKey(col => col.Idciudad);

    modelBuilder.Entity<Suscriptor>()
      .HasOne(sus => sus.Colonia)
      .WithMany()
      .HasForeignKey(sus => sus.Idcolonia);

    modelBuilder.Entity<Contrato>()
      .HasOne(con => con.Suscriptor)
      .WithOne()
      .HasForeignKey<Contrato>(con => con.Idsuscriptor);

    modelBuilder.Entity<PromoPersonalizada>()
      .HasOne<Contrato>()
      .WithMany()
      .HasForeignKey(pp => pp.Idcontrato);

    // Define las llaves primarias de relaciones.
    modelBuilder.Entity<ContratoPaquete>().HasKey(e => new { e.Idcontrato, e.Idpaquete });
    modelBuilder.Entity<PaqueteServicio>().HasKey(e => new { e.Idpaquete, e.Idservicio });
    modelBuilder.Entity<PromocionCiudad>().HasKey(e => new { e.Idpromocion, e.Idciudad });
    modelBuilder.Entity<PromocionColonia>().HasKey(e => new { e.Idpromocion, e.Idcolonia });
    modelBuilder.Entity<PromocionContrato>().HasKey(e => new { e.Idpromocion, e.Idcontrato });
    modelBuilder.Entity<PromocionPaquete>().HasKey(e => new { e.Idpromocion, e.Idpaquete });

    // Define las llaves foráneas de relaciones.
    modelBuilder.Entity<ContratoPaquete>()
      .HasOne(cp => cp.Contrato)
      .WithMany()
      .HasForeignKey(cp => cp.Idcontrato);

    modelBuilder.Entity<ContratoPaquete>()
      .HasOne(cp => cp.Paquete)
      .WithMany()
      .HasForeignKey(cp => cp.Idpaquete);

    modelBuilder.Entity<PaqueteServicio>()
      .HasOne(ps => ps.Paquete)
      .WithMany()
      .HasForeignKey(ps => ps.Idpaquete);

    modelBuilder.Entity<PaqueteServicio>()
      .HasOne(ps => ps.Servicio)
      .WithMany()
      .HasForeignKey(ps => ps.Idservicio);

    modelBuilder.Entity<PromocionCiudad>()
      .HasOne(pc => pc.Promocion)
      .WithMany()
      .HasForeignKey(pc => pc.Idpromocion);

    modelBuilder.Entity<PromocionCiudad>()
      .HasOne(pc => pc.Ciudad)
      .WithMany()
      .HasForeignKey(pc => pc.Idciudad);

    modelBuilder.Entity<PromocionColonia>()
      .HasOne(pc => pc.Promocion)
      .WithMany()
      .HasForeignKey(pc => pc.Idpromocion);

    modelBuilder.Entity<PromocionColonia>()
      .HasOne(pc => pc.Colonia)
      .WithMany()
      .HasForeignKey(pc => pc.Idcolonia);

    modelBuilder.Entity<PromocionContrato>()
      .HasOne(pc => pc.Promocion)
      .WithMany()
      .HasForeignKey(pc => pc.Idpromocion);

    modelBuilder.Entity<PromocionContrato>()
      .HasOne(pc => pc.Contrato)
      .WithMany()
      .HasForeignKey(pc => pc.Idcontrato);

    modelBuilder.Entity<PromocionPaquete>()
      .HasOne(pp => pp.Promocion)
      .WithMany()
      .HasForeignKey(pp => pp.Idpromocion);

    modelBuilder.Entity<PromocionPaquete>()
      .HasOne(pp => pp.Paquete)
      .WithMany()
      .HasForeignKey(pp => pp.Idpaquete);
  }
}