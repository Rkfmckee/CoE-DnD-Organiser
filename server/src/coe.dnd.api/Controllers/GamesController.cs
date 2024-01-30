using System.Net;
using coe.dnd.api.ViewModels.Games;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GameViewModel>), (int)HttpStatusCode.OK)]
    public IActionResult GetGames()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GameViewModel), (int)HttpStatusCode.OK)]
    public IActionResult GetGameById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public IActionResult CreateGame(CreateGameViewModel gameMasterDetails)
    {
        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult UpdateGame(int id, UpdateGameViewModel gameMasterDetails)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public IActionResult DeleteGame(int id)
    {
        return NoContent();
    }
}