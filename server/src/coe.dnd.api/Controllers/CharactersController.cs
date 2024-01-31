using System.Net;
using coe.dnd.api.ViewModels.Characters;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CharacterViewModel>), StatusCodes.Status200OK)]
    public IActionResult GetCharacter()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status200OK)]
    public IActionResult GetCharacterById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCharacterViewModel), StatusCodes.Status201Created)]
    public IActionResult CreateCharacter(CreateCharacterViewModel characterDetails)
    {
        return CreatedAtAction(nameof(CreateCharacter), characterDetails);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateCharacter(int id, UpdateCharacterViewModel characterDetails)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteCharacter(int id)
    {
        return NoContent();
    }
}