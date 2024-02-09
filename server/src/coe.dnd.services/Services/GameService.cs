using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.Games;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
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

    public bool GameExists(int id)
    {
        return _database.Get<Game>()
            .Where(new GameByIdSpec(id))
            .Any();
    }
    
    public GameDto GetGame(int id)
    {
        return _mapper
            .ProjectTo<GameDto>(GetGameQueryable(id))
            .SingleOrDefault();
    }
    
    public IList<GameDto> GetGames(int? gameMasterId = null, int? campaignId = null)
    {
        return _mapper
            .ProjectTo<GameDto>(GetGamesQueryable(gameMasterId, campaignId))
            .ToList();
    }

    public void CreateGame(GameDto gameData)
    {
        var game = _mapper.Map<Game>(gameData);
        game.Created = DateTime.Now;
        
        _database.Add(game);
        _database.SaveChanges();
    }

    public void UpdateGame(int id, GameDto gameData)
    {
        var game = GetGameObject(id);
        
        _mapper.Map(gameData, game);
        _database.SaveChanges();
    }

    public void DeleteGame(int id)
    {
        var game = GetGameObject(id);
        
        _database.Delete(game);
        _database.SaveChanges();
    }

    private IQueryable<Game> GetGameQueryable(int id)
    {
        return _database.Get<Game>()
            .Where(new GameByIdSpec(id));
    }
    
    private Game GetGameObject(int id)
    {
        return GetGameQueryable(id).SingleOrDefault();
    }
    
    private IQueryable<Game> GetGamesQueryable(int? gameMasterId = null, int? campaignId = null)
    {
        return _database.Get<Game>()
            .Where(new GameSearchSpec(gameMasterId, campaignId));
    }
}