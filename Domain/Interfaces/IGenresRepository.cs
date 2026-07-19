using Domain.Entities.Genre;

namespace Domain.Interfaces;

public interface IGenresRepository
{
    Task<IEnumerable<Genre>> GetAllAsync(CancellationToken token = default);
    Task<Genre?> GetByIdAsync(int id, CancellationToken token = default);
    Task<Genre> AddAsync(Genre input, CancellationToken token = default);
    Task<Genre?> UpdateAsync(int id, Genre input, CancellationToken token = default);
    Task<bool> DeleteAsync(int id, CancellationToken token = default);
}