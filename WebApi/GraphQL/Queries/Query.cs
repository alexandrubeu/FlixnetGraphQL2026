using BusinessLogic;
using BusinessLogic.Dtos;
using HotChocolate.Types.Pagination;
using WebApi.GraphQL.Cursor;
using WebApi.GraphQL.Cursor.Types;
using Entities;
using HotChocolate.Authorization;
using WebApi.GraphQL.Services;
using PageInfo = WebApi.GraphQL.Cursor.Types.PageInfo;

namespace WebApi.GraphQL.Queries;

public class Query
{
    // public Pagination<DMovieSummary> GetMovies(
    //     string? term,
    //     PaginationParam pagination,
    //     [Service] IMoviesService moviesService)
    //     => moviesService.GetAll(term, pagination);
    
    [Authorize(Policy = Permissions.MoviesRead)]
    public Pagination<DMovieSummary> GetMovies(
        string? term,
        bool? published,
        List<string>? genres,
        MovieSortBy? sortBy,
        PaginationParam pagination,
        [Service] IMoviesService moviesService,
        bool ascending = true)
    {
        return moviesService.GetAll(
            genres,
            term,
            published,
            sortBy,
            ascending,
            pagination);
    }
    
    [Authorize(Policy = Permissions.MoviesRead)]
    public DMovie? GetMovie(
        int id,
        [Service] IMoviesService moviesService)
        => moviesService.GetById(id);

    
    [Authorize(Policy = Permissions.MoviesRead)]
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
        
    [Authorize(Policy = Permissions.GenresRead)]
    public IEnumerable<DGenre> GetGenres(
        string? name,
        [Service] IGenresService genresService,
        bool ascending = true) 
        => genresService.GetAll(name, ascending);

    // [UseFiltering]
    // [UseSorting]
    // public IQueryable<DCollection> GetGenres(
    //     [Service] IGenresService genresService)
    //     => genresService.GetAll();
    
    [Authorize(Policy = Permissions.GenresRead)]
    public DGenre? GetGenre(
        int id,
        [Service] IGenresService genresService) 
        => genresService.GetById(id);
    
    [Authorize(Policy = Permissions.CollectionsRead)]
    public Pagination<DCollection> GetCollections(
        PaginationParam pagination,
        [Service] ICollectionsService collectionsService
    ) => collectionsService.GetAll(pagination);

    [Authorize(Policy = Permissions.CollectionsRead)]
    public DCollection? GetCollection(int id, [Service] ICollectionsService collectionsService) =>
        collectionsService.GetById(id);
}
