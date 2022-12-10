using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.EducationalSector.Commands.DeleteEducationalSector;
using PGK.Application.App.User.EducationalSector.Commands.Registration;
using PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorDetails;
using PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorList;

namespace PGK.WebApi.Controllers
{
    public class EducationalSectorController : Controller
    {
        /// <summary>
        /// Получить список пользователей из учебной части
        /// </summary>
        /// <param name="query">GetEducationalSectorListQuery object</param>
        /// <returns>EducationalSectorListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EducationalSectorListVm))]
        public async Task<ActionResult> GetAll(
            [FromQuery] GetEducationalSectorListQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить пользователя из учебной части по идентификатору
        /// </summary>
        /// <param name="id">>Идентификатор пользователя из учебной части</param>
        /// <returns>DepartmentHeadDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EducationalSectorDto))]
        public async Task<ActionResult> GetById(int id)
        {
            var query = new GetEducationalSectorDetailsQuery
            {
                Id = id
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить пользователя из учебной части
        /// </summary>
        /// <param name="id">>Идентификатор пользователя из учебной части</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteById(int id)
        {
            var command = new DeleteEducationalSectorCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Зарегестрировать пользователя из учебной части
        /// </summary>
        /// <param name="command">RegistrationEducationalSectorCommand object</param>
        /// <returns>RegistrationEducationalSectorVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>

        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPost("Registration")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegistrationEducationalSectorVm))]
        public async Task<ActionResult> Registration(
            RegistrationEducationalSectorCommand command
            )
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
