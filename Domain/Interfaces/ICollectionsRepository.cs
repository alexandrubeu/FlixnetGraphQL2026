using Domain.Entities.Collection;

namespace Domain.Interfaces;

public interface ICollectionsRepository
{
    Task<IEnumerable<Collection>> GetAllAsync(CancellationToken token = default);
    Task<Collection?> GetByIdAsync(int id, CancellationToken token = default);
    Task<Collection> AddAsync(Collection input, CancellationToken token = default);
    Task<Collection> UpdateAsync(int id, Collection input, CancellationToken token = default);
    Task<bool> DeleteAsync(int id, CancellationToken token = default);
}