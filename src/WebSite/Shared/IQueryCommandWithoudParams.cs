namespace WebSite.Shared
{
    public interface IQueryCommandWithoudParams<TResultT> where TResultT : class
    {
        Task<IQueryable<TResultT>> HandleAsync(CancellationToken cancellationToken = default);
    }
}
