using BusinessLogic;
using BusinessLogic.Dtos;
using WebApi.GraphQL.Services;

namespace WebApi.GraphQL.Queries;

public class Query
{
    // public Pagination<DMovieSummary> GetMovies(
    //     string? term,
    //     PaginationParam pagination,
    //     [Service] IMoviesService moviesService)
    //     => moviesService.GetAll(term, pagination);
    
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
    
    public DMovie? GetMovie(
        int id,
        [Service] IMoviesService moviesService)
        => moviesService.GetById(id);
    
    public Task<PagedResult<DMovie>> GetMoviesWithCursor(
        int pageSize, 
        string? afterCursor,
        [Service] IMovieServiceWithCursor movieServiceWithCurosr)
        => movieServiceWithCurosr.GetMoviesAsync(pageSize, afterCursor);
    
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