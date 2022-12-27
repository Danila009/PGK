using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Commands.AddPhoto;
using PGK.Application.App.User.Commands.EmailPaswordReset;
using PGK.Application.App.User.Commands.EmailVerification;
using PGK.Application.App.User.Commands.SendEmailPaswordReset;
using PGK.Application.App.User.Commands.SendEmailVerification;
using PGK.Application.App.User.Commands.UpdateDrarkMode;
using PGK.Application.App.User.Commands.UpdateEmail;
using PGK.Application.App.User.Commands.UpdateThemeStyle;
using PGK.Application.App.User.Commands.UpdateTelegramId;
using PGK.Application.App.User.Commands.UpdateUser;
using PGK.Application.App.User.Queries.GetUserNotification;
using PGK.Application.App.User.Queries.GetUserPhoto;
using PGK.Application.App.User.Queries.GetUserSettings;
using PGK.WebApi.Models.User;
using PGK.Domain.User.Enums;
using PGK.Application.App.User.Commands.UpdateThemeFontStyle;
using PGK.Application.App.User.Commands.UpdateThemeFontSize;
using PGK.Application.App.User.Commands.UpdateThemeCorners;
using PGK.Application.App.User.Commands.UpdateLanguage;
using PGK.Application.App.User.Queries.GetUserById;
using PGK.Application.App.User.Queries.GetUserLits;
using PGK.Application.App.User.Commands.UpdateNotificationsSettings;
using PGK.Application.App.User.Commands.UpdatePassword;
using PGK.Application.App.User.Queries.GetTelegramToken;

namespace PGK.WebApi.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public async Task<ActionResult> GetUser()
        {
            var query = new GetUserByIdQuery
            {
                UserId = UserId
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        [Authorize]
        [HttpPatch("Password")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult> UpdatePassword()
        {
            var command = new UpdatePasswordCommand
            {
                UserId = UserId
            };

            var newPassword = await Mediator.Send(command);

            return Ok(newPassword);
        }

        /// <summary>
        /// Получить уведомления пользователя
        /// </summary>
        /// <param name="model">GetUserNotificationModel object</param>
        /// <returns>NotificationListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Notifications")]
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

        [Authorize]
        [HttpGet("Telegram/Token")]
        public async Task<ActionResult> GetTelegramToken()
        {
            var query = new GetTelegramTokenQuery
            {
                UserId = UserId
            };

            var token = await Mediator.Send(query);

            return Ok(token);
        }

        /// <summary>
        /// Обновить или добавить TelegramId
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="telegramToken"></param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        [HttpPatch("Telegram/{telegramId}")]
        public async Task<ActionResult> UpdateTelegramId(int telegramId, [FromHeader] string telegramToken)
        {
            var command = new UpdateTelegramIdCommand
            {
                TelegramId = telegramId,
                TelegramToken = telegramToken
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

        [Authorize]
        [HttpPatch("Settings/Notifications")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSettingsDto))]
        public async Task<ActionResult> UpdateSettingsNotifications(
            UpdateNotificationsSettingsModel model)
        {
            var command = new UpdateNotificationsSettingsCommand
            {
                UserId = UserId,
                IncludedNotifications = model.IncludedNotifications,
                SoundNotifications = model.SoundNotifications,
                VibrationNotifications = model.VibrationNotifications,
                IncludedSchedulesNotifications = model.IncludedSchedulesNotifications,
                IncludedJournalNotifications = model.IncludedJournalNotifications,
                IncludedRaportichkaNotifications = model.IncludedRaportichkaNotifications,
                IncludedTechnicalSupportNotifications = model.IncludedTechnicalSupportNotifications
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }


        /// <summary>
        /// Изменить Drark Mode
        /// </summary>
        /// <returns>UserSettingsDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("Settings/DrarkMode")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSettingsDto))]
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
        /// Изменить Theme Style
        /// </summary>
        /// <returns>UpdateThemeStyleVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("Settings/ThemeStyle")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateThemeStyleVm))]
        public async Task<ActionResult> SettingsUpdateThemeStyle(ThemeStyle themeStyle)
        {
            var command = new UpdateThemeStyleCommand
            {
                UserId = UserId,
                ThemeStyle = themeStyle
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Изменить Theme Font Style
        /// </summary>
        /// <returns>UserSettingsDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("Settings/ThemeFontStyle")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSettingsDto))]
        public async Task<ActionResult> SettingsUpdateThemeFontStyle(ThemeFontStyle themeFontStyle)
        {
            var command = new UpdateThemeFontStyleCommand
            {
                UserId = UserId,
                ThemeFontStyle = themeFontStyle
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Изменить Theme Font Size
        /// </summary>
        /// <returns>UserSettingsDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("Settings/ThemeFontSize")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSettingsDto))]
        public async Task<ActionResult> SettingsUpdateThemeFontSize(ThemeFontSize themeFontSize)
        {
            var command = new UpdateThemeFontSizeCommand
            {
                UserId = UserId,
                ThemeFontSize = themeFontSize
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Изменить Theme Corners
        /// </summary>
        /// <returns>UserSettingsDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("Settings/ThemeCorners")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSettingsDto))]
        public async Task<ActionResult> SettingsUpdateThemeCorners(ThemeCorners themeCorners)
        {
            var command = new UpdateThemeCornersCommand
            {
                UserId = UserId,
                ThemeCorners = themeCorners
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Изменить язык
        /// </summary>
        /// <returns>UserSettingsDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpPatch("Settings/Language")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSettingsDto))]
        public async Task<ActionResult> SettingsUpdateLanguage(int languageId)
        {
            var command = new UpdateLanguageCommand
            {
                UserId = UserId,
                LanguageId = languageId
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
