using Duende.IdentityServer.Test;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;

namespace IdentityCat.Application;

public class UserResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly IUserStore _users;
    private readonly ISystemClock _clock;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestUserResourceOwnerPasswordValidator"/> class.
    /// </summary>
    /// <param name="users">The users.</param>
    /// <param name="clock">The clock.</param>
    public UserResourceOwnerPasswordValidator(
        IUserStore users,
        ISystemClock clock)
    {
        _users = users;
        _clock = clock;
    }

    /// <summary>
    /// Validates the resource owner password credential
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public async Task ValidateAsync(
        ResourceOwnerPasswordValidationContext context)
    {
        if (await _users.ValidateCredentials(context.UserName, context.Password))
        {
            var user = await _users.FindByUsername(context.UserName);
            context.Result = new GrantValidationResult(
                user.SubjectId ?? throw new ArgumentException("Subject ID not set", nameof(user.SubjectId)),
                OidcConstants.AuthenticationMethods.Password, _clock.UtcNow.UtcDateTime,
                user.Claims);
        }
    }
}