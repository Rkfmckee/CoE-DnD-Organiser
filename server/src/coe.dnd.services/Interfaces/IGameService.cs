using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface IGameService
{
    Task<bool> GameExistsAsync(int id);
    Task<GameDto>  GetGameAsync(int id);
    Task<IList<GameDto>> GetGamesAsync(int? gameMasterId = null, int? campaignId = null);
    Task CreateGameAsync(GameDto gameData);
    Task UpdateGameAsync(int id, GameDto gameData);
    Task DeleteGameAsync(int id);
}