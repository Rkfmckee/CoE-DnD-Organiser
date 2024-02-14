using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface IPlayerService
{
    Task<bool> PlayerExistsAsync(int id);
    Task<PlayerDto> GetPlayerAsync(int id);
    Task<IList<PlayerDto>> GetPlayersAsync(string name = null, string email = null);
    Task CreatePlayerAsync(PlayerDto playerData);
    Task UpdatePlayerAsync(int id, PlayerDto playerData);
    Task DeletePlayerAsync(int id);
}