using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Test;
using Microsoft.Extensions.Logging;

namespace IdentityCat.Application;

/// <summary>
/// Profile service for test users
/// </summary>
/// <seealso cref="IProfileService" />
public class UserProfileService : IProfileService
{
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<UserProfileService> _logger;

    /// <summary>
    /// The users
    /// </summary>
    private readonly IUserStore _users;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestUserProfileService"/> class.
    /// </summary>
    /// <param name="users">The users.</param>
    /// <param name="logger">The logger.</param>
    public UserProfileService(
        IUserStore users,
        ILogger<UserProfileService> logger)
    {
        _users = users;
        _logger = logger;
    }

    /// <summary>
    /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public virtual async Task GetProfileDataAsync(
        ProfileDataRequestContext context)
    {
        context.LogProfileRequest(_logger);

        if (context.RequestedClaimTypes.Any())
        {
            var user = await _users.FindBySubjectId(context.Subject.GetSubjectId());
            context.AddRequestedClaims(user.Claims);
        }

        context.LogIssuedClaims(_logger);
    }

    /// <summary>
    /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
    /// (e.g. during token issuance or validation).
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public virtual async Task IsActiveAsync(
        IsActiveContext context)
    {
        _logger.LogDebug("IsActive called from: {caller}", context.Caller);

        var user = await _users.FindBySubjectId(context.Subject.GetSubjectId());
        context.IsActive = user?.IsActive == true;
    }
}