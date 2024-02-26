using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyFinances.Common.Core.UnitOfWork;

public class UnitOfWork(DbContext databaseContext) : IUnitOfWork
{
    private IDbContextTransaction _transaction = null!;

    public async Task BeginTransactionAsync()
    {
        _transaction = await databaseContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await databaseContext.SaveChangesAsync();
        await _transaction.CommitAsync();
    }

    public Task Rollback()
    {
        return _transaction.RollbackAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _transaction.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}