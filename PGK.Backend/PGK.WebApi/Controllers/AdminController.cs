using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.User.Admin.Commands.DeleteAdmin;
using PGK.Application.App.User.Admin.Commands.Registration;
using PGK.Application.App.User.Admin.Queries.GetAdminDetails;
using PGK.Application.App.User.Admin.Queries.GetAdminList;
using PGK.Application.App.Vedomost.Commands.CreateVedomost;
using PGK.Application.App.Vedomost.Commands.DeleteVedomost;

namespace PGK.WebApi.Controllers
{
    public class AdminController : Controller
    {

        /// <summary>
        /// Получить список пользователей-администраторов
        /// </summary>
        /// <param name="query">GetAdminListQuery object</param>
        /// <returns>AdminListVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль ADMIN</response>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminListVm))]
        public async Task<ActionResult> GetAll(
            [FromQuery] GetAdminListQuery query)
        {
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Получить пользователя-администратора по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор администратора пользователя</param>
        /// <returns>AdminDto object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль ADMIN</response>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminDto))]
        public async Task<ActionResult> GetById(
            int id
            )
        {
            var query = new GetAdminDetailsQuery
            {
                AdminId = id
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>
        /// Удалить администратора
        /// </summary>
        /// <param name="id">Идентификатор администратора пользователя</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль ADMIN</response>
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteById(
            int id
            )
        {
            var command = new DeleteAdminCommand
            {
                AdminId = id
            };

            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Зарегистрировать администратора
        /// </summary>
        /// <param name="command">RegistrationAdminCommand object</param>
        /// <returns>RegistrationAdminVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль ADMIN</response>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("Registration")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegistrationAdminVm))]
        public async Task<ActionResult> Registration(
            RegistrationAdminCommand command
            )
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Создать ведомость за месяц
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="groupId">Индификатор группы</param>
        /// <param name="file">Электронная таблица</param>
        /// <returns>CreateVedomostVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль ADMIN</response>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("Vedomost")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateVedomostVm))]
        public async Task<ActionResult> CreateStatement(
            DateTime date,int groupId, IFormFile file)
        {
            var command = new CreateVedomostCommand
            {
                UserId = UserId,
                Role = UserRole.Value,
                File = file,
                Date = date,
                GroupId = groupId
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Удалить ведомость за месяц
        /// </summary>
        /// <param name="id">Идентификатор ведомасти</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль ADMIN</response>
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("Vedomost/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteVedomost(
            int id)
        {
            var command = new DeleteVedomostCommand
            {
                UserId = UserId,
                Role = UserRole.Value,
                VedomostId = id
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
