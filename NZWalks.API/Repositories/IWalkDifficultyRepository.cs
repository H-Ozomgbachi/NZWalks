using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficultyDTO>> GetAllAsync();
        Task<WalkDifficulty> GetAsync(Guid id);
        Task<WalkDifficulty> AddAsync(WalkDifficulty walk);
        Task<WalkDifficulty> UpdateAsync(WalkDifficulty walk);
        Task DeleteAsync(WalkDifficulty walkDifficulty);
    }
}
