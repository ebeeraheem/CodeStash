using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Net.Http.Headers;
using CodeStash.Application.Interfaces;

namespace CodeStash.Infrastructure.EmailModule;
internal class EmailService(
    IOptions<EmailSettings> emailSettings,
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration) : IEmailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    public async Task SendEmailAsync(string toName, string toEmail, string subject, string body)
    {
        var emailMessage = new MailMessage();
        var mimeMessage = new MimeMessage();
        var message = new
        {
            From = new
            {
                address = _emailSettings.FromEmail,
                name = _emailSettings.FromName
            },
            Subject = subject,
            HtmlBody = body,
            To = new List<object>
            {
                new
                {
                    email_address = new
                    {
                        address = toEmail,
                        name = toName
                    }
                }
            }
        };

        var baseUrl = configuration["ZeptoMail:BaseUrl"];
        var token = configuration["ZeptoMail:Token"];
        ArgumentNullException.ThrowIfNull(baseUrl);
        ArgumentNullException.ThrowIfNull(token);

        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(baseUrl),
            Content = new StringContent(JsonSerializer.Serialize(message)),
            Headers =
            {
                { "Accept", "application/json" }
            }
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Zoho-enczapikey", token);

        using var httpClient = httpClientFactory.CreateClient();
        var response = await httpClient.SendAsync(request);

        var responseBody = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
    }
}
