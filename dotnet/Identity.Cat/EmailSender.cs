using Microsoft.AspNetCore.Identity.UI.Services;

namespace Identity.Cat;

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