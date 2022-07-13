using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        /* ====================< (C)REATE >==================== */
        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateRegionDTO createRegion)
        {
            // Convert request(DTO) to domain model
            //var domainRegion = new Region()
            //{
            //    Code = createRegion.Code,
            //    Name = createRegion.Name,
            //    Area = createRegion.Area,
            //    Latitude = createRegion.Latitude,
            //    Longitude = createRegion.Longitude,
            //    Population = createRegion.Population
            //};

            var domainRegion = _mapper.Map<Region>(createRegion);

            // Pass details to repository
            domainRegion = await _regionRepository.AddAsync(domainRegion);

            // Convert back to DTO
            //var regionDTO = new Region()
            //{
            //    Id = domainRegion.Id,
            //    Code = domainRegion.Code,
            //    Name = domainRegion.Name,
            //    Area = domainRegion.Area,
            //    Latitude = domainRegion.Latitude,
            //    Longitude = domainRegion.Longitude,
            //    Population = domainRegion.Population
            //};

            var regionDTO = _mapper.Map<RegionDTO>(domainRegion);

            return CreatedAtAction(nameof(GetAsync), new { id = regionDTO.Id }, regionDTO);
        }

        /* ====================< (R)EAD >==================== */
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var domainRegions = await _regionRepository.GetAllAsync();

            // return regions DTO
            //var regionsDTO = new List<RegionDTO>();
            //domainRegions.ToList().ForEach(domainRegion =>
            //{
            //    var regionDTO = new RegionDTO()
            //    {
            //        Id = domainRegion.Id,
            //        Code = domainRegion.Code,
            //        Name = domainRegion.Name,
            //        Area = domainRegion.Area,
            //        Latitude = domainRegion.Latitude,
            //        Longitude = domainRegion.Longitude,
            //        Population = domainRegion.Population
            //    };

            //    regionsDTO.Add(regionDTO);
            //});

            var regionsDTO = _mapper.Map<List<RegionDTO>>(domainRegions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var domainRegion = await _regionRepository.GetAsync(id);

            if (domainRegion == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<RegionDTO>(domainRegion);

            return Ok(regionDTO);
        }

        /* ====================< (U)PDATE >==================== */
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionDTO updatedRegion)
        {
            // Convert DTO to domain model
            var domainRegion = _mapper.Map<Region>(updatedRegion);

            // Update region using repository
            domainRegion = await _regionRepository.UpdateAsync(id, domainRegion);

            // If null, return not found
            if (domainRegion == null)
            {
                return NotFound();
            }

            // Convert domain to DTO
            var regionDTO = _mapper.Map<UpdateRegionDTO>(domainRegion);

            // Return Ok response
            return Ok(regionDTO);
        }

        /* ====================< (D)ELETE >==================== */
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            // Get region from DB
            var domainRegion = await _regionRepository.DeleteAsync(id);

            // If null, return not found
            if (domainRegion == null)
            {
                return NotFound();
            }

            // Convert response to DTO
            var regionDTO = _mapper.Map<RegionDTO>(domainRegion);

            // Return OK response
            return Ok(regionDTO);
        }
    }
}
