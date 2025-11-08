using Software2.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Aquí se registra la relación: Pide la Interfaz (ICatalogoService) y entrega la Clase (CatalogoService).
// Esto hace que la Inyección de Dependencias funcione y aplica el principio DIP.
builder.Services.AddScoped<ICatalogoService, CatalogoService>();

// Registro de la Interfaz y Clase para Notificaciones (SRP)
builder.Services.AddScoped<INotificacionService, NotificacionService>();

// Registro de la Interfaz y Clase para Reservas (DIP)
// ReservaService ahora puede recibir sus dependencias (ICatalogoService e INotificacionService)
builder.Services.AddScoped<IReservaService, ReservaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
