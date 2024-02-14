using AutoMapper;
using coe.dnd.api.ViewModels.Games;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : Controller
{
    private readonly IGameService _gameService;
    private readonly IMapper _mapper;

    public GamesController(IGameService gameService, IMapper mapper)
    {
        _gameService = gameService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IEnumerable<GameViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGames([FromQuery] int? gameMasterId, [FromQuery] int? campaignId)
    {
        var gamesData = await _gameService.GetGamesAsync(gameMasterId, campaignId);
        if (gamesData == null) return NotFound();
        
        return Ok(_mapper.Map<IList<GameViewModel>>(gamesData));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GameViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGame(int id)
    {
        if (!(await _gameService.GameExistsAsync(id))) return NotFound();

        var gameData = await _gameService.GetGameAsync(id);
        
        return Ok(_mapper.Map<GameViewModel>(gameData));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateGameViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateGame(CreateGameViewModel gameDetails)
    {
        var gameData = _mapper.Map<GameDto>(gameDetails);
        await _gameService.CreateGameAsync(gameData);
        
        return CreatedAtAction(nameof(CreateGame), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateGame(int id, UpdateGameViewModel gameDetails)
    {
        if (!(await _gameService.GameExistsAsync(id))) return NotFound();

        var gameData = _mapper.Map<GameDto>(gameDetails);
        await _gameService.UpdateGameAsync(id, gameData);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteGame(int id)
    {
        if (!(await _gameService.GameExistsAsync(id))) return NotFound();
        
        await _gameService.DeleteGameAsync(id);
        
        return NoContent();
    }
}