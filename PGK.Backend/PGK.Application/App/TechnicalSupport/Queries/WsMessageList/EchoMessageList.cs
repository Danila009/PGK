using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageList;
using System.Net.WebSockets;
using System.Text;

namespace PGK.Application.App.TechnicalSupport.Queries.WsMessageList
{
    public class EchoMessageList
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public EchoMessageList(ILogger logger, IMediator mediator) =>
            (_logger, _mediator) = (logger, mediator);

        public async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            _logger.Log(LogLevel.Information, "Message received from Client");

            while (!result.CloseStatus.HasValue)
            {
                var jsonData = Encoding.UTF8.GetString(buffer).ToString();
                var query = JsonConvert.DeserializeObject<GetMessageListQuery>(jsonData);

                if (query != null)
                {
                    var vm = await _mediator.Send(query);

                    var serverMsg = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(vm));
                    await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
                    _logger.Log(LogLevel.Information, "Message sent to Client");
                }

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                _logger.Log(LogLevel.Information, "Message received from Client");

            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            _logger.Log(LogLevel.Information, "WebSocket connection closed");
        }
    }
}
