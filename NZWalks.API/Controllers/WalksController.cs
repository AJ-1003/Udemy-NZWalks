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

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        /* ====================< (C)REATE >==================== */
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateWalkDTO createWalk)
        {
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
    }
}
