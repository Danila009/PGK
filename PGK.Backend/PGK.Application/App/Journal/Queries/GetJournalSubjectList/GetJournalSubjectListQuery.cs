using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectList
{
    public class GetJournalSubjectListQuery : IRequest<JournalSubjectListVm>
    {
        [FromQuery(Name = "journalId")] public int? JournalId { get; set; }

        [FromQuery(Name = "pageNumber"), DefaultValue("1")] public int PageNumber { get; set; } = 1;
        [FromQuery(Name = "pageSize"), DefaultValue("20")] public int PageSize { get; set; } = 20;
    }
}
