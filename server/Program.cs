using megaapi.data;
using megaapi.interfaces;
using megaapi.repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- INICIO DE CONFIGURACIÓN DE CORS ---
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
        options.AddPolicy(name: MyAllowSpecificOrigins,
                        policy =>
                        {
                            // Se permite el origen de tu aplicación de Angular
                            policy.WithOrigins("http://localhost:4200")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                        });
    });
// --- FIN DE CONFIGURACIÓN DE CORS ---


// Se lee la cadena de conexión explícitamente.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Se registra el DbContext con la cadena de conexión.
builder.Services.AddDbContext<MEGADbContext>(options =>
    options.UseSqlServer(connectionString));

// Inyección de dependencias para los repositorios.
builder.Services.AddScoped<IContrato, RepoContrato>();
builder.Services.AddScoped<IPaquete, RepoPaquete>();
builder.Services.AddScoped<IContratoPaquete, RepoContratoPaquete>();
// ... (Aquí van tus otras inyecciones de dependencias si las tienes)
// Modelos de relaciones.
builder.Services.AddScoped<IPaqueteServicio, RepoPaqueteServicio>();
builder.Services.AddScoped<IPromocionCiudad, RepoPromocionCiudad>();
builder.Services.AddScoped<IPromocionColonia, RepoPromocionColonia>();
builder.Services.AddScoped<IPromocionContrato, RepoPromocionContrato>();
builder.Services.AddScoped<IPromocionPaquete, RepoPromocionPaquete>();
// ** Dependencias de interfaces y repositorios.
builder.Services.AddScoped<ICiudad, RepoCiudad>(); // Modelos de Ciudad.
builder.Services.AddScoped<IColonia, RepoColonia>(); // Modelos de Colonia.
builder.Services.AddScoped<IPromoPersonalizada, RepoPromoPersonalizada>(); // Modelos de promos personalizadas.
builder.Services.AddScoped<IPromocion, RepoPromocion>(); // Modelos de Promoción.
builder.Services.AddScoped<IServicio, RepoServicio>(); // Modelos de Servicio.
builder.Services.AddScoped<ISuscriptor, RepoSuscriptor>(); // Modelos de Suscriptor,

builder.Services.AddControllers()
    // Añadir esta configuración para evitar errores con los datos anidados
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Se comenta la redirección a HTTPS para desarrollo local
// app.UseHttpsRedirection();

// --- SE APLICA LA POLÍTICA DE CORS ---
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();