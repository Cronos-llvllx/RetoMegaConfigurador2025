using megaapi.data;
using megaapi.interfaces;
using megaapi.repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- INICIO DE LA CORRECCIÓN ---
// 1. Leer la cadena de conexión explícitamente desde la configuración.
//    Esto buscará en appsettings.json y luego en appsettings.Development.json,
//    usando el valor del último archivo si existe.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Añadir el servicio DbContext, pasándole la cadena de conexión que acabamos de leer.
//    Esta es la forma estándar y más robusta de registrar el DbContext.
builder.Services.AddDbContext<MEGADbContext>(options =>
    options.UseSqlServer(connectionString));
// --- FIN DE LA CORRECCIÓN ---


// Add services to the container.
// Se inyectan las dependencias de los repositorios para que los controladores puedan usarlos.
builder.Services.AddScoped<ICiudad, RepoCiudad>();
builder.Services.AddScoped<IColonia, RepoColonia>();
builder.Services.AddScoped<IContrato, RepoContrato>();
builder.Services.AddScoped<IPaquete, RepoPaquete>();
builder.Services.AddScoped<IPromoPersonalizada, RepoPromopersonalizada>();
builder.Services.AddScoped<IPromocion, RepoPromocion>();
builder.Services.AddScoped<IServicio, RepoServicio>();
builder.Services.AddScoped<ISuscriptor, RepoSuscriptor>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();