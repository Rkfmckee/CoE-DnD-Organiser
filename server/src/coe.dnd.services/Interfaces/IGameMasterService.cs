using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface IGameMasterService
{
    Task<bool> GameMasterExistsAsync(int id);
    Task<GameMasterDto> GetGameMasterAsync(int id);
    Task<IList<GameMasterDto>> GetGameMastersAsync();
    Task CreateGameMasterAsync(GameMasterDto gameMasterData);
    Task UpdateGameMasterAsync(int id, GameMasterDto gameMasterData);
    Task DeleteGameMasterAsync(int id);
}