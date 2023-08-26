using System.Collections.ObjectModel;

namespace com.b_velop.Mqtt.Domain;

/// <summary>
///     Ein Sensor ist ein physisches Bauteil, das eine physikalische Größe misst, z.B. Temperatur, Luftfeuchtigkeit, ...
/// </summary>
public class Sensor : Entity
{
    public string Name { get; set; }
    public virtual Unit Unit { get; set; }
    public Guid UnitId { get; set; }

    public virtual Device Device { get; set; }
    public Guid DeviceId { get; set; }

    public virtual ICollection<Measurement> Measurements { get; set; } = new Collection<Measurement>();

    public static Sensor Create(
        string name,
        Device device,
        Unit unit)
    {
        return new Sensor
        {
            Created = DateTimeOffset.Now,
            DeviceId = device.Id,
            Name = name ?? throw new ArgumentNullException(nameof(name)),
            Unit = unit ?? throw new ArgumentNullException(nameof(unit))
        };
    }
}