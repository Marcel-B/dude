using Identity.Servus.Authentication.Handler;
using MediatR;

namespace Identity.Servus.Authentication.Commands;

public class CreateAppUserCommand : IRequest<AppUserCreated>
{
  public string Email { get; set; }
  public string Password { get; set; }
}
