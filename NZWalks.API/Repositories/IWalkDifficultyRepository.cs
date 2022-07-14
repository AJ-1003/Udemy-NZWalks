using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        /* ====================< (C)REATE >==================== */
        Task<WalkDifficulty> CreateAsync(WalkDifficulty walkDifficulty);

        /* ====================< (R)EAD >==================== */
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetAsync(Guid id);

        /* ====================< (U)PDATE >==================== */
        Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty);

        /* ====================< (D)ELETE >==================== */
        Task<WalkDifficulty> DeleteAsync(Guid id);
    }
}
