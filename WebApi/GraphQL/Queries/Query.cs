using BusinessLogic;
using BusinessLogic.Dtos;

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