namespace Api.DTO;

public class PaginationResult<T> where T : class
{
    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int TotalItems { get; set; }
}