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

            var figther = _context.Fighters.FirstOrDefault(f => f.Id == id);

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
    }
}
