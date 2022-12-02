using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Journal.Queries.GetJournalList;
using PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList;
using PGK.Application.App.Journal.Queries.GetJournalSubjectList;
using PGK.Application.App.Journal.Queries.GetJournalSubjectRowList;
using PGK.Application.App.Journal.Queries.GetJournalTopicList;

namespace PGK.WebApi.Controllers
{
    public class JournalController : Controller
    {
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
    }
}
