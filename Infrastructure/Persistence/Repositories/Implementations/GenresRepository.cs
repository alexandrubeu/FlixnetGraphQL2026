using Domain.Entities.Genre;
using Domain.Interfaces;

namespace Infrastructure.Persistence.Repositories.Implementations;

public class GenresRepository : IGenresRepository
{
    public Task<IEnumerable<Genre>> GetAllAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Genre?> GetByIdAsync(int id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Genre> AddAsync(Genre input, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Genre?> UpdateAsync(int id, Genre input, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}