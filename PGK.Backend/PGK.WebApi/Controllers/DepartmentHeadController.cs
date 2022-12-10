using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.DepartmentHead.Commands.DeleteDepartmentHead;
using PGK.Application.App.User.DepartmentHead.Commands.Registration;
using PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadDetails;
using PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadList;

namespace PGK.WebApi.Controllers
{
    public class DepartmentHeadController : Controller
    {
        /// <summary>
        /// Получить список заведующих отделением
        /// </summary>
        /// <param name="query">GetDepartmentHeadListQuery object</param>
        /// <returns>DepartmentHeadListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentHeadListVm))]
        public async Task<ActionResult> GetAll(
            [FromQuery] GetDepartmentHeadListQuery query
            )
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить заведующего отделениям по идентификатору
        /// </summary>
        /// <param name="id">>Идентификатор пользователя из учебной части</param>
        /// <returns>DepartmentHeadDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentHeadDto))]
        public async Task<ActionResult> GetById(int id)
        {
            var query = new GetDepartmentHeadDetailsQuery
            {
                Id = id
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить заведующего отделениям
        /// </summary>
        /// <param name="id">>Идентификатор пользователя из учебной части</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDepartmentHeadCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }


        /// <summary>
        /// Зарегестрировать заведующего отделением
        /// </summary>
        /// <param name="command">RegistrationDepartmentHeadCommand object</param>
        /// <returns>RegistrationDepartmentHeadVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>

        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPost("Registration")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegistrationDepartmentHeadVm))]
        public async Task<ActionResult> Registration(
            RegistrationDepartmentHeadCommand command
            )
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

    }
}
