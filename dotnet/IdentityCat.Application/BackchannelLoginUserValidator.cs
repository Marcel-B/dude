using System.Security.Claims;
using Duende.IdentityServer.Validation;
using IdentityCat.Domain;
using IdentityModel;

namespace IdentityCat.Application;

public class BackchannelLoginUserValidator : IBackchannelAuthenticationUserValidator
{
    private readonly IUserStore _testUserStore;

    /// <summary>
    /// Ctor
    /// </summary>
    public BackchannelLoginUserValidator(
        IUserStore testUserStore)
    {
        _testUserStore = testUserStore;
    }

    /// <inheritdoc/>
    public async Task<BackchannelAuthenticationUserValidationResult> ValidateRequestAsync(
        BackchannelAuthenticationUserValidatorContext userValidatorContext)
    {
        var result = new BackchannelAuthenticationUserValidationResult();

        User? user = default;

        if (userValidatorContext.LoginHint != null)
        {
            user = await _testUserStore.FindByUsername(userValidatorContext.LoginHint);
        }
        else if (userValidatorContext.IdTokenHintClaims != null)
        {
            user = await _testUserStore
                .FindBySubjectId(userValidatorContext.IdTokenHintClaims
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