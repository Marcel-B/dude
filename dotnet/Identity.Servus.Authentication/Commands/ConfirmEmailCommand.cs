using Identity.Servus.Domain;
using MediatR;

namespace Identity.Servus.Authentication.Commands;

/// <summary>
///     Command to confirm a user's email address.
/// </summary>
/// <param name="User">New User</param>
/// <param name="CallbackUrl">The Callback URL send by Email</param>
public record ConfirmEmailCommand(
    AppUser User,
    string CallbackUrl) : IRequest<bool>;