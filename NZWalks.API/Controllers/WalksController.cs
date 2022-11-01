using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/v1/walks")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walks = await _walkRepository.GetAllAsync();

            var walksDTO = _mapper.Map<IEnumerable<WalkDTO>>(walks);

            return Ok(walksDTO);
        }

        [HttpGet("{id:guid}", Name = "GetWalk")]
        public async Task<IActionResult> GetWalk(Guid id)
        {
            var walk = await _walkRepository.GetAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDTO = _mapper.Map<WalkDTO>(walk);

            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] CreateWalkDTO walkDTO)
        {
            var walk = _mapper.Map<Walk>(walkDTO);

            var newlyAddedWalk = await _walkRepository.AddAsync(walk);

            return CreatedAtAction("GetWalk", new { id = newlyAddedWalk.Id }, newlyAddedWalk);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync(Guid id, UpdateWalkDTO walkDTO)
        {
            var existingWalk = await _walkRepository.GetAsync(id);

            if (existingWalk == null)
            {
                return NotFound();
            }

            var walk = _mapper.Map(walkDTO, existingWalk);

            return Ok(await _walkRepository.UpdateAsync(id, walk));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var existingWalk = await _walkRepository.GetAsync(id);

            if (existingWalk == null)
            {
                return NotFound();
            }

            await _walkRepository.DeleteAsync(existingWalk);

            return NoContent();
        }
    }
}
