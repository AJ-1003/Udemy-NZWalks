using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            // CreateMap<TSource, TDestination>
            CreateMap<Region, RegionDTO>()
                .ReverseMap();
            CreateMap<CreateRegionDTO, Region>()
                .ForMember(r => r.Id, options => options.Ignore())
                .ReverseMap();
            CreateMap<UpdateRegionDTO, Region>()
                .ForMember(r => r.Id, options => options.Ignore())
                .ReverseMap();
        }
    }
}
