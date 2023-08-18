using Identity.Servus;
using Identity.Servus.Application;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddIdentityDomain();
services.AddRazorPages();
services.AddScoped<IEmailSender, EmailSender>();
services.AddCors();

IdentityModelEventSource.ShowPII = true;

// Note: in a real world application, this step should be part of a setup script.
services.AddHostedService<Worker>();
var app = builder.Build();

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyOrigin();
    policyBuilder.AllowAnyMethod();
    policyBuilder.AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(options => options.MapRazorPages());

await app.RunAsync();