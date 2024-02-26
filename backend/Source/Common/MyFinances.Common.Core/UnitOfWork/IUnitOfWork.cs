namespace MyFinances.Common.Core.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task Rollback();
}