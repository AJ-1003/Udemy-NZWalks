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

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
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
    }
}
