using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface IGameService
{
    bool GameExists(int id);
    GameDto GetGame(int id);
    IList<GameDto> GetGames(int? gameMasterId = null, int? campaignId = null);
    void CreateGame(GameDto gameData);
    void UpdateGame(int id, GameDto gameData);
    void DeleteGame(int id);
}