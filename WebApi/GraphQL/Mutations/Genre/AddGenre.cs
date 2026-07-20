using Application.Dtos.Genre;
using AutoMapper;
using Infrastructure.Persistence.Context;

namespace WebApi.GraphQL.Mutations.Genre;

[MutationType]
public class AddGenre
{
    public async Task<DOutputGenre> AddGenreAsync(
        DInputCreateGenre genre,
        [Service] AppDbContext context,
        [Service] IMapper mapper,
        CancellationToken ct)
    {
        var entity = mapper.Map<Domain.Entities.Genre.Genre>(genre);
        
        context.Genres.Add(entity);
        await context.SaveChangesAsync(ct);

        return mapper.Map<DOutputGenre>(entity);
    }
}