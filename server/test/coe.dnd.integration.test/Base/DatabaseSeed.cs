using coe.dnd.dal.Contexts;
using coe.dnd.dal.Models;

namespace coe.dnd.integration.test.Base;

public static class DatabaseSeed
{
    public static void SeedDatabase(DndOrganiserContext database)
    {
        var recordsForEachTable = 10;
        
        var campaigns = new List<Campaign>();
        var characters = new List<Character>();
        var games = new List<Game>();
        var gameCharacters = new List<GameCharacter>();
        var gameMasters = new List<GameMaster>();
        var players = new List<Player>();
        
        for (int i = 0; i < recordsForEachTable; i++)
        {
            campaigns.Add(
                new Campaign
                {
                    Id = i,
                    Name = $"Campaign {i}",
                    Theme = $"Theme {i}",
                    Details = $"Campaign {i} details",
                    Writer = $"Writer {i}",
                    Created = DateTime.UtcNow
                });
            
            characters.Add(
                new Character
                {
                    Id = i,
                    Name = $"Character name {i}",
                    Race = $"Race {i}",
                    ClassLevels = $"Class {i} (subclass)",
                    Created = DateTime.UtcNow,
                    PlayerId = i
                });
            
            games.Add(
                new Game
                {
                    Id = i,
                    Details = $"Details of game {i}",
                    Created = DateTime.UtcNow,
                    GameMasterId = i,
                    CampaignId = i
                });
            
            gameCharacters.Add(
                new GameCharacter
                {
                    Id = i,
                    GameId = i,
                    CharacterId = i
                });
            
            gameMasters.Add(
                new GameMaster
                {
                    Id = i,
                    PlanningNotes = $"Planning notes for Game master {i}",
                    Created = DateTime.UtcNow,
                    PlayerId = i
                });
            
            players.Add(
                new Player
                {
                    Id = i,
                    EmailAddress = $"player{i}@dnd.com",
                    Password = $"Password{i}",
                    Name = $"Player name {i}",
                    DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 01, 01).AddDays(i)),
                    Created = DateTime.UtcNow
                });
        }

        database.Add(campaigns);
        database.Add(characters);
        database.Add(games);
        database.Add(gameCharacters);
        database.Add(gameMasters);
        database.Add(players);

        database.SaveChanges();
    }
}