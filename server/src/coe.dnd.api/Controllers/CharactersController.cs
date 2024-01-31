using System.Net;
using coe.dnd.api.ViewModels.Characters;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CharacterViewModel>), (int)HttpStatusCode.OK)]
    public IActionResult GetCharacter()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CharacterViewModel), (int)HttpStatusCode.OK)]
    public IActionResult GetCharacterById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public IActionResult CreateCharacter(CreateCharacterViewModel characterDetails)
    {
        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult UpdateCharacter(int id, UpdateCharacterViewModel characterDetails)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public IActionResult DeleteCharacter(int id)
    {
        return NoContent();
    }
}