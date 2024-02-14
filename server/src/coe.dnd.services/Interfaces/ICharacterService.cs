using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface ICharacterService
{
    Task<bool> CharacterExistsAsync(int id);
    Task<CharacterDto> GetCharacterAsync(int id);
    Task<IList<CharacterDto>> GetCharactersAsync(string name = null, string race = null, string @class = null);
    Task CreateCharacterAsync(CharacterDto characterData);
    Task UpdateCharacterAsync(int id, CharacterDto characterData);
    Task DeleteCharacterAsync(int id);
}