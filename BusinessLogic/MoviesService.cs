using BusinessLogic.Dtos;
using Entities;
using Repository;

namespace BusinessLogic;

public class MoviesService : IMoviesService
{
    private readonly IMovieRepository _movieRepository;
    
    public MoviesService(IMovieRepository movieRepository) => _movieRepository = movieRepository;

    public DMovie? GetById(int id)
    {
        var movie = _movieRepository.GetById(id);
        return movie is null ? null : MapToDto(movie);
    }

    public Pagination<DMovieSummary> GetAll(string? term, PaginationParam paginationParam)
    {
        var query = _movieRepository.GetAll();
        
        if (!string.IsNullOrWhiteSpace(term))
        {
            query = query.Where(m => 
                m.Title.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        var total = query.Count();
        
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
        var movie = new EMovie
        {
            Title = input.Title,
            ImageUrl = input.ImageUrl,
            VideoSource = input.VideoSource,
            TrailerUrl = input.TrailerUrl,
            Published = input.Published,
            Genres = input.GenreIds.Select(id => new EGenre { Id = id }).ToList()
        };

        _movieRepository.Add(movie);
        return MapToDto(movie);
    }

    public DMovie? Update(int id, DInputUpdateMovie input)
    {
        var existing = _movieRepository.GetById(id);
        if (existing is null) return null;

        existing.Title = input.Title;
        existing.ImageUrl = input.ImageUrl;
        existing.VideoSource = input.VideoSource;
        existing.TrailerUrl = input.TrailerUrl;
        existing.Published = input.Published;
        existing.Genres = input.GenreIds.Select(gid => new EGenre { Id = gid }).ToList();
        
        _movieRepository.Update(existing);
        return MapToDto(existing);
    }

    public bool Delete(int id)
    {
        var existing = _movieRepository.GetById(id);
        if (existing is null) return false;

        _movieRepository.Delete(id);
        return true;
    }
    
    private static DMovie MapToDto(EMovie movie) => new()
    {
        Id = movie.Id,
        Title = movie.Title,
        ImageUrl = movie.ImageUrl,
        VideoSource = movie.VideoSource,
        TrailerUrl = movie.TrailerUrl,
        Published = movie.Published,
        Genres = movie.Genres.Select(g => new DGenre (g.Id, g.Name )).ToList(),
        CastAndCrew = movie.CastAndCrew.Select(c => new DCastCrewCredit
        {
            Id = c.Id,
            Name = c.Name,
            Role = c.Role,
            StageName = c.StageName
        }).ToList()
    };
}