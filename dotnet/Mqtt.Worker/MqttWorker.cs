using System.ComponentModel;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace Mqtt.Worker;

public static class ServiceCollectionExtension
{
  public static IServiceCollection AddWorker(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddHostedService<MqttWorker>();
    return services;
  }
}


internal static class ObjectExtensions
{
  public static TObject DumpToConsole<TObject>(this TObject @object)
  {
    var output = "NULL";
    if (@object != null)
    {
      output = JsonSerializer.Serialize(@object, new JsonSerializerOptions
      {
        WriteIndented = true
      });
    }

    Console.WriteLine($"[{@object?.GetType().Name}]:\r\n{output}");
    return @object;
  }
}

public class Worker : BackgroundService
{
  private IManagedMqttClient _client;
  private readonly ILogger<Worker> _logger;

  public Worker(ILogger<Worker> logger)
  {
    _logger = logger;
  }

  public override async Task StopAsync(CancellationToken stoppingToken)
  {
    await _client.StopAsync();
    _client.Dispose();
    Console.WriteLine("Client stopped.");
    await base.StopAsync(stoppingToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var log = true;
    var counter = 0;

    var foo = "FkiDons+#geV^FbJpPG7"u8.ToArray();
    try
    {
      var mqttFactory = new MqttFactory();

      _client = mqttFactory.CreateManagedMqttClient();
      var clientOptions = new MqttClientOptions()
      {
        ClientId = "MQTTnetManagedClientTest",
        Credentials = new MqttClientCredentials("aquahi", foo),
        ChannelOptions = new MqttClientTcpOptions
        {
          Server = "13e448dabb7745f592dd52dae76edf23.s2.eu.hivemq.cloud",
          Port = 8883,
          TlsOptions = new MqttClientTlsOptions
          {
            UseTls = true,
            AllowUntrustedCertificates = false,
            IgnoreCertificateChainErrors = false,
            IgnoreCertificateRevocationErrors = false
          }
        }
      };
      var managedMqttClientOptions = new ManagedMqttClientOptionsBuilder()
        .WithClientOptions(clientOptions)
        .Build();
      await _client.StartAsync(managedMqttClientOptions);

      // The application message is not sent. It is stored in an internal queue and
      // will be sent when the client is connected.
      await _client.EnqueueAsync("foo/DS18B20/TEMP", "hahaha");
      await _client.SubscribeAsync("proxima/DS18B20/TEMP");
      await _client.SubscribeAsync("foo/lala");

      _client.ApplicationMessageReceivedAsync += e =>
      {
        Console.WriteLine("Received application message.");
        var payload = System.Text.Encoding.Default.GetString(e.ApplicationMessage.PayloadSegment);
        var address = e.ApplicationMessage.Topic;
        Console.WriteLine("Topic: " + address);
        Console.WriteLine("Value: " + payload);
        return Task.CompletedTask;
      };

      Console.WriteLine("The managed MQTT client is connected.");

      // Wait until the queue is fully processed.
      SpinWait.SpinUntil(() => _client.PendingApplicationMessagesCount == 0, 10000);
      Console.WriteLine(_client.IsConnected);
      Console.WriteLine($"Pending messages = {_client.PendingApplicationMessagesCount}");
      Console.WriteLine("Hello, World!");
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
    }

    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
    await Task.Delay(10_000, stoppingToken);
  }
}

public static class Subscribe
{
  public static async Task Handle()
  {
  }
}

