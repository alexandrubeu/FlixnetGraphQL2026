using Application.Dtos.Collection;
using Application.Services.Common.Pagination;

namespace Application.Services.Interfaces;

public interface ICollectionsServices
{
    Task<DOutputCollection?> GetByIdAsync(int id, CancellationToken token = default);
    Task<Pagination<DOutputCollection>> GetAllAsync(PaginationParams paginationParams, CancellationToken token = default);
    Task<DOutputCollection> AddAsync(DInputCreateCollection input, CancellationToken token = default);
    Task<DOutputCollection?> UpdateAsync(int id, DInputUpdateCollection input, CancellationToken token = default);
    Task<bool> DeleteAsync(int id, CancellationToken token = default);
}