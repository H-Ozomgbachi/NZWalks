using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class RegionProfiles : Profile
    {
        public RegionProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, AddRegionDTO>().ReverseMap();
        }
    }
}
