using WebSite.Database;
using WebSite.Shared;
using WebSite.ValueObject;

namespace WebSite.Features.Store
{
    public class StoreQuery(ApplicationDbContext applicationDbContext) : IQueryCommandWithoudParams<StoreResponse>
    {
        public async Task<IQueryable<StoreResponse>> Handle(CancellationToken cancellationToken)
        {
            var stores = applicationDbContext.Stores
                .Select(store => new StoreResponse
                {
                    Id = store.Id,
                    Name = store.Name,
                    Owner = store.Owner,
                    Transactions = store.Transactions.Select(transaction => new TransactionsStoreResponse
                    {
                        Id = transaction.Id,
                        Amount = transaction.Amount,
                        Date = transaction.Date,
                        Card = transaction.Card,
                        Cpf = transaction.Cpf,
                        TypeDescription = TransactionType.FromCode(transaction.TypeCode).Description
                    })
                });

            return stores;
        }
    }
}


