using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.data;

/// <summary>
/// El contexto de la API a la base de datos.
/// </summary>
/// <param name="options"></param>
public class MEGADbContext(DbContextOptions<MEGADbContext> options) : DbContext(options)
{
    // DbSet<T> deben ser plurales para seguir la convención.
    public DbSet<Ciudad> Ciudades => Set<Ciudad>();
    public DbSet<Colonia> Colonias => Set<Colonia>();
    public DbSet<Contrato> Contratos => Set<Contrato>(); // <<-- CORREGIDO
    public DbSet<PromoPersonalizada> PromosPersonalizadas => Set<PromoPersonalizada>();
    public DbSet<ContratoPaquete> ContratosPaquetes => Set<ContratoPaquete>(); // <<-- CORREGIDO
    public DbSet<Paquete> Paquetes => Set<Paquete>();
    public DbSet<PaqueteServicio> PaquetesServicios => Set<PaqueteServicio>(); // <<-- CORREGIDO
    public DbSet<Promocion> Promociones => Set<Promocion>(); // <<-- CORREGIDO
    public DbSet<PromocionCiudad> PromocionesCiudades => Set<PromocionCiudad>(); // <<-- CORREGIDO
    public DbSet<PromocionColonia> PromocionesColonias => Set<PromocionColonia>(); // <<-- CORREGIDO
    public DbSet<PromocionContrato> PromocionesContratos => Set<PromocionContrato>(); // <<-- CORREGIDO
    public DbSet<PromocionPaquete> PromocionesPaquetes => Set<PromocionPaquete>(); // <<-- CORREGIDO
    public DbSet<Servicio> Servicios => Set<Servicio>();
    public DbSet<Suscriptor> Suscriptores => Set<Suscriptor>();

    /// <summary>Se ejecuta cuando se crea el modelo de la BD.</summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ... (las llaves primarias de entidades están correctas) ...
        modelBuilder.Entity<Ciudad>().HasKey(e => e.Idciudad);
        modelBuilder.Entity<Colonia>().HasKey(e => e.IdColonia);
        modelBuilder.Entity<Contrato>().HasKey(e => e.Idcontrato);
        modelBuilder.Entity<PromoPersonalizada>().HasKey(e => e.Idpromopersonalizada);
        modelBuilder.Entity<Paquete>().HasKey(e => e.Idpaquete);
        modelBuilder.Entity<Promocion>().HasKey(e => e.Idpromocion);
        modelBuilder.Entity<Servicio>().HasKey(e => e.Idservicio);
        modelBuilder.Entity<Suscriptor>().HasKey(e => e.Idsuscriptor);

        // ... (precisión para tipos de dato decimal - corrige la propiedad `PrecioBase` en Promocion.cs a `PrecioPorcen` si es necesario) ...
        modelBuilder.Entity<Contrato>().Property(c => c.PrecioBase).HasPrecision(6, 2);
        modelBuilder.Entity<Paquete>().Property(c => c.PrecioBase).HasPrecision(6, 2);
        modelBuilder.Entity<Promocion>().Property(c => c.PrecioBase).HasPrecision(6, 2); 
        modelBuilder.Entity<PromoPersonalizada>().Property(c => c.PrecioPorcen).HasPrecision(6, 2);
        modelBuilder.Entity<Servicio>().Property(c => c.PrecioBase).HasPrecision(6, 2);
        
        // ... (llaves foráneas de entidades están correctas) ...
        modelBuilder.Entity<Colonia>().HasOne(col => col.Ciudad).WithMany(ciu => ciu.Colonias).HasForeignKey(col => col.Idciudad);
        modelBuilder.Entity<Suscriptor>().HasOne(sus => sus.Colonia).WithMany(col => col.Suscriptores).HasForeignKey(sus => sus.Idcolonia);
        modelBuilder.Entity<Contrato>().HasOne(con => con.Suscriptor).WithOne(sus => sus.Contrato).HasForeignKey<Contrato>(con => con.Idsuscriptor);
        modelBuilder.Entity<PromoPersonalizada>().HasOne(pp => pp.Contrato).WithMany(con => con.PromosPersonalizadas).HasForeignKey(pp => pp.Idcontrato);

        // ... (llaves primarias de relaciones están correctas) ...
        modelBuilder.Entity<ContratoPaquete>().HasKey(e => new { e.Idcontrato, e.Idpaquete });
        modelBuilder.Entity<PaqueteServicio>().HasKey(e => new { e.Idpaquete, e.Idservicio });
        modelBuilder.Entity<PromocionCiudad>().HasKey(e => new { e.Idpromocion, e.Idciudad });
        modelBuilder.Entity<PromocionColonia>().HasKey(e => new { e.Idpromocion, e.Idcolonia });
        modelBuilder.Entity<PromocionContrato>().HasKey(e => new { e.Idpromocion, e.Idcontrato });
        modelBuilder.Entity<PromocionPaquete>().HasKey(e => new { e.Idpromocion, e.Idpaquete });

        // ... (llaves foráneas de relaciones están correctas, pero las colecciones deben coincidir con los modelos) ...
        modelBuilder.Entity<ContratoPaquete>().HasOne(cp => cp.Contrato).WithMany(con => con.Paquetes).HasForeignKey(cp => cp.Idcontrato);
        modelBuilder.Entity<ContratoPaquete>().HasOne(cp => cp.Paquete).WithMany(paq => paq.Contratos).HasForeignKey(cp => cp.Idpaquete);

        modelBuilder.Entity<PaqueteServicio>().HasOne(ps => ps.Paquete).WithMany(paq => paq.Servicios).HasForeignKey(ps => ps.Idpaquete);
        modelBuilder.Entity<PaqueteServicio>().HasOne(ps => ps.Servicio).WithMany(ser => ser.Paquetes).HasForeignKey(ps => ps.Idservicio);

        modelBuilder.Entity<PromocionCiudad>().HasOne(pc => pc.Promocion).WithMany(pro => pro.Ciudades).HasForeignKey(pc => pc.Idpromocion);
        modelBuilder.Entity<PromocionCiudad>().HasOne(pc => pc.Ciudad).WithMany(ciu => ciu.Promociones).HasForeignKey(pc => pc.Idciudad);

        modelBuilder.Entity<PromocionColonia>().HasOne(pc => pc.Promocion).WithMany(pro => pro.Colonias).HasForeignKey(pc => pc.Idpromocion);
        modelBuilder.Entity<PromocionColonia>().HasOne(pc => pc.Colonia).WithMany(col => col.Promociones).HasForeignKey(pc => pc.Idcolonia);

        modelBuilder.Entity<PromocionContrato>().HasOne(pc => pc.Promocion).WithMany(pro => pro.Contratos).HasForeignKey(pc => pc.Idpromocion);
        modelBuilder.Entity<PromocionContrato>().HasOne(pc => pc.Contrato).WithMany(con => con.Promociones).HasForeignKey(pc => pc.Idcontrato); // <<-- AQUI ESTA EL ERROR, LA PROPIEDAD NO EXISTE EN EL MODELO
        
        modelBuilder.Entity<PromocionPaquete>().HasOne(pp => pp.Promocion).WithMany(pro => pro.Paquetes).HasForeignKey(pp => pp.Idpromocion);
        modelBuilder.Entity<PromocionPaquete>().HasOne(pp => pp.Paquete).WithMany(paq => paq.Promociones).HasForeignKey(pp => pp.Idpaquete);
    }
}