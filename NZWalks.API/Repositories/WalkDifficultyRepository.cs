using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        private readonly IMapper _mapper;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext, IMapper mapper)
        {
            _nZWalksDbContext = nZWalksDbContext;
            _mapper = mapper;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walk)
        {
            walk.Id = Guid.NewGuid();
            var newWalkDifficulty = _nZWalksDbContext.WalkDifficulties.Add(walk);
            await _nZWalksDbContext.SaveChangesAsync();
            return newWalkDifficulty.Entity;
        }

        public async Task DeleteAsync(WalkDifficulty walkDifficulty)
        {
            _nZWalksDbContext.Remove(walkDifficulty);
            await _nZWalksDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<WalkDifficultyDTO>> GetAllAsync()
        {
            var walks = await _nZWalksDbContext.WalkDifficulties.ToListAsync();

            return _mapper.Map<IEnumerable<WalkDifficultyDTO>>(walks);
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            var walk = await _nZWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);

            return walk;
        }

        public async Task<WalkDifficulty> UpdateAsync(WalkDifficulty walk)
        {
            var updated = _nZWalksDbContext.Update(walk);
            await _nZWalksDbContext.SaveChangesAsync();
            return updated.Entity;
        }
    }
}
