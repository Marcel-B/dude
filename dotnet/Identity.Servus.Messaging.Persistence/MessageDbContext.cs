using Microsoft.EntityFrameworkCore;

namespace Identity.Servus.Persistence;

public class MessageDbContext : DbContext
{
    public MessageDbContext(
        DbContextOptions<MessageDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
    }
}