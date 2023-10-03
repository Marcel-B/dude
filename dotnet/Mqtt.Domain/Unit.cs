using System.Collections.ObjectModel;

namespace com.b_velop.Mqtt.Domain;

/// <summary>
///     Eine Unit ist eine physikalische Einheit, z.B. °C, %, m/s, ...
/// </summary>
public class Unit : Entity
{
    public Unit()
    {
    }

    public Unit(
        Guid id,
        string name,
        string shortName)
    {
        Id = id;
        Name = name;
        ShortName = shortName;
    }

    public string Name { get; set; }
    public string ShortName { get; set; }
    public virtual ICollection<Sensor> Sensors { get; set; } = new Collection<Sensor>();
}