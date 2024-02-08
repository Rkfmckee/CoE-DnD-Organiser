using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface IGameMasterService
{
    bool GameMasterExists(int id);
    GameMaster GetGameMaster(int id);
    GameMasterDto GetGameMasterData(int id);
    IList<GameMaster> GetGameMasters();
    IList<GameMasterDto> GetGameMastersData();
    void CreateGameMaster(GameMasterDto gameMasterData);
    void UpdateGameMaster(int id, GameMasterDto gameMasterData);
    void DeleteGameMaster(int id);
}