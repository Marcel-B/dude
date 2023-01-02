var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
  .AddReverseProxy()
  .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapReverseProxy(); });
app.Run();
