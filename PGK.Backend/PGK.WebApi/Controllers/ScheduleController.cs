using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Schedule.Commands.CreateSchedule;
using PGK.Application.App.Schedule.Commands.CreateScheduleColumn;
using PGK.Application.App.Schedule.Commands.CreateScheduleDepartment;
using PGK.Application.App.Schedule.Commands.CreateScheduleRow;
using PGK.Application.App.Schedule.Commands.DeleteSchedule;
using PGK.Application.App.Schedule.Commands.DeleteScheduleColumn;
using PGK.Application.App.Schedule.Commands.DeleteScheduleDepartment;
using PGK.Application.App.Schedule.Commands.DeleteScheduleRow;
using PGK.Application.App.Schedule.Commands.FileCreateSchedule;
using PGK.Application.App.Schedule.Commands.UpdateSchedule;
using PGK.Application.App.Schedule.Commands.UpdateScheduleColumn;
using PGK.Application.App.Schedule.Commands.UpdateScheduleDepartment;
using PGK.Application.App.Schedule.Commands.UpdateScheduleRow;
using PGK.Application.App.Schedule.GetScheduleList.Queries;
using PGK.Application.App.Schedule.Queries.GetScheduleColumnList;
using PGK.Application.App.Schedule.Queries.GetScheduleDepartmentList;
using PGK.Application.App.Schedule.Queries.GetScheduleRowList;
using PGK.WebApi.Models.Schedule;

namespace PGK.WebApi.Controllers
{
    public class ScheduleController : Controller
    {
        /// <summary>
        /// Получить распичания
        /// </summary>
        /// <param name="onlyDate">Только за эту дату</param>
        /// <param name="startDate">Минимальный дата</param>
        /// <param name="endDate">Максимальный дата</param>
        /// <param name="groupIds">Список id групп разделенные запятой. Например groupIds=1,2,3</param>
        /// <param name="teacherIds">Список id преподавателей разделенные запятой. Например teacherIds=1,2,3</param>
        /// <param name="departmentIds">Список id отделов разделенные запятой. Например departmentIds=1,2,3</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество результатов для возврата на страницу</param>
        /// <returns>ScheduleListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleListVm))]
        public async Task<ActionResult> GetAll(
            DateTime? onlyDate, DateTime? startDate, DateTime? endDate,
            [FromQuery] List<int> departmentIds, [FromQuery] List<int> groupIds,
            [FromQuery] List<int> teacherIds,
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetScheduleListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                OnlyDate = onlyDate,
                StartDate = startDate,
                EndDate = endDate,
                DepartmentIds = departmentIds,
                GroupIds = groupIds,
                TeacherIds = teacherIds
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить распичание по отделу
        /// </summary>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество результатов для возврата на страницу</param>
        /// <returns>ScheduleDepartmentListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Departments")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleDepartmentListVm))]
        public async Task<ActionResult> GetDepartments(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetScheduleDepartmentListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить столбцы расписания
        /// </summary>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество результатов для возврата на страницу</param>
        /// <returns>ScheduleColumnListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Columns")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleColumnListVm))]
        public async Task<ActionResult> GetColumns(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetScheduleColumnListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить строки расписания
        /// </summary>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество результатов для возврата на страницу</param>
        /// <returns>ScheduleRowListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("Rows")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleRowListVm))]
        public async Task<ActionResult> GetRows(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetScheduleRowListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Добавить расписания. Преобразования файла
        /// </summary>
        /// <param name="file">Файл</param>
        /// <returns>ScheduleDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("File")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleDto))]
        public async Task<ActionResult> FileCreate(IFormFile file)
        {
            var command = new FileCreateScheduleCommand
            {
                File = file
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Добавить расписания
        /// </summary>
        /// <param name="command">CreateScheduleCommand object</param>
        /// <returns>CreateScheduleVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateScheduleVm))]
        public async Task<ActionResult> Create(CreateScheduleCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Добавить отделения в распичание
        /// </summary>
        /// <param name="command">CreateScheduleDepartmentCommand object</param>
        /// <returns>CreateScheduleDepartmentVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("Department")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateScheduleDepartmentVm))]
        public async Task<ActionResult> CreateDepartment(
            CreateScheduleDepartmentCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Добавить колонку в распичание
        /// </summary>
        /// <param name="command">CreateScheduleColumnCommand object</param>
        /// <returns>CreateScheduleColumnVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("Department/Column")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateScheduleColumnVm))]
        public async Task<ActionResult> CreateColumn(
            CreateScheduleColumnCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Добавить страку в распичание
        /// </summary>
        /// <param name="command">CreateScheduleRowCommand object</param>
        /// <returns>CreateScheduleRowVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("Department/Column/Row")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateScheduleRowVm))]
        public async Task<ActionResult> CreateRow(
            CreateScheduleRowCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Изменить расписания
        /// </summary>
        /// <param name="id">Индификатор расписания</param>
        /// <param name="model">UpdateScheduleModel object</param>
        /// <returns>ScheduleDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleDto))]
        public async Task<ActionResult> Update(int id, UpdateScheduleModel model)
        {
            var command = new UpdateScheduleCommand
            {
                Id = id,
                Date = model.Date
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Изменить отдел в расписании
        /// </summary>
        /// <param name="id">Индификатор отделв в расписании</param>
        /// <param name="model">UpdateScheduleDepartmentModel object</param>
        /// <returns>ScheduleDepartmentDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPut("Department/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleDepartmentDto))]
        public async Task<ActionResult> UpdateDepartment(
            int id, UpdateScheduleDepartmentModel model)
        {
            var command = new UpdateScheduleDepartmentCommand
            {
                Id = id,
                Text = model.Text,
                DepartmentId = model.DepartmentId
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Изменить колонку
        /// </summary>
        /// <param name="id">Индификатор колонки</param>
        /// <param name="model">UpdateScheduleColumnModel object</param>
        /// <returns>ScheduleColumnDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPut("Department/Column/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleColumnDto))]
        public async Task<ActionResult> UpdateColumn(
            int id, UpdateScheduleColumnModel model)
        {
            var command = new UpdateScheduleColumnCommand
            {
                Id = id,
                Time = model.Time,
                GroupId = model.GroupId
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Изменить строку
        /// </summary>
        /// <param name="id">Индификатор строки</param>
        /// <param name="model">UpdateScheduleColumnModel object</param>
        /// <returns>UpdateScheduleRowModel object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPut("Department/Column/Row/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScheduleRowDto))]
        public async Task<ActionResult> UpdateRow(
            int id, UpdateScheduleRowModel model)
        {
            var command = new UpdateScheduleRowCommand
            {
                Id = id,
                Text = model.Text,
                TeacherId = model.TeacherId
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Удалить расписания
        /// </summary>
        /// <param name="id">Индификатор расписания</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteScheduleCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Удалить отдел в расписание
        /// </summary>
        /// <param name="id">Индификатор отделения в расписание</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpDelete("Department/{id}")]
        public async Task<ActionResult> DeleteDepartment(
            int id)
        {
            var command = new DeleteScheduleDepartmentCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Удалить колонку в расписание
        /// </summary>
        /// <param name="id">Индификатор колонки в расписание</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpDelete("Department/Column/{id}")]
        public async Task<ActionResult> DeleteColumn(
            int id)
        {
            var command = new DeleteScheduleColumnCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }


        /// <summary>
        /// Удалить строку в расписание
        /// </summary>
        /// <param name="id">Индификатор стрки в расписание</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpDelete("Department/Column/Row/{id}")]
        public async Task<ActionResult> CreateRow(
            int id)
        {
            var command = new DeleteScheduleRowCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
