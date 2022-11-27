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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<RaportichkaListVm>> GetAll(
            bool? confirmation,DateTime? onlyDate, DateTime? startDate, DateTime? endDate,
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

        [Authorize(Roles = "HEADMAN,DEPUTY_HEADMAN,ADMIN")]
        [HttpPost("{id}/Row")]
        public async Task<ActionResult<CreateRaportichkaRowVm>> AddRow(
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
                Confirmation = model.Confirmation,
                Hours = model.Hours,
                SubjectId = model.SubjectId,
                StudentId = model.StudentId,
                RaportichkaId = model.RaportichkaId
            };

            await Mediator.Send(command);

            return Ok("Successfully");
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPatch("Row/{id}/Confirmation")]
        public async Task<ActionResult<UpdateConfirmationVm>> UpdateConfirmation(int id)
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

        [Authorize(Roles = "HEADMAN,DEPUTY_HEADMAN,ADMIN")]
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
