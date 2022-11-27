using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Raportichka.Commands.CreateRaportichka;
using PGK.Application.App.Raportichka.Row.Commands.UpdateRow;
using PGK.Application.App.User.Headman.Commands.Registration;
using PGK.WebApi.Models.Headman;
using PGK.WebApi.Models.Raportichka;

namespace PGK.WebApi.Controllers
{
    public class HeadmanController : Controller
    {
        [Authorize(Roles = "TEACHER")]
        [HttpPost("Registration")]
        public async Task<ActionResult<RegistrationHeadmanVm>> Registration(
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

        [Authorize(Roles = "HEADMAN,DEPUTY_HEADMAN")]
        [HttpPost("Raportichka")]
        public async Task<ActionResult<CreateRaportichkaVm>> CreateRaportichka()
        {
            var command = new CreateRaportichkaCommand
            {
                Role = UserRole.Value,
                UserId = UserId
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "HEADMAN,DEPUTY_HEADMAN,ADMIN")]
        [HttpPut("Raportichka/Row/{id}")]
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
