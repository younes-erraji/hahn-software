using HahnSoftware.Domain.Pagination;

using Microsoft.EntityFrameworkCore;

namespace HahnSoftware.Infrastructure.Persistence.Pagination;

public class Pagination<T>
{
    public static async Task<Page<T>> PaginateAsync(IQueryable<T> source, PaginationParam paginationParam, CancellationToken cancellationToken = default)
    {
        List<T> content;
        if (paginationParam.PageSize > 0)
        {
            content = await source.Skip((paginationParam.PageNumber - 1) * paginationParam.PageSize).Take(paginationParam.PageSize).ToListAsync(cancellationToken);
        }
        else
        {
            content = await source.ToListAsync(cancellationToken);
        }

        int count;
        try
        {
            count = await source.CountAsync(cancellationToken);
        }
        catch
        {
            count = 0;
        }

        return new Page<T>(content, count, paginationParam);
    }

    public static Page<T> Paginate(IQueryable<T> source, PaginationParam paginationParam)
    {
        int count = source.Count();
        List<T> content = source.Skip((paginationParam.PageNumber - 1) * paginationParam.PageSize).Take(paginationParam.PageSize).ToList();

        return new Page<T>(content, count, paginationParam);
    }

    public static async Task<Page<T>> PaginateAsync(IQueryable<T> source, long count, PaginationParam paginationParam)
    {
        List<T> content;
        if (paginationParam.PageSize > 0)
        {
            content = await source.Skip((paginationParam.PageNumber - 1) * paginationParam.PageSize).Take(paginationParam.PageSize).ToListAsync();
        }
        else
        {
            content = await source.ToListAsync();
        }

        return new Page<T>(content, count, paginationParam);
    }

    public static Page<T> Paginate(IQueryable<T> source, long count, PaginationParam paginationParam)
    {
        List<T> content = source.Skip((paginationParam.PageNumber - 1) * paginationParam.PageSize).Take(paginationParam.PageSize).ToList();

        return new Page<T>(content, count, paginationParam);
    }
}
