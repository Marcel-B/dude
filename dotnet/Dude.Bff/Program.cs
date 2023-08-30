using com.b_velop.Dude.Bff.Services;
using com.b_velop.Dude.Shared;
using Grpc.Core;
using AbrechnungService = com.b_velop.Dude.Bff.Services.AbrechnungService;
using EintragService = com.b_velop.Dude.Bff.Services.EintragService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder
    .Services.AddGrpcClient<Measurement.MeasurementClient>(o =>
    {
        o.Address = new Uri(builder.Configuration["grpc"] ??
                            throw new InvalidOperationException("No grpc address configured"));
    })
    .ConfigureChannel(options =>
    {
        options.UnsafeUseInsecureChannelCallCredentials = true;
        options.Credentials = ChannelCredentials.Insecure;
    })
    .Services.AddGrpcClient<com.b_velop.Dude.Shared.AbrechnungService.AbrechnungServiceClient>(o =>
    {
        o.Address = new Uri(builder.Configuration["devit"] ??
                            throw new InvalidOperationException("No grpc address configured"));
    })
    .ConfigureChannel(options =>
    {
        options.UnsafeUseInsecureChannelCallCredentials = true;
        options.Credentials = ChannelCredentials.Insecure;
    })
    .Services.AddGrpcClient<com.b_velop.Dude.Shared.EintragService.EintragServiceClient>(o =>
    {
        o.Address = new Uri(builder.Configuration["devit"] ??
                            throw new InvalidOperationException("No devit address configured"));
    })
    .ConfigureChannel(options =>
    {
        options.UnsafeUseInsecureChannelCallCredentials = true;
        options.Credentials = ChannelCredentials.Insecure;
    })
    ;
builder.Services.AddTransient<MeasurementService>();
builder.Services.AddScoped<IEintragService, EintragService>();
builder.Services.AddScoped<IAbrechnungService, AbrechnungService>();

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