using WebSite.Database;
using WebSite.Entities;
using WebSite.Shared;

namespace WebSite.Features.Upload
{
    public class UploadHandler(ApplicationDbContext applicationDbContext, UploadValidator validtor, ILogger<UploadHandler> logger) : ICommandHandler<UploadCommand>
    {
        public async Task<Result> Handle(UploadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = validtor.Validate(request);

                if (!result.IsValid)
                {
                    return Result.Failure(new Error("ValidationError", string.Join("; ", result.Errors.Select(e => e.ErrorMessage))));
                }

                var store = new Entities.Store(request.StoreName, request.StoreOwner);
                store.Transactions.Add(new TransactionStore
                {
                    Amount = request.TransactionType.Positive ? request.Value : -request.Value,
                    Card = request.Card,
                    Cpf = request.Cpf,
                    Date = request.Date,
                    StoreId = store.Id,
                    TypeCode = request.TypeCode
                });

                applicationDbContext.Stores.Add(store);
                await applicationDbContext.SaveChangesAsync(cancellationToken);
                return Result.Success();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing upload command");
                return Result.Failure(new Error("UploadError", "An error occurred while processing the upload."));
            }
        }
    }
}
