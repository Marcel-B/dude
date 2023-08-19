using Microsoft.EntityFrameworkCore;
using Mqtt.Domain;

namespace Mqtt.Persistence;

public class ApplicationContext : DbContext
{
  public DbSet<Device> Devices { get; set; }
  public DbSet<Room> Rooms { get; set; }
  public DbSet<Sensor> Sensors { get; set; }
  public DbSet<Timestamp> Timestamps { get; set; }
  public DbSet<Unit> Units { get; set; }
  public DbSet<Measurement> Measurements { get; set; }

  public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Room>()
      .Property(x => x.Name)
      .HasMaxLength(128);

    modelBuilder.Entity<Unit>()
      .Property(x => x.Name)
      .HasMaxLength(64);

    modelBuilder.Entity<Unit>()
      .Property(x => x.ShortName)
      .HasMaxLength(64);

    modelBuilder.Entity<Device>()
      .Property(x => x.Name)
      .HasMaxLength(128);

    modelBuilder.Entity<Sensor>()
      .Property(x => x.Name)
      .HasMaxLength(64);

    modelBuilder
      .Entity<Unit>()
      .HasData(
        new Unit(id: Guid.Parse("F5DCC688-E60A-421D-AE44-AC527F7BFBA7"), name: "TEMP", shortName: "°C"),
        new Unit(id: Guid.Parse("7D85889D-B1F2-487B-AAFF-8F3E8F6AF445"), name: "HUM", shortName: "%"),
        new Unit(id: Guid.Parse("266E2EE3-869A-473E-8C26-ED333DEB9301"), name: "PRESS", shortName: "hPa"),
        new Unit(id: Guid.Parse("8B9460EA-6C8E-4DD4-BBE0-A51537F9CE5A"), name: "ETHANOL", shortName: "%"),
        new Unit(id: Guid.Parse("37CD8490-4D8D-41F2-A188-6C5E74C6E223"), name: "CO2", shortName: "%"),
        new Unit(id: Guid.Parse("A727103F-5342-428B-9D9A-B3A0958B1640"), name: "TVOC", shortName: "%"),
        new Unit(id: Guid.Parse("DAC4DBA1-0D62-4BA7-90A7-36666CB0CACA"), name: "H2", shortName: "%"));
  }
}
