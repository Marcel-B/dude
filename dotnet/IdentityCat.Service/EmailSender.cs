using Microsoft.AspNetCore.Identity.UI.Services;

namespace com.b_velop.IdentityCat.Service;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(
        string email,
        string subject,
        string htmlMessage)
    {
        Console.WriteLine("Email sent");
        return Task.CompletedTask;
    }
}