using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _context;

        public RegionRepository(NZWalksDbContext context)
        {
            _context = context;
        }

        /* ====================< (C)REATE >==================== */
        public async Task<Region> CreateAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _context.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        /* ====================< (R)EAD >==================== */
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetAsync(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (region == null)
            {
                return null;
            }

            return region;
        }

        /* ====================< (U)PDATE >==================== */
        public async Task<Region?> UpdateAsync(Guid id, Region updatedRegion)
        {
            var domainRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (domainRegion == null)
            {
                return null;
            }

            domainRegion.Code = updatedRegion.Code;
            domainRegion.Name = updatedRegion.Name;
            domainRegion.Area = updatedRegion.Area;
            domainRegion.Latitude = updatedRegion.Latitude;
            domainRegion.Longitude = updatedRegion.Longitude;
            domainRegion.Population = updatedRegion.Population;

            await _context.SaveChangesAsync();
            return domainRegion;
        }

        /* ====================< (D)ELETE >==================== */
        public async Task<Region?> DeleteAsync(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (region == null)
            {
                return null;
            }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return region;
        }
    }
}
