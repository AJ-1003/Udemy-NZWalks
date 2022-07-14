using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext _context;

        public WalkDifficultyRepository(NZWalksDbContext context)
        {
            _context = context;
        }

        /* ====================< (C)REATE >==================== */
        public async Task<WalkDifficulty> CreateAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await _context.AddAsync(walkDifficulty);
            await _context.SaveChangesAsync();
            return walkDifficulty;
        }

        /* ====================< (R)EAD >==================== */
        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await _context.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            var walkDifficulty = await _context.WalkDifficulty.FirstOrDefaultAsync(wd => wd.Id == id);

            if (walkDifficulty == null)
            {
                return null;
            }

            return walkDifficulty;
        }

        /* ====================< (U)PDATE >==================== */
        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty updatedWalkDifficulty)
        {
            var domainWalkDifficulty = await _context.WalkDifficulty.FirstOrDefaultAsync(wd => wd.Id == id);

            if (domainWalkDifficulty == null)
            {
                return null;
            }

            domainWalkDifficulty.Code = updatedWalkDifficulty.Code;

            await _context.SaveChangesAsync();
            return domainWalkDifficulty;
        }

        /* ====================< (D)ELETE >==================== */
        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDifficulty = await _context.WalkDifficulty.FirstOrDefaultAsync(wd => wd.Id == id);

            if (walkDifficulty == null)
            {
                return null;
            }

            _context.Remove(walkDifficulty);
            await _context.SaveChangesAsync();
            return walkDifficulty;
        }
    }
}
