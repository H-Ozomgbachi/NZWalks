using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/v1/walkdifficulties")]
    [ApiController]
    public class WalkDifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _walkDifficultyRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetOneAsync")]
        public async Task<IActionResult> GetOneAsync(Guid id)
        {
            var result = await _walkDifficultyRepository.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDifficultyDTO>(result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneAsync([FromBody] CreateWalkDifficultyDTO difficultyDTO)
        {
            if (!ValidateWalkDifficulty(difficultyDTO))
            {
                return BadRequest(ModelState);
            }

            var walkDifficulty = _mapper.Map<WalkDifficulty>(difficultyDTO);

            var result = await _walkDifficultyRepository.AddAsync(walkDifficulty);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOneAsync(Guid id, UpdateWalkDifficultyDTO difficultyDTO)
        {
            var existingWalkDifficulty = await _walkDifficultyRepository.GetAsync(id);

            if (existingWalkDifficulty == null)
            {
                return NotFound();
            }

            var walk = _mapper.Map(difficultyDTO, existingWalkDifficulty);

            var res = await _walkDifficultyRepository.UpdateAsync(walk);

            return Ok(_mapper.Map<WalkDifficultyDTO>(res));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneAsync(Guid id)
        {
            var existingWalkDifficulty = await _walkDifficultyRepository.GetAsync(id);

            if (existingWalkDifficulty == null)
            {
                return NotFound();
            }

            await _walkDifficultyRepository.DeleteAsync(existingWalkDifficulty);

            return NoContent();
        }
        #region Private Methods
        private bool ValidateWalkDifficulty(CreateWalkDifficultyDTO difficultyDTO)
        {
            if (string.IsNullOrWhiteSpace(difficultyDTO.Code))
            {
                ModelState.AddModelError($"{nameof(difficultyDTO.Code)}", $"{nameof(difficultyDTO.Code)} cannot be null or whitespace");
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
