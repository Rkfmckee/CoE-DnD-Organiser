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
    
    public GameMaster GetGameMaster(int id)
    {
        return _database.Get<GameMaster>()
            .Where(new GameMasterByIdSpec(id))
            .SingleOrDefault();
    }
    
    public GameMasterDto GetGameMasterData(int id)
    {
        var gameMasterData = _mapper.Map<GameMasterDto>(GetGameMaster(id));
        gameMasterData.PlayerName = GetGameMasterName(gameMasterData);

        return gameMasterData;
    }

    public IList<GameMaster> GetGameMasters()
    {
        return _database.Get<GameMaster>()
            .ToList();
    }
    
    public IList<GameMasterDto> GetGameMastersData()
    {
        var gameMastersData = _mapper.Map<IList<GameMasterDto>>(GetGameMasters());
        
        foreach (var gameMasterData in gameMastersData)
            gameMasterData.PlayerName = GetGameMasterName(gameMasterData);
        
        return gameMastersData;
    }

    public void CreateGameMaster(GameMasterDto gameMasterData)
    {
        var gameMaster = _mapper.Map<GameMaster>(gameMasterData);
        gameMaster.Created = DateTime.Now;
        
        _database.Add(gameMaster);
        _database.SaveChanges();
    }

    public void UpdateGameMaster(int id, GameMasterDto gameMasterData)
    {
        var gameMaster = GetGameMaster(id);
        
        _mapper.Map(gameMasterData, gameMaster);
        _database.SaveChanges();
    }

    public void DeleteGameMaster(int id)
    {
        var gameMaster = GetGameMaster(id);
        
        _database.Delete(gameMaster);
        _database.SaveChanges();
    }

    private string GetGameMasterName(GameMasterDto gameMasterData)
    {
        return _playerService.GetPlayer(gameMasterData.PlayerId).Name;
    }
}