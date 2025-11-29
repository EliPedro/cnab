using EP.AspireApp.Cnab.AppHost.Shared;
using MediatR;

namespace EP.AspireApp.Cnab.AppHost.Features.Upload
{
    internal class UploadHandler : IRequestHandler<UploadCommand, Result<Guid>>
    {
        public Task<Result<Guid>> Handle(UploadCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
