using Diving.Infrastructure;

namespace Diving.Application;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DivingContext _dbContext;

    public UnitOfWork(DivingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
