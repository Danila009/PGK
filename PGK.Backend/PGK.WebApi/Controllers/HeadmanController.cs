using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Raportichka.Commands.CreateRaportichka;
using PGK.Application.App.Raportichka.Row.Commands.UpdateRow;
using PGK.Application.App.User.Headman.Commands.DeputyRegistration;
using PGK.Application.App.User.Headman.Commands.Registration;
using PGK.Application.App.Vedomost.Commands.CreateVedomost;
using PGK.Application.App.Vedomost.Commands.DeleteVedomost;
using PGK.WebApi.Models.Headman;
using PGK.WebApi.Models.Raportichka;

namespace PGK.WebApi.Controllers
{
    public class HeadmanController : Controller
    {
        /// <summary>
        /// Регестрация старосты
        /// </summary>
        /// <param name="model">RegistrationHeadmanModel object</param>
        /// <returns>RegistrationHeadmanVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("Registration")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegistrationHeadmanVm))]
        public async Task<ActionResult> Registration(
            RegistrationHeadmanModel model)
        {
            var command = new RegistrationHeadmanCommand
            {
                StudentId = model.StudentId,
                TeacherId = UserId
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Регестрация зам старосты
        /// </summary>
        /// <param name="model">RegistrationHeadmanModel object</param>
        /// <returns>RegistrationHeadmanVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль TEACHER,ADMIN</response>
        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("Deputy/Registration")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegistrationHeadmanVm))]
        public async Task<ActionResult> RegistrationDeputy(
            RegistrationHeadmanModel model)
        {
            var command = new RegistrationDeputyHeadmanCommand
            {
                StudentId = model.StudentId,
                TeacherId = UserId
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Создания рапортички
        /// </summary>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль HEADMAN,DEPUTY_HEADMAN</response>
        [Authorize(Roles = "HEADMAN,DEPUTY_HEADMAN")]
        [HttpPost("Raportichka")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateRaportichkaVm))]
        public async Task<ActionResult> CreateRaportichka()
        {
            var command = new CreateRaportichkaCommand
            {
                Role = UserRole.Value,
                UserId = UserId
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Добавить ведомасть
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="file">Электронная таблица</param>
        /// <returns>CreateVedomostVm object</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль HEADMAN,DEPUTY_HEADMAN</response>
        [Authorize(Roles = "HEADMAN,DEPUTY_HEADMAN")]
        [HttpPost("Vedomost")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateVedomostVm))]
        public async Task<ActionResult> CreateVedomost(DateTime date, FormFile file)
        {
            var command = new CreateVedomostCommand
            {
                UserId = UserId,
                Role = UserRole.Value,
                File = file,
                Date = date
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Удалить ведомасть
        /// </summary>
        /// <param name="id">Индификатор ведомасти</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль HEADMAN,DEPUTY_HEADMAN</response>
        [Authorize(Roles = "HEADMAN,DEPUTY_HEADMAN")]
        [HttpDelete("Vedomost/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteVedomost(int id)
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

        /// <summary>
        /// Обнавить ряд у рапортички
        /// </summary>
        /// <param name="id">Индификатор ряда</param>
        /// <param name="model">UpdateRaportichkaRowModel object</param>
        /// <returns>Returns NoContend</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="401">Пустой или неправильный токен</response>
        /// <response code="403">Авторизация роль HEADMAN,DEPUTY_HEADMAN,ADMIN</response>
        [Authorize(Roles = "HEADMAN,DEPUTY_HEADMAN,ADMIN")]
        [HttpPut("Raportichka/Row/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> HeadmanUpdateRow(int id, UpdateRaportichkaRowModel model)
        {
            var command = new UpdateRaportichkaRowCommand
            {
                RowId = id,
                UserId = UserId,
                Role = UserRole.Value,
                NumberLesson = model.NumberLesson,
                Confirmation = model.Confirmation,
                Hours = model.Hours,
                SubjectId = model.SubjectId,
                TeacherId = model.TeacherId,
                StudentId = model.StudentId,
                RaportichkaId = model.RaportichkaId
            };

            await Mediator.Send(command);

            return Ok("Successfully");
        }
    }
}
