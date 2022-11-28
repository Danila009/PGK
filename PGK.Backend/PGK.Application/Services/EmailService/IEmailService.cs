namespace PGK.Application.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email,string subject,string message);
    }
}
