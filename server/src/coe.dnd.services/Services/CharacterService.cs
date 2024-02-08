using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.Characters;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace coe.dnd.services.Services;

public class CharacterService : ICharacterService
{
    private readonly IDndOrganiserDatabase _database;
    private readonly IMapper _mapper;

    public CharacterService(IDndOrganiserDatabase database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public bool CharacterExists(int id)
    {
        return _database.Get<Character>()
            .Where(new CharacterByIdSpec(id))
            .Any();
    }
    
    public Character GetCharacter(int id)
    {
        return _database.Get<Character>()
            .Where(new CharacterByIdSpec(id))
            .SingleOrDefault();
    }
    
    public CharacterDto GetCharacterData(int id)
    {
        return _mapper.Map<CharacterDto>(GetCharacter(id));
    }

    public IList<Character> GetCharacters(string name = null, string race = null, string @class = null)
    {
        return _database.Get<Character>()
            .Where(new CharacterSearchSpec(name, race, @class))
            .ToList();
    }
    
    public IList<CharacterDto> GetCharactersData(string name = null, string race = null, string @class = null)
    {
        return _mapper.Map<IList<CharacterDto>>(GetCharacters(name, race, @class));
    }

    public void CreateCharacter(CharacterDto characterData)
    {
        var character = _mapper.Map<Character>(characterData);
        character.Created = DateTime.Now;
        
        _database.Add(character);
        _database.SaveChanges();
    }

    public void UpdateCharacter(int id, CharacterDto characterData)
    {
        var character = GetCharacter(id);
        
        _mapper.Map(characterData, character);
        _database.SaveChanges();
    }

    public void DeleteCharacter(int id)
    {
        var character = GetCharacter(id);
        
        _database.Delete(character);
        _database.SaveChanges();
    }
}