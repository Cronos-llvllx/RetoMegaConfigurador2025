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