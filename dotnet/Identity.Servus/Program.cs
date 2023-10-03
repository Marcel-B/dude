using Identity.Servus;
using Identity.Servus.Application;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddIdentityDomain();
services.AddRazorPages();
services.AddScoped<IEmailSender, EmailSender>();

// Note: in a real world application, this step should be part of a setup script.
services.AddHostedService<Worker>();
var app = builder.Build();

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(options => options.MapRazorPages());

await app.RunAsync();
