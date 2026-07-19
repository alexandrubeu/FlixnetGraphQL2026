using Application.Dtos.Movie;
using Application.Services.Common.Pagination;

namespace Application.Services.Interfaces;

public interface IMoviesServices
{
    Task<Pagination<DOutputMovieSummary>> GetAllAsync(string? term, PaginationParams paginationParams, CancellationToken ct = default);
    Task<DOutputMovie?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<DOutputMovie> AddAsync(DInputCreateMovie input, CancellationToken ct = default);
    Task<DOutputMovie?> UpdateAsync(int id, DInputUpdateMovie input, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}