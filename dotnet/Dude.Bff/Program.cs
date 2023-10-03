using com.b_velop.Dude.Bff.Services;
using com.b_velop.Dude.Shared;
using Grpc.Core;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using AbrechnungService = com.b_velop.Dude.Bff.Services.AbrechnungService;
using EintragService = com.b_velop.Dude.Bff.Services.EintragService;
using Measurement = com.b_velop.Dude.Shared.Measurement;
using PbiService = com.b_velop.Dude.Bff.Services.PbiService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder
    .Services.AddGrpcClient<Gps.GpsClient>(o =>
    {
        o.Address = new Uri(builder.Configuration["grpc"] ??
                            throw new InvalidOperationException("No grpc address configured"));
    })
    .ConfigureChannel(options =>
    {
        options.UnsafeUseInsecureChannelCallCredentials = true;
        options.Credentials = ChannelCredentials.Insecure;
    })
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
    .Services.AddGrpcClient<com.b_velop.Dude.Shared.PbiService.PbiServiceClient>(o =>
    {
        o.Address = new Uri(builder.Configuration["devit"] ??
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
builder.Services.AddScoped<IPbiService, PbiService>();
builder.Services.AddScoped<IProjektService, ProjektService>();
builder.Services.AddScoped<IGpsService, GpsService>();

builder
    .Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration.GetSection("authority").Value ?? "https://idsrv.marcelbenders.com";
        options.RequireHttpsMetadata = false;
        IdentityModelEventSource.ShowPII = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
var app = builder.Build();
app.UseCors(options =>
{
    options
        .WithOrigins("http://localhost:9000", "http://localhost:8067")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .Build();
});
app.UseStaticFiles();
app.MapFallbackToFile("index.html");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();