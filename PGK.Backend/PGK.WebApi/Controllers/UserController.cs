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
using PGK.Application.App.User.Commands.UpdateUser;
using PGK.Application.App.User.Queries.GetUserPhoto;
using PGK.Application.App.User.Queries.GetUserSettings;
using PGK.Domain.User;
using PGK.WebApi.Models.User;

namespace PGK.WebApi.Controllers
{
    public class UserController : Controller
    {
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

        [Authorize]
        [HttpGet("Settings")]
        public async Task<ActionResult<UserSettingsDto>> GetSettings()
        {
            var query = new GetUserSettingsQuery
            {
                UserId = UserId
            };

            var dto = await Mediator.Send(query);

            return dto;
        }

        [Authorize]
        [HttpPatch("Settings/DrarkMode")]
        public async Task<ActionResult<UpdateDrarkModeVm>> SettingsUpdateDrarkMode()
        {
            var command = new UpdateDrarkModeCommand
            {
                UserId = UserId
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize]
        [HttpPatch("Settings/SecondaryBackground")]
        public async Task<ActionResult<UpdateSecondaryBackgroundVm>> SettingsUpdateSecondaryBackground(
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

        [Authorize]
        [HttpGet("Photo/{userId}.jpg")]
        public async Task<ActionResult> GetPhoto(int userId)
        {
            var query = new GetUserPhotoQuery { UserId = userId };

            var image = await Mediator.Send(query);

            return File(image, "image/jpeg");
        }

        [Authorize]
        [HttpPost("Photo")]
        public async Task<ActionResult<UserPhotoVm>> AddPhoto(IFormFile photo)
        {
            var command = new UserAddPhotoCommand
            {
                UserId = UserId,
                Photo = photo
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

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
