using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.GameMasters;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace coe.dnd.services.Services;

public class GameMasterService : IGameMasterService
{
    private readonly IDndOrganiserDatabase _database;
    private readonly IMapper _mapper;

    public GameMasterService(IDndOrganiserDatabase database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<bool> GameMasterExistsAsync(int id)
    {
        return await _database.Get<GameMaster>()
            .Where(new GameMasterByIdSpec(id))
            .AnyAsync();
    }
    
    public async Task<GameMasterDto> GetGameMasterAsync(int id)
    {
        return await _mapper
            .ProjectTo<GameMasterDto>(GetGameMasterQuery(id))
            .SingleOrDefaultAsync();
    }
    
    public async Task<IList<GameMasterDto>> GetGameMastersAsync()
    {
        return await _mapper
            .ProjectTo<GameMasterDto>(GetGameMastersQuery())
            .ToListAsync();
    }

    public async Task CreateGameMasterAsync(GameMasterDto gameMasterData)
    {
        var gameMaster = _mapper.Map<GameMaster>(gameMasterData);
        gameMaster.Created = DateTime.UtcNow;
        
        _database.Add(gameMaster);
        await _database.SaveChangesAsync();
    }

    public async Task UpdateGameMasterAsync(int id, GameMasterDto gameMasterData)
    {
        var gameMaster = await GetGameMasterFromQueryAsync(id);
        
        _mapper.Map(gameMasterData, gameMaster);
        await _database.SaveChangesAsync();
    }

    public async Task DeleteGameMasterAsync(int id)
    {
        var gameMaster = await GetGameMasterFromQueryAsync(id);
        
        _database.Delete(gameMaster);
        await _database.SaveChangesAsync();
    }
    
    private IQueryable<GameMaster> GetGameMasterQuery(int id)
    {
        return _database.Get<GameMaster>()
            .Where(new GameMasterByIdSpec(id));
    }
    
    private async Task<GameMaster> GetGameMasterFromQueryAsync(int id)
    {
        return await GetGameMasterQuery(id).SingleOrDefaultAsync();
    }
    
    private IQueryable<GameMaster> GetGameMastersQuery()
    {
        return _database.Get<GameMaster>();
    }
}