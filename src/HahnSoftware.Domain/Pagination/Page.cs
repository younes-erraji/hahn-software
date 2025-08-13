namespace HahnSoftware.Domain.Pagination;

public class Page<T> : List<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public long TotalElements { get; set; }

    public Page(List<T> items, long count, PaginationParam paginationParam)
    {
        PageSize = paginationParam.PageSize;
        PageNumber = paginationParam.PageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)paginationParam.PageSize);
        TotalElements = count;

        AddRange(items);
    }
}
