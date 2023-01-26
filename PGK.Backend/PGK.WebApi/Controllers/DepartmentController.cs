using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Department.Commands.CreateDepartment;
using PGK.Application.App.Department.Commands.DeleteDepartment;
using PGK.Application.App.Department.Commands.UpdateDepartment;
using PGK.Application.App.Department.Commands.UpdateDepartmentHead;
using PGK.Application.App.Department.Queries.GetDepartmentDetails;
using PGK.Application.App.Department.Queries.GetDepartmentList;
using PGK.WebApi.Models.Department;

namespace PGK.WebApi.Controllers
{
    public class DepartmentController : Controller
    {
        /// <summary>
        /// Получите список отделов
        /// </summary>
        /// <param name="search">Ключивые слова для поиска</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество результатов для возврата на страницу</param>
        /// <returns>DepartmentListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentListVm))]
        public async Task<ActionResult> GetAll(
            string? search, int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetDepartmentListQuery
            {
                Search = search,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получите отдел по идентификатору
        /// </summary>
        /// <param name="id">Индификатор отдела</param>
        /// <returns>DepartmentDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentDto))]
        public async Task<ActionResult> GetById(int id)
        {
            var query = new GetDepartmentDetailsQuery
            {
                Id = id
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        /// <summary>
        /// Добавить отдел
        /// </summary>
        /// <param name="command">CreateDepartmentCommand object</param>
        /// <returns>DepartmentDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentDto))]
        public async Task<ActionResult> Create(
            [FromBody] CreateDepartmentCommand command)
        {
            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Изменить отдел
        /// </summary>
        /// <param name="id">Индификатор отдела</param>
        /// <returns>DepartmentDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentDto))]
        public async Task<ActionResult> Update(
            int id,
            [FromBody] UpdateDepartmentModel model)
        {
            var command = new UpdateDepartmentCommand
            {
                DepartmentId = id,
                DepartmentHeadId = model.DepartmentHeadId,
                Name = model.Name
            };


            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить отдел
        /// </summary>
        /// <param name="id">Индификатор отдела</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDepartmentCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPatch("{departmentId}/DepartmentHead/{departmentHeadId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdetaDepartmentHead(int departmentId, int departmentHeadId)
        {
            var command = new UpdateDepartmentHeadCommand
            {
                DepartmentId = departmentId,
                DepartmentHeadId = departmentHeadId
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
