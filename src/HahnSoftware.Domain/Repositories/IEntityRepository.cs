using HahnSoftware.Domain.Entities.Primitives;
using HahnSoftware.Domain.Pagination;

using System.Linq.Expressions;

namespace HahnSoftware.Domain.Interfaces;

public interface IEntityRepository<T> where T : class, IEntity
{
    Task<bool> Exists(Guid id, CancellationToken cancellationToken = default);
    Task<bool> Exists(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    IQueryable<T> Query();
    IQueryable<T> Query(Expression<Func<T, bool>> expression);
    Task<Page<T>> Query(PaginationParam pagination, Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<Page<T>> Paginate(PaginationParam pagination, IQueryable<T> query, CancellationToken cancellationToken = default);
    Task<Page<T>> Paginate(PaginationParam pagination, CancellationToken cancellationToken = default);
    Task<List<T>> Get(CancellationToken cancellationToken = default);
    Task<T> Get(Guid id, CancellationToken cancellationToken = default);
    Task Create(T entity, CancellationToken cancellationToken = default);
    Task Create(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task<bool> SaveChanges(CancellationToken cancellationToken = default);
    Task Update(T entity, CancellationToken cancellationToken = default);
    Task Update(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task Delete(Guid entityId, CancellationToken cancellationToken = default);
    Task Delete(T entity, CancellationToken cancellationToken = default);
    Task Delete(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}
