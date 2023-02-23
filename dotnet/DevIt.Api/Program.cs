using DevIt.Application;
using DevIt.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//   .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

var app = builder.Build();
app.UseCors(builder => builder
  .WithOrigins(app.Configuration["Cors"]
    .Split(','))
  .AllowAnyHeader()
  .AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


//app.UseAuthentication();
// app.UseAuthorization();
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
