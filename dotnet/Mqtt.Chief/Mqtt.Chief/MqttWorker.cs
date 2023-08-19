using System.Text;
using System.Text.Json;
using Mqtt.Measurement.Adapter;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace com.b_velop.Mqtt.Chief;

internal static class ObjectExtensions
{
    public static TObject DumpToConsole<TObject>(
        this TObject @object)
    {
        var output = "NULL";
        if (@object != null)
            output = JsonSerializer.Serialize(@object, new JsonSerializerOptions
            {
                WriteIndented = true
            });

        Console.WriteLine($"[{@object?.GetType().Name}]:\r\n{output}");
        return @object;
    }
}

public class MqttWorker : BackgroundService
{
    private readonly ILogger<MqttWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private IManagedMqttClient _client;
    private IEnumerable<string> _subscriptions;

    public MqttWorker(
        ILogger<MqttWorker> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public override async Task StopAsync(
        CancellationToken stoppingToken)
    {
        foreach (var subscription in _subscriptions)
            await _client.UnsubscribeAsync(subscription);

        await _client.StopAsync();
        _client.Dispose();
        Console.WriteLine("Client stopped.");
        await base.StopAsync(stoppingToken);
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var log = true;
        var counter = 0;

        do
        {
            try
            {
                var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<CreateMeasurementCommand>>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var application = configuration
                    .GetSection("Application")
                    .Get<Application>();
                _subscriptions = application?.Subscriptions ??
                                 throw new InvalidOperationException("No subscriptions found.");
                Console.WriteLine(application.TestMode);
                var mqttFactory = new MqttFactory();
                _client = mqttFactory.CreateManagedMqttClient();
                var clientOptions = new MqttClientOptions
                {
                    ClientId = application.ClientId,
                    Credentials = new MqttClientCredentials(application.UserName,
                        Encoding.ASCII.GetBytes(application.Password)),
                    ChannelOptions = new MqttClientTcpOptions
                    {
                        Server = application.Server,
                        Port = int.Parse(application.Port),
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

                foreach (var subscription in _subscriptions)
                    await _client.SubscribeAsync(subscription);

                _client.ApplicationMessageReceivedAsync += async e =>
                {
                    Console.WriteLine("Received application message.");
                    var payload = Encoding.Default.GetString(e.ApplicationMessage.PayloadSegment);
                    var address = e.ApplicationMessage.Topic;
                    Console.WriteLine("Topic: " + address);
                    Console.WriteLine("Value: " + payload);
                    var command = new CreateMeasurementCommand(address, payload);
                    await handler.HandleAsync(command, stoppingToken);
                };

                Console.WriteLine("The managed MQTT client is connected.");

                // Wait until the queue is fully processed.
                SpinWait.SpinUntil(() => _client.PendingApplicationMessagesCount == 0, 10000);
                Console.WriteLine(_client.IsConnected);
                Console.WriteLine($"Pending messages = {_client.PendingApplicationMessagesCount}");
                Console.WriteLine("Hello, World!");
                log = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await Task.Delay(3_000, stoppingToken);
            }

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        } while (log);
    }
}