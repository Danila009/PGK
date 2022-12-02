using AutoMapper;
using PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList;
using PGK.Application.App.Journal.Queries.GetJournalSubjectList;
using PGK.Application.App.User.Student.Queries.GetStudentUserList;
using PGK.Application.Common.Mappings;
using PGK.Domain.Journal;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectRowList
{
    public class JournalSubjectRowDto : IMapWith<JournalSubjectRow>
    {
        [Key] public int Id { get; set; }
        [Required] public StudentDto Student { get; set; }

        [Required] public JournalSubjectDto JournalSubject { get; set; }

        public virtual List<JournalSubjectColumnDto> Columns { get; set; } = new List<JournalSubjectColumnDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JournalSubjectRow, JournalSubjectRowDto>();
        }
    }
}
