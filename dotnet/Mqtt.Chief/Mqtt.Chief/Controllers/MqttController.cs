using MQTTnet.Server;

namespace com.b_velop.Mqtt.Chief.Controllers;

public class MqttController
{
    public Task OnClientConnected(
        ClientConnectedEventArgs eventArgs)
    {
        Console.WriteLine($"Client '{eventArgs.ClientId}' connected.");
        return Task.CompletedTask;
    }


    public Task ValidateConnection(
        ValidatingConnectionEventArgs eventArgs)
    {
        Console.WriteLine($"Client '{eventArgs.ClientId}' wants to connect. Accepting!");
        return Task.CompletedTask;
    }
}