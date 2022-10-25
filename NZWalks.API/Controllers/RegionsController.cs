using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/v1/regions")]
    [ApiController]
    public class RegionsController : ControllerBase
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
            var regions = await _regionRepository.GetAll();
            return Ok(_mapper.Map<List<RegionDTO>>(regions));
        }

        [HttpGet("{id}", Name = "GetRegion")]
        public async Task<IActionResult> GetRegion(Guid id)
        {
            var region = await _regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionDTO>(region));
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(AddRegionDTO addRegionDTO)
        {
            var region = _mapper.Map<Region>(addRegionDTO);

            var created = await _regionRepository.Add(region);

            return CreatedAtAction(nameof(GetRegion), new {id = created.Id}, _mapper.Map<RegionDTO>(created));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region = await _regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            await _regionRepository.Delete(region);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRegion(Guid id, AddRegionDTO payload)
        {
            var region = await _regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            _mapper.Map(payload, region);
            var updated = await _regionRepository.Update(region);
            return Ok(_mapper.Map<RegionDTO>(updated));
        }
    }
}
