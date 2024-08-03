using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperAndEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FighterDapperController : ControllerBase
    {
        private readonly IConfiguration _config; //For connection string
        public FighterDapperController(IConfiguration config)
        {
            this._config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fighter>>> GetFighters()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FightersDbContext"));

            var fighters = await connection.QueryAsync<Fighter>("SELECT * FROM Fighters");

            if(fighters == null)
            {
                return NotFound();
            }

            return Ok(fighters);
        }

        [HttpGet("{fighterId}")]
        public async Task<ActionResult<Fighter>> GetFighterById(int fighterId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FightersDbContext"));

            var fighter = await connection.QueryFirstOrDefaultAsync<Fighter>("SELECT * FROM Fighters WHERE Id = @id", new { id = fighterId });

            if (fighter == null)
            {
                return NotFound($"Fighter with ID {fighterId} not found.");
            }

            return Ok(fighter);
        }
    }
}
