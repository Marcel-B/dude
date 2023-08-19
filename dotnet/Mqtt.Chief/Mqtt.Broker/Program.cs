using Microsoft.EntityFrameworkCore;
using Mqtt.Broker;
using Mqtt.Measurement.Adapter;
using Mqtt.Persistence;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddPersistence(builder.Configuration);
services.AddHostedService<MqttWorker>();
services.AddCommandHandlers();
var app = builder.Build();
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
context.Database.Migrate();
app.UseRouting();
app.Run();
