using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.Players;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace coe.dnd.services.Services;

public class PlayerService : IPlayerService
{
    private readonly IDndOrganiserDatabase _database;
    private readonly IMapper _mapper;

    public PlayerService(IDndOrganiserDatabase database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public bool PlayerExists(int id)
    {
        return _database.Get<Player>()
            .Where(new PlayerByIdSpec(id))
            .Any();
    }
    
    public PlayerDto GetPlayer(int id)
    {
        return _mapper
            .ProjectTo<PlayerDto>(GetPlayerQueryable(id))
            .SingleOrDefault();
    }
    
    public IList<PlayerDto> GetPlayers(string name = null, string email = null)
    {
        return _mapper
            .ProjectTo<PlayerDto>(GetPlayersQueryable(name, email))
            .ToList();
    }

    public void CreatePlayer(PlayerDto playerData)
    {
        var player = _mapper.Map<Player>(playerData);
        player.Created = DateTime.Now;
        player.Password = BCrypt.Net.BCrypt.HashPassword(player.Password);
        
        _database.Add(player);
        _database.SaveChanges();
    }

    public void UpdatePlayer(int id, PlayerDto playerData)
    {
        var player = GetPlayerObject(id);
        
        if (!string.IsNullOrEmpty(playerData.Password))
            playerData.Password = BCrypt.Net.BCrypt.HashPassword(playerData.Password);
        
        _mapper.Map(playerData, player);
        _database.SaveChanges();
    }

    public void DeletePlayer(int id)
    {
        var player = GetPlayerObject(id);
        
        _database.Delete(player);
        _database.SaveChanges();
    }
    
    private IQueryable<Player> GetPlayerQueryable(int id)
    {
        return _database.Get<Player>()
            .Where(new PlayerByIdSpec(id));
    }
    
    private Player GetPlayerObject(int id)
    {
        return GetPlayerQueryable(id).SingleOrDefault();
    }
    
    private IQueryable<Player> GetPlayersQueryable(string name = null, string email = null)
    {
        return _database.Get<Player>()
            .Where(new PlayerSearchSpec(name, email));
    }
}