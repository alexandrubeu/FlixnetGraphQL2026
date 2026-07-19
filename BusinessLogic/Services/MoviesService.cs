using BusinessLogic.Dtos.Movies;
using Common;
using Data.Interfaces;

namespace BusinessLogic.Services;

public class MoviesService(IMoviesRepository repo) : IMoviesService
{
    public DMovie? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public DMovie Add(DInputCreateMovie input)
    {
        throw new NotImplementedException();
    }

    public DMovie? Update(int id, DInputUpdateMovie input)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Pagination<DMovie> GetAll(string? term, PaginationParam paginationParam)
    {
        var entities = repo.GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(term))
            entities = entities.Where(p => p.Title.ToLower().Contains(term.ToLower()));

        var totalCount = entities.Count();
        var items = entities
            .Skip(paginationParam.Page * paginationParam.PerPage)
            .Take(paginationParam.PerPage)
            .ToList();
        return new Pagination<DMovie>
        {
            TotalCount = totalCount,
            Page = paginationParam.Page,
            PerPage = paginationParam.PerPage,
            // Items = items.Select(MovieMapper.toDto).ToList(),
            Items = new List<DMovie>(),
        };
    }
}
