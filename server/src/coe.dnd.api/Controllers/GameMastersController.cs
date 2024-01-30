using System.Net;
using coe.dnd.api.ViewModels.GameMasters;
using coe.dnd.api.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameMastersController : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PlayerViewModel>), (int)HttpStatusCode.OK)]
    public IActionResult GetGameMasters()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PlayerViewModel), (int)HttpStatusCode.OK)]
    public IActionResult GetGameMasterById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public IActionResult CreateGameMaster(CreateGameMastersViewModel gameMasterDetails)
    {
        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult UpdateGameMaster(int id, UpdateGameMastersViewModel gameMasterDetails)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public IActionResult DeleteGameMaster(int id)
    {
        return NoContent();
    }
}