using System.Net;
using coe.dnd.api.ViewModels.Games;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GameViewModel>), StatusCodes.Status200OK)]
    public IActionResult GetGames()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GameViewModel), StatusCodes.Status200OK)]
    public IActionResult GetGameById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateGameViewModel), StatusCodes.Status201Created)]
    public IActionResult CreateGame(CreateGameViewModel gameDetails)
    {
        return CreatedAtAction(nameof(CreateGame), gameDetails);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateGame(int id, UpdateGameViewModel gameDetails)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteGame(int id)
    {
        return NoContent();
    }
}