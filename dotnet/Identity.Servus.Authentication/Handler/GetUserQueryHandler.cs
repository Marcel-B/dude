using Identity.Servus.Authentication.Queries;
using Identity.Servus.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Servus.Authentication.Handler;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, AppUser>
{
    private readonly UserManager<AppUser> _userManager;

    public GetUserQueryHandler(
        UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AppUser> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        return user;
    }
}