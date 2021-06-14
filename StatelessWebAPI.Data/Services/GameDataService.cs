using StatelessWebAPI.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StatelessWebAPI.Data.Services
{
    public class GameDataService : IGameDataService
    {
        private readonly GameDbContext _dbContext;

        public GameDataService(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ValueTask<Game> GetGameAsync(int gameId)
        {
            return _dbContext.FindAsync<Game>(gameId);
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            await _dbContext.AddAsync(game);
            await _dbContext.SaveChangesAsync();

            return game;
        }

        public Task UpdateGameAsync(Game game)
        {
            _dbContext.GameItems.Update(game);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<Game> GetUserGames(int userId)
        {
            return _dbContext.GameItems.Where(x => x.UserId == userId);
        }

        public async Task DeleteGameAsync(Game game)
        {
            _dbContext.Remove(game);
            await _dbContext.SaveChangesAsync();
        }
    }
}