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
        public async Task<IActionResult> CreateAsync(CreateRegionDTO createRegion)
        {
            // Validate request
            if (!ValidateCreateAsync(createRegion))
            {
                return BadRequest(ModelState);
            }

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
            domainRegion = await _regionRepository.CreateAsync(domainRegion);

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
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegion)
        {
            if (!ValidateUpdateAsync(updateRegion))
            {
                return BadRequest(ModelState);
            }

            // Convert DTO to domain model
            var domainRegion = _mapper.Map<Region>(updateRegion);

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


        #region Private Methods
        private bool ValidateCreateAsync(CreateRegionDTO createRegion)
        {
            if (createRegion == null)
            {
                ModelState.AddModelError(nameof(createRegion), "Data is required!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(createRegion.Code))
            {
                ModelState.AddModelError(nameof(createRegion.Code), $"{nameof(createRegion.Code)} cannot be null, empty or white space!");
            }

            if (string.IsNullOrWhiteSpace(createRegion.Name))
            {
                ModelState.AddModelError(nameof(createRegion.Name), $"{nameof(createRegion.Name)} cannot be null, empty or white space!");
            }

            if (createRegion.Area <= 0)
            {
                ModelState.AddModelError(nameof(createRegion.Area), $"{nameof(createRegion.Area)} must be greater than zero!");
            }

            if (createRegion.Latitude >= -90 && createRegion.Latitude <= 90)
            {
                ModelState.AddModelError(nameof(createRegion.Latitude), $"{nameof(createRegion.Latitude)} must be between -90 and 90!");
            }

            if (createRegion.Longitude >= -180 && createRegion.Longitude <= 180)
            {
                ModelState.AddModelError(nameof(createRegion.Longitude), $"{nameof(createRegion.Longitude)} must be between -180 and 180!");
            }

            if (createRegion.Population <= 0)
            {
                ModelState.AddModelError(nameof(createRegion.Population), $"{nameof(createRegion.Population)} must be greater than zero!");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateAsync(UpdateRegionDTO updateRegion)
        {
            if (updateRegion == null)
            {
                ModelState.AddModelError(nameof(updateRegion), "Data is required!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegion.Code))
            {
                ModelState.AddModelError(nameof(updateRegion.Code), $"{nameof(updateRegion.Code)} cannot be null, empty or white space!");
            }

            if (string.IsNullOrWhiteSpace(updateRegion.Name))
            {
                ModelState.AddModelError(nameof(updateRegion.Name), $"{nameof(updateRegion.Name)} cannot be null, empty or white space!");
            }

            if (updateRegion.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Area), $"{nameof(updateRegion.Area)} must be greater than zero!");
            }

            if (updateRegion.Latitude >= -90 && updateRegion.Latitude <= 90)
            {
                ModelState.AddModelError(nameof(updateRegion.Latitude), $"{nameof(updateRegion.Latitude)} must be between -90 and 90!");
            }

            if (updateRegion.Longitude >= -180 && updateRegion.Longitude <= 180)
            {
                ModelState.AddModelError(nameof(updateRegion.Longitude), $"{nameof(updateRegion.Longitude)} must be between -180 and 180!");
            }

            if (updateRegion.Population <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Population), $"{nameof(updateRegion.Population)} must be greater than zero!");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
