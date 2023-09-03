using IdentityCat.Domain;
using Microsoft.EntityFrameworkCore;

namespace IdentityCat.Persistence;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }

    public UserDbContext(
        DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .Property(x => x.Password)
            .HasMaxLength(512);
        
        modelBuilder
            .Entity<User>()
            .Property(x => x.Salt)
            .HasMaxLength(128);

        modelBuilder
            .Entity<User>()
            .Property(x => x.Username)
            .HasMaxLength(64);

        modelBuilder
            .Entity<User>()
            .Property(x => x.Email)
            .HasMaxLength(128);

        modelBuilder
            .Entity<User>()
            .Property(x => x.ProviderName)
            .HasMaxLength(128);

        modelBuilder
            .Entity<User>()
            .Property(x => x.ProviderSubjectId)
            .HasMaxLength(128);

        modelBuilder
            .Entity<User>()
            .Property(x => x.SubjectId)
            .HasMaxLength(64);

        modelBuilder
            .Entity<UserClaim>()
            .Property(x => x.Value)
            .HasMaxLength(64);

        modelBuilder
            .Entity<UserClaim>()
            .Property(x => x.Type)
            .HasMaxLength(64);
    }
}