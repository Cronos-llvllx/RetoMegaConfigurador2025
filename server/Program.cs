using megaapi.data;
using megaapi.interfaces;
using megaapi.repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ** CORS.
builder.Services.AddCors(options =>
{
  options.AddPolicy("Frontend", policy =>
  {
    // @deprecated Esto permite cualquier origen, debe ser reeplazado por el origen del frontend.
    policy.AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod();
  });
});

// ** Dependencia DbContext.
builder.Services.AddDbContext<MEGADbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("db")!);
});

// ** Dependencias de interfaces y repositorios.
builder.Services.AddScoped<ICiudad, RepoCiudad>(); // Modelos de Ciudad.
builder.Services.AddScoped<IColonia, RepoColonia>(); // Modelos de Colonia.
builder.Services.AddScoped<IContrato, RepoContrato>(); // Modelos de Contrato.
builder.Services.AddScoped<IPromoPersonalizada, RepoPromoPersonalizada>(); // Modelos de promos personalizadas.
builder.Services.AddScoped<IPaquete, RepoPaquete>(); // Modelos de Paquete.
builder.Services.AddScoped<IPromocion, RepoPromocion>(); // Modelos de Promoción.
builder.Services.AddScoped<IServicio, RepoServicio>(); // Modelos de Servicio.
builder.Services.AddScoped<ISuscriptor, RepoSuscriptor>(); // Modelos de Suscriptor,
// Modelos de relaciones.
builder.Services.AddScoped<IContratoPaquete, RepoContratoPaquete>();
builder.Services.AddScoped<IPaqueteServicio, RepoPaqueteServicio>();
builder.Services.AddScoped<IPromocionCiudad, RepoPromocionCiudad>();
builder.Services.AddScoped<IPromocionColonia, RepoPromocionColonia>();
builder.Services.AddScoped<IPromocionContrato, RepoPromocionContrato>();
builder.Services.AddScoped<IPromocionPaquete, RepoPromocionPaquete>();

// ** Agregar controladores.
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ** Build.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// ** Redirección HTTPS (si lo permite).
//app.UseHttpsRedirection();
// ** Cors.
app.UseCors("Frontend");
// ** Mapeo de controladores (para que pueda detectar los controladores definidos).
app.MapControllers();

app.Run();