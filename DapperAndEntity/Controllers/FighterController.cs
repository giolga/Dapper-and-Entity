using DapperAndEntity.Data;
using DapperAndEntity.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DapperAndEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FighterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FighterController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fighter>>> GetFighters()
        {
            if (_context.Fighters == null)
            {
                return NoContent();
            }

            return await _context.Fighters.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fighter>> GetFighterById(int id)
        {
            if (_context.Fighters == null)
            {
                return NoContent();
            }

            var figther =  await _context.Fighters.FirstOrDefaultAsync(f => f.Id == id);

            if (figther == null)
            {
                return BadRequest();
            }

            return Ok(figther);
        }

        [HttpPost]
        public async Task<ActionResult<Fighter>> PostFighter(FighterDto fighterDto)
        {
            if (fighterDto == null)
            {
                return Problem("Entity set 'AppDbContext.Fighter'  is null.");
            }

            var fighter = new Fighter()
            {
                Name = fighterDto.Name,
                LastName = fighterDto.LastName,
                Division = fighterDto.Division
            };

            _context.Fighters.Add(fighter);
            await _context.SaveChangesAsync();

            return Ok(fighter);
        }

        [HttpPut]
        public async Task<ActionResult<Fighter>> UpdateFighter(int id, FighterDto fighterDto)
        {
            if (_context.Fighters == null)
            {
                return NoContent();
            }

            var fighter = _context.Fighters.FirstOrDefault(f => f.Id == id);
            if(fighter == null)
            {
                return NotFound();
            }

            fighter.Name = fighterDto.Name;
            fighter.LastName = fighterDto.LastName;
            fighter.Division = fighterDto.Division;

            _context.Entry(fighter).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return Ok(fighter);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Fighter>> DeleteFighter(int id)
        {
            if(_context.Fighters == null)
            {
                return NoContent();
            }

            var fighter = _context.Fighters.FirstOrDefault(f => f.Id == id);

            if(fighter == null)
            {
                return NotFound();
            }

            _context.Fighters.Remove(fighter);
            await _context.SaveChangesAsync();

            return Ok($"Fighter with the is {fighter.Id} has been removed!");
        }


    }
}
