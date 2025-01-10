using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using CodeStash.Application.Services;

namespace CodeStash.Infrastructure.EmailModule;
internal class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    public async Task SendEmailAsync(string toName, string toEmail, string subject, string body)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
        email.To.Add(new MailboxAddress(toName, toEmail));
        email.Subject = subject;
        
        var builder = new BodyBuilder { HtmlBody = body };
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();

        try
        {
            await smtp.ConnectAsync(_emailSettings.Host,
                                    _emailSettings.Port,
                                    useSsl: true);

            await smtp.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
            await smtp.SendAsync(email);
        }
        finally
        {
            await smtp.DisconnectAsync(quit: true);
        }
    }
}
