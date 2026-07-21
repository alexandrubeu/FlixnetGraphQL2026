namespace WebApi.GraphQL.Cursor.Types;

public class Connection<T>
{
    public List<Edges<T>> EdgesList { get; set; } = new();

    public PageInfo PageInfo { get; set; } = new();
}