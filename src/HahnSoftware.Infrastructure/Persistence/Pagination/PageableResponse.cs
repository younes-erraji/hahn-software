using HahnSoftware.Domain.Pagination;

namespace HahnSoftware.Infrastructure.Persistence.Pagination;

public class PageableResponse<T>
{
    public PaginationModel Pageable { get; set; }
    public IEnumerable<T> Content { get; set; }

    public static PaginationModel GetPageable(Page<T> page)
    {
        return new PaginationModel
        {
            PageSize = page.PageSize,
            PageNumber = page.PageNumber,
            TotalElements = page.TotalElements,
            TotalPages = page.TotalPages
        };
    }

    public static PageableResponse<T> Get(Page<T> page)
    {
        return new PageableResponse<T>
        {
            Pageable = GetPageable(page),
            Content = page
        };
    }
}

public class PageableResponse<TE, TC>
{
    public PaginationModel Pageable { get; set; }
    public IEnumerable<TC> Content { get; set; }

    public static PageableResponse<TE, TC> Get(Page<TE> page, IEnumerable<TC> content)
    {
        return new PageableResponse<TE, TC>
        {
            Pageable = PageableResponse<TE>.GetPageable(page),
            Content = content
        };
    }
}