using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Raportichka.Commands.DeleteRaportichka;
using PGK.Application.App.Raportichka.Commands.UpdateRaportichka;
using PGK.Application.App.Raportichka.Queries.GetRaportichkaList;
using PGK.Application.App.Raportichka.Row.Commands.CreateRow;
using PGK.Application.App.Raportichka.Row.Commands.DeleteRow;
using PGK.Application.App.Raportichka.Row.Commands.UpdateConfirmation;
using PGK.Application.App.Raportichka.Row.Commands.UpdateRow;
using PGK.Application.App.Raportichka.Row.Queries.GetRaportichkaRowList;
using PGK.WebApi.Models.Raportichka;
using PGK.WebApi.Models.Teacher;

namespace PGK.WebApi.Controllers
{
    public class RaportichkaController : Controller
    {

        /// <summary>
        /// Получить список рапортичик
        /// </summary>
        /// <param name="confirmation">Подверждение преподавателя</param>
        /// <param name="onlyDate">Только за эту дату</param>
        /// <param name="startDate">Минимальный дата</param>
        /// <param name="endDate">Максимальный дата</param>
        /// <param name="raportichkaId"></param>
        /// <param name="groupIds">Список id групп разделенные запятой. Например groupIds=1,2,3</param>
        /// <param name="subjectIds">Список id предметов разделенные запятой. Например subjectIds=1,2,3</param>
        /// <param name="classroomTeacherIds">Список id классного руковаделя разделенные запятой. Например classroomTeacherIds=1,2,3</param>
        /// <param name="numberLessons">Список номер пары разделенные предметов запятой. Например numberLessons=1,2,3</param>
        /// <param name="teacherIds">Список id преподавателей разделенные запятой. Например teacherIds=1,2,3</param>
        /// <param name="studentIds">Список id студентов разделенные запятой. Например studentIds=1,2,3</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество результатов для возврата на страницу</param>
        /// <returns>RaportichkaListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RaportichkaListVm))]
        public async Task<ActionResult> GetAll(
            bool? confirmation,DateTime? onlyDate, DateTime? startDate, DateTime? endDate,
            [FromQuery] List<int> raportichkaId,
            [FromQuery] List<int> groupIds, [FromQuery] List<int> subjectIds,
            [FromQuery] List<int> classroomTeacherIds, [FromQuery] List<int> numberLessons,
            [FromQuery] List<int> teacherIds, [FromQuery] List<int> studentIds,
            int pageNumber = 1, int pageSize = 20)
        {
            var query = new GetRaportichkaListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Confirmation = confirmation,
                OnlyDate = onlyDate,
                StartDate = startDate,
                EndDate = endDate,
                RaportichkaId = raportichkaId,
                GroupIds = groupIds,
                SubjectIds = subjectIds,
                ClassroomTeacherIds = classroomTeacherIds,
                NumberLessons = numberLessons,
                TeacherIds = teacherIds,
                StudentIds = studentIds
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить список строк из рапортички
        /// </summary>
        /// <param name="id">Индификатор рапортички</param>
        /// <param name="subjectIds">Список id предметов разделенные запятой. Например subjectIds=1,2,3</param>
        /// <param name="numberLessons">Список номер пары разделенные предметов запятой. Например numberLessons=1,2,3</param>
        /// <param name="teacherIds">Список id преподавателей разделенные запятой. Например teacherIds=1,2,3</param>
        /// <param name="studentIds">Список id студентов разделенные запятой. Например studentIds=1,2,3</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество результатов для возврата на страницу</param>
        /// <returns>RaportichkaRowListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("{id}/Rows")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RaportichkaRowListVm))]
        public async Task<ActionResult> GetRowsAll(
            int id, [FromQuery] List<int> subjectIds,
            [FromQuery] List<int> numberLessons,
            [FromQuery] List<int> teacherIds, [FromQuery] List<int> studentIds,
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetRaportichkaRowListQuery
            {
                RaportichkaId = id,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SubjectIds = subjectIds,
                NumberLessons = numberLessons,
                TeacherIds = teacherIds,
                StudentIds = studentIds
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Добавить струку к рапортички
        /// </summary>
        /// <param name="id">Индификатор рапортички</param>
        /// <param name="model">CreateRaportichkaRowModel object</param>
        /// <returns>CreateRaportichkaRowVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль HEADMAN,DEPUTY_HEADMAN,ADMIN</response>
        [Authorize(Roles = "HEADMAN,DEPUTY_HEADMAN,ADMIN")]
        [HttpPost("{id}/Row")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateRaportichkaRowVm))]
        public async Task<ActionResult> AddRow(
            int id, CreateRaportichkaRowModel model)
        {
            var command = new CreateRaportichkaRowCommand
            {
                UserId = UserId,
                Role = UserRole.Value,
                RaportichkaId = id,
                NumberLesson = model.NumberLesson,
                Hours = model.Hours,
                StudentId = model.StudentId,
                SubjectId = model.SubjectId,
                TeacherId = model.TeacherId
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Изменить рапортичку
        /// </summary>
        /// <param name="id">Индификатор рапортички</param>
        /// <param name="model">UpdateRaportichkaModel object</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateRaportichkaModel model)
        {
            var command = new UpdateRaportichkaCommand
            {
                Id = id,
                GroupId = model.GroupId
            };

            await Mediator.Send(command);

            return Ok("Successfully");
        }

        /// <summary>
        /// Изменить строку из рапортички
        /// </summary>
        /// <param name="id">Индификатор строки</param>
        /// <param name="model">TeacherUpdateRaportichkaRowModel object</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER</response>
        [Authorize(Roles = "TEACHER")]
        [HttpPut("Row/{id}")]
        public async Task<ActionResult> UpdateRow(int id, TeacherUpdateRaportichkaRowModel model)
        {
            var command = new UpdateRaportichkaRowCommand
            {
                RowId = id,
                UserId = UserId,
                Role = UserRole.Value,
                NumberLesson = model.NumberLesson,
                Hours = model.Hours,
                SubjectId = model.SubjectId,
                StudentId = model.StudentId,
                RaportichkaId = model.RaportichkaId
            };

            await Mediator.Send(command);

            return Ok("Successfully");
        }

        /// <summary>
        /// Изменить подверждения преподпвателя
        /// </summary>
        /// <param name="id">Индификатор строки</param>
        /// <returns>UpdateConfirmationVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPatch("Row/{id}/Confirmation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateConfirmationVm))]
        public async Task<ActionResult> UpdateConfirmation(int id)
        {
            var command = new UpdateConfirmationCommand
            { 
                UserId = UserId,
                Role = UserRole.Value,
                RaportichkaRowId = id
            };
        
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Удалить рапортичку
        /// </summary>
        /// <param name="id">Индифиувтор рапортички</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль HEADMAN,DEPUTY_HEADMAN,TEACHER,ADMIN</response>
        [Authorize(Roles = "HEADMAN,DEPUTY_HEADMAN,TEACHER,ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var query = new DeleteRaportichkaCommand 
            {
                Id = id,
                UserId = UserId,
                UserRole = UserRole.Value
            };

            await Mediator.Send(query);

            return Ok("Successfully");
        }

        /// <summary>
        /// Удалить строку в рапортички
        /// </summary>
        /// <param name="id">Индификатор строки</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль HEADMAN,DEPUTY_HEADMAN,TEACHER,ADMIN</response>
        [Authorize(Roles = "HEADMAN,DEPUTY_HEADMAN,TEACHER,ADMIN")]
        [HttpDelete("Row/{id}")]
        public async Task<ActionResult> DeleteRowById(int id)
        {
            var query = new DeleteRaportichkaRowCommand 
            { 
                Id = id,
                UserId = UserId,
                UserRole = UserRole.Value
            };

            await Mediator.Send(query);

            return Ok("Successfully");
        }
    }
}
