using com.b_velop.DevIt.Service;
using com.b_velop.DevIt.Service.Services;
using DevIt.Application;
using DevIt.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<EintragService>();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddGrpc();
var mocoConfiguration = builder
    .Configuration.GetSection("Moco")
    .Get<MocoConfiguration>();
builder.Services.TryAddSingleton(mocoConfiguration ?? throw new InvalidOperationException());
builder.Services.AddQuartz(q =>
{
    // Erstellen Sie einen Job
    var jobKey = new JobKey("MeinJob", "MeineGruppe");
    q.AddJob<GetFoo>(opts => opts.WithIdentity(jobKey));

    // Erstellen Sie einen Trigger
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("MeinJobTrigger", "MeineGruppe")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithIntervalInHours(1)
            .RepeatForever())
    );
});

builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);

var app = builder.Build();
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
await context.Database.MigrateAsync();

app.UseCors(builder => builder
    .WithOrigins(app
        .Configuration["Cors"]
        .Split(','))
    .AllowAnyHeader()
    .AllowAnyMethod());


//app.UseAuthentication();
// app.UseAuthorization();
app.MapControllers();
app.MapGrpcService<EintragService>();
app.MapGrpcService<AbrechnungService>();
app.MapGrpcService<PbiService>();
await app.RunAsync();

// Notwendig für die IntigrationTests
// Nähere Infos unter https://docs.microsoft.com/de-de/aspnet/core/test/integration-tests?view=aspnetcore-6.0#basic-tests-with-the-default-webapplicationfactory
namespace com.b_velop.DevIt.Service
{
    public class Program
    {
    }
}