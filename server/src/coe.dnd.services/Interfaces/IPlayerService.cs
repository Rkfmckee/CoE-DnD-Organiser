using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface IPlayerService
{
    bool PlayerExists(int id);
    Player GetPlayer(int id);
    PlayerDto GetPlayerData(int id);
    IList<Player> GetPlayers(string name = null, string email = null);
    IList<PlayerDto> GetPlayersData(string name = null, string email = null);
    void CreatePlayer(PlayerDto playerData);
    void UpdatePlayer(int id, PlayerDto playerData);
    void DeletePlayer(int id);
}