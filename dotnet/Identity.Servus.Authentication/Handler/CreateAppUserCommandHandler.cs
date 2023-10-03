using System.Security.Claims;
using Azure.Core;
using Identity.Servus.Authentication.Commands;
using Identity.Servus.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Servus.Authentication.Handler;

public record AppUserCreated(bool Success, AppUser? user = null, IList<IdentityError>? ErrorMessages = null);

public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, AppUserCreated>
{
  private readonly IUserStore<AppUser> _userStore;
  private readonly UserManager<AppUser> _userManager;

  public CreateAppUserCommandHandler(
    IUserStore<AppUser> userStore,
    UserManager<AppUser> userManager)
  {
    _userStore = userStore;
    _userManager = userManager;
  }

  private IUserEmailStore<Domain.AppUser> GetEmailStore()
  {
    if (!_userManager.SupportsUserEmail)
    {
      throw new NotSupportedException("The default UI requires a user store with email support.");
    }

    return (IUserEmailStore<AppUser>) _userStore;
  }

  public async Task<AppUserCreated> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
  {
    var user = AppUser.Create(request.Email, request.Email);
    await _userStore.SetUserNameAsync(user, request.Email, CancellationToken.None);
    var emailStore = GetEmailStore();
    await emailStore.SetEmailAsync(user, request.Email, CancellationToken.None);
    var result = await _userManager.CreateAsync(user, request.Password);
    await _userManager.AddClaimAsync(user, new Claim("email", request.Email));
    await _userManager.AddClaimAsync(user, new Claim("administrator", "true"));
    return result.Succeeded
      ? new AppUserCreated(true, user)
      : new AppUserCreated(false, ErrorMessages: result.Errors.ToList());
  }
}
