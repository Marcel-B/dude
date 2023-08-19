using com.b_velop.Mqtt.Chief;
using com.b_velop.Mqtt.Persistence;
using Microsoft.EntityFrameworkCore;
using Mqtt.Measurement.Adapter;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddPersistence(builder.Configuration);
services.AddHostedService<MqttWorker>();
services.AddCommandHandlers();
var app = builder.Build();
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
context.Database.Migrate();
app.UseRouting();
app.Run();