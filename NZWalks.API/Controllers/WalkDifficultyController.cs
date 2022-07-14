using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
            _mapper = mapper;
        }

        /* ====================< (C)REATE >==================== */
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateWalkDifficultyDTO createWalkDifficulty)
        {
            if (!await ValidateCreateAsync(createWalkDifficulty))
            {
                return BadRequest(ModelState);
            }

            // Conver DTO to domain object
            var domainWalkDifficulty = _mapper.Map<WalkDifficulty>(createWalkDifficulty);

            // Pass domain object to repository
            domainWalkDifficulty = await _walkDifficultyRepository.CreateAsync(domainWalkDifficulty);

            // Convert domain object to DTO
            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(domainWalkDifficulty);

            // Return response
            return Ok(walkDifficultyDTO);
        }

        /* ====================< (R)EAD >==================== */
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Get all domain objects from DB
            var domainWalkDifficulties = await _walkDifficultyRepository.GetAllAsync();

            // Convert domain object to DTO
            var walkDifficultiesDTO = _mapper.Map<List<WalkDifficultyDTO>>(domainWalkDifficulties);

            // Return response
            return Ok(walkDifficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            // Get domain object from DB with id
            var domainWalkDifficulty = await _walkDifficultyRepository.GetAsync(id);

            // Handle null
            if (domainWalkDifficulty == null)
            {
                return NotFound();
            }

            // Convert domain object to DTO
            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(domainWalkDifficulty);

            // Return response
            return Ok(walkDifficultyDTO);
        }

        /* ====================< (U)PDATE >==================== */
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyDTO updateWalkDifficulty)
        {
            if (!await ValidateUpdateAsync(updateWalkDifficulty))
            {
                return BadRequest(ModelState);
            }

            // Conver DTO to domain object
            var domainWalkDifficulty = _mapper.Map<WalkDifficulty>(updateWalkDifficulty);

            // Pass domain object to repository
            domainWalkDifficulty = await _walkDifficultyRepository.UpdateAsync(id, domainWalkDifficulty);

            // Handle null
            if (domainWalkDifficulty == null)
            {
                return NotFound();
            }

            // Convert domain object to DTO
            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(domainWalkDifficulty);

            // Return response
            return CreatedAtAction(nameof(GetAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        /* ====================< (D)ELETE >==================== */
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            // Get domain object from DB with id
            var domainWalkDifficulty = await _walkDifficultyRepository.DeleteAsync(id);

            // Handle null
            if (domainWalkDifficulty == null)
            {
                return NotFound();
            }

            // Convert domain object to DTO
            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(domainWalkDifficulty);

            // Return response
            return Ok(walkDifficultyDTO);
        }

        #region Private Methods
        private async Task<bool> ValidateCreateAsync(CreateWalkDifficultyDTO createWalkDifficulty)
        {
            if (createWalkDifficulty == null)
            {
                ModelState.AddModelError(nameof(createWalkDifficulty), "Data is required!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(createWalkDifficulty.Code))
            {
                ModelState.AddModelError(nameof(createWalkDifficulty.Code), $"{nameof(createWalkDifficulty.Code)} cannot be null, empty or white space!");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateAsync(UpdateWalkDifficultyDTO updateWalkDifficulty)
        {
            if (updateWalkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficulty), "Data is required!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficulty.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficulty.Code), $"{nameof(updateWalkDifficulty.Code)} cannot be null, empty or white space!");
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
