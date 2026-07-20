using Application.Dtos.Genre;
using AutoMapper;

namespace Application.MappingProfiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<DInputCreateGenre, Domain.Entities.Genre.Genre>();
        
        CreateMap<DInputUpdateGenre, Domain.Entities.Genre.Genre>();
        
        CreateMap<Domain.Entities.Genre.Genre, DOutputGenre>();
    }
}
