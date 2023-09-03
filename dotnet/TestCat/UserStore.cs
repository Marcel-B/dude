using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Test;
using Duende.IdentityServer.Validation;
using IdentityCat.Domain;
using IdentityCat.UserAdapter;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;

namespace TestCat;

public class BackchannelLoginUserValidator : IBackchannelAuthenticationUserValidator
{
    private readonly UserStore _testUserStore;

    /// <summary>
    /// Ctor
    /// </summary>
    public BackchannelLoginUserValidator(
        UserStore testUserStore)
    {
        _testUserStore = testUserStore;
    }

    /// <inheritdoc/>
    public async Task<BackchannelAuthenticationUserValidationResult> ValidateRequestAsync(
        BackchannelAuthenticationUserValidatorContext userValidatorContext)
    {
        var result = new BackchannelAuthenticationUserValidationResult();

        AppUser user = default;

        if (userValidatorContext.LoginHint != null)
        {
            user = await _testUserStore.FindByUsername(userValidatorContext.LoginHint);
        }
        else if (userValidatorContext.IdTokenHintClaims != null)
        {
            user = await _testUserStore.FindBySubjectId(userValidatorContext.IdTokenHintClaims
                .SingleOrDefault(x => x.Type == JwtClaimTypes.Subject)
                ?.Value);
        }

        if (user != null && user.IsActive)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.SubjectId)
            };
            var ci = new ClaimsIdentity(claims, "ciba");
            result.Subject = new ClaimsPrincipal(ci);
        }

        return result;
    }
}

