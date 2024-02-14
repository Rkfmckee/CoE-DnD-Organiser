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
    public async Task<IActionResult> GetGameMasters()
    {
        var gameMastersData = await _gameMasterService.GetGameMastersAsync();
        if (gameMastersData == null) return NotFound();
        
        return Ok(_mapper.Map<IList<GameMasterViewModel>>(gameMastersData));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GameMasterViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGameMaster(int id)
    {
        if (!(await _gameMasterService.GameMasterExistsAsync(id))) return NotFound();
        
        var gameMasterData = await _gameMasterService.GetGameMasterAsync(id);
        
        return Ok(_mapper.Map<GameMasterViewModel>(gameMasterData));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CreateGameMasterViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateGameMaster(CreateGameMasterViewModel gameMasterDetails)
    {
        if (!(await _playerService.PlayerExistsAsync(gameMasterDetails.PlayerId)))
            return NotFound("GameMaster can only be created if linked to a valid PlayerId");
        
        var gameMasterData = _mapper.Map<GameMasterDto>(gameMasterDetails);
        await _gameMasterService.CreateGameMasterAsync(gameMasterData);
        
        return CreatedAtAction(nameof(CreateGameMaster), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateGameMaster(int id, UpdateGameMasterViewModel gameMasterDetails)
    {
        if (!(await _gameMasterService.GameMasterExistsAsync(id))) return NotFound();

        var gameMasterData = _mapper.Map<GameMasterDto>(gameMasterDetails);
        await _gameMasterService.UpdateGameMasterAsync(id, gameMasterData);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteGameMaster(int id)
    {
        if (!(await _gameMasterService.GameMasterExistsAsync(id))) return NotFound();
        
        await _gameMasterService.DeleteGameMasterAsync(id);
        
        return NoContent();
    }
}