using Microsoft.EntityFrameworkCore;
using StatelessWebAPI.Data.Models;

namespace StatelessWebAPI.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        public DbSet<Game> GameItems { get; set; }
    }
}