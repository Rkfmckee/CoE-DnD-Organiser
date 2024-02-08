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
    
    public Player GetPlayer(int id)
    {
        return _database.Get<Player>()
            .Where(new PlayerByIdSpec(id))
            .SingleOrDefault();
    }
    
    public PlayerDto GetPlayerData(int id)
    {
        return _mapper.Map<PlayerDto>(GetPlayer(id));
    }

    public IList<Player> GetPlayers(string name = null, string email = null)
    {
        return _database.Get<Player>()
            .Where(new PlayerSearchSpec(name, email))
            .ToList();
    }
    
    public IList<PlayerDto> GetPlayersData(string name = null, string email = null)
    {
        return _mapper.Map<IList<PlayerDto>>(GetPlayers(name, email));
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
        var player = GetPlayer(id);
        
        if (!string.IsNullOrEmpty(playerData.Password))
            playerData.Password = BCrypt.Net.BCrypt.HashPassword(playerData.Password);
        
        _mapper.Map(playerData, player);
        _database.SaveChanges();
    }

    public void DeletePlayer(int id)
    {
        var player = GetPlayer(id);
        
        _database.Delete(player);
        _database.SaveChanges();
    }
}