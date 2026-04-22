using Microsoft.EntityFrameworkCore;
using Cafeteria.Data;


var builder = WebApplication.CreateBuilder(args);

// 1. Obtener la cadena de conexión del archivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("CadenaConnection");

// 2. Registrar el DbContext para usar SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(connectionString));

// Agregar servicios al contenedor (Controladores, Swagger, etc.)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();