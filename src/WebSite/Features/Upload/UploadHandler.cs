using WebSite.Shared;
using MediatR;

namespace WebSite.Features.Upload
{
    internal class UploadHandler : IRequestHandler<UploadCommand, Result<Guid>>
    {
        public Task<Result<Guid>> Handle(UploadCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
