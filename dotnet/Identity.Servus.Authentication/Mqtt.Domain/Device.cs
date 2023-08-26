using System.Collections.ObjectModel;

namespace com.b_velop.Mqtt.Domain;

/// <summary>
///     Ein Device ist ein physisches Gerät, das in einem Raum steht, z.B. ein Arduino mit Sensoren.
/// </summary>
public class Device : Entity
{
    public string Name { get; set; }
    public Room? Room { get; set; }
    public Guid? RoomId { get; set; }

    public virtual ICollection<Sensor> Sensors { get; set; } = new Collection<Sensor>();

    public static Device Create(
        string name,
        Room? room = null)
    {
        return new Device
        {
            Created = DateTimeOffset.Now,
            Name = name ?? throw new ArgumentNullException(nameof(name)),
            Room = room
        };
    }
}