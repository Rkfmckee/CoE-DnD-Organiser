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
    public IActionResult GetCharacters()
    {
        var charactersData = _characterService.GetCharactersData();
        if (charactersData == null) return NotFound();
        
        return Ok(_mapper.Map<IList<CharacterViewModel>>(charactersData));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status200OK)]
    public IActionResult GetCharacterById(int id)
    {
        if (!_characterService.CharacterExists(id)) return NotFound();

        var characterData = _characterService.GetCharacterData(id);
        
        return Ok(_mapper.Map<CharacterViewModel>(characterData));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCharacterViewModel), StatusCodes.Status201Created)]
    public IActionResult CreateCharacter(CreateCharacterViewModel characterDetails)
    {
        var characterData = _mapper.Map<CharacterDto>(characterDetails);
        _characterService.CreateCharacter(characterData);
        
        return CreatedAtAction(nameof(CreateCharacter), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateCharacter(int id, UpdateCharacterViewModel characterDetails)
    {
        if (!_characterService.CharacterExists(id)) return NotFound();

        var characterData = _mapper.Map<CharacterDto>(characterDetails);
        _characterService.UpdateCharacter(id, characterData);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteCharacter(int id)
    {
        if (!_characterService.CharacterExists(id)) return NotFound();
        
        _characterService.DeleteCharacter(id);
        
        return NoContent();
    }
}