using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.Student;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Raportichka.Row.Commands.UpdateRow
{
    public class UpdateRaportichkaRowCommandHandler
        : IRequestHandler<UpdateRaportichkaRowCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public UpdateRaportichkaRowCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateRaportichkaRowCommand request,
            CancellationToken cancellationToken)
        {
            var row = await _dbContext.RaportichkaRows.FindAsync(request.RowId);

            if (row == null)
            {
                throw new NotFoundException(nameof(Domain.Raportichka.RaportichkaRow),
                    request.RowId);
            }

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

            row.NumberLesson = request.NumberLesson;
            row.Confirmation = request.Confirmation;
            row.Hours = request.Hours;
            row.Subject = subject;
            row.Teacher = teacher;
            row.Student = student;
            row.Raportichka = raportichka;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
