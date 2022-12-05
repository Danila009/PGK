using MediatR;

namespace PGK.Application.App.Vedomost.Queries.GetVedomostFile
{
    public class GetVedomostFileQuery : IRequest<byte[]>
    {
        public string FileId { get; set; } = string.Empty;
    }
}
