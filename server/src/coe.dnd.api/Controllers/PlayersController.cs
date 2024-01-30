using System.Net;
using coe.dnd.api.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : Controller
{
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult GetPlayers()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult GetPlayerById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatePlayerViewModel), (int)HttpStatusCode.Created)]
    public IActionResult CreatePlayer(CreatePlayerViewModel playerDetails)
    {
        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdatePlayerViewModel), (int)HttpStatusCode.OK)]
    public IActionResult UpdatePlayer(int id, UpdatePlayerViewModel playerDetails)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public IActionResult DeletePlayer(int id)
    {
        return NoContent();
    }
}