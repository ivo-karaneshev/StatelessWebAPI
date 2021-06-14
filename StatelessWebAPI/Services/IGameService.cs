using StatelessWebAPI.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatelessWebAPI.Services
{
    public interface IGameService
    {
        /// <summary>
        /// Gets game by id.
        /// This method implements caching.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        Task<Game> GetGameAsync(int gameId);

        /// <summary>
        /// Creates a new game and sets its state to <see cref="State.Bought"/>.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The newly created game.</returns>
        Task<Game> BuyGameAsync(int userId);

        /// <summary>
        /// Changes game state.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="state"></param>
        /// <returns>The updated game or null if not found.</returns>
        Task<Game> ChangeGameStateAsync(int gameId, State state);

        /// <summary>
        /// Gets all games for a specific user.
        /// This method implements caching.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All games for the specified user.</returns>
        Task<IEnumerable<Game>> GetUserGamesAsync(int userId);

        /// <summary>
        /// Deletes game by id.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>The result of the deletion.</returns>
        Task<bool> DeleteGameAsync(int gameId);
    }
}