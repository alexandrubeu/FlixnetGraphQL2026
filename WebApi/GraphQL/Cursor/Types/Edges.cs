namespace WebApi.GraphQL.Cursor.Types;

public class Edges<T>
{
    public string? Cursor { get; set; }
    
    public T Node { get; set; }
}