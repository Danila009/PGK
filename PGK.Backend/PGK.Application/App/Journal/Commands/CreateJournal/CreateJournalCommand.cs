using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalList;
using PGK.Domain.User;

namespace PGK.Application.App.Journal.Commands.CreateJournal
{
    public class CreateJournalCommand : IRequest<JournalDto>
    {
        public int Course { get; set; }
        public int Semester { get; set; }
        public int GroupId { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}