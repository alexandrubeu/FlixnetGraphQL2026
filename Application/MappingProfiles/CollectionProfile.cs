using Application.Dtos.Collection;
using AutoMapper;
using Domain.Entities.Collection;

namespace Application.MappingProfiles;

public class  CollectionProfile : Profile
{
    public CollectionProfile()
    {
        CreateMap<DInputCreateCollection, Collection>()
            .ForMember(dest => dest.Movies, opt => opt.Ignore());

        CreateMap<DInputUpdateCollection, Collection>()
            .ForMember(dest => dest.Movies, opt => opt.Ignore());

        CreateMap<Collection, DOutputCollection>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Movies.Select(movie => movie.Id).ToList()));
    }
}
