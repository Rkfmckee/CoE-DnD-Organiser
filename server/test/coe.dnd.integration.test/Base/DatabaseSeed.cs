using coe.dnd.dal.Contexts;
using coe.dnd.dal.Models;

namespace coe.dnd.integration.test.Base;

public static class DatabaseSeed
{
    public static void SeedDatabase(DndOrganiserContext database)
    {
        var recordsForEachTable = 10;
        
        for (int i = 1; i <= recordsForEachTable; i++)
        {
            var campaign = new Campaign 
            {
                Id = i,
                Name = $"Campaign {i}",
                Theme = $"Theme {i}",
                Details = $"Campaign {i} details",
                Writer = $"Writer {i}",
                Created = DateTime.UtcNow
            };
            
            var character = new Character
            {
                Id = i,
                Name = $"Character name {i}",
                Race = $"Race {i}",
                ClassLevels = $"Class {i} (subclass)",
                Created = DateTime.UtcNow,
                PlayerId = i
            };
            
            var game = new Game
            {
                Id = i,
                Details = $"Details of game {i}",
                Created = DateTime.UtcNow,
                GameMasterId = i,
                CampaignId = i
            };
            
            var gameCharacter = new GameCharacter
            {
                Id = i,
                GameId = i,
                CharacterId = i
            };
            
            var gameMaster = new GameMaster
            {
                Id = i,
                PlanningNotes = $"Planning notes for Game master {i}",
                Created = DateTime.UtcNow,
                PlayerId = i
            };
            
            var player = new Player
            {
                Id = i,
                EmailAddress = $"player{i}@dnd.com",
                Password = BCrypt.Net.BCrypt.HashPassword($"Password{i}"),
                Name = $"Player name {i}",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 01, 01).AddDays(i)),
                Created = DateTime.UtcNow
            };
            
            database.Add(campaign);
            database.Add(character);
            database.Add(game);
            database.Add(gameCharacter);
            database.Add(gameMaster);
            database.Add(player);
            
            database.SaveChanges();
        }
    }
}