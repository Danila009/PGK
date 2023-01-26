using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Journal.Commands.CreateJournal;
using PGK.Application.App.Journal.Commands.CreateJournalSubject;
using PGK.Application.App.Journal.Commands.CreateJournalSubjectColumn;
using PGK.Application.App.Journal.Commands.CreateJournalSubjectRow;
using PGK.Application.App.Journal.Commands.CreateJournalTopic;
using PGK.Application.App.Journal.Commands.DeleteJournal;
using PGK.Application.App.Journal.Commands.DeleteJournalSubject;
using PGK.Application.App.Journal.Commands.DeleteJournalSubjectColumn;
using PGK.Application.App.Journal.Commands.DeleteJournalSubjectRow;
using PGK.Application.App.Journal.Commands.DeleteJournalTopic;
using PGK.Application.App.Journal.Commands.UpdateJournalEvaluation;
using PGK.Application.App.Journal.Queries.GetJournalList;
using PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList;
using PGK.Application.App.Journal.Queries.GetJournalSubjectList;
using PGK.Application.App.Journal.Queries.GetJournalSubjectRowList;
using PGK.Application.App.Journal.Queries.GetJournalTopicList;
using PGK.Domain.Journal;
using PGK.WebApi.Models.Journal;

namespace PGK.WebApi.Controllers
{
    public class JournalController : Controller
    {
        /// <summary>
        /// Получить список журналов
        /// </summary>
        /// <param name="query">GetJournalListQuery object</param>
        /// <returns>JournalListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JournalListVm))]
        public async Task<ActionResult> GetAll(
            [FromQuery] GetJournalListQuery query
            )
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Создать журнал
        /// </summary>
        /// <param name="model">CreateJournalModel object</param>
        /// <returns>JournalDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JournalDto))]
        public async Task<ActionResult> Create(CreateJournalModel model)
        {
            var command = new CreateJournalCommand
            {
                Course = model.Course,
                Semester = model.Semester,
                GroupId = model.GroupId,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить журнал
        /// </summary>
        /// <param name="id">Индификатор журнала</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteJournalCommand
            {
                Id = id,
                UserId = UserId,
                Role = UserRole.Value
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Получить список предметов из журнала
        /// </summary>
        /// <param name="query">GetJournalSubjectListQuery object</param>
        /// <returns>JournalSubjectListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Subject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JournalSubjectListVm))]
        public async Task<ActionResult> GetSubjectAll(
            [FromQuery] GetJournalSubjectListQuery query
            )
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Создать предмет в журнале
        /// </summary>
        /// <param name="id">Индификатор журнала</param>
        /// <param name="model">CreateJournalSubjectModel object</param>
        /// <returns>JournalSubjectDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER</response>
        [Authorize(Roles = "TEACHER")]
        [HttpPost("{id}/Subject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JournalSubjectDto))]
        public async Task<ActionResult> CreateSubject(
            int id,CreateJournalSubjectModel model)
        {
            var command = new CreateJournalSubjectCommand
            {
                Hours = model.Hours,
                SubjectId = model.SubjectId,
                JournalId = id,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить предмет из журнала
        /// </summary>
        /// <param name="id">Индификатор предмет из журнала</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpDelete("Subject/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteSubject(int id)
        {
            var command = new DeleteJournalSubjectCommand
            {
                Id = id,
                UserId = UserId,
                Role = UserRole.Value
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Получить список тем из журнала
        /// </summary>
        /// <param name="query">GetJournalTopicListQuery object</param>
        /// <returns>JournalTopicListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Subject/Topic")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JournalTopicListVm))]
        public async Task<ActionResult> GetTopicAll(
            [FromQuery] GetJournalTopicListQuery query
            )
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Создать тему в журнале
        /// </summary>
        /// <param name="id">Индификатор предмета из журнала</param>
        /// <param name="model">CreateJournalTopicModel object</param>
        /// <returns>JournalTopicDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("Subject/{id}/Topic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateTopic(int id,CreateJournalTopicModel model)
        {
            var command = new CreateJournalTopicCommand
            {
                Title = model.Title,
                HomeWork = model.HomeWork,
                Hours = model.Hours,
                Date = model.Date,
                JournalSubjectId = id,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить тему из журнала
        /// </summary>
        /// <param name="id">Индификатор темы</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpDelete("Subject/Topic/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteTopic(int id)
        {
            var command = new DeleteJournalTopicCommand
            {
                Id = id,
                UserId = UserId,
                Role = UserRole.Value
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Получить список рядов из журнала
        /// </summary>
        /// <param name="query">GetJournalSubjectRowListQuery object</param>
        /// <returns>JournalSubjectRowListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Subject/Row")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JournalSubjectRowListVm))]
        public async Task<ActionResult> GetRowAll(
            [FromQuery] GetJournalSubjectRowListQuery query
            )
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Создать ряд в журнале
        /// </summary>
        /// <param name="id">Индификатор предмета из журнала</param>
        /// <param name="model">CreateJournalSubjectRowModel object</param>
        /// <returns>JournalSubjectRowDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("Subject/{id}/Row")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JournalSubjectRowDto))]
        public async Task<ActionResult> CreateRow(
            int id,CreateJournalSubjectRowModel model)
        {
            var command = new CreateJournalSubjectRowCommand
            {
                StudentId = model.StudentId,
                JournalSubjectId = id,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить ряд из журнала
        /// </summary>
        /// <param name="id">Индификатор ряда</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpDelete("Subject/Row/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteRow(int id)
        {
            var command = new DeleteJournalSubjectRowCommand
            {
                Id = id,
                UserId = UserId,
                Role = UserRole.Value
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Получить список колонкок из журнала
        /// </summary>
        /// <param name="query">GetJournalSubjectColumnListQuery object</param>
        /// <returns>JournalSubjectColumnListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Subject/Row/Column")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JournalSubjectColumnListVm))]
        public async Task<ActionResult> GetColumnAll(
            [FromQuery] GetJournalSubjectColumnListQuery query
            )
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Обновить оценку в журнале
        /// </summary>
        /// <param name="id">Индификатор колонки из журнала</param>
        /// <param name="evaluation">Новая оценка</param>
        /// <returns>JournalSubjectColumnDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPatch("Subject/Row/Column/{id}/Evaluation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JournalSubjectColumnDto))]
        public async Task<ActionResult> UpdateEvaluation(
            int id, JournalEvaluation evaluation)
        {
            var command = new UpdateJournalEvaluationCommand
            {
                JournalColumnId = id,
                Evaluation = evaluation,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Создать колонку в журнале
        /// </summary>
        /// <param name="id">Индификатор ряда из журнала</param>
        /// <param name="model">CreateJournalSubjectColumnModel object</param>
        /// <returns>JournalSubjectColumnDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("Subject/Row/Column")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JournalSubjectColumnDto))]
        public async Task<ActionResult> CreateColumn( CreateJournalSubjectColumnModel model)
        {
            var command = new CreateJournalSubjectColumnCommand
            {
                Evaluation = model.Evaluation,
                Date = model.Date,
                JournalSubjectRowId = model.JournalSubjectRowId,
                StudentId = model.StudentId,
                JournalSubjectId = model.JournalSubjectId,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить колонку из журнала
        /// </summary>
        /// <param name="id">Индификатор колонки</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpDelete("Subject/Row/Column/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteColumn(int id)
        {
            var command = new DeleteJournalSubjectColumnCommand
            {
                Id = id,
                UserId = UserId,
                Role = UserRole.Value
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
