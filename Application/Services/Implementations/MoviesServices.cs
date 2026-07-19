using Application.Dtos.Movie;
using Application.Services.Common.Pagination;
using Application.Services.Interfaces;
using Domain.Interfaces;

namespace Application.Services.Implementations;

// public class MoviesServices(IMoviesRepository _repo) : IMoviesServices
// {
//     public Task<Pagination<DOutputMovieSummary>> GetAllAsync(string? terms, PaginationParams paginationParams, CancellationToken ct = default)
//         => _repo.GetAllAsync(terms);
//
//     public Task<DOutputMovie?> GetByIdAsync(int id, CancellationToken ct = default)
//         => _repo.GetByIdAsync(id);
//
//     public Task<DOutputMovie> AddAsync(DInputCreateMovie input, CancellationToken ct = default)
//         => _repo.AddAsync(input);
//
//     public Task<DOutputMovie?> UpdateAsync(int id, DInputCreateMovie input, CancellationToken ct = default)
//         => _repo.UpdateAsync(id, input);
//
//     public Task<bool> DeleteAsync(int id, CancellationToken ct = default)
//         => _repo.DeleteAsync(id);
// }