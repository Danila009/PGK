using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Auth.Commands.RefreshToken;
using PGK.Application.App.User.Auth.Commands.RevokeRefreshToken;
using PGK.Application.App.User.Auth.Commands.SignIn;

namespace PGK.WebApi.Controllers
{
    public class AuthController : Controller
    {

        [HttpPost("SignIn")]
        public async Task<ActionResult<SignInVm>> SignIn(SignInCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpPost("Revoke")]
        public async Task<ActionResult> RevokeRefreshToken(
            [FromBody] RevokeRefreshTokenCommand command)
        {
            await Mediator.Send(command);

            return Ok("Refresh token is revoked");
        }

        [HttpPost("Refresh")]
        public async Task<ActionResult<RefreshTokenVm>> RefreshToken(
            [FromBody] RefreshTokenCommand command
            )
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
