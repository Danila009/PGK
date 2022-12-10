using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Speciality.Commands.CreateSpeciality;
using PGK.Application.App.Speciality.Commands.DeleteSpeciality;
using PGK.Application.App.Speciality.Commands.UpdateSpeciality;
using PGK.Application.App.Speciality.Queries.GetSpecialityDetails;
using PGK.Application.App.Speciality.Queries.GetSpecialityList;
using PGK.WebApi.Models.Speciality;

namespace PGK.WebApi.Controllers
{
    public class SpecialityController : Controller
    {
        /// <summary>
        /// Получить список спицальностей
        /// </summary>
        /// <param name="search">Ключивые слова для поиска</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество результатов для возврата на страницу</param>
        /// <returns>SpecialityListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpecialityListVm))]
        public async Task<ActionResult> GetAll(
            string? search, int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetSpecialityListQuery
            {
                Search = search,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получите спецальность по идентификатору
        /// </summary>
        /// <param name="id">Индификатор спецальности</param>
        /// <returns>SpecialityDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpecialityDto))]
        public async Task<ActionResult> GetById(int id)
        {
            var query = new GetSpecialityDetailsQuery
            {
                Id = id
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        /// <summary>
        /// Добавить спецальность
        /// </summary>
        /// <param name="command">CreateSpecialityCommand object</param>
        /// <returns>SpecialityDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpecialityDto))]
        public async Task<ActionResult> Create(CreateSpecialityCommand command)
        {
            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Изменить спецальность
        /// </summary>
        /// <param name="id">Индификатор спецальности</param>
        /// <param name="model">UpdateSpecialityModel object</param>
        /// <returns>SpecialityDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpecialityDto))]
        public async Task<ActionResult> Update(int id, UpdateSpecialityModel model)
        {
            var command = new UpdateSpecialityCommand
            {
                Id = id,
                Number = model.Number,
                Name = model.Name,
                NameAbbreviation = model.NameAbbreviation,
                Qualification = model.Qualification,
                DepartmentId = model.DepartmentId
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить спецальность
        /// </summary>
        /// <param name="id">Индификатор спецальности</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,DEPARTMENT_HEAD,ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSpecialityCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
