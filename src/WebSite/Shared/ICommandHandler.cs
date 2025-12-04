namespace WebSite.Shared
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task<Result> HandleAsync(TCommand request, CancellationToken cancellationToken =  default);
    }
}
