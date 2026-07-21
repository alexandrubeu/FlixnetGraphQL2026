using BusinessLogic;
using BusinessLogic.Dtos;
using Entities;
using HotChocolate.Authorization;
using WebApi.GraphQL.Services;

namespace WebApi.GraphQL.Queries;

public class Query
{
    [Authorize(Policy = Permissions.MoviesRead)]
    public Pagination<DMovieSummary> GetMovies(
        string? term,
        PaginationParam pagination,
        [Service] IMoviesService moviesService
    ) => moviesService.GetAll(term, pagination);

    [Authorize(Policy = Permissions.MoviesRead)]
    public DMovie? GetMovie(int id, [Service] IMoviesService moviesService) =>
        moviesService.GetById(id);

    [Authorize(Policy = Permissions.MoviesRead)]
    public Task<PagedResult<DMovie>> GetMoviesWithCursor(
        int pageSize,
        string? afterCursor,
        [Service] IMovieServiceWithCursor movieServiceWithCurosr
    ) => movieServiceWithCurosr.GetMoviesAsync(pageSize, afterCursor);

    [Authorize(Policy = Permissions.GenresRead)]
    public IEnumerable<DGenre> GetGenres([Service] IGenresService genresService) =>
        genresService.GetAll();

    [Authorize(Policy = Permissions.GenresRead)]
    public DGenre? GetGenre(int id, [Service] IGenresService genresService) =>
        genresService.GetById(id);

    [Authorize(Policy = Permissions.CollectionsRead)]
    public Pagination<DCollection> GetCollections(
        PaginationParam pagination,
        [Service] ICollectionsService collectionsService
    ) => collectionsService.GetAll(pagination);

    [Authorize(Policy = Permissions.CollectionsRead)]
    public DCollection? GetCollection(int id, [Service] ICollectionsService collectionsService) =>
        collectionsService.GetById(id);
}
