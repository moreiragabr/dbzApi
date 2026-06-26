using dbzApi.Data;
using dbzApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dbzApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CharactersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter([FromBody] Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _appDbContext.Dbz.Add(character);
            await _appDbContext.SaveChangesAsync();

            return Created("Character created.", character);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
        {
            var characters = await _appDbContext.Dbz.ToListAsync();

            return Ok(characters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            var character = await _appDbContext.Dbz.FindAsync(id);

            if (character == null)
            {
                return NotFound("Character not found");
            }

            return Ok(character);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(int id, [FromBody] Character characterUpdate)
        {
            var characterActual = await _appDbContext.Dbz.FindAsync(id);

            if (characterActual == null)
            {
                return NotFound("Character not found");
            }

            _appDbContext.Entry(characterActual).CurrentValues.SetValues(characterUpdate);
            await _appDbContext.SaveChangesAsync();
            return StatusCode(201, characterActual);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character = await _appDbContext.Dbz.FindAsync(id);

            if (character == null)
            {
                return NotFound("Character not found");
            }

            _appDbContext.Dbz.Remove(character);
            await _appDbContext.SaveChangesAsync();
            return Ok("Character deleted");
        }
    }
}
