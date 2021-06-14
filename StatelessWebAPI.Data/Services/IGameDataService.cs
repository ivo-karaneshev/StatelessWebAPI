using StatelessWebAPI.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StatelessWebAPI.Data.Services
{
    public interface IGameDataService
    {
        /// <summary>
        /// Gets game by id.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>The game found, or null.</returns>
        ValueTask<Game> GetGameAsync(int gameId);

        /// <summary>
        /// Creates the given game.
        /// </summary>
        /// <param name="game"></param>
        /// <returns>The newly created game with unique <see cref="Game.Id"/>.</returns>
        Task<Game> CreateGameAsync(Game game);

        /// <summary>
        /// Updates the given game.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        Task UpdateGameAsync(Game game);

        /// <summary>
        /// Gets all games for a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All games for the specified user.</returns>
        IQueryable<Game> GetUserGames(int userId);

        /// <summary>
        /// Deletes game by id.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        Task DeleteGameAsync(Game game);
    }
}