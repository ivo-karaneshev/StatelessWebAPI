using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatelessWebAPI.Data.Models;
using StatelessWebAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatelessWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _gameService.GetGameAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("buy/{user-id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Buy([FromRoute(Name = "user-id")] int userId)
        {
            var game = await _gameService.BuyGameAsync(userId);

            return CreatedAtAction(nameof(Get), new { id = game.Id }, game);
        }

        [HttpPatch("play/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
        public async Task<IActionResult> Play(int id)
        {
            var game = await _gameService.ChangeGameStateAsync(id, State.Play);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        [HttpPatch("finish/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
        public async Task<IActionResult> Finish(int id)
        {
            var game = await _gameService.ChangeGameStateAsync(id, State.Finish);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        [HttpGet("get-game-state/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(State))]
        public async Task<IActionResult> GetGameState(int id)
        {
            var result = await _gameService.GetGameAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.State);
        }

        [HttpGet("get-user-games/{user-id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Game>))]
        public async Task<IActionResult> GetUserGames([FromRoute(Name = "user-id")] int userId)
        {
            var userGames = await _gameService.GetUserGamesAsync(userId);

            return Ok(userGames);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var successfulDelete = await _gameService.DeleteGameAsync(id);
            if (!successfulDelete)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}