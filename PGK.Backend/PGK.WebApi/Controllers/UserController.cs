using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Commands.AddPhoto;
using PGK.Application.App.User.Commands.EmailPaswordReset;
using PGK.Application.App.User.Commands.EmailVerification;
using PGK.Application.App.User.Commands.SendEmailPaswordReset;
using PGK.Application.App.User.Commands.SendEmailVerification;
using PGK.Application.App.User.Commands.UpdateEmail;
using PGK.Application.App.User.Queries.GetUserPhoto;

namespace PGK.WebApi.Controllers
{
    public class UserController : Controller
    {

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

        [HttpGet("{userId}/Email/Verification/{token}")]
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

        [HttpGet("{userId}/Email/Pasword/Reset/{token}")]
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
