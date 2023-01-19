using Identity.Servus.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Servus.Persistence;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
  public ApplicationDbContext(DbContextOptions options)
    : base(options)
  {
  }
}
