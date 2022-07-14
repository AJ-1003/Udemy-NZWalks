using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _context;

        public WalkRepository(NZWalksDbContext context)
        {
            _context = context;
        }

        /* ====================< (C)REATE >==================== */
        public async Task<Walk> CreateAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        /* ====================< (R)EAD >==================== */
        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await _context.Walks
                .Include(wRegion => wRegion.Region)
                .Include(wDifficulty => wDifficulty.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk?> GetAsync(Guid id)
        {
            var walk = await _context.Walks
                .Include(wRegion => wRegion.Region)
                .Include(wDifficulty => wDifficulty.WalkDifficulty)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (walk == null)
            {
                return null;
            }

            return walk;
        }

        /* ====================< (U)PDATE >==================== */
        public async Task<Walk?> UpdateAsync(Guid id, Walk updatedWalk)
        {
            var domainWalk = await _context.Walks
                .Include(wRegion => wRegion.Region)
                .Include(wDifficulty => wDifficulty.WalkDifficulty)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (domainWalk == null)
            {
                return null;
            }

            domainWalk.Name = updatedWalk.Name;
            domainWalk.Length = updatedWalk.Length;
            domainWalk.RegionId = updatedWalk.RegionId;
            domainWalk.WalkDifficultyId = updatedWalk.WalkDifficultyId;

            await _context.SaveChangesAsync();
            return domainWalk;
        }

        /* ====================< (D)ELETE >==================== */
        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await _context.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if (walk == null)
            {
                return null;
            }

            _context.Remove(walk);
            await _context.SaveChangesAsync();
            return walk;
        }
    }
}
