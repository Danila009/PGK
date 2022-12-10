using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Group.Commands.CreateGroup;
using PGK.Application.App.Group.Commands.DeleteGroup;
using PGK.Application.App.Group.Commands.UpdateGroup;
using PGK.Application.App.Group.Queries.GetClassroomTeacher;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.App.Group.Queries.GetGroupList;
using PGK.Application.App.Group.Queries.GetGroupStudentList;
using PGK.Application.App.Raportichka.Commands.CreateRaportichka;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using PGK.WebApi.Models.Group;

namespace PGK.WebApi.Controllers
{
    public class GroupController : Controller
    {
        /// <summary>
        /// Получить список групп
        /// </summary>
        /// <param name="query">GetGroupListQuery object</param>
        /// <returns>GroupListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupListVm))]
        public async Task<ActionResult> GetAll(
            [FromQuery] GetGroupListQuery query
            )
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить группу по идентификатору
        /// </summary>
        /// <param name="id">идентификатор группы</param>
        /// <returns>GroupDetails object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupDetails))]
        public async Task<ActionResult> GetDetails(int id)
        {
            var query = new GetGroupDetailsQuery
            {
                GroupId = id
            };

            var details = await Mediator.Send(query);

            return Ok(details);
        }

        /// <summary>
        /// Получить классного руководителя по идентификатору группы
        /// </summary>
        /// <param name="id">идентификатор группы</param>
        /// <returns>TeacherUserDetails object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("{id}/ClassroomTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherUserDetails))]
        public async Task<ActionResult> GetClassroomTeacher(int id)
        {
            var query = new GetClassroomTeacherQuery
            {
                GroupId = id
            };

            var details = await Mediator.Send(query);

            return Ok(details);
        }

        /// <summary>
        /// Получить студентов по идентификатору группы
        /// </summary>
        /// <param name="id">идентификатор группы</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество результатов для возврата на страницу</param>
        /// <returns>GroupStudentListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        [Authorize]
        [HttpGet("{id}/Students")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupStudentListVm))]
        public async Task<ActionResult> GetStudentAll(
            int id, int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetGroupStudentListQuery
            {
                GroupId = id,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Создать новую группу
        /// </summary>
        /// <param name="command">CreateGroupCommand object</param>
        /// <returns>CreateGroupVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateGroupVm))]
        public async Task<ActionResult> Create(CreateGroupCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Добавить рапортичку группе
        /// </summary>
        /// <param name="id">идентификатор группы</param>
        /// <returns>CreateRaportichkaVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("{id}/Raportichka")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateRaportichkaVm))]
        public async Task<ActionResult> CreateRaportichka(int id)
        {
            var command = new CreateRaportichkaCommand
            {
                Role = UserRole.Value,
                UserId = UserId,
                GroupId = id
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Обновить группу
        /// </summary>
        /// <param name="id">идентификатор группы</param>
        /// <param name="model">UpdateGroupModel object</param>
        /// <returns>GroupDetails object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupDetails))]
        public async Task<ActionResult> Update(
            int id, [FromBody] UpdateGroupModel model)
        {
            var command = new UpdateGroupCommand
            {
                Id = id,
                Number = model.Number,
                SpecialityId = model.SpecialityId,
                ClassroomTeacherId = model.ClassroomTeacherId,
                HeadmanId = model.HeadmanId,
                DeputyHeadmaId = model.DeputyHeadmaId,
                DepartmentId = model.DepartmentId
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Удалить группу
        /// </summary>
        /// <param name="id">идентификатор группы</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,EDUCATIONAL_SECTOR,ADMIN</response>
        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteGroupCommand { GroupId = id };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
