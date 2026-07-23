using BusinessLogic.Dtos;

namespace BusinessLogic
{
    public interface ICollectionsService
    {
        DCollection? GetById(int id);
        Pagination<DCollection> GetAll(PaginationParam paginationParam);
        DCollection Add(DInputCreateCollection input);
        DCollection? Update(int id, DInputUpdateCollection input);
        bool Delete(int id);
    }
}
