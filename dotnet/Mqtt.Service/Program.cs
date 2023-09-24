using com.b_velop.Mqtt.Gps.Service;
using com.b_velop.Mqtt.Measurement.Service;
using com.b_velop.Mqtt.Persistence;
using com.b_velop.Mqtt.Service;
using Microsoft.EntityFrameworkCore;
using Mqtt.Measurement.Adapter;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddPersistence(builder.Configuration);
services.AddCommandHandlers();
services.AddMeasurementService();
services.AddGpsService();
services.AddHostedService<MqttWorker>();

var app = builder.Build();
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
context.Database.Migrate();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints
        .MapGrpcService<GpsService>();
    endpoints
        .MapGrpcService<MeasurementService>();
    endpoints.MapFallback(() => "Alive and kickin'");
});

app.Run();