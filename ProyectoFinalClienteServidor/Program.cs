using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProyectoFinalClienteServidor.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<sistem21_aeromexdbContext>(
optionsBuilder => optionsBuilder.UseMySql("server=198.38.83.169;database=sistem21_aeromexdb;password=Slr!s1587;user=sistem21_AeroMexDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb"))
) ;
var app = builder.Build();
app.UseDeveloperExceptionPage();
app.MapControllers();
app.Run();
