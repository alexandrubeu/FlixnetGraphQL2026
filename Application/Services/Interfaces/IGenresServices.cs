using Application.Dtos.Genre;

namespace Application.Services.Interfaces;

public interface IGenresServices
{
    Task<IEnumerable<DOutputGenre>> GetAllAsync(CancellationToken token = default);
    Task<DOutputGenre?> GetByIdAsync(int id, CancellationToken token = default);
    Task<DOutputGenre> AddAsync(DInputCreateGenre input, CancellationToken token = default);
    Task<DOutputGenre?> UpdateAsync(int id, DInputUpdateGenre input, CancellationToken token = default);
    Task<bool> DeleteAsync(int id, CancellationToken token = default);
}