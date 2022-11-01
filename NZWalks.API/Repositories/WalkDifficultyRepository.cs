using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
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

        public async Task<IEnumerable<WalkDifficultyDTO>> GetAllAsync()
        {
            var walks = await _nZWalksDbContext.WalkDifficulties.ToListAsync();

            return _mapper.Map<IEnumerable<WalkDifficultyDTO>>(walks);
        }
    }
}
