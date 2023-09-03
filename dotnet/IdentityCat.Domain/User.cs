using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentValidation;
using IdentityModel;

namespace IdentityCat.Domain;

using System;

public class AppUser
{
    public string SubjectId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string? Email { get; set; }
    public string? ProviderName { get; set; }
    public string? ProviderSubjectId { get; set; }
    public bool IsActive { get; set; } = false;
    public virtual ICollection<Claim> Claims { get; set; }

    private AppUser()
    {
    }

    public static AppUser Create(
        User user)
    {
        var filtered = new List<Claim>();
        foreach (var claim in user.Claims)
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
                filtered.Add(new Claim(claim.Type, claim.Value));
            }
        }
        
        filtered.Add(new Claim(JwtClaimTypes.PreferredUserName, user.Username));

        var au = new AppUser
        {
            SubjectId = user.SubjectId,
            Username = user.Username,
            Password = user.Password,
            Salt = user.Salt,
            Email = user.Email,
            ProviderName = user.ProviderName,
            ProviderSubjectId = user.ProviderSubjectId,
            IsActive = user.IsActive,
            Claims = filtered
        };
        return au;
    }
}

public class User
{
    private class UserValidator : AbstractValidator<CreateUserCommand>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email));
        }
    }

    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the subject identifier.
    /// </summary>
    public string SubjectId { get; set; }

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the Salt.
    /// </summary>
    public string Salt { get; set; }

    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the provider name.
    /// </summary>
    public string? ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the provider subject identifier.
    /// </summary>
    public string? ProviderSubjectId { get; set; }

    /// <summary>
    /// Gets or sets if the user is active.
    /// </summary>
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// Gets or sets the claims.
    /// </summary>
    public virtual ICollection<UserClaim> Claims { get; set; }


    private User()
    {
        Claims = new HashSet<UserClaim>();
    }

    public static User Create(
        CreateUserCommand cmd)
    {
        var isValid = new UserValidator().Validate(cmd);
        if (!isValid.IsValid)
            throw new ValidationException(isValid.Errors);

        var salt = PasswordExtensions.GenerateSalt(16);
        var hashedPassword = cmd.Password.Hash(salt);
        return new User
        {
            Username = cmd.Username,
            Password = hashedPassword,
            SubjectId = CryptoRandom.CreateUniqueId(format: CryptoRandom.OutputFormat.Hex),
            Salt = Convert.ToBase64String(salt)
        };
    }

    public record CreateUserCommand(
        string Username,
        string Password,
        string? Email = default);
}