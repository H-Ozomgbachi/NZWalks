using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficultyDTO>> GetAllAsync();
    }
}
