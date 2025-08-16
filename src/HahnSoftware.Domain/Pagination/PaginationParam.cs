namespace HahnSoftware.Domain.Pagination;

public class PaginationParam
{
    private const int MaxPageSize = 10;

    private int _pageSize = 10;
    private int _pageNumber = 1;

    public int PageSize
    {
        get { return _pageSize; }

        set { _pageSize = value > 0 && value < MaxPageSize ? value : MaxPageSize; }
    }

    public int PageNumber
    {
        get { return _pageNumber; }

        set { _pageNumber = value > 0 ? value : 1; }
    }
}
