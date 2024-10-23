namespace Diving.Application.Abstarction.Messaging;

internal abstract class UnitOfWorkCommandHandler<T, R> : ICommandHandler<T, R> where T : ICommand<R>
{
    private readonly IUnitOfWork _unitOfWork;

    protected UnitOfWorkCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    protected abstract Task<R> HandleCommand(T command, CancellationToken cancellationToken);

    public async Task<R> Handle(T request, CancellationToken cancellationToken)
    {
        var result = await HandleCommand(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}

internal abstract class UnitOfWorkCommandHandler<T> : ICommandHandler<T> where T : ICommand
{
    private readonly IUnitOfWork _unitOfWork;

    protected UnitOfWorkCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public virtual async Task Handle(T request, CancellationToken cancellationToken)
    {
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
