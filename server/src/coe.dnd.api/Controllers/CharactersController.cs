using AutoMapper;
using coe.dnd.api.ViewModels.Characters;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : Controller
{
    private readonly ICharacterService _characterService;
    private readonly IMapper _mapper;

    public CharactersController(ICharacterService characterService, IMapper mapper)
    {
        _characterService = characterService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IEnumerable<CharacterViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCharacters([FromQuery] string name, [FromQuery] string race, [FromQuery] string @class)
    {
        var charactersData = await _characterService.GetCharactersAsync(name, race, @class);
        if (charactersData == null) return NotFound();
        
        return Ok(_mapper.Map<IList<CharacterViewModel>>(charactersData));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCharacter(int id)
    {
        if (!(await _characterService.CharacterExistsAsync(id))) return NotFound();

        var characterData = await _characterService.GetCharacterAsync(id);
        
        return Ok(_mapper.Map<CharacterViewModel>(characterData));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCharacterViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCharacter(CreateCharacterViewModel characterDetails)
    {
        var characterData = _mapper.Map<CharacterDto>(characterDetails);
        await _characterService.CreateCharacterAsync(characterData);
        
        return CreatedAtAction(nameof(CreateCharacter), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCharacter(int id, UpdateCharacterViewModel characterDetails)
    {
        if (!(await _characterService.CharacterExistsAsync(id))) return NotFound();

        var characterData = _mapper.Map<CharacterDto>(characterDetails);
        await _characterService.UpdateCharacterAsync(id, characterData);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
        if (!(await _characterService.CharacterExistsAsync(id))) return NotFound();
        
        await _characterService.DeleteCharacterAsync(id);
        
        return NoContent();
    }
}