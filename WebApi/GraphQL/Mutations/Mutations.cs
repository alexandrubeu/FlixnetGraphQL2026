using BusinessLogic;
using BusinessLogic.Dtos;

namespace WebApi.GraphQL.Mutations;

public class Mutations
{
    public DMovie AddMovie(
        DInputCreateMovie input,
        [Service] IMoviesService moviesService)
        => moviesService.Add(input);

    public DMovie? UpdateMovie(
        int id,
        DInputUpdateMovie input,
        [Service] IMoviesService moviesService)
        => moviesService.Update(id, input);

    public bool DeleteMovie(
        int id,
        [Service] IMoviesService moviesService)
        => moviesService.Delete(id);


    public DGenre AddGenre(
        DInputCreateGenre input,
        [Service] IGenresService genresService)
        => genresService.Add(input);

    public DGenre? UpdateGenre(
        int id,
        DInputUpdateGenre input,
        [Service] IGenresService genresService)
        =>  genresService.Update(id, input);

    public bool DeleteGenre(
        int id,
        [Service] IGenresService genresService)
        => genresService.Delete(id);

    public DCollection AddCollection(
        DInputCreateCollection input,
        [Service] ICollectionsService collectionsService) 
        => collectionsService.Add(input);

    public DCollection? UpdateCollection(
        int id,
        DInputUpdateCollection input,
        [Service] ICollectionsService collectionsService) 
    => collectionsService.Update(id, input);

    public bool DeleteCollection(
        int id,
        [Service] ICollectionsService collectionsService)
        => collectionsService.Delete(id);
}