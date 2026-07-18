using BusinessLogic.Dtos;

namespace BusinessLogic
{
    public interface IMoviesService
    {
        Task<DMovie?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Pagination<DMovieSummary>> GetAllAsync(string? term, PaginationParam paginationParam, CancellationToken ct = default);
        Task<DMovie> AddAsync(DInputCreateMovie input, CancellationToken ct = default);
        Task<DMovie?> UpdateAsync(int id, DInputUpdateMovie input, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}