public class UserResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserStore _users;
    private readonly ISystemClock _clock;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestUserResourceOwnerPasswordValidator"/> class.
    /// </summary>
    /// <param name="users">The users.</param>
    /// <param name="clock">The clock.</param>
    public UserResourceOwnerPasswordValidator(
        UserStore users,
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

/// <summary>
/// Profile service for test users
/// </summary>
/// <seealso cref="IProfileService" />
public class UserProfileService : IProfileService
{
    /// <summary>
    /// The logger
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// The users
    /// </summary>
    protected readonly UserStore Users;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestUserProfileService"/> class.
    /// </summary>
    /// <param name="users">The users.</param>
    /// <param name="logger">The logger.</param>
    public UserProfileService(
        UserStore users,
        ILogger<UserProfileService> logger)
    {
        Users = users;
        Logger = logger;
    }

    /// <summary>
    /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public virtual async Task GetProfileDataAsync(
        ProfileDataRequestContext context)
    {
        context.LogProfileRequest(Logger);

        if (context.RequestedClaimTypes.Any())
        {
            var user = await Users.FindBySubjectId(context.Subject.GetSubjectId());
            if (user != null)
            {
                context.AddRequestedClaims(user.Claims);
            }
        }

        context.LogIssuedClaims(Logger);
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
        Logger.LogDebug("IsActive called from: {caller}", context.Caller);

        var user = await Users.FindBySubjectId(context.Subject.GetSubjectId());
        context.IsActive = user?.IsActive == true;
    }
}

public interface IUserStore
{
}

public class UserStore : IUserStore
{
    private readonly IUserAdapter _userAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestUserStore"/> class.
    /// </summary>
    /// <param name="userAdapter"></param>
    public UserStore(
        IUserAdapter userAdapter)
    {
        _userAdapter = userAdapter;
    }

    /// <summary>
    /// Validates the credentials.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <returns></returns>
    public async Task<bool> ValidateCredentials(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = await _userAdapter.FindByUsername(username, cancellationToken);
        var valid = false;

        if (user is not null)
        {
            valid = password.Verify(user.Salt, user.Password);
        }

        return valid;
    }

    /// <summary>
    /// Finds the user by subject identifier.
    /// </summary>
    /// <param name="subjectId">The subject identifier.</param>
    /// <returns></returns>
    public async Task<AppUser> FindBySubjectId(
        string subjectId,
        CancellationToken cancellationToken = default)
    {
        var user = await _userAdapter.FindBySubjectId(subjectId, cancellationToken);
        return AppUser.Create(user);
    }

    /// <summary>
    /// Finds the user by username.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <returns></returns>
    public async Task<AppUser> FindByUsername(
        string username,
        CancellationToken cancellationToken = default)
    {
        var user = await _userAdapter.FindByUsername(username, cancellationToken);
        return AppUser.Create(user);
    }

    /// <summary>
    /// Finds the user by external provider.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    public async Task<User> FindByExternalProvider(
        string provider,
        string userId)
    {
        throw new NotImplementedException();
        // return _users.FirstOrDefault(x =>
        //     x.ProviderName == provider &&
        //     x.ProviderSubjectId == userId);
    }

    /// <summary>
    /// Automatically provisions a user.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="claims">The claims.</param>
    /// <returns></returns>
    public User AutoProvisionUser(
        string provider,
        string userId,
        List<Claim> claims)
    {
        // create a list of claims that we want to transfer into our store
        var filtered = new List<Claim>();

        foreach (var claim in claims)
        {
            // if the external system sends a display name - translate that to the standard OIDC name claim
            if (claim.Type == ClaimTypes.Name)
            {
                filtered.Add(new Claim(JwtClaimTypes.Name, claim.Value));
            }
            // if the JWT handler has an outbound mapping to an OIDC claim use that
            else if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(claim.Type))
            {
                filtered.Add(new Claim(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[claim.Type], claim.Value));
            }
            // copy the claim as-is
            else
            {
                filtered.Add(claim);
            }
        }

        // if no display name was provided, try to construct by first and/or last name
        if (!filtered.Any(x => x.Type == JwtClaimTypes.Name))
        {
            var first = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)
                ?.Value;
            var last = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)
                ?.Value;
            if (first != null && last != null)
            {
                filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
            }
            else if (first != null)
            {
                filtered.Add(new Claim(JwtClaimTypes.Name, first));
            }
            else if (last != null)
            {
                filtered.Add(new Claim(JwtClaimTypes.Name, last));
            }
        }

        // create a new unique subject id
        var sub = CryptoRandom.CreateUniqueId(format: CryptoRandom.OutputFormat.Hex);

        // check if a display name is available, otherwise fallback to subject id
        var name = filtered.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)
            ?.Value ?? sub;

        // create new user
        // var user = new User
        // {
        //     SubjectId = sub,
        //     Username = name,
        //     ProviderName = provider,
        //     ProviderSubjectId = userId,
        //     Claims = filtered
        // };
        //
        // // add user to in-memory store
        // _users.Add(user);
        //
        // return user;
        return null;
    }

    // /// <summary>
    // /// Adds a new a user.
    // /// </summary>
    // /// <returns></returns>
    // public TestUser CreateUser(
    //     string username,
    //     string password,
    //     string name = null,
    //     string email = null)
    // {
    //     if (_users.Any(x => x.Username == username))
    //     {
    //         throw new Exception("That username already exists.");
    //     }
    //
    //     // create a new unique subject id
    //     var sub = CryptoRandom.CreateUniqueId(format: CryptoRandom.OutputFormat.Hex);
    //
    //     var claims = new List<Claim>();
    //     if (!String.IsNullOrEmpty(name))
    //     {
    //         claims.Add(new Claim(ClaimTypes.Name, name));
    //     }
    //
    //     if (!String.IsNullOrEmpty(email))
    //     {
    //         claims.Add(new Claim(ClaimTypes.Email, email));
    //     }
    //
    //     // create new user
    //     var user = new TestUser
    //     {
    //         SubjectId = sub,
    //         Username = username,
    //         Password = password,
    //         Claims = claims
    //     };
    //
    //     // add user to in-memory store
    //     _users.Add(user);
    //
    //     // success
    //     return user;
    // }
}