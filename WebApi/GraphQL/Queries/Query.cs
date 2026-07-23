using BusinessLogic;
using BusinessLogic.Dtos;
using HotChocolate.Types.Pagination;
using WebApi.GraphQL.Cursor;
using WebApi.GraphQL.Cursor.Types;
using WebApi.GraphQL.Services;
using PageInfo = WebApi.GraphQL.Cursor.Types.PageInfo;

namespace WebApi.GraphQL.Queries;

public class Query
{
    public Pagination<DMovieSummary> GetMovies(
        string? term,
        PaginationParam pagination,
        [Service] IMoviesService moviesService)
        => moviesService.GetAll(term, pagination);

    public DMovie? GetMovie(
        int id,
        [Service] IMoviesService moviesService)
        => moviesService.GetById(id);

    public async Task<Cursor.Types.Connection<DMovie>> GetMoviesWithCursor(
        int pageSize,
        string? afterCursor,
        [Service] IMovieServiceWithCursor movieServiceWithCurosr)
    {
        var pagedResult = await movieServiceWithCurosr.GetMoviesAsync(pageSize, afterCursor);

        var edges = pagedResult.Items.Select(movie => new Edges<DMovie>
        {
            Node = movie,
            Cursor = CursorEncoder.Encoder(movie.CreatedAt, movie.Id)
        }).ToList();

        return new Cursor.Types.Connection<DMovie>()
        {
            EdgesList = edges,
            PageInfo = new PageInfo
            {
                HasNextPage = pagedResult.HasNextPage,
                HasPreviousPage = pagedResult.HasPreviousPage,
                StartCursor = edges.FirstOrDefault()?.Cursor,
                EndCursor = edges.LastOrDefault()?.Cursor
            }
        };
    }
        
    
    public IEnumerable<DGenre> GetGenres(
        [Service] IGenresService genresService) 
        => genresService.GetAll();

    public DGenre? GetGenre(
        int id,
        [Service] IGenresService genresService) 
        => genresService.GetById(id);

    public Pagination<DCollection> GetCollections(
        PaginationParam pagination,
        [Service] ICollectionsService collectionsService)
        => collectionsService.GetAll(pagination);
    
    public  DCollection? GetCollection(
        int id,
        [Service] ICollectionsService collectionsService) 
        => collectionsService.GetById(id);
}