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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ScheduleListVm>> GetAll(
            DateTime? onlyDate, DateTime? startDate, DateTime? endDate,
            [FromQuery] List<int> departmentIds, [FromQuery] List<int> groupIds,
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
                GroupIds = groupIds
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("Departments")]
        public async Task<ActionResult<ScheduleDepartmentListVm>> GetDepartments(
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

        [Authorize]
        [HttpGet("Columns")]
        public async Task<ActionResult<ScheduleColumnListVm>> GetColumns(
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

        [Authorize]
        [HttpGet("Rows")]
        public async Task<ActionResult<ScheduleRowListVm>> GetRows(
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

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("File")]
        public async Task<ActionResult<ScheduleDto>> FileCreate(IFormFile file)
        {
            var command = new FileCreateScheduleCommand
            {
                File = file
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost]
        public async Task<ActionResult<CreateScheduleVm>> Create(CreateScheduleCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("Department")]
        public async Task<ActionResult<CreateScheduleDepartmentVm>> CreateDepartment(
            CreateScheduleDepartmentCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("Department/Column")]
        public async Task<ActionResult<CreateScheduleColumnVm>> CreateColumn(
            CreateScheduleColumnCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost("Department/Column/Row")]
        public async Task<ActionResult<CreateScheduleRowVm>> CreateRow(
            CreateScheduleRowCommand command)
        {
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ScheduleDto>> Update(int id, UpdateScheduleModel model)
        {
            var command = new UpdateScheduleCommand
            {
                Id = id,
                Date = model.Date
            };

            await Mediator.Send(command);

            return Ok();
        }

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPut("Department/{id}")]
        public async Task<ActionResult<ScheduleDepartmentDto>> UpdateDepartment(
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

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPut("Department/Column/{id}")]
        public async Task<ActionResult<ScheduleColumnDto>> UpdateColumn(
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

        [Authorize(Roles = "EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPut("Department/Column/Row/{id}")]
        public async Task<ActionResult<ScheduleRowDto>> UpdateRow(
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
