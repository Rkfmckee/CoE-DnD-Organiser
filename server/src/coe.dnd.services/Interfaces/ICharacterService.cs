using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface ICharacterService
{
    bool CharacterExists(int id);
    Character GetCharacter(int id);
    CharacterDto GetCharacterData(int id);
    IList<Character> GetCharacters(string name = null, string race = null, string @class = null);
    IList<CharacterDto> GetCharactersData(string name = null, string race = null, string @class = null);
    void CreateCharacter(CharacterDto characterData);
    void UpdateCharacter(int id, CharacterDto characterData);
    void DeleteCharacter(int id);
}