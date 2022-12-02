using AutoMapper;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.App.Journal.Queries.GetJournalSubjectList;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Journal.Queries.GetJournalList
{
    public class JournalDto : IMapWith<Domain.Journal.Journal>
    {
        [Key] public int Id { get; set; }
        [Required] public int Course { get; set; }
        [Required] public int Semester { get; set; }
        [Required] public GroupDetails Group { get; set; }

        public virtual List<JournalSubjectDto> Subjects { get; set; } = new List<JournalSubjectDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Journal.Journal, JournalDto>();
        }
    }
}
