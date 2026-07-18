using BusinessLogic.Dtos;

namespace BusinessLogic
{
    public interface IGenresService
    {
        Task<IEnumerable<DGenre>> GetAllAsync(CancellationToken token = default);
        Task<DGenre?> GetByIdAsync(int id, CancellationToken token = default);
        Task<DGenre> AddAsync(DInputCreateGenre input, CancellationToken token = default);
        Task<DGenre?> UpdateAsync(int id, DInputUpdateGenre input, CancellationToken token = default);
        Task<bool> DeleteAsync(int id, CancellationToken token = default);
    }
}