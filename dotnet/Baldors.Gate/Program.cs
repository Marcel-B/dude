using LettuceEncrypt;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel((context, options) =>
{
  options.ConfigureHttpsDefaults(httpsOptions =>
  {
    options.ListenAnyIP(443,
      portOptions => { portOptions.UseHttps(h => { h.UseLettuceEncrypt(options.ApplicationServices); }); });
  });
});
var services = builder.Services;

services
  .AddReverseProxy()
  .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

services
  .AddLettuceEncrypt();
  //.PersistDataToDirectory(new DirectoryInfo("/app/config"), "Password123");

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapReverseProxy(); });
app.Run();
