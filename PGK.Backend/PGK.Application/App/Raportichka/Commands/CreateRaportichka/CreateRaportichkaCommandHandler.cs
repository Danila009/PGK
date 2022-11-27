using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User;
using PGK.Domain.User.Student;

namespace PGK.Application.App.Raportichka.Commands.CreateRaportichka
{
    public class CreateRaportichkaCommandHandler
        : IRequestHandler<CreateRaportichkaCommand, CreateRaportichkaVm>
    {
        public readonly IPGKDbContext _dbContext;

        public CreateRaportichkaCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<CreateRaportichkaVm> Handle(CreateRaportichkaCommand request,
            CancellationToken cancellationToken)
        {
            Domain.Raportichka.Raportichka? raportichka;

            if (request.Role == UserRole.HEADMAN || request.Role == UserRole.DEPUTY_HEADMAN)
            {
               raportichka = await CreateRaportichkaHeadmanOrDeputyHeadman(studentId: request.UserId);
            }
            else if (request.Role == UserRole.TEACHER || request.Role == UserRole.ADMIN)
            {
                raportichka = await CreateRaportichkaTeacherOrAdmin(groupId: request.GroupId ?? 0);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

            await _dbContext.Raportichkas.AddAsync(raportichka);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateRaportichkaVm
            {
                Id = raportichka.Id
            };
        }

        private async Task<Domain.Raportichka.Raportichka> CreateRaportichkaHeadmanOrDeputyHeadman(
            int studentId
            )
        {
            var student = await _dbContext.StudentsUsers
                .Include(u => u.Group)
                    .ThenInclude(u => u.Raportichkas)
                .FirstOrDefaultAsync(u => u.Id == studentId);

            if (student == null)
            {
                throw new NotFoundException(nameof(StudentUser), studentId);
            }

            var date = DateTime.Now;

            if(student.Group.Raportichkas.Any(u => u.Date.Date == date.Date))
            {
                throw new Exception("Группа может иметь только одну рапортичку за день.");
            }

            return new Domain.Raportichka.Raportichka
            {
                Date = date,
                Group = student.Group
            };
        }

        private async Task<Domain.Raportichka.Raportichka> CreateRaportichkaTeacherOrAdmin(int groupId)
        {
            var group = await _dbContext.Groups
                .Include(u => u.Raportichkas)
                .FirstOrDefaultAsync(u => u.Id == groupId);

            if (group == null)
            {
                throw new NotFoundException(nameof(Domain.Group.Group), groupId);
            }

            var date = DateTime.Now;

            if (group.Raportichkas.Any(u => u.Date.Date == date.Date))
            {
                throw new Exception("Группа может иметь только одну рапортичку за день.");
            }

            return new Domain.Raportichka.Raportichka
            {
                Date = date,
                Group = group
            };
        }
    }
}
