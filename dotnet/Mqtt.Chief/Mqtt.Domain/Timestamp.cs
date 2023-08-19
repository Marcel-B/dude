using System.Collections.ObjectModel;

namespace com.b_velop.Mqtt.Domain;

public class Timestamp : Entity
{
    public DateTimeOffset DateTime { get; set; }
    public virtual ICollection<Measurement> Measurements { get; set; } = new Collection<Measurement>();

    public static Timestamp Create(
        DateTimeOffset dateTime)
    {
        return new Timestamp
        {
            Created = DateTimeOffset.Now,
            DateTime = dateTime
        };
    }
}