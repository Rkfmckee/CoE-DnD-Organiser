using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface IGameMasterService
{
    bool GameMasterExists(int id);
    GameMasterDto GetGameMaster(int id);
    IList<GameMasterDto> GetGameMasters();
    void CreateGameMaster(GameMasterDto gameMasterData);
    void UpdateGameMaster(int id, GameMasterDto gameMasterData);
    void DeleteGameMaster(int id);
}