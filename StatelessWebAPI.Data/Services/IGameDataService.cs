using StatelessWebAPI.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StatelessWebAPI.Data.Services
{
    public interface IGameDataService
    {
        /// <summary>
        /// Creates a new game and sets its state to <see cref="State.Bought"/>.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The newly created game.</returns>
        Task<Game> BuyGame(int userId);

        /// <summary>
        /// Changes game state.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="state"></param>
        /// <returns>The updated game or null if not found.</returns>
        Task<Game> ChangeGameState(int gameId, State state);

        /// <summary>
        /// Gets game state.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>The state of the game or null if not found.</returns>
        Task<State?> GetGameState(int gameId);

        /// <summary>
        /// Gets all games for a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All games for the specified user.</returns>
        IQueryable<Game> GetUserGames(int userId);
    }
}