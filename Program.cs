using Lonches_Restaurant.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Obtenemos la cadena de conexi�n
var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");

//Agregamos la configuraci�n para SQLServer
builder.Services.AddDbContext<LonchesRestaurantContext>(options => options.UseSqlServer(connectionString));

//Definimos la nueva pol�tica Cross-Origin Resource Sharing (CORS o Uso compartido de recurso entre or�genes) para que la API sea accesible para cualquier aplicaci�n
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Activamos la nueva pol�tica
app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
