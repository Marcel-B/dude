using Mqtt.Chief;

Host.CreateDefaultBuilder(args)
  .ConfigureServices(services => { services.AddHostedService<Worker>(); })
  .Build()
  .Run();
