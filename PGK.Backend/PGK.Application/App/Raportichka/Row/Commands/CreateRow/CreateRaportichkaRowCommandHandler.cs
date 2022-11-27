using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Raportichka;
using PGK.Domain.User.Student;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Raportichka.Row.Commands.CreateRow
{
    public class CreateRaportichkaRowCommandHandler
        : IRequestHandler<CreateRaportichkaRowCommand, CreateRaportichkaRowVm>
    {
        private readonly IPGKDbContext _dbContext;

        public CreateRaportichkaRowCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<CreateRaportichkaRowVm> Handle(CreateRaportichkaRowCommand request,
            CancellationToken cancellationToken)
        {
            var raportichka = await _dbContext.Raportichkas.FindAsync(request.RaportichkaId);

            if (raportichka == null)
            {
                throw new NotFoundException(nameof(Domain.Raportichka.Raportichka),
                    request.RaportichkaId);
            }

            var subject = await _dbContext.Subjects.FindAsync(request.SubjectId);

            if (subject == null)
            {
                throw new NotFoundException(nameof(Domain.Subject.Subject),
                    request.SubjectId);
            }

            var student = await _dbContext.StudentsUsers.FindAsync(request.StudentId);

            if (student == null)
            {
                throw new NotFoundException(nameof(StudentUser),
                    request.StudentId);
            }

            var teacher = await _dbContext.TeacherUsers.FindAsync(request.TeacherId);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(TeacherUser),
                    request.TeacherId);
            }

            var row = new RaportichkaRow
            {
                NumberLesson = request.NumberLesson,
                Confirmation = false,
                Hours = request.Hours,
                Student = student,
                Subject = subject,
                Teacher = teacher,
                Raportichka = raportichka
            };

            await _dbContext.RaportichkaRows.AddAsync(row);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateRaportichkaRowVm
            {
                Id = row.Id
            };
        }
    }
}
