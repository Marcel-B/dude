namespace com.b_velop.Mqtt.Domain;

public class Measurement : Entity
{
    public Sensor Sensor { get; set; }
    public Guid SensorId { get; set; }
    public double Value { get; set; }
    public Timestamp Timestamp { get; set; }
    public Guid TimestampId { get; set; }

    public static Measurement Create(
        Sensor sensor,
        double value,
        Timestamp timestamp)
    {
        return new Measurement
        {
            Created = DateTimeOffset.Now,
            SensorId = sensor.Id,
            Value = value,
            TimestampId = timestamp.Id
        };
    }
}