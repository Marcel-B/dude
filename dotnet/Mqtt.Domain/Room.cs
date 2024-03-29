using System.Collections.ObjectModel;

namespace com.b_velop.Mqtt.Domain;

public class Room : Entity
{
    public string Name { get; set; }
    public virtual ICollection<Device> Devices { get; set; } = new Collection<Device>();
}