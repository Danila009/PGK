using AutoMapper;
using PGK.Application.App.Journal.Queries.GetJournalList;
using PGK.Application.App.Journal.Queries.GetJournalSubjectRowList;
using PGK.Application.App.Journal.Queries.GetJournalTopicList;
using PGK.Application.App.Subject.Queries.GetSubjectList;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using PGK.Application.Common.Mappings;
using PGK.Domain.Journal;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectList
{
    public class JournalSubjectDto : IMapWith<JournalSubject>
    {
        [Key] public int Id { get; set; }
        [Required] public int Hours { get; set; }
        [Required] public SubjectDto Subject { get; set; }
        [Required] public TeacherUserDetails Teacher { get; set; }
        [Required] public JournalDto Journal { get; set; }

        public virtual List<JournalTopicDto> Topics { get; set; } = new List<JournalTopicDto>();
        public virtual List<JournalSubjectRowDto> Rows { get; set; } = new List<JournalSubjectRowDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JournalSubject, JournalSubjectDto>();
        }
    }
}
