using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;
        private readonly IRegionRepository _regionRepository;
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
            _regionRepository = regionRepository;
            _walkDifficultyRepository = walkDifficultyRepository;
        }

        /* ====================< (C)REATE >==================== */
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateWalkDTO createWalk)
        {
            if (!await ValidateCreateAsync(createWalk))
            {
                return BadRequest(ModelState);
            }

            // Conver DTO to domain object
            var domainWalk = _mapper.Map<Walk>(createWalk);

            // Pass domain object to repository
            domainWalk = await _walkRepository.CreateAsync(domainWalk);

            // Convert domain object to DTO
            var walkDTO = _mapper.Map<WalkDTO>(domainWalk);

            // Return response
            return CreatedAtAction(nameof(GetAsync), new { id = walkDTO.Id }, walkDTO);
        }

        /* ====================< (R)EAD >==================== */
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Get all domain objects from DB
            var domainWalks = await _walkRepository.GetAllAsync();

            // Convert domain object to DTO
            var walksDTO = _mapper.Map<List<WalkDTO>>(domainWalks);

            // Return response
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            // Get domain object from DB with id
            var domainWalk = await _walkRepository.GetAsync(id);

            // Handle null
            if (domainWalk == null)
            {
                return NotFound();
            }

            // Convert domain object to DTO
            var walkDTO = _mapper.Map<WalkDTO>(domainWalk);

            // Return response
            return Ok(walkDTO);
        }

        /* ====================< (U)PDATE >==================== */
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkDTO updateWalk)
        {
            if (!await ValidateUpdateAsync(updateWalk))
            {
                return BadRequest(ModelState);
            }

            // Conver DTO to domain object
            var domainWalk = _mapper.Map<Walk>(updateWalk);

            // Pass domain object to repository
            domainWalk = await _walkRepository.UpdateAsync(id, domainWalk);

            // Handle null
            if (domainWalk == null)
            {
                return NotFound();
            }

            // Convert domain object to DTO
            var walkDTO = _mapper.Map<WalkDTO>(domainWalk);

            // Return response
            return Ok(walkDTO);
        }

        /* ====================< (D)ELETE >==================== */
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            // Get domain object from DB with id
            var domainWalk = await _walkRepository.DeleteAsync(id);

            // Handle null
            if (domainWalk == null)
            {
                return NotFound();
            }

            // Convert domain object to DTO
            var walkDTO = _mapper.Map<WalkDTO>(domainWalk);

            // Return response
            return Ok(walkDTO);
        }

        #region Private Methods
        private async Task<bool> ValidateCreateAsync(CreateWalkDTO createWalk)
        {
            if (createWalk == null)
            {
                ModelState.AddModelError(nameof(createWalk), "Data is required!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(nameof(createWalk.Name)))
            {
                ModelState.AddModelError(nameof(createWalk.Name), $"{nameof(createWalk.Name)} cannot be null, empty or white space!");
            }

            if (createWalk.Length < 0)
            {
                ModelState.AddModelError(nameof(createWalk.Length), $"{nameof(createWalk.Length)} must be greater than zero!");
            }

            var region = await _regionRepository.GetAsync(createWalk.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(createWalk.RegionId), $"{nameof(createWalk.RegionId)} is invalid!");
            }

            var walkDifficulty = await _walkDifficultyRepository.GetAsync(createWalk.WalkDifficultyId);

            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(createWalk.WalkDifficultyId), $"{nameof(createWalk.WalkDifficultyId)} is invalid!");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateAsync(UpdateWalkDTO updateWalk)
        {
            if (updateWalk == null)
            {
                ModelState.AddModelError(nameof(updateWalk), "Data is required!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(nameof(updateWalk.Name)))
            {
                ModelState.AddModelError(nameof(updateWalk.Name), $"{nameof(updateWalk.Name)} cannot be null, empty or white space!");
            }

            if (updateWalk.Length < 0)
            {
                ModelState.AddModelError(nameof(updateWalk.Length), $"{nameof(updateWalk.Length)} must be greater than zero!");
            }

            var region = await _regionRepository.GetAsync(updateWalk.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalk.RegionId), $"{nameof(updateWalk.RegionId)} is invalid!");
            }

            var walkDifficulty = await _walkDifficultyRepository.GetAsync(updateWalk.WalkDifficultyId);

            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalk.WalkDifficultyId), $"{nameof(updateWalk.WalkDifficultyId)} is invalid!");
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
