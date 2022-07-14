using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile()
        {
            // CreateMap<TSource, TDestination>
            CreateMap<Walk, WalkDTO>()
                .ForMember(wdto => wdto.RegionDTO, options => options.MapFrom(w => w.Region))
                .ForMember(wdto => wdto.WalkDifficultyDTO, options => options.MapFrom(w => w.WalkDifficulty))
                .ReverseMap();
            CreateMap<WalkDifficulty, WalkDifficultyDTO>()
                .ReverseMap();
            CreateMap<CreateWalkDTO, Walk>()
                .ForMember(w => w.Id, options => options.Ignore())
                .ReverseMap();
            CreateMap<UpdateWalkDTO, Walk>()
                .ForMember(w => w.Id, options => options.Ignore())
                .ReverseMap();
        }
    }
}
