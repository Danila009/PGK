using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace PGK.Application.Services.FCMService
{
    internal class FCMService : IFCMService
    {
        private readonly HttpClient httpClient = new();

        private const string URL = "https://fcm.googleapis.com/fcm/send";
        
        private readonly IConfiguration _configuration;

        public FCMService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public async Task SendMessage(string title, string message, string topic)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, URL);

            request.Content = JsonContent.Create(new
            {
                data = new
                {
                    title,
                    message
                },
                to = $"topics/{topic}"
            });

            request.Headers.TryAddWithoutValidation("Authorization", $"key={_configuration["fcm_service:server_key"]}");

            await httpClient.SendAsync(request);
        }
    }
}
