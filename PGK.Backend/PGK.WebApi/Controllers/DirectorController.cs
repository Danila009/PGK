using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Director.Commands.Registration;
using PGK.Application.App.User.Director.Commands.UpdateCurrent;
using PGK.Application.App.User.Director.Queries.GetDirectorDetails;
using PGK.Application.App.User.Director.Queries.GetDirectorList;

namespace PGK.WebApi.Controllers
{
    public class DirectorController : Controller
    {
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DirectorListVm))]
        public async Task<ActionResult> GetAll(
            [FromQuery] GetDirectorListQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DirectorDto))]
        public async Task<ActionResult> GetById(int id)
        {
            var query = new GetDirectorDetailsQuery
            {
                DirectorId = id
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,DIRECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,DIRECTOR,ADMIN")]
        [HttpPost("Registration")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegistrationDirectorVm))]
        public async Task<ActionResult> Registration(
            RegistrationDirectorCommand command
            )
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,DIRECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,DIRECTOR,ADMIN")]
        [HttpPatch("{id}/Current")]
        public async Task<ActionResult> DirectorUpdateCurrent(int id)
        {
            var command = new DirectorUpdateCurrentCommand
            {
                DirectorId = id
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
