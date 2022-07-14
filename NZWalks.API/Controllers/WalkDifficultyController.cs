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
    }
}
