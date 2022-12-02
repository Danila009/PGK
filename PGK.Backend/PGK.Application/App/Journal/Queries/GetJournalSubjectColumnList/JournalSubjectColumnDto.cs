
using AutoMapper;
using PGK.Application.App.Journal.Queries.GetJournalSubjectRowList;
using PGK.Application.Common.Mappings;
using PGK.Domain.Journal;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList
{
    public class JournalSubjectColumnDto : IMapWith<JournalSubjectColumn>
    {
        [Key] public int Id { get; set; }
        [Required] public JournalEvaluation Evaluation { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public JournalSubjectRowDto Row { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JournalSubjectColumn, JournalSubjectColumnDto>();
        }
    }
}
