using BusinessLogic.Dtos;

namespace BusinessLogic
{
    public interface IMoviesService
    {
        DMovie? GetById(int id);
        Pagination<DMovieSummary> GetAll(string? term, PaginationParam paginationParam);
        DMovie Add(DInputCreateMovie input);
        DMovie? Update(int id, DInputUpdateMovie input);
        bool Delete(int id);
    }
}