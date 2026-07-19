using Common.Entities;

namespace Data.Interfaces;

public interface IMoviesRepository
{
    IEnumerable<EMovie> GetAll();
}
