namespace com.b_velop.Mqtt.Chief;

internal record Application
{
    public string UserName { get; init; }
    public string ClientId { get; init; }
    public string Password { get; init; }
    public string Server { get; init; }
    public string Port { get; init; }
    public string TestMode { get; init; }
    public IEnumerable<string> Subscriptions { get; init; }
}