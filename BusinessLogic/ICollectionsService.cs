using BusinessLogic.Dtos;

namespace BusinessLogic
{
    public interface ICollectionsService
    {
        Task<DCollection?> GetByIdAsync(int id, CancellationToken token = default);
        Task<Pagination<DCollection>> GetAllAsync(PaginationParam paginationParam, CancellationToken token = default);
        Task<DCollection> AddAsync(DInputCreateCollection input, CancellationToken token = default);
        Task<DCollection?> UpdateAsync(int id, DInputUpdateCollection input, CancellationToken token = default);
        Task<bool> DeleteAsync(int id, CancellationToken token = default);
    }
}