using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface IPlayerService
{
    bool PlayerExists(int id);
    PlayerDto GetPlayer(int id);
    IList<PlayerDto> GetPlayers(string name = null, string email = null);
    void CreatePlayer(PlayerDto playerData);
    void UpdatePlayer(int id, PlayerDto playerData);
    void DeletePlayer(int id);
}