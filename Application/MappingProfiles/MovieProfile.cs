using Application.Dtos.CastCrewCredit;
using Application.Dtos.Genre;
using Application.Dtos.Movie;
using AutoMapper;

namespace Application.MappingProfiles;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<DInputCreateMovie, Domain.Entities.Movie.Movie>()
            .ForMember(dest => dest.Genres, opt => opt.Ignore());
        
        CreateMap<DInputUpdateMovie, Domain.Entities.Movie.Movie>()
            .ForMember(dest => dest.Genres, opt => opt.Ignore());
        
        CreateMap<Domain.Entities.Movie.Movie, DOutputMovie>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genres.Select(g => new DInputCreateGenre { Name = g.Name }).ToList()));
    }
}
