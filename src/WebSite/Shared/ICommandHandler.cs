namespace WebSite.Shared
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task<Result> Handle(TCommand request, CancellationToken cancellationToken =  default);
    }
}
