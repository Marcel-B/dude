namespace com.b_velop.Mqtt.Domain;

public class Entity
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
}