namespace PGK.Application.Services.FCMService
{
    public interface IFCMService
    {
        Task SendMessage(string title, string message, string topic);
    }
}
