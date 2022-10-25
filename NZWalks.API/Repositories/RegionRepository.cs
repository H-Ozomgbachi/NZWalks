using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> Add(Region region)
        {
            region.Id = Guid.NewGuid();
            var x = _nZWalksDbContext.Add(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return x.Entity;
        }

        public async Task Delete(Region region)
        {
             _nZWalksDbContext.Remove(region);
            await _nZWalksDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            var region = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            return region;
        }

        public async Task<Region> Update(Region region)
        {
            var x =  _nZWalksDbContext.Regions.Update(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return await GetAsync(x.Entity.Id);
        }
    }
}
