using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Auth.Commands.RefreshToken;
using PGK.Application.App.User.Auth.Commands.RevokeRefreshToken;
using PGK.Application.App.User.Auth.Commands.SignIn;

namespace PGK.WebApi.Controllers
{
    public class AuthController : Controller
    {

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="command">SignInCommand object</param>
        /// <returns>SignInVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        [HttpPost("SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignInVm))]
        public async Task<ActionResult> SignIn(SignInCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Отозвать токен
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        [HttpPost("Revoke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RevokeRefreshToken(
            [FromHeader] string refreshToken)
        {
            var command = new RevokeRefreshTokenCommand
            {
                RefreshToken = refreshToken
            };

            await Mediator.Send(command);

            return Ok("Refresh token is revoked");
        }

        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>RefreshTokenVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        [HttpPost("Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshTokenVm))]
        public async Task<ActionResult> RefreshToken(
            [FromHeader] string refreshToken
            )
        {
            var command = new RefreshTokenCommand
            {
                RefreshToken = refreshToken
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
