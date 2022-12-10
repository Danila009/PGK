using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Subject.Commands.CreateSubject;
using PGK.Application.App.Subject.Commands.DeleteSubject;
using PGK.Application.App.Subject.Commands.UpdateSubject;
using PGK.Application.App.Subject.Queries.GetSubjectDetails;
using PGK.Application.App.Subject.Queries.GetSubjectList;
using PGK.WebApi.Models.Subject;

namespace PGK.WebApi.Controllers
{
    public class SubjectController : Controller
    {
        /// <summary>
        /// Получить список предметов
        /// </summary>
        /// <param name="query">GetSubjectListQuery object</param>
        /// <returns>SubjectListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubjectListVm))]
        public async Task<ActionResult> GetAll(
            [FromQuery] GetSubjectListQuery query)
        {

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получите предмет по идентификатору
        /// </summary>
        /// <param name="id">Индификатор предмета</param>
        /// <returns>SubjectDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubjectDto))]
        public async Task<ActionResult> GetById(int id)
        {
            var query = new GetSubjectDetailsQuery { Id = id };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        /// <summary>
        /// Добавить предмет
        /// </summary>
        /// <param name="command">CreateSubjectCommand object</param>
        /// <returns>CreateSubjectVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateSubjectVm))]
        public async Task<ActionResult> Create(CreateSubjectCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Изменить предмет
        /// </summary>
        /// <param name="id">Индификаатор предмета</param>
        /// <param name="model">UpdateSubjectModel object</param>
        /// <returns>SubjectDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubjectDto))]
        public async Task<ActionResult<SubjectDto>> Update(
            int id, UpdateSubjectModel model)
        {
            var command = new UpdateSubjectCommand
            {
                Id = id,
                SubjectTitle = model.SubjectTitle
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить предмет
        /// </summary>
        /// <param name="id">Индификатор предмета</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSubjectCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
