using Application.Dtos.Genre;
using AutoMapper;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace WebApi.GraphQL.Mutations.Genre;

[MutationType]
public class UpdateGenre
{
    public async Task<DOutputGenre> UpdateGenreAsync(
        int id,
        DInputUpdateGenre genre,
        [Service] AppDbContext context,
        [Service] IMapper mapper,
        CancellationToken ct)
    {
        var entity = await context.Genres.FirstOrDefaultAsync(g => g.Id == id, ct)
            ?? throw new KeyNotFoundException($"Genre with id {id} not found");

        mapper.Map(genre, entity);
        context.Genres.Update(entity);
        await context.SaveChangesAsync(ct);

        return mapper.Map<DOutputGenre>(entity);
    }
}