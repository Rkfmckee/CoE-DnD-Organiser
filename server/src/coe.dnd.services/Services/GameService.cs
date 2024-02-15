using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.Games;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace coe.dnd.services.Services;

public class GameService : IGameService
{
    private readonly IDndOrganiserDatabase _database;
    private readonly IMapper _mapper;

    public GameService(IDndOrganiserDatabase database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<bool> GameExistsAsync(int id)
    {
        return await _database.Get<Game>()
            .Where(new GameByIdSpec(id))
            .AnyAsync();
    }
    
    public async Task<GameDto> GetGameAsync(int id)
    {
        return await _mapper
            .ProjectTo<GameDto>(GetGameQuery(id))
            .SingleOrDefaultAsync();
    }
    
    public async Task<IList<GameDto>> GetGamesAsync(int? gameMasterId = null, int? campaignId = null)
    {
        return await _mapper
            .ProjectTo<GameDto>(GetGamesQuery(gameMasterId, campaignId))
            .ToListAsync();
    }

    public async Task CreateGameAsync(GameDto gameData)
    {
        var game = _mapper.Map<Game>(gameData);
        game.Created = DateTime.UtcNow;
        
        _database.Add(game);
        await _database.SaveChangesAsync();
    }
    
    public async Task AddCharacterToGameAsync(GameCharacterDto gameCharacterData)
    {
        var gameCharacter = _mapper.Map<GameCharacter>(gameCharacterData);
        
        _database.Add(gameCharacter);
        await _database.SaveChangesAsync();
    }

    public async Task UpdateGameAsync(int id, GameDto gameData)
    {
        var game = await GetGameFromQueryAsync(id);
        
        _mapper.Map(gameData, game);
        await _database.SaveChangesAsync();
    }

    public async Task DeleteGameAsync(int id)
    {
        var game = await GetGameFromQueryAsync(id);
        
        _database.Delete(game);
        await _database.SaveChangesAsync();
    }

    private IQueryable<Game> GetGameQuery(int id)
    {
        return _database.Get<Game>()
            .Where(new GameByIdSpec(id));
    }
    
    private async Task<Game> GetGameFromQueryAsync(int id)
    {
        return await GetGameQuery(id).SingleOrDefaultAsync();
    }
    
    private IQueryable<Game> GetGamesQuery(int? gameMasterId = null, int? campaignId = null)
    {
        return _database.Get<Game>()
            .Where(new GameSearchSpec(gameMasterId, campaignId));
    }
}