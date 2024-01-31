using coe.dnd.api.ViewModels.GameMasters;
using coe.dnd.api.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameMastersController : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PlayerViewModel>), StatusCodes.Status200OK)]
    public IActionResult GetGameMasters()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PlayerViewModel), StatusCodes.Status200OK)]
    public IActionResult GetGameMasterById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateGameMastersViewModel), StatusCodes.Status201Created)]
    public IActionResult CreateGameMaster(CreateGameMastersViewModel gameMasterDetails)
    {
        return CreatedAtAction(nameof(CreateGameMaster), gameMasterDetails);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateGameMaster(int id, UpdateGameMastersViewModel gameMasterDetails)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteGameMaster(int id)
    {
        return NoContent();
    }
}