using AutoMapper;
using coe.dnd.api.ViewModels.GameMasters;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameMastersController : Controller
{
    private readonly IGameMasterService _gameMasterService;
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;

    public GameMastersController(IGameMasterService gameMasterService, IPlayerService playerService, IMapper mapper)
    {
        _gameMasterService = gameMasterService;
        _playerService = playerService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IEnumerable<GameMasterViewModel>), StatusCodes.Status200OK)]
    public IActionResult GetGameMasters()
    {
        var gameMastersData = _gameMasterService.GetGameMasters();
        if (gameMastersData == null) return NotFound();
        
        return Ok(_mapper.Map<IList<GameMasterViewModel>>(gameMastersData));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GameMasterViewModel), StatusCodes.Status200OK)]
    public IActionResult GetGameMaster(int id)
    {
        if (!_gameMasterService.GameMasterExists(id)) return NotFound();
        
        var gameMasterData = _gameMasterService.GetGameMaster(id);
        
        return Ok(_mapper.Map<GameMasterViewModel>(gameMasterData));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CreateGameMasterViewModel), StatusCodes.Status201Created)]
    public IActionResult CreateGameMaster(CreateGameMasterViewModel gameMasterDetails)
    {
        if (!_playerService.PlayerExists(gameMasterDetails.PlayerId)) 
            return NotFound("GameMaster can only be created if linked to a valid PlayerId");
        
        var gameMasterData = _mapper.Map<GameMasterDto>(gameMasterDetails);
        _gameMasterService.CreateGameMaster(gameMasterData);
        
        return CreatedAtAction(nameof(CreateGameMaster), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateGameMaster(int id, UpdateGameMasterViewModel gameMasterDetails)
    {
        if (!_gameMasterService.GameMasterExists(id)) return NotFound();

        var gameMasterData = _mapper.Map<GameMasterDto>(gameMasterDetails);
        _gameMasterService.UpdateGameMaster(id, gameMasterData);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteGameMaster(int id)
    {
        if (!_gameMasterService.GameMasterExists(id)) return NotFound();
        
        _gameMasterService.DeleteGameMaster(id);
        
        return NoContent();
    }
}