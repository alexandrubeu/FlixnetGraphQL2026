using BusinessLogic;
using BusinessLogic.Dtos;

namespace WebApi.GraphQL.Services;

public class CollectionsService : ICollectionsService
{
    public DCollection? GetById(int id)
    {
        return Db.Collections.FirstOrDefault(c => c.Id == id);
    }

    public Pagination<DCollection> GetAll(PaginationParam paginationParam)
    {
        var totalCount = Db.Collections.Count;

        var items = Db
            .Collections.Skip(paginationParam.Page * paginationParam.PerPage)
            .Take(paginationParam.PerPage)
            .ToList();

        return new Pagination<DCollection>
        {
            TotalCount = totalCount,
            Page = paginationParam.Page,
            PerPage = paginationParam.PerPage,
            Items = items,
        };
    }

    public DCollection Add(DInputCreateCollection input)
    {
        var nextId = Db.Collections.Count != 0 ? Db.Collections.Max(c => c.Id) + 1 : 1;

        var collection = new DCollection(nextId, input.Published)
        {
            Name = input.Name,
            Movies = Db
                .Movies.Where(m => input.MovieIds.Contains(m.Id))
                .Select(m => new DMovieSummary(
                    m.Id,
                    m.Title,
                    m.ImageUrl,
                    m.Genres.Select(g => g.Name).ToList()
                ))
                .ToList(),
        };

        Db.Collections.Add(collection);

        return collection;
    }

    public DCollection? Update(int id, DInputUpdateCollection input)
    {
        var collection = Db.Collections.FirstOrDefault(c => c.Id == id);

        if (collection is null)
            return null;

        collection.Name = input.Name;
        collection.Published = input.Published;
        collection.Movies = Db
            .Movies.Where(m => input.MovieIds.Contains(m.Id))
            .Select(m => new DMovieSummary(
                m.Id,
                m.Title,
                m.ImageUrl,
                m.Genres.Select(g => g.Name).ToList()
            ))
            .ToList();

        return collection;
    }

    public bool Delete(int id)
    {
        var collection = Db.Collections.FirstOrDefault(c => c.Id == id);

        if (collection is null)
            return false;

        Db.Collections.Remove(collection);

        return true;
    }
}
