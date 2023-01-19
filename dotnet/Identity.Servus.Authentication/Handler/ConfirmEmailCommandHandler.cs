using System.Text;
using System.Text.Encodings.Web;
using Identity.Servus.Authentication.Commands;
using Identity.Servus.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace Identity.Servus.Authentication.Handler;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
  private readonly UserManager<AppUser> _userManager;
  private readonly IEmailSender _emailSender;

  public ConfirmEmailCommandHandler(
    UserManager<AppUser> userManager,
    IEmailSender emailSender)
  {
    _userManager = userManager;
    _emailSender = emailSender;
  }

  public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
  {
    var user = request.User;
    var userId = await _userManager.GetUserIdAsync(user);
    var email = user.Email;
    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
    var callbackUrl = request.CallbackUrl.Replace("%7B0%7D", userId)
      .Replace("%7B1%7D", code);

    await _emailSender.SendEmailAsync(email, "Confirm your email",
      $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

    return _userManager.Options.SignIn.RequireConfirmedAccount;
  }
}
