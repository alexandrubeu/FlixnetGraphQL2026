using BusinessLogic.Dtos;

namespace BusinessLogic
{
    public interface IMoviesService
    {
        DMovie? GetById(int id);
        Pagination<DMovieSummary> GetAll(
            List<string>? genres,
            string? term,
            bool? published,
            MovieSortBy? sortBy,
            bool ascending,
            PaginationParam paginationParam);
        DMovie Add(DInputCreateMovie input);
        DMovie? Update(int id, DInputUpdateMovie input);
        bool Delete(int id);
    }
}