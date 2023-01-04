using DevIt.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevIt.Persistence;

public class ApplicationContext : DbContext
{
  public DbSet<Projekt> Projekte { get; set; }
  public DbSet<Pbi> Pbis { get; set; }

  public ApplicationContext(DbContextOptions<ApplicationContext> builder) : base(builder)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder
      .Entity<Pbi>()
      .HasOne<Projekt>()
      .WithMany()
      .HasForeignKey(x => x.ProjektId);
  }
}
