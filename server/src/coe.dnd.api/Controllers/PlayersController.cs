using coe.dnd.api.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PlayerViewModel>), StatusCodes.Status200OK)]
    public IActionResult GetPlayers()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PlayerViewModel), StatusCodes.Status200OK)]
    public IActionResult GetPlayerById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatePlayerViewModel), StatusCodes.Status201Created)]
    public IActionResult CreatePlayer(CreatePlayerViewModel playerDetails)
    {
        return CreatedAtAction(nameof(CreatePlayer), playerDetails);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdatePlayer(int id, UpdatePlayerViewModel playerDetails)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletePlayer(int id)
    {
        return NoContent();
    }
}