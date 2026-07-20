using BusinessLogic;
using BusinessLogic.Dtos;

namespace WebApi.GraphQL.Services;

public class GenresService : IGenresService
{
    public IEnumerable<DGenre> GetAll() => 
        Db.Genres;
    

    public DGenre? GetById(int id) => 
        Db.Genres.FirstOrDefault(g => g.Id == id);
    

    public DGenre Add(DInputCreateGenre input)
    {
        var nextId = Db.Genres.Count != 0
            ? Db.Genres.Max(g => g.Id) + 1
            : 1;

        var genre = new DGenre(nextId, input.Name);

        Db.Genres.Add(genre);

        return genre;
    }

    public DGenre? Update(int id, DInputUpdateGenre input)
    {
        var genre = Db.Genres.FirstOrDefault(g => g.Id == id);

        if (genre is null)
            return null;

        genre.Name = input.Name;

        return genre;
    }

    public bool Delete(int id)
    {
        var genre = Db.Genres.FirstOrDefault(g => g.Id == id);

        if (genre is null)
            return false;

        Db.Genres.Remove(genre);

        return true;
    }
}