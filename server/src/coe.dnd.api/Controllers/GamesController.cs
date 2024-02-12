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
    public IActionResult GetGames([FromQuery] int? gameMasterId, [FromQuery] int? campaignId)
    {
        var gamesData = _gameService.GetGames(gameMasterId, campaignId);
        if (gamesData == null) return NotFound();
        
        return Ok(_mapper.Map<IList<GameViewModel>>(gamesData));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GameViewModel), StatusCodes.Status200OK)]
    public IActionResult GetGame(int id)
    {
        if (!_gameService.GameExists(id)) return NotFound();

        var gameData = _gameService.GetGame(id);
        
        return Ok(_mapper.Map<GameViewModel>(gameData));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateGameViewModel), StatusCodes.Status201Created)]
    public IActionResult CreateGame(CreateGameViewModel gameDetails)
    {
        var gameData = _mapper.Map<GameDto>(gameDetails);
        _gameService.CreateGame(gameData);
        
        return CreatedAtAction(nameof(CreateGame), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateGame(int id, UpdateGameViewModel gameDetails)
    {
        if (!_gameService.GameExists(id)) return NotFound();

        var gameData = _mapper.Map<GameDto>(gameDetails);
        _gameService.UpdateGame(id, gameData);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteGame(int id)
    {
        if (!_gameService.GameExists(id)) return NotFound();
        
        _gameService.DeleteGame(id);
        
        return NoContent();
    }
}