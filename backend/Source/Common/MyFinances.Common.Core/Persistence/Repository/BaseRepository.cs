// using System.Linq.Expressions;
// using DeliveryGo.Shared.Core.Domain;
// using DeliveryGo.Shared.Core.RequestContext;
// using Microsoft.EntityFrameworkCore;
//
// namespace DeliveryGo.Shared.Core.Persistence.Repository;
//
// public abstract class BaseRepository<T>(
//     DbContext context,
//     IRequestContext requestContext,
//     TimeProvider timeProvider) : IBaseRepository<T>
//     where T : BaseEntity
// {
//     protected readonly DbSet<T> Entity = context.Set<T>();
//
//     public async Task InsertOneAsync(T entity, CancellationToken cancellationToken)
//     {
//         entity.SetCreated(requestContext.User, timeProvider.GetUtcNow().UtcDateTime);
//         await Entity.AddAsync(entity, cancellationToken);
//     }
//
//     public void UpdateOne(T entity)
//     {
//         entity.SetUpdated(requestContext.User, timeProvider.GetUtcNow().UtcDateTime);
//         Entity.Update(entity);
//     }
//
//     public void DeleteOne(T entity)
//     {
//         entity.SetDeleted(requestContext.User, timeProvider.GetUtcNow().UtcDateTime);
//         Entity.Remove(entity);
//     }
//
//     public async Task DeleteOneAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
//     {
//         await Entity.Where(expression).ExecuteDeleteAsync(cancellationToken);
//     }
//
//     public Task<bool> ExistsByAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
//     {
//         return Entity.AnyAsync(expression, cancellationToken);
//     }
//
//     public Task<T?> GetOneByAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken,
//         bool track = false)
//     {
//         return track
//             ? Entity.FirstOrDefaultAsync(expression, cancellationToken)
//             : Entity.AsNoTracking().FirstOrDefaultAsync(expression, cancellationToken);
//     }
//
//     public async Task SaveChangesAsync()
//     {
//         await context.SaveChangesAsync();
//     }
// }