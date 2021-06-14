using Microsoft.EntityFrameworkCore;
using StatelessWebAPI.Caching.Services;
using StatelessWebAPI.Data.Models;
using StatelessWebAPI.Data.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatelessWebAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IGameDataService _dataService;
        private readonly ICachingProvider _cachingProvider;

        public GameService(
            IGameDataService dataService,
            ICachingProvider cachingProvider)
        {
            _dataService = dataService;
            _cachingProvider = cachingProvider;
        }

        public async Task<Game> GetGameAsync(int gameId)
        {
            Game game;
            var key = CreateGameCacheKey(gameId);
            // Get game from cache
            game = await _cachingProvider.GetValueAsync<Game>(key);
            if (game == null)
            {
                // Get game from db
                game = await _dataService.GetGameAsync(gameId);
                if (game != null)
                {
                    // Cache game
                    await _cachingProvider.SetValueAsync(key, game);
                }
            }

            return game;
        }

        public async Task<Game> BuyGameAsync(int userId)
        {
            var game = new Game
            {
                State = State.Bought,
                UserId = userId
            };

            await _dataService.CreateGameAsync(game);

            return game;
        }

        public async Task<Game> ChangeGameStateAsync(int gameId, State state)
        {
            var game = await _dataService.GetGameAsync(gameId);
            if (game != null)
            {
                game.State = state;
                await _dataService.UpdateGameAsync(game);
                await InvalidateCacheOnUpdate(game);
            }

            return game;
        }

        public async Task<IEnumerable<Game>> GetUserGamesAsync(int userId)
        {
            List<Game> games;
            var key = CreateUserGamesCacheKey(userId);
            // Get user games from cache
            games = await _cachingProvider.GetValueAsync<List<Game>>(key);
            if (games == null || games.Count == 0)
            {
                // Get games from db
                games = await _dataService.GetUserGames(userId).ToListAsync();
                if (games != null && games.Count > 0)
                {
                    // Cache user games
                    await _cachingProvider.SetValueAsync(key, games);
                }
            }

            return games;
        }

        public async Task<bool> DeleteGameAsync(int gameId)
        {
            var game = await _dataService.GetGameAsync(gameId);
            if (game == null)
            {
                return false;
            }

            await _dataService.DeleteGameAsync(game);
            await InvalidateCacheOnUpdate(game);

            return true;
        }

        private string CreateGameCacheKey(int gameId) => $"game:{gameId}";

        private string CreateUserGamesCacheKey(int userId) => $"user:{userId}:games";

        private Task InvalidateCacheOnUpdate(Game game)
        {
            var gameKey = CreateGameCacheKey(game.Id);
            var userGamesKey = CreateUserGamesCacheKey(game.UserId);

            return Task.WhenAll(
                _cachingProvider.InvalidateAsync(gameKey),
                _cachingProvider.InvalidateAsync(userGamesKey));
        }
    }
}