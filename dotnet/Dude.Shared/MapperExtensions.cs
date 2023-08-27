using Google.Protobuf;

namespace com.b_velop.Dude.Shared;

public static class MapperExtensions
{
    public static DateTimeOffset? ToDateTimeOffset(
        this long timestamp)
    {
        return timestamp <= 0 ? null : DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
    }

    public static long ToProto(
        this DateTimeOffset? dateTimeOffset)
    {
        return dateTimeOffset is null ? 0 : dateTimeOffset.Value.ToUnixTimeMilliseconds();
    }

    public static System.Guid ToSystem(
        this Guid guid)
    {
        try
        {
            return new System.Guid(guid.Value.ToByteArray());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{guid}] [{guid.Value.ToByteArray()}] {ex.Message}");
            throw;
        }
    }

    public static Guid ToProto(
        this System.Guid guid)
    {
        try
        {
            return new Guid
            {
                Value = ByteString.CopyFrom(guid.ToByteArray())
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{guid}] {ex.Message}");
            throw;
        }
    }
}