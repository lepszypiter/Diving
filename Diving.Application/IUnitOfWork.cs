namespace Diving.Application;

public interface IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return SaveChangesAsync(cancellationToken);
    }
}
