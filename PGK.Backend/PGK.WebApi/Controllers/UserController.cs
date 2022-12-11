using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Commands.AddPhoto;
using PGK.Application.App.User.Commands.EmailPaswordReset;
using PGK.Application.App.User.Commands.EmailVerification;
using PGK.Application.App.User.Commands.SendEmailPaswordReset;
using PGK.Application.App.User.Commands.SendEmailVerification;
using PGK.Application.App.User.Commands.UpdateDrarkMode;
using PGK.Application.App.User.Commands.UpdateEmail;
using PGK.Application.App.User.Commands.UpdateSecondaryBackground;
using PGK.Application.App.User.Commands.UpdateTelegramId;
using PGK.Application.App.User.Commands.UpdateUser;
using PGK.Application.App.User.Queries.GetUserNotification;
using PGK.Application.App.User.Queries.GetUserPhoto;
using PGK.Application.App.User.Queries.GetUserSettings;
using PGK.Domain.User;
using PGK.WebApi.Models.User;

namespace PGK.WebApi.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Получить уведомления пользователя
        /// </summary>
        /// <param name="model">GetUserNotificationModel object</param>
        /// <returns>NotificationListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Notification")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationListVm))]
        public async Task<ActionResult> GetAllNotification(
            [FromQuery] GetUserNotificationModel model)
        {
            var query = new GetUserNotificationQuery
            {
                Search = model.Search,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                UserId = UserId
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Обновить или добавить TelegramId
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("TelegramId")]
        public async Task<ActionResult> UpdateTelegramId(int telegramId)
        {
            var command = new UpdateTelegramIdCommand
            {
                TelegramId = telegramId,
                UserId = UserId
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Изменить данные пользователя
        /// </summary>
        /// <param name="model">UpdateUserModel object</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateUserModel model)
        {
            var command = new UpdateUserCommand
            {
                Id = UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName
            };

            await Mediator.Send(command);

            return Ok();
        }


        /// <summary>
        /// Получить настройки пользователя
        /// </summary>
        /// <returns>UserSettingsDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Settings")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSettingsDto))]
        public async Task<ActionResult> GetSettings()
        {
            var query = new GetUserSettingsQuery
            {
                UserId = UserId
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        /// <summary>
        /// Изменить "Drark Mode"
        /// </summary>
        /// <returns>UpdateDrarkModeVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("Settings/DrarkMode")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateDrarkModeVm))]
        public async Task<ActionResult> SettingsUpdateDrarkMode()
        {
            var command = new UpdateDrarkModeCommand
            {
                UserId = UserId
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Изменить "Secondary Background"
        /// </summary>
        /// <returns>UpdateSecondaryBackgroundVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("Settings/SecondaryBackground")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateSecondaryBackgroundVm))]
        public async Task<ActionResult> SettingsUpdateSecondaryBackground(
            SecondaryBackground secondaryBackground)
        {
            var command = new UpdateSecondaryBackgroundCommand
            {
                UserId = UserId,
                SecondaryBackground = secondaryBackground
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Получить фото профеля пользователя
        /// </summary>
        /// <param name="userId">Индификатор пользователя</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Photo/{userId}.jpg")]
        public async Task<ActionResult> GetPhoto(int userId)
        {
            var query = new GetUserPhotoQuery { UserId = userId };

            var image = await Mediator.Send(query);

            return File(image, "image/jpeg");
        }

        /// <summary>
        /// Добавить или изменить фото пользователя
        /// </summary>
        /// <param name="photo">Фото пользователя</param>
        /// <returns>UserPhotoVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPost("Photo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserPhotoVm))]
        public async Task<ActionResult> AddPhoto(IFormFile photo)
        {
            var command = new UserAddPhotoCommand
            {
                UserId = UserId,
                Photo = photo
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Отправить письмо на почту для подверждения почты
        /// </summary>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPost("Email/Verification")]
        public async Task<ActionResult> SendEmailVerification()
        {
            var command = new SendEmailVerificationCommand
            {
                UserId = UserId
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Подвердить почту пользователя
        /// </summary>
        /// <returns>HTML страница</returns>
        /// <param name="userId">Индификатор пользователя</param>
        /// <param name="token">Токен электронный почты</param>
        /// <response code="200">Запрос выполнен успешно</response>
        [HttpGet("{userId}/Email/Verification/{token}.html")]
        public async Task<ActionResult> EmailVerification(int userId, string token)
        {
            var command = new EmailVerificationCommand
            {
                UserId = userId,
                Token = token
            };

            var contentResult = await Mediator.Send(command);

            return Ok(contentResult);
        }

        /// <summary>
        /// Отправить письмо на почту для сброса пароля
        /// </summary>
        /// <param name="email">Электроная почта пользователя</param>
        /// <response code="200">Запрос выполнен успешно</response>
        [HttpPost("Email/Pasword/Reset")]
        public async Task<ActionResult> SendEmailPaswordReset(string email)
        {
            var command = new SendEmailPaswordResetCommand
            {
                Email = email
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Сбросить пароль
        /// </summary>
        /// <returns>HTML страница</returns>
        /// <param name="userId">Индификатор пользователя</param>
        /// <param name="token">Токен электронный почты</param>
        /// <response code="200">Запрос выполнен успешно</response>
        [HttpGet("{userId}/Email/Pasword/Reset/{token}.html")]
        public async Task<ActionResult> PassowrdReset(int userId, string token)
        {
            var command = new EmailPaswordResetCommand
            {
                UserId = userId,
                Token = token
            };

            var contentResult = await Mediator.Send(command);

            return Ok(contentResult);
        }

        /// <summary>
        /// Добавить или сменить электронную почту пользователя
        /// </summary>
        /// <param name="newEmail">Электроная почта пользователя</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("Email")]
        public async Task<ActionResult> UpdateEmail(string newEmail)
        {
            var coomand = new UserUpdateEmailCommand
            {
                UserId = UserId,
                Email = newEmail
            };

            await Mediator.Send(coomand);

            return Ok("Новая почта сохранен, мы отправили письмо для подтверждения почты");
        }
    }
}
