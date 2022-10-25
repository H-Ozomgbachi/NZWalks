using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAll();
        Task<Region> GetAsync(Guid id);
        Task<Region> Add(Region region);
        Task Delete(Region region);
        Task<Region> Update(Region region);
    }
}
