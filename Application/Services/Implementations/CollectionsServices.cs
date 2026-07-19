using Application.Dtos.Collection;
using Application.Services.Common.Pagination;
using Application.Services.Interfaces;

namespace Application.Services.Implementations;

public class CollectionsServices : ICollectionsServices
{
    public Task<Pagination<DOutputCollection>> GetAllAsync(PaginationParams paginationParams, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<DOutputCollection?> GetByIdAsync(int id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<DOutputCollection> AddAsync(DInputCreateCollection input, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<DOutputCollection?> UpdateAsync(int id, DInputUpdateCollection input, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}