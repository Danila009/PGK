using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace PGK.Application.Services.EmailService
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration) =>
            _configuration = configuration;

        public async Task SendEmailAsync(
            string email,
            string subject,
            string message
            )
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(MailboxAddress.Parse(_configuration["email_service:from_email"]));
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _configuration["email_service:smtp_server_name"],
                int.Parse(_configuration["email_service:smtp_server_port"]),
                bool.Parse(_configuration["email_service:useSsl"]));

            await client.AuthenticateAsync(
                _configuration["email_service:from_email"],
                _configuration["email_service:from_email_password"]);
            
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
