using com.b_velop.Dude.Shared;
using com.b_velop.Measurement.Services;
using Grpc.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder
    .Services.AddGrpcClient<Measurement.MeasurementClient>(o => { o.Address = new Uri(builder.Configuration["grpc"]); })
    .ConfigureChannel(options =>
    {
        options.UnsafeUseInsecureChannelCallCredentials = true;
        options.Credentials = ChannelCredentials.Insecure;
    });
builder.Services.AddTransient<MeasurementService>();
var app = builder.Build();

app.UseCors(options =>
{
    options
        .WithOrigins("http://localhost:9000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .Build();
});
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

// Configure the HTTP request pipeline.
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
// var s = app.Services.GetRequiredService<MeasurementService>();
// var r = await s.GetMeasurement(Guid.Parse("7E5099F9-7C6C-46A2-F47F-08DBA421818D"));
// Console.WriteLine($"____DD {r}");
app.Run();