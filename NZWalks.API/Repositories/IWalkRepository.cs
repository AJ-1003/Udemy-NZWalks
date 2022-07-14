using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        /* ====================< (C)REATE >==================== */
        Task<Walk> CreateAsync(Walk walk);

        /* ====================< (R)EAD >==================== */
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetAsync(Guid id);

        /* ====================< (U)PDATE >==================== */
        Task<Walk> UpdateAsync(Guid id, Walk walk);

        /* ====================< (D)ELETE >==================== */
        Task<Walk> DeleteAsync(Guid id);
    }
}
