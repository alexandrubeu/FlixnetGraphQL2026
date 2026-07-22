namespace WebApi.GraphQL.Cursor;

public class CursorEncoder
{
    public static string Encoder(DateTime date, int id)
    {
        var rawcursor = $"{date:0}_{id}";
        var bytes = System.Text.Encoding.UTF8.GetBytes(rawcursor);
        return Convert.ToBase64String(bytes);
    }

    public static (DateTime date, int id)? Decoder(string? cursor)
    {
        if (string.IsNullOrEmpty(cursor))
        {
            return null;
        }
        var bytes = Convert.FromBase64String(cursor);
        var decoded = System.Text.Encoding.UTF8.GetString(bytes);
        var parts = decoded.Split('_');
        if (parts.Length == 2 &&
            DateTime.TryParse(parts[0], out var date) &&
            int.TryParse(parts[1], out var id)
           )
        {
            return (date, id);
        }
        return null;
    }
}