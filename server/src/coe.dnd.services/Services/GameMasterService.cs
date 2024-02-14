using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.GameMasters;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace coe.dnd.services.Services;

public class GameMasterService : IGameMasterService
{
    private readonly IDndOrganiserDatabase _database;
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;

    public GameMasterService(IDndOrganiserDatabase database, IPlayerService playerService, IMapper mapper)
    {
        _database = database;
        _playerService = playerService;
        _mapper = mapper;
    }

    public bool GameMasterExists(int id)
    {
        return _database.Get<GameMaster>()
            .Where(new GameMasterByIdSpec(id))
            .Any();
    }
    
    public GameMasterDto GetGameMaster(int id)
    {
        return _mapper
            .ProjectTo<GameMasterDto>(GetGameMasterQueryable(id))
            .SingleOrDefault();
    }
    
    public IList<GameMasterDto> GetGameMasters()
    {
        return _mapper
            .ProjectTo<GameMasterDto>(GetGameMastersQueryable())
            .ToList();
    }

    public void CreateGameMaster(GameMasterDto gameMasterData)
    {
        var gameMaster = _mapper.Map<GameMaster>(gameMasterData);
        gameMaster.Created = DateTime.UtcNow;
        
        _database.Add(gameMaster);
        _database.SaveChanges();
    }

    public void UpdateGameMaster(int id, GameMasterDto gameMasterData)
    {
        var gameMaster = GetGameMasterObject(id);
        
        _mapper.Map(gameMasterData, gameMaster);
        _database.SaveChanges();
    }

    public void DeleteGameMaster(int id)
    {
        var gameMaster = GetGameMasterObject(id);
        
        _database.Delete(gameMaster);
        _database.SaveChanges();
    }
    
    private IQueryable<GameMaster> GetGameMasterQueryable(int id)
    {
        return _database.Get<GameMaster>()
            .Where(new GameMasterByIdSpec(id));
    }
    
    private GameMaster GetGameMasterObject(int id)
    {
        return GetGameMasterQueryable(id).SingleOrDefault();
    }
    
    private IQueryable<GameMaster> GetGameMastersQueryable()
    {
        return _database.Get<GameMaster>();
    }
}