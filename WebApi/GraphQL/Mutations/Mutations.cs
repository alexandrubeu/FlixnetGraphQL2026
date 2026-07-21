using BusinessLogic;
using BusinessLogic.Dtos;
using Entities;
using HotChocolate.Authorization;

namespace WebApi.GraphQL.Mutations;

public class Mutations
{
    [Authorize(Policy = Permissions.MoviesCreate)]
    public DMovie AddMovie(DInputCreateMovie input, [Service] IMoviesService moviesService) =>
        moviesService.Add(input);

    [Authorize(Policy = Permissions.MoviesUpdate)]
    public DMovie? UpdateMovie(
        int id,
        DInputUpdateMovie input,
        [Service] IMoviesService moviesService
    ) => moviesService.Update(id, input);

    [Authorize(Policy = Permissions.MoviesDelete)]
    public bool DeleteMovie(int id, [Service] IMoviesService moviesService) =>
        moviesService.Delete(id);

    [Authorize(Policy = Permissions.GenresCreate)]
    public DGenre AddGenre(DInputCreateGenre input, [Service] IGenresService genresService) =>
        genresService.Add(input);

    [Authorize(Policy = Permissions.GenresUpdate)]
    public DGenre? UpdateGenre(
        int id,
        DInputUpdateGenre input,
        [Service] IGenresService genresService
    ) => genresService.Update(id, input);

    [Authorize(Policy = Permissions.GenresDelete)]
    public bool DeleteGenre(int id, [Service] IGenresService genresService) =>
        genresService.Delete(id);

    [Authorize(Policy = Permissions.CollectionsCreate)]
    public DCollection AddCollection(
        DInputCreateCollection input,
        [Service] ICollectionsService collectionsService
    ) => collectionsService.Add(input);

    [Authorize(Policy = Permissions.CollectionsUpdate)]
    public DCollection? UpdateCollection(
        int id,
        DInputUpdateCollection input,
        [Service] ICollectionsService collectionsService
    ) => collectionsService.Update(id, input);

    [Authorize(Policy = Permissions.CollectionsDelete)]
    public bool DeleteCollection(int id, [Service] ICollectionsService collectionsService) =>
        collectionsService.Delete(id);
}
