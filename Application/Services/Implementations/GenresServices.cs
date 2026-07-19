using Application.Dtos.Genre;
using Application.Services.Interfaces;

namespace Application.Services.Implementations;

public class GenresServices : IGenresServices
{
    public Task<IEnumerable<DOutputGenre>> GetAllAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<DOutputGenre?> GetByIdAsync(int id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<DOutputGenre> AddAsync(DInputCreateGenre input, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<DOutputGenre?> UpdateAsync(int id, DInputUpdateGenre input, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}