using Microsoft.EntityFrameworkCore;

namespace DapperAndEntity.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Fighter> Fighters { get; set; }
    }
}
