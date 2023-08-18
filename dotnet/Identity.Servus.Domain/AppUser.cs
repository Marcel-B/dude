using Microsoft.AspNetCore.Identity;

namespace Identity.Servus.Domain;

public class AppUser : IdentityUser
{
    private AppUser()
    {
    }

    public static AppUser Create(
        string userName,
        string email)
    {
        return new AppUser
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName)),
            Email = email ?? throw new ArgumentNullException(nameof(email))
        };
    }
}