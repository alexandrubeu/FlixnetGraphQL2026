using Domain.Entities.Collection;
using Domain.Interfaces;

namespace Infrastructure.Persistence.Repositories.Implementations;

public class CollectionsRepository : ICollectionsRepository
{
    public Task<IEnumerable<Collection>> GetAllAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Collection?> GetByIdAsync(int id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Collection> AddAsync(Collection input, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Collection> UpdateAsync(int id, Collection input, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}