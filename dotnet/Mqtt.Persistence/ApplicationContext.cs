using com.b_velop.Mqtt.Domain;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Mqtt.Persistence;

public class ApplicationContext : DbContext
{
    public ApplicationContext(
        DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Device> Devices { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<Timestamp> Timestamps { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<Coordinate> Coordinates { get; set; }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Room>()
            .Property(x => x.Name)
            .HasMaxLength(128);

        modelBuilder
            .Entity<Unit>()
            .Property(x => x.Name)
            .HasMaxLength(64);

        modelBuilder
            .Entity<Unit>()
            .Property(x => x.ShortName)
            .HasMaxLength(64);
        
        modelBuilder
            .Entity<Device>()
            .Property(x => x.Name)
            .HasMaxLength(128);
        
        modelBuilder
            .Entity<Sensor>()
            .Property(x => x.Name)
            .HasMaxLength(64);

        modelBuilder
            .Entity<Coordinate>()
            .Property(x => x.Value)
            .HasMaxLength(32);

        modelBuilder
            .Entity<Coordinate>()
            .HasKey(p => p.Id);

        modelBuilder
            .Entity<Unit>()
            .HasData(
                new Unit(Guid.Parse("F5DCC688-E60A-421D-AE44-AC527F7BFBA7"), "TEMP", "°C"),
                new Unit(Guid.Parse("7D85889D-B1F2-487B-AAFF-8F3E8F6AF445"), "HUM", "%"),
                new Unit(Guid.Parse("266E2EE3-869A-473E-8C26-ED333DEB9301"), "PRESS", "hPa"),
                new Unit(Guid.Parse("8B9460EA-6C8E-4DD4-BBE0-A51537F9CE5A"), "ETHANOL", "%"),
                new Unit(Guid.Parse("37CD8490-4D8D-41F2-A188-6C5E74C6E223"), "CO2", "%"),
                new Unit(Guid.Parse("A727103F-5342-428B-9D9A-B3A0958B1640"), "TVOC", "%"),
                new Unit(Guid.Parse("DAC4DBA1-0D62-4BA7-90A7-36666CB0CACA"), "H2", "%"));
    }
}