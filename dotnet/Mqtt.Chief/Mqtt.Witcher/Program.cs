using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

var log = true;
var counter = 0;

var foo = "FkiDons+#geV^FbJpPG7"u8.ToArray();
try
{
  var mqttFactory = new MqttFactory();

  using (var managedMqttClient = mqttFactory.CreateManagedMqttClient())
  {
    var ClientOptions = new MqttClientOptions()
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
      .WithClientOptions(ClientOptions)
      .Build();
    await managedMqttClient.StartAsync(managedMqttClientOptions);

    // The application message is not sent. It is stored in an internal queue and
    // will be sent when the client is connected.
    await managedMqttClient.EnqueueAsync("foo/DS18B20/TEMP", "hahaha");
    await managedMqttClient.SubscribeAsync("proxima/DS18B20/TEMP");

    managedMqttClient.ApplicationMessageReceivedAsync += e =>
    {
      Console.WriteLine("Received application message.");
      var payload = System.Text.Encoding.Default.GetString(e.ApplicationMessage.PayloadSegment);
      var address = e.ApplicationMessage.Topic;
      Console.WriteLine("Topic: " + address);
      Console.WriteLine("Value: " + payload);
      //e.DumpToConsole();
      return Task.CompletedTask;
    };

    Console.WriteLine("The managed MQTT client is connected.");

    // Wait until the queue is fully processed.
    SpinWait.SpinUntil(() => managedMqttClient.PendingApplicationMessagesCount == 0, 10000);
    Console.WriteLine(managedMqttClient.IsConnected);
    Console.WriteLine($"Pending messages = {managedMqttClient.PendingApplicationMessagesCount}");
    Console.WriteLine("Hello, World!");
    Console.ReadKey();
    Console.WriteLine("Hello, asdf!");
  }
//////////////////////////
}
catch (Exception e)
{
  Console.WriteLine(e);
}
