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

        public async Task<Game> BuyGame(int userId)
        {
            var game = new Game
            {
                State = State.Bought,
                UserId = userId
            };

            await _dbContext.AddAsync(game);
            await _dbContext.SaveChangesAsync();

            return game;
        }

        public async Task<Game> ChangeGameState(int gameId, State state)
        {
            var game = await _dbContext.FindAsync<Game>(gameId);
            if (game != null)
            {
                game.State = state;
                await _dbContext.SaveChangesAsync();
            }

            return game;
        }

        public async Task<State?> GetGameState(int gameId)
        {
            var game = await _dbContext.FindAsync<Game>(gameId);
            if (game == null)
            {
                return null;
            }

            return game.State;
        }

        public IQueryable<Game> GetUserGames(int userId)
        {
            return _dbContext.GameItems.Where(x => x.UserId == userId);
        }
    }
}