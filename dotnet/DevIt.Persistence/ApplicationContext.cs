using System.Text.Json;
using com.b_velop.DevIt.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevIt.Persistence;

public class JsonValueConverter<T> : ValueConverter<T, string>
{
  public JsonValueConverter(JsonSerializerOptions options = null) : base(
    v => JsonSerializer.Serialize(v, options),
    v => JsonSerializer.Deserialize<T>(v, options))
  {
  }

  public override Type ModelClrType => typeof(T);

  public override Type ProviderClrType => typeof(string);
}

public class ApplicationContext : DbContext
{
  public DbSet<Projekt> Projekte { get; set; }
  public DbSet<Pbi> Pbis { get; set; }
  public DbSet<Eintrag> Eintraege { get; set; }

  public ApplicationContext(DbContextOptions<ApplicationContext> builder) : base(builder)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Projekt>().HasMany(o => o.Pbis).WithOne();
    modelBuilder.Entity<Eintrag>().Property(p => p.ExterneId).HasMaxLength(128);
    modelBuilder.Entity<Eintrag>().HasIndex(p => p.ExterneId).IsUnique();
    modelBuilder.Entity<Eintrag>().Property(p => p.Text).HasMaxLength(1024);
    modelBuilder.Entity<Pbi>().Property(p => p.Name).HasMaxLength(1024);
    modelBuilder.Entity<Projekt>().Property(p => p.ExterneId).HasMaxLength(128);
    modelBuilder.Entity<Projekt>().HasIndex(p => p.ExterneId).IsUnique();
    modelBuilder.Entity<Projekt>().Property(p => p.Name).HasMaxLength(1024);
  }
}
