using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Commands.AddPhoto;
using PGK.Application.App.User.Commands.EmailPaswordReset;
using PGK.Application.App.User.Commands.EmailVerification;
using PGK.Application.App.User.Commands.SendEmailVerification;
using PGK.Application.App.User.Queries.GetUserPhoto;
using PGK.Application.Services;

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

        [HttpGet("Email/Verification/{userId}_{token}")]
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

        [Authorize]
        [HttpPost("Email/Pasword/Reset")]
        public async Task<ActionResult> SendEmailPaswordReset()
        {
            var command = new SendEmailVerificationCommand
            {
                UserId = UserId
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet("Email/Pasword/Reset/{userId}_{token}")]
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
    }
}
