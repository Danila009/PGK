using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.TechnicalSupport.Commands.CreateMessageContent;
using PGK.Application.App.TechnicalSupport.Commands.DeleteMessage;
using PGK.Application.App.TechnicalSupport.Commands.DeleteMessageContent;
using PGK.Application.App.TechnicalSupport.Commands.SendMessage;
using PGK.Application.App.TechnicalSupport.Commands.UpdateMessage;
using PGK.Application.App.TechnicalSupport.Commands.UpdateMessageUserVisible;
using PGK.Application.App.TechnicalSupport.Commands.UpdatePin;
using PGK.Application.App.TechnicalSupport.Queries.GetChatDetails;
using PGK.Application.App.TechnicalSupport.Queries.GetChatList;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageContentFile;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageContentList;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageList;
using PGK.Application.App.TechnicalSupport.Queries.WsMessageList;
using PGK.Domain.TechnicalSupport.Enums;
using PGK.WebApi.Models.TechnicalSupport;

namespace PGK.WebApi.Controllers
{
    public class TechnicalSupportController : Controller
    {
        private readonly ILogger<TechnicalSupportController> _logger;

        public TechnicalSupportController(ILogger<TechnicalSupportController> logger)
            => _logger = logger;

        /// <summary>
        /// Получить список сообщения (WebSocket)
        /// </summary>
        /// <remarks>
        ///     <para>
        ///          Url : wss://localhost:7002/pgk63/ws/Chat/Message     
        ///     </para>
        ///     <para>
        ///         Send message: json string GetMessageListQuery object
        ///     </para>
        ///     <para>
        ///         Returns: json string MessageListVm object
        ///     </para>
        /// </remarks>
        /// <returns>MessageListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="400">Не является запросом WebSocket</response>
        [Authorize]
        [HttpGet("/ws/Chat/Message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageListVm))]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _logger.Log(LogLevel.Information, "WebSocket connection established");

                var echo = new EchoMessageList(_logger, Mediator);

                await echo.Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        /// <summary>
        /// Получить список чатов
        /// </summary>
        /// <param name="query">GetChatListQuery object</param>
        /// <returns>ChatListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль ADMIN</response>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("Chat")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChatListVm))]
        public async Task<ActionResult> GetChat(
            [FromQuery] GetChatListQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить чат по индификатору
        /// </summary>
        /// <param name="id">Идентификатор чата</param>
        /// <returns>ChatDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Chat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChatDto))]
        public async Task<ActionResult> GetChatById(int id)
        {
            var query = new GetChatDetailsQuery
            {
                Id = id
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        /// <summary>
        /// Получить список сообщений
        /// </summary>
        /// <param name="query">GetMessageListQuery object</param>
        /// <returns>MessageListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Chat/Message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageListVm))]
        public async Task<ActionResult> GetMessageAll(
            [FromQuery] GetMessageListQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить лист контета из сообщений
        /// </summary>
        /// <param name="query">GetMessageContentListQuery object</param>
        /// <returns>MessageContentListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Chat/Message/Content")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageContentListVm))]
        public async Task<ActionResult> GetMessageContentAll(
            [FromQuery] GetMessageContentListQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить файл контент сообщение
        /// </summary>
        /// <param name="fileId">Идентификатор файла</param>
        /// <param name="type">Тип файла</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Chat/Message/Content/{fileId}")]
        public async Task<ActionResult> GetMessageContentFile(string fileId, MessageContentType type)
        {
            var query = new GetMessageContentFileQuery
            {
                FileId = fileId,
                Type = type
            };

            var file = await Mediator.Send(query);

            return File(file, type == MessageContentType.IMAGE ? "image/jpeg" 
                : type == MessageContentType.VIDEO ? "video/mp4" : "multipart/form-data");
        }

        /// <summary>
        /// Добавить к сообщению контент
        /// </summary>
        /// <param name="id">Идентификатор сообщения</param>
        /// <param name="type">Тип файла</param>
        /// <param name="file">Файл</param>
        /// <returns>MessageContentDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPost("Chat/Message/{id}/Content")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageContentDto))]
        public async Task<ActionResult> CreateMessageContentFile(int id,
            MessageContentType type, IFormFile file)
        {
            var command = new CreateMessageContentCommand
            {
                MessageId = id,
                Type = type,
                File = file,
                UserId = UserId
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить контент в сообщение
        /// </summary>
        /// <param name="id">Идентификатор контента</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpDelete("Chat/Message/Content/{id}")]
        public async Task<ActionResult> DeleteMessageContent(int id)
        {
            var command = new DeleteMessageContentCommand
            {
                MessageContentId = id,
                Role = UserRole.Value,
                UserId = UserId
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <remarks>
        /// Параметр ChatId нужно указывать только администратору
        /// </remarks>
        /// <param name="model">SendMessageModel object</param>
        /// <returns>MessageDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPost("Chat/Message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageDto))]
        public async Task<ActionResult> SendMessage(SendMessageModel model)
        {
            var command = new SendMessageCommand
            {
                Text = model.Text,
                ChatId = model.ChatId,
                UserId = UserId,
                Role = UserRole.Value
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Обновить сообщение
        /// </summary>
        /// <param name="id">Идентификатор сообщения</param>
        /// <param name="model">UpdateMessageModel object</param>
        /// <returns>MessageDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPut("Chat/Message/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageDto))]
        public async Task<ActionResult> UpdateMessage(int id, UpdateMessageModel model)
        {
            var command = new UpdateMessageCommand
            {
                MessageId = id,
                Text = model.Text,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Пользователь прочитал сообщение
        /// </summary>
        /// <param name="id">Идентификатор сообщения</param>
        /// <returns>MessageDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("Chat/Message/{id}/UserVisible")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageDto))]
        public async Task<ActionResult> UpdateMessageUserVisible(int id)
        {
            var command = new UpdateMessageUserVisibleCommand
            {
                MessageId = id,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Закрепить сообщение
        /// </summary>
        /// <param name="id">Идентификатор сообщения</param>
        /// <returns>MessageDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("Chat/Message/{id}/Pin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageDto))]
        public async Task<ActionResult> UpdateMessagePin(int id)
        {
            var command = new UpdateMessagePinCommand
            {
                MessageId = id,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить сообщение
        /// </summary>
        /// <param name="id">Идентификатор сообщения</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpDelete("Chat/Message/{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var command = new DeleteMessageCommand
            {
                MessageId = id,
                UserId = UserId,
                Role = UserRole.Value
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
