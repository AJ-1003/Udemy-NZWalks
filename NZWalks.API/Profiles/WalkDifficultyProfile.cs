using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class WalkDifficultyProfile : Profile
    {
        public WalkDifficultyProfile()
        {
            // CreateMap<TSource, TDestination>
            CreateMap<WalkDifficulty, WalkDifficultyDTO>()
                .ReverseMap();
            CreateMap<CreateWalkDifficultyDTO, WalkDifficulty>()
                .ForMember(wd => wd.Id, options => options.Ignore())
                .ReverseMap();
            CreateMap<UpdateWalkDifficultyDTO, WalkDifficulty>()
                .ForMember(wd => wd.Id, options => options.Ignore())
                .ReverseMap();
        }
    }
}
