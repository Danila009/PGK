using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Raportichka.Commands.CreateRaportichka;
using PGK.Application.App.Raportichka.Commands.DeleteRaportichka;
using PGK.Application.App.Raportichka.Commands.UpdateRaportichka;
using PGK.Application.App.Raportichka.Queries.GetRaportichkaList;
using PGK.Application.App.Raportichka.Row.Commands.CreateRow;
using PGK.Application.App.Raportichka.Row.Commands.DeleteRow;
using PGK.Application.App.Raportichka.Row.Commands.UpdateConfirmation;
using PGK.Application.App.Raportichka.Row.Commands.UpdateRow;
using PGK.Application.App.Raportichka.Row.Queries.GetRaportichkaRowList;
using PGK.WebApi.Models.Raportichka;

namespace PGK.WebApi.Controllers
{
    public class RaportichkaController : Controller
    {

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<RaportichkaListVm>> GetAll(
            bool? confirmation, DateTime? startDate, DateTime? endDate,
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
                StartDate = startDate,
                EndDate = endDate,
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

        [Authorize]
        [HttpGet("{id}/Rows")]
        public async Task<ActionResult<RaportichkaRowListVm>> GetRowsAll(
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CreateRaportichkaVm>> Create(CreateRaportichkaModel model)
        {
            var command = new CreateRaportichkaCommand
            {
                GroupId = model.GroupId,
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize]
        [HttpPost("{id}/Row")]
        public async Task<ActionResult<CreateRaportichkaRowVm>> AddRow(int id, CreateRaportichkaRowModel model)
        {
            var command = new CreateRaportichkaRowCommand
            {
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

        [Authorize]
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

        [Authorize]
        [HttpPut("Row/{id}")]
        public async Task<ActionResult> UpdateRow(int id, UpdateRaportichkaRowModel model)
        {
            var command = new UpdateRaportichkaRowCommand
            {
                RowId = id,
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

        [Authorize]
        [HttpPatch("{id}/Confirmation")]
        public async Task<ActionResult<UpdateConfirmationVm>> UpdateConfirmation(int id)
        {
            var command = new UpdateConfirmationCommand { RaportichkaRowId = id};
        
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var query = new DeleteRaportichkaCommand { Id = id };

            await Mediator.Send(query);

            return Ok("Successfully");
        }

        [HttpDelete("Row/{id}")]
        public async Task<ActionResult> DeleteRowById(int id)
        {
            var query = new DeleteRaportichkaRowCommand { Id = id };

            await Mediator.Send(query);

            return Ok("Successfully");
        }
    }
}
