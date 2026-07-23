using BusinessLogic.Dtos;
using Entities;
using Repository;

namespace BusinessLogic;

public enum MovieSortBy
{
    Title,
    Published
}
public class MoviesService : IMoviesService
{
    private readonly IMovieRepository _movieRepository;

    public MoviesService(IMovieRepository movieRepository) => _movieRepository = movieRepository;

    public DMovie? GetById(int id)
    {
        var movie = _movieRepository.GetById(id);
        return movie is null ? null : MapToDto(movie);
    }

    public Pagination<DMovieSummary> GetAll(
        List<string>? genres,
        string? term,
        bool? published,
        MovieSortBy? sortBy,
        bool ascending,
        PaginationParam paginationParam)
    {
        var query = _movieRepository.GetAll();

        if (!string.IsNullOrWhiteSpace(term))
        {
            query = query.Where(m => m.Title.Contains(term));
        }

        if (published.HasValue)
        {
            query = query.Where(m => m.Published == published.Value);
        }

        if (genres is not null && genres.Count > 0)
        {
            /// <summary>
            /// aici as fi scris : query = query.Where(m => m.Genres.Any(g => genres.Contains(g.Name)));
            /// doar ca aparent pachetul mysql adaugat nu poate
            /// traduce bine in sql si am gasit o solutie manuala
            /// ca sa pot pastra tipul de date la IQueryable
            /// </summary>
            
            // creez empty query unde pt fiecare genre se modifica
            var filtered = query.Where(m => false);

            foreach (var genre in genres)
            {
                var currentGenre = genre;
                
                // folosesc union pentru a elimina duplicatele
                // in cazul in care un film are mai multe genuri sa nu fie adaugat de mai multe ori
                filtered = filtered.Union(
                    query.Where(m => m.Genres.Any(g => g.Name == currentGenre)));
            }

            query = filtered;
        }

        switch (sortBy)
        {
            case MovieSortBy.Title:
                query = ascending
                    ? query.OrderBy(m => m.Title)
                    : query.OrderByDescending(m => m.Title);
                break;
            case MovieSortBy.Published:
                query = ascending
                    ? query.OrderBy(m => m.Published)
                    : query.OrderByDescending(m => m.Published);
                break;
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
            Genres = input.GenreIds.Select(id => new EGenre { Id = id }).ToList(),
            CastAndCrew = input.CastAndCrew.Select(c => new ECastCrewCredit
            {
                Name = c.Name,
                Role = c.Role,
                StageName = c.StageName
            }).ToList()
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
        existing.CastAndCrew = input.CastAndCrew.Select(c => new ECastCrewCredit
        {
            Name = c.Name,
            Role = c.Role,
            StageName = c.StageName
        }).ToList();

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
        Genres = movie.Genres.Select(g => new DGenre(g.Id, g.Name)).ToList(),
        CastAndCrew = movie.CastAndCrew.Select(c => new DCastCrewCredit
        {
            Id = c.Id,
            Name = c.Name,
            Role = c.Role,
            StageName = c.StageName
        }).ToList()
    };
}