using com.b_velop.IdentityCat.Service.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace com.b_velop.IdentityCat.Service.Pages.Account.Logout;

[SecurityHeaders]
[AllowAnonymous]
public class LoggedOut : PageModel
{
    private readonly IIdentityServerInteractionService _interactionService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoggedOutViewModel View { get; set; }

    public LoggedOut(
        IIdentityServerInteractionService interactionService, 
        SignInManager<ApplicationUser> signInManager)

    {
        _interactionService = interactionService;
        _signInManager = signInManager;
    }

    public async Task OnGet(
        string logoutId)
    {
        // get context information (client name, post logout redirect URI and iframe for federated signout)
        var logout = await _interactionService.GetLogoutContextAsync(logoutId);
        await _signInManager.SignOutAsync();

        View = new LoggedOutViewModel
        {
            AutomaticRedirectAfterSignOut = LogoutOptions.AutomaticRedirectAfterSignOut,
            PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
            ClientName = String.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
            SignOutIframeUrl = logout?.SignOutIFrameUrl
        };
    }
}