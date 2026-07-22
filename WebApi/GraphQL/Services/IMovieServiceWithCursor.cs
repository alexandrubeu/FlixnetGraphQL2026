using BusinessLogic.Dtos;
using WebApi.GraphQL.Cursor.Types;

namespace WebApi.GraphQL.Services;

public interface IMovieServiceWithCursor
{
    public  Task<PagedResult<DMovie>> GetMoviesAsync(int pageSize, string? afterCursor);
    

}