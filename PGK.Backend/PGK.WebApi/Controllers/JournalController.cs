using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Journal.Commands.CreateJournal;
using PGK.Application.App.Journal.Commands.CreateJournalSubject;
using PGK.Application.App.Journal.Commands.CreateJournalSubjectColumn;
using PGK.Application.App.Journal.Commands.CreateJournalSubjectRow;
using PGK.Application.App.Journal.Commands.CreateJournalTopic;
using PGK.Application.App.Journal.Queries.GetJournalList;
using PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList;
using PGK.Application.App.Journal.Queries.GetJournalSubjectList;
using PGK.Application.App.Journal.Queries.GetJournalSubjectRowList;
using PGK.Application.App.Journal.Queries.GetJournalTopicList;
using PGK.WebApi.Models.Journal;

namespace PGK.WebApi.Controllers
{
    public class JournalController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<JournalListVm>> GetAll(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetJournalListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = "TEACHER,EDUCATIONAL_SECTOR,ADMIN")]
        [HttpPost]
        public async Task<ActionResult<JournalDto>> Create(CreateJournalModel model)
        {
            var command = new CreateJournalCommand
            {
                Course = model.Course,
                Semester = model.Semester,
                GroupId = model.GroupId,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [Authorize]
        [HttpGet("Subject")]
        public async Task<ActionResult<JournalSubjectListVm>> GetSubjectAll(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetJournalSubjectListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = "TEACHER")]
        [HttpPost("Subject")]
        public async Task<ActionResult<JournalSubjectDto>> CreateSubject(CreateJournalSubjectModel model)
        {
            var command = new CreateJournalSubjectCommand
            {
                Hours = model.Hours,
                SubjectId = model.SubjectId,
                JournalId = model.JournalId,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [Authorize]
        [HttpGet("Subject/Topic")]
        public async Task<ActionResult<JournalTopicListVm>> GetTopicAll(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetJournalTopicListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("Subject/Topic")]
        public async Task<ActionResult<JournalTopicDto>> CreateTopic(CreateJournalTopicModel model)
        {
            var command = new CreateJournalTopicCommand
            {
                Title = model.Title,
                HomeWork = model.HomeWork,
                Hours = model.Hours,
                Date = model.Date,
                JournalSubjectId = model.JournalSubjectId,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [Authorize]
        [HttpGet("Subject/Row")]
        public async Task<ActionResult<JournalSubjectRowListVm>> GetRowAll(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetJournalSubjectRowListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("Subject/Row")]
        public async Task<ActionResult<JournalSubjectRowDto>> CreateRow(CreateJournalSubjectRowModel model)
        {
            var command = new CreateJournalSubjectRowCommand
            {
                StudentId = model.StudentId,
                JournalSubjectId = model.JournalSubjectId,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [Authorize]
        [HttpGet("Subject/Row/Column")]
        public async Task<ActionResult<JournalSubjectColumnListVm>> GetColumnAll(
            int pageNumber = 1, int pageSize = 20
            )
        {
            var query = new GetJournalSubjectColumnListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = "TEACHER,ADMIN")]
        [HttpPost("Subject/Row/Column")]
        public async Task<ActionResult<JournalSubjectColumnDto>> CreateColumn(
            CreateJournalSubjectColumnModel model)
        {
            var command = new CreateJournalSubjectColumnCommand
            {
                Evaluation = model.Evaluation,
                Date = model.Date,
                JournalSubjectRowId = model.JournalSubjectRowId,
                UserId = UserId,
                Role = UserRole.Value
            };

            var dto = await Mediator.Send(command);

            return Ok(dto);
        }
    }
}
