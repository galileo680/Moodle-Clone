using Microsoft.Extensions.Configuration;
using MoodleClone.Domain.Interfaces;
using System.Net;
using System.Net.Mail;

namespace MoodleClone.Application.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        Console.WriteLine("sendemailasync");
        var mail = "*";
        var password = "*";

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, password)
        };

        await client.SendMailAsync(
            new MailMessage(from: mail,
                to: email,
                subject,
                message
                ));
    }
}