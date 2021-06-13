using Microsoft.EntityFrameworkCore;
using StatelessWebAPI.Data.Models;

namespace StatelessWebAPI.Data
{
    internal class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        public DbSet<Game> GameItems { get; set; }
    }
}