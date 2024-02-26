using System.Linq.Expressions;

namespace MyFinances.Common.Core.Persistence.Repository;

public interface IBaseRepository<T> where T : class
{
    Task InsertOneAsync(T entity, CancellationToken cancellationToken);
    void UpdateOne(T entity);
    void DeleteOne(T entity);
    Task DeleteOneAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
    Task<bool> ExistsByAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);

    Task<T?> GetOneByAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken,
        bool track = false);

    Task SaveChangesAsync();
}