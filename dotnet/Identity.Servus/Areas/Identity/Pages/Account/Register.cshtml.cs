// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.ComponentModel.DataAnnotations;
using Identity.Servus.Authentication.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Servus.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly ILogger<RegisterModel> _logger;
    private readonly IMediator _mediator;
    private readonly SignInManager<Domain.AppUser> _signInManager;

    public RegisterModel(
        IMediator mediator,
        SignInManager<Domain.AppUser> signInManager,
        ILogger<RegisterModel> logger)
    {
        _mediator = mediator;
        _signInManager = signInManager;
        _logger = logger;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public async Task OnGetAsync(
        string returnUrl = null)
    {
        ReturnUrl = returnUrl;
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    }

    public async Task<IActionResult> OnPostAsync(
        string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        if (!ModelState.IsValid) return Page();

        var result = await _mediator.Send(new CreateAppUserCommand
        {
            Email = Input.Email,
            Password = Input.Password
        }, CancellationToken.None);

        if (result.Success)
        {
            var user = result.user;
            _logger.LogInformation("User created a new account with password.");

            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                null,
                new {area = "Identity", userId = "{0}", code = "{1}", returnUrl},
                Request.Scheme);

            var requireConfirmedAccount =
                await _mediator.Send(new ConfirmEmailCommand(user, callbackUrl), CancellationToken.None);

            if (requireConfirmedAccount)
                return RedirectToPage("RegisterConfirmation", new {email = Input.Email, returnUrl});

            await _signInManager.SignInAsync(user, false);
            return LocalRedirect(returnUrl);
        }

        foreach (var error in result.ErrorMessages) ModelState.AddModelError(string.Empty, error.Description);

        // If we got this far, something failed, redisplay form
        return Page();
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel
    {
      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
      [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
      [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
      [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}