using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface ICharacterService
{
    bool CharacterExists(int id);
    CharacterDto GetCharacter(int id);
    IList<CharacterDto> GetCharacters(string name = null, string race = null, string @class = null);
    void CreateCharacter(CharacterDto characterData);
    void UpdateCharacter(int id, CharacterDto characterData);
    void DeleteCharacter(int id);
}