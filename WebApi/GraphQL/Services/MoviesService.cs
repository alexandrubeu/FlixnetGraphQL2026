using BusinessLogic;
using BusinessLogic.Dtos;

namespace WebApi.GraphQL.Services;

public class MoviesService : IMoviesService
{
    public DMovie? GetById(int id)=>
        Db.Movies.FirstOrDefault(m => m.Id == id);
    

    public Pagination<DMovieSummary> GetAll(string? term, PaginationParam paginationParam)
    {
        var query = Db.Movies;
        
        if (!string.IsNullOrWhiteSpace(term))
        {
            query = query.Where(m => 
                m.Title.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        var total = query.Count;
        
        var items = query
            .Skip(paginationParam.Page * paginationParam.PerPage)
            .Take(paginationParam.PerPage)
            .Select(m => new DMovieSummary(
                m.Id,
                m.Title,
                m.ImageUrl,
                m.Genres.Select(g => g.Name).ToList()))
            .ToList();

        return new Pagination<DMovieSummary>
        {
            TotalCount = total,
            Page = paginationParam.Page,
            PerPage = paginationParam.PerPage,
            Items = items
        };
    }
    

    public DMovie Add(DInputCreateMovie input)
    {
        var nextId = Db.Movies.Count != 0
            ? Db.Movies.Max(m => m.Id) + 1
            : 1;

        var createdAt = DateTime.UtcNow;
        var genres = Db.Genres
            .Where(g => input.GenreIds.Contains(g.Id))
            .ToList();

        var movie = new DMovie(
            nextId,
            input.TrailerUrl,
            input.Published,
            createdAt,
            genres)
        {
            Title = input.Title,
            ImageUrl = input.ImageUrl,
            VideoSource = input.VideoSource,
            // cast crew ..
        };

        Db.Movies.Add(movie);

        return movie;
    }
    

    public DMovie? Update(int id, DInputUpdateMovie input)
    {
        var movie = Db.Movies.FirstOrDefault(m => m.Id == id);

        if (movie is null)
            return null;

        movie.Title = input.Title;
        movie.ImageUrl = input.ImageUrl;
        movie.VideoSource = input.VideoSource;
        movie.TrailerUrl = input.TrailerUrl;
        movie.Published = input.Published;
        movie.Genres = Db.Genres
            .Where(g => input.GenreIds.Contains(g.Id))
            .ToList();
        // cast crew 

        return movie;
    }
    

    public bool Delete(int id)
    {
        var movie = Db.Movies.FirstOrDefault(m => m.Id == id);

        if (movie is null)
            return false;

        Db.Movies.Remove(movie);

        return true;
    }
}