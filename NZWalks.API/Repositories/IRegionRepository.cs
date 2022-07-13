using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        /* ====================< (C)REATE >==================== */
        Task<Region> AddAsync(Region region);

        /* ====================< (R)EAD >==================== */
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid id);

        /* ====================< (U)PDATE >==================== */
        Task<Region> UpdateAsync(Guid id, Region region);

        /* ====================< (D)ELETE >==================== */
        Task<Region> DeleteAsync(Guid id);
    }
}
