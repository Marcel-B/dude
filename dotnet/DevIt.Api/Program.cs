using DevIt.Application;
using DevIt.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplication();

var app = builder.Build();
app.UseCors(builder => builder
  .WithOrigins(app.Configuration["Cors"])
  .AllowAnyHeader()
  .AllowAnyMethod());

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
context.Database.Migrate();

app.Run();

// Notwendig für die IntigrationTests
// Nähere Infos unter https://docs.microsoft.com/de-de/aspnet/core/test/integration-tests?view=aspnetcore-6.0#basic-tests-with-the-default-webapplicationfactory
namespace DevIt.Api
{
  public class Program
  {
  }
}
