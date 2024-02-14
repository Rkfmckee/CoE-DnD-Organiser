using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.Players;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> PlayerExistsAsync(int id)
    {
        return await _database.Get<Player>()
            .Where(new PlayerByIdSpec(id))
            .AnyAsync();
    }
    
    public async Task<PlayerDto> GetPlayerAsync(int id)
    {
        return await _mapper
            .ProjectTo<PlayerDto>(GetPlayerQuery(id))
            .SingleOrDefaultAsync();
    }
    
    public async Task<IList<PlayerDto>> GetPlayersAsync(string name = null, string email = null)
    {
        return await _mapper
            .ProjectTo<PlayerDto>(GetPlayersQuery(name, email))
            .ToListAsync();
    }

    public async Task CreatePlayerAsync(PlayerDto playerData)
    {
        var player = _mapper.Map<Player>(playerData);
        player.Created = DateTime.UtcNow;
        player.Password = BCrypt.Net.BCrypt.HashPassword(player.Password);
        
        _database.Add(player);
        await _database.SaveChangesAsync();
    }

    public async Task UpdatePlayerAsync(int id, PlayerDto playerData)
    {
        var player = await GetPlayerFromQueryAsync(id);
        
        if (!string.IsNullOrEmpty(playerData.Password))
            playerData.Password = BCrypt.Net.BCrypt.HashPassword(playerData.Password);
        
        _mapper.Map(playerData, player);
        await _database.SaveChangesAsync();
    }

    public async Task DeletePlayerAsync(int id)
    {
        var player = await GetPlayerFromQueryAsync(id);
        
        _database.Delete(player);
        await _database.SaveChangesAsync();
    }
    
    private IQueryable<Player> GetPlayerQuery(int id)
    {
        return _database.Get<Player>()
            .Where(new PlayerByIdSpec(id));
    }
    
    private async Task<Player> GetPlayerFromQueryAsync(int id)
    {
        return await GetPlayerQuery(id).SingleOrDefaultAsync();
    }
    
    private IQueryable<Player> GetPlayersQuery(string name = null, string email = null)
    {
        return _database.Get<Player>()
            .Where(new PlayerSearchSpec(name, email));
    }
}