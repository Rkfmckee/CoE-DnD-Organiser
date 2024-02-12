using AutoMapper;
using coe.dnd.api.ViewModels.Players;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : Controller
{
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;

    public PlayersController(IPlayerService playerService, IMapper mapper)
    {
        _playerService = playerService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IEnumerable<PlayerViewModel>), StatusCodes.Status200OK)]
    public IActionResult GetPlayers([FromQuery] string name, [FromQuery] string email)
    {
        var playersData = _playerService.GetPlayers(name, email);
        if (playersData == null) return NotFound();
        
        return Ok(_mapper.Map<IList<PlayerViewModel>>(playersData));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PlayerViewModel), StatusCodes.Status200OK)]
    public IActionResult GetPlayer(int id)
    {
        if (!_playerService.PlayerExists(id)) return NotFound();

        var playerData = _playerService.GetPlayer(id);
        
        return Ok(_mapper.Map<PlayerViewModel>(playerData));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreatePlayer(CreatePlayerViewModel playerDetails)
    {
        var playerData = _mapper.Map<PlayerDto>(playerDetails);
        _playerService.CreatePlayer(playerData);
        
        return CreatedAtAction(nameof(CreatePlayer), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdatePlayer(int id, UpdatePlayerViewModel playerDetails)
    {
        if (!_playerService.PlayerExists(id)) return NotFound();

        var playerData = _mapper.Map<PlayerDto>(playerDetails);
        _playerService.UpdatePlayer(id, playerData);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletePlayer(int id)
    {
        if (!_playerService.PlayerExists(id)) return NotFound();
        
        _playerService.DeletePlayer(id);
        
        return NoContent();
    }
}