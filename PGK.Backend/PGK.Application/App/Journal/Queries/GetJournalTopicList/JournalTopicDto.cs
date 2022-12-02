using AutoMapper;
using PGK.Application.App.Journal.Queries.GetJournalSubjectList;
using PGK.Application.Common.Mappings;
using PGK.Domain.Journal;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Journal.Queries.GetJournalTopicList
{
    public class JournalTopicDto : IMapWith<JournalTopic>
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        public string? HomeWork { get; set; }
        [Required] public int Hours { get; set; }
        [Required] public DateTime Date { get; set; }

        [Required] public JournalSubjectDto JournalSubject { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JournalTopic, JournalTopicDto>();
        }
    }
}
