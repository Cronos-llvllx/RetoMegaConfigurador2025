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
  public DbSet<Ciudad> Ciudades => Set<Ciudad>();
  /// <summary>Entidad Colonia.</summary>
  public DbSet<Colonia> Colonias => Set<Colonia>();
  /// <summary>Entidad Contrato.</summary>
  public DbSet<Contrato> Contratos => Set<Contrato>();
  /// <summary>Relación Contrato-Paquete.</summary>
  public DbSet<ContratoPaquete> ContratosPaquetes => Set<ContratoPaquete>();
  /// <summary>Entidad Paquete.</summary>
  public DbSet<Paquete> Paquetes => Set<Paquete>();
  /// <summary>Relación Paquete-Servicio.</summary>
  public DbSet<PaqueteServicio> PaquetesServicios => Set<PaqueteServicio>();
  /// <summary>Entidad Promocion.</summary>
  public DbSet<Promocion> Promociones => Set<Promocion>();
  /// <summary>Relación Promocion-Ciudad</summary>
  public DbSet<PromocionCiudad> PromocionesCiudades => Set<PromocionCiudad>();
  /// <summary>Relación Promocion-Colonia.</summary>
  public DbSet<PromocionColonia> PromocionesColonias => Set<PromocionColonia>();
  /// <summary>Relación Promocion-Contrato.</summary>
  public DbSet<PromocionContrato> PromocionesContratos => Set<PromocionContrato>();
  /// <summary>Relación Promocion-Paquete.</summary>
  public DbSet<PromocionPaquete> PromocionesPaquetes => Set<PromocionPaquete>();
  /// <summary>Entidad Servicio.</summary>
  public DbSet<Servicio> Servicios => Set<Servicio>();
  /// <summary>Entidad Suscriptor.</summary>
  public DbSet<Suscriptor> Suscriptores => Set<Suscriptor>();

  /// <summary>Se ejecuta cuando se crea el modelo de la BD.</summary>
  /// <param name="modelBuilder"></param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Define las llaves primarias de entidades.
    modelBuilder.Entity<Ciudad>().HasKey(e => e.Idciudad);
    modelBuilder.Entity<Colonia>().HasKey(e => e.IdColonia);
    modelBuilder.Entity<Contrato>().HasKey(e => e.Idcontrato);
    modelBuilder.Entity<Paquete>().HasKey(e => e.Idpaquete);
    modelBuilder.Entity<Promocion>().HasKey(e => e.Idpromocion);
    modelBuilder.Entity<Servicio>().HasKey(e => e.Idservicio);
    modelBuilder.Entity<Suscriptor>().HasKey(e => e.Idsuscriptor);

    // Define las llaves foráneas de las entidades.
    modelBuilder.Entity<Colonia>()
      .HasOne(col => col.Ciudad)
      .WithMany(ciu => ciu.Colonias)
      .HasForeignKey(col => col.Idciudad);

    modelBuilder.Entity<Suscriptor>()
      .HasOne(sus => sus.Colonia)
      .WithMany(col => col.Suscriptores)
      .HasForeignKey(sus => sus.Idcolonia);

    modelBuilder.Entity<Contrato>()
      .HasOne(con => con.Suscriptor)
      .WithOne(sus => sus.Contrato)
      .HasForeignKey<Contrato>(con => con.Idsuscriptor);

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
      .WithMany(con => con.Paquetes)
      .HasForeignKey(cp => cp.Idcontrato);

    modelBuilder.Entity<ContratoPaquete>()
      .HasOne(cp => cp.Paquete)
      .WithMany(paq => paq.Contratos)
      .HasForeignKey(cp => cp.Idpaquete);

    modelBuilder.Entity<PaqueteServicio>()
      .HasOne(ps => ps.Paquete)
      .WithMany(paq => paq.Servicios)
      .HasForeignKey(ps => ps.Idpaquete);

    modelBuilder.Entity<PaqueteServicio>()
      .HasOne(ps => ps.Servicio)
      .WithMany(ser => ser.Paquetes)
      .HasForeignKey(ps => ps.Idservicio);

    modelBuilder.Entity<PromocionCiudad>()
      .HasOne(pc => pc.Promocion)
      .WithMany(pro => pro.Ciudades)
      .HasForeignKey(pc => pc.Idpromocion);

    modelBuilder.Entity<PromocionCiudad>()
      .HasOne(pc => pc.Ciudad)
      .WithMany(ciu => ciu.Promociones)
      .HasForeignKey(pc => pc.Idciudad);

    modelBuilder.Entity<PromocionColonia>()
      .HasOne(pc => pc.Promocion)
      .WithMany(pro => pro.Colonias)
      .HasForeignKey(pc => pc.Idpromocion);

    modelBuilder.Entity<PromocionColonia>()
      .HasOne(pc => pc.Colonia)
      .WithMany(col => col.Promociones)
      .HasForeignKey(pc => pc.Idcolonia);

    modelBuilder.Entity<PromocionContrato>()
      .HasOne(pc => pc.Promocion)
      .WithMany(pro => pro.Contratos)
      .HasForeignKey(pc => pc.Idpromocion);

    modelBuilder.Entity<PromocionContrato>()
      .HasOne(pc => pc.Contrato)
      .WithMany(con => con.Promociones)
      .HasForeignKey(pc => pc.Idcontrato);

    modelBuilder.Entity<PromocionPaquete>()
      .HasOne(pp => pp.Promocion)
      .WithMany(pro => pro.Paquetes)
      .HasForeignKey(pp => pp.Idpromocion);

    modelBuilder.Entity<PromocionPaquete>()
      .HasOne(pp => pp.Paquete)
      .WithMany(paq => paq.Promociones)
      .HasForeignKey(pp => pp.Idpaquete);
  }
}