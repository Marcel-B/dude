using System.Security.Claims;
using System.Text.Json;
using IdentityCat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdentityCat.Persistence;

public class JsonListConverter : ValueConverter<HashSet<Claim>, string>
{
    private record UserClaim(
        string Type,
        string Value);

    public JsonListConverter() : base(
        v => JsonSerializer.Serialize(v.Select(x => new UserClaim(x.Type, x.Value)), JsonSerializerOptions.Default),
        v => (JsonSerializer
                .Deserialize<List<UserClaim>>(v, JsonSerializerOptions.Default) ?? new List<UserClaim>())
            .Select(x => new Claim(x.Type, x.Value))
            .ToHashSet())
    {
    }
}

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserDbContext(
        DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasKey(x => x.SubjectId);

        modelBuilder
            .Entity<User>()
            .Property(x => x.Password)
            .HasMaxLength(512);

        modelBuilder
            .Entity<User>()
            .Property(x => x.Name)
            .IsUnicode()
            .HasMaxLength(512);

        modelBuilder
            .Entity<User>()
            .Property(x => x.GivenName)
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
            .Property(x => x.UsernameNormalized)
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
            .Entity<User>()
            .Property(x => x.Claims)
            .HasConversion(new JsonListConverter());
    }
}