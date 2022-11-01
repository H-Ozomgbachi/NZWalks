using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class WalkProfiles : Profile
    {
        public WalkProfiles()
        {
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<WalkDifficulty, WalkDifficultyDTO>().ReverseMap();
            CreateMap<Walk, CreateWalkDTO>().ReverseMap();
            CreateMap<Walk, UpdateWalkDTO>().ReverseMap();
        }
    }
}
