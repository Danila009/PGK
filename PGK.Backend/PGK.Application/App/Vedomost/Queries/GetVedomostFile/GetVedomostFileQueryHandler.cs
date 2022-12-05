using MediatR;
using PGK.Application.Common;
using PGK.Application.Interfaces;
using PGK.Application.Repository.FileRepository;

namespace PGK.Application.App.Vedomost.Queries.GetVedomostFile
{
    internal class GetVedomostFileQueryHandler
        : IRequestHandler<GetVedomostFileQuery, byte[]>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IFileRepository _fileRepository;

        public GetVedomostFileQueryHandler(IPGKDbContext dbContext,
            IFileRepository fileRepository) => (_dbContext, _fileRepository) = (dbContext, fileRepository);

        public async Task<byte[]> Handle(GetVedomostFileQuery request,
            CancellationToken cancellationToken)
        {
            var filePath = Constants.STATEMENT_FILE_PATH;

            var file = _fileRepository.GetFile($"{filePath}{request.FileId}");

            if(file == null)
            {
                throw new Exception($"Not found vedomost file {request.FileId}");
            }

            return file;
        }
    }
}
