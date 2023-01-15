using System.Net;
using LettuceEncrypt;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel((context, options) =>
{
  var appServices = options.ApplicationServices;
  options.Listen(
    IPAddress.Any, 443,
    o => o.UseHttps(h => { h.UseLettuceEncrypt(appServices); }));
});
var services = builder.Services;

services
  .AddReverseProxy()
  .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

services
  .AddLettuceEncrypt()
  .PersistDataToDirectory(new DirectoryInfo("/app/config"), "Password123");

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapReverseProxy(); });
app.Run();
