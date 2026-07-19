using BusinessLogic.Dtos.Genres;

namespace BusinessLogic.Services;

public interface IGenresService
{
    IEnumerable<DGenre> GetAll();
    DGenre? GetById(int id);
    DGenre Add(DInputCreateGenre input);
    DGenre? Update(int id, DInputUpdateGenre input);
    bool Delete(int id);
}
