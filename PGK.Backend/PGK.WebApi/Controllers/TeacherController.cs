using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Raportichka.Row.Commands.CreateRow;
using PGK.Application.App.User.Teacher.Commands.DeleteTeacher;
using PGK.Application.App.User.Teacher.Commands.Registration;
using PGK.Application.App.User.Teacher.Commands.TeacherAddSubject;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserList;
using PGK.WebApi.Models.Teacher;

namespace PGK.WebApi.Controllers
{
    public class TeacherController : Controller
    {
        /// <summary>
        /// Получите список прподавателей
        /// </summary>
        /// <param name="search">Ключивые слова для поиска</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество результатов для возврата на страницу</param>
        /// <returns>TeacherUserListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherUserListVm))]
        public async Task<ActionResult> GetAll(
            string? search,
            int pageNumber = 1, 
            int pageSize = 20)
        {
            var query = new GetTeacherUserListQuery
            {
                Search = search,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получите прподавателя по индификатору
        /// </summary>
        /// <param name="id">Индификатор преподавателя</param>
        /// <returns>TeacherUserDetails object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherUserDetails))]
        public async Task<ActionResult> GetById(int id)
        {
            var query = new GetTeacherUserDetailsQuery
            {
                Id = id
            };

            var dto = await Mediator.Send(query);

            return Ok(dto);
        }

        /// <summary>
        /// Зарегистрировать преподавателя
        /// </summary>
        /// <param name="command">RegistrationTeacherCommand object</param>
        /// <returns>RegistrationTeacherVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("Registration")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegistrationTeacherVm))]
        public async Task<ActionResult> Registration(
            RegistrationTeacherCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Удалить преподавателя
        /// </summary>
        /// <param name="id">Идентификатор преподавателя</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteTeacherCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }


        /// <summary>
        /// Добавить к природавателю предмет
        /// </summary>
        /// <param name="id">Идентификатор преподавателя</param>
        /// <param name="subjectId">Идентификатор предмета</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("{id}/Subject")]
        public async Task<ActionResult> AddSubject(int id, int subjectId)
        {
            var command = new TeacherAddSubjectCommand
            {
                TeacgerId = id,
                SubjectId = subjectId
            };

            await Mediator.Send(command);

            return Ok("Successfully");
        }

        /// <summary>
        /// Добавить ряд в рапортички
        /// </summary>
        /// <param name="id">Идентификатор рапортички</param>
        /// <param name="model">TeacherCreateRaportichkaRowModel object</param>
        /// <returns>CreateRaportichkaRowVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER</response>
        [Authorize(Roles = "TEACHER")]
        [HttpPost("Raportichka/{id}/Row")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateRaportichkaRowVm))]
        public async Task<ActionResult> TeacherAddRow(
            int id, TeacherCreateRaportichkaRowModel model)
        {
            var command = new CreateRaportichkaRowCommand
            {
                UserId = UserId,
                Role = UserRole.Value,
                RaportichkaId = id,
                NumberLesson = model.NumberLesson,
                Hours = model.Hours,
                StudentId = model.StudentId,
                SubjectId = model.SubjectId
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
    }
}
