using MediatR;
using Microsoft.AspNetCore.Mvc;
using PGK.Domain.Journal;
using System.ComponentModel;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList
{
    public class GetJournalSubjectColumnListQuery : IRequest<JournalSubjectColumnListVm>
    {
        [FromQuery(Name = "journalRowId")] public int? JournalRowId { get; set; }
        [FromQuery(Name = "studentIds")] public List<int>? StudentIds { get; set; } = new List<int>();
        [FromQuery(Name = "evaluation")] public JournalEvaluation? Evaluation { get; set; }

        [FromQuery(Name = "pageNumber"), DefaultValue("1")] public int PageNumber { get; set; } = 1;
        [FromQuery(Name = "pageSize"), DefaultValue("20")] public int PageSize { get; set; } = 20;
    }
}
