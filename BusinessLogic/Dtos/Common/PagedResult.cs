namespace BusinessLogic.Dtos;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new List<T>();

    public bool HasNextPage { get; set; }

    public bool HasPreviousPage { get; set; }

    public string? EndCursor { get; set; }
}