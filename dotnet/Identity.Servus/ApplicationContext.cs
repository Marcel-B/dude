using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Servus;

public class ApplicationContext : IdentityDbContext<AppUser>
{
  public ApplicationContext(DbContextOptions options)
    : base(options)
  {
  }
}
