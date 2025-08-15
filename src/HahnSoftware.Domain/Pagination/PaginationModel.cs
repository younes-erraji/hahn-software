namespace HahnSoftware.Domain.Pagination;

public class PaginationModel
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public long TotalElements { get; set; }
    public int TotalPages { get; set; }
}
