using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Linq.Expressions;

using HahnSoftware.Domain.Pagination;
using HahnSoftware.Domain.Entities.Primitives;
using HahnSoftware.Infrastructure.Persistence.Pagination;
using HahnSoftware.Domain.Interfaces.Repositories;

namespace HahnSoftware.Infrastructure.Persistence.Repositories;

public abstract class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity
{
    private readonly HahnSoftwareDbContext _context;

    public EntityRepository(HahnSoftwareDbContext context)
    {
        _context = context;
    }

    public Task Update(T entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Detached;
        EntityEntry<T> model = _context.Entry(entity);
        model.State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task Update(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().UpdateRange(entities);
        return Task.CompletedTask;
    }

    public Task Delete(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task Delete(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().RemoveRange(entities);
        return Task.CompletedTask;
    }

    public async Task Delete(Guid entityId, CancellationToken cancellationToken = default)
    {
        await Query(x => x.Id == entityId).ExecuteUpdateAsync(x => x.SetProperty(z => z.DeletionDate, DateTimeOffset.Now), cancellationToken);
    }

    public async Task Create(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task Create(IEnumerable<T> entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddRangeAsync(entity, cancellationToken);
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
    {
        int changes = await _context.SaveChangesAsync(cancellationToken);
        await _context.DisposeAsync();
        return changes > 0;
    }

    public virtual async Task<T> Get(Guid id, CancellationToken cancellationToken = default)
    {
        T? entity = await Query(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        ArgumentNullException.ThrowIfNull(entity);
        return entity;
    }

    public IQueryable<T> Query()
    {
        return _context.Set<T>().AsNoTrackingWithIdentityResolution();
    }

    public IQueryable<T> Query(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>()
            .Where(x => x.DeletionDate == null)
            .Where(expression)
            .AsNoTrackingWithIdentityResolution();
    }

    public Task<Page<T>> Paginate(PaginationParam pagination, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = Query();

        return Pagination<T>.PaginateAsync(query, pagination, cancellationToken);
    }

    public Task<Page<T>> Query(PaginationParam pagination, Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = Query(expression);
        return Pagination<T>.PaginateAsync(query, pagination, cancellationToken);
    }

    public Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        return Query(x => x.Id == id).AnyAsync(cancellationToken);
    }

    public Task<bool> Exists(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Query(expression).AnyAsync(cancellationToken);
    }

    public Task<List<T>> Get(CancellationToken cancellationToken = default)
    {
        return Query().ToListAsync(cancellationToken);
    }

    public Task<Page<T>> Paginate(PaginationParam pagination, IQueryable<T> query, CancellationToken cancellationToken = default)
    {
        return Pagination<T>.PaginateAsync(query, pagination, cancellationToken);
    }
}
