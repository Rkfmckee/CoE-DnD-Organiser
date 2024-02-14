using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.Characters;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> CharacterExistsAsync(int id)
    {
        return await _database.Get<Character>()
            .Where(new CharacterByIdSpec(id))
            .AnyAsync();
    }
    
    public async Task<CharacterDto> GetCharacterAsync(int id)
    {
        return await _mapper
            .ProjectTo<CharacterDto>(GetCharacterQuery(id))
            .SingleOrDefaultAsync();
    }
    
    public async Task<IList<CharacterDto>> GetCharactersAsync(string name = null, string race = null, string @class = null)
    {
        return await _mapper
            .ProjectTo<CharacterDto>(GetCharactersQuery(name, race, @class))
            .ToListAsync();
    }

    public async Task CreateCharacterAsync(CharacterDto characterData)
    {
        var character = _mapper.Map<Character>(characterData);
        character.Created = DateTime.UtcNow;
        
        _database.Add(character);
        await _database.SaveChangesAsync();
    }

    public async Task UpdateCharacterAsync(int id, CharacterDto characterData)
    {
        var character = await GetCharacterFromQueryAsync(id);
        
        _mapper.Map(characterData, character);
        await _database.SaveChangesAsync();
    }

    public async Task DeleteCharacterAsync(int id)
    {
        var character = await GetCharacterFromQueryAsync(id);
        
        _database.Delete(character);
        await _database.SaveChangesAsync();
    }
    
    private IQueryable<Character> GetCharacterQuery(int id)
    {
        return _database.Get<Character>()
            .Where(new CharacterByIdSpec(id));
    }
    
    private async Task<Character> GetCharacterFromQueryAsync(int id)
    {
        return await GetCharacterQuery(id).SingleOrDefaultAsync();
    }
    
    private IQueryable<Character> GetCharactersQuery(string name = null, string race = null, string @class = null)
    {
        return _database.Get<Character>()
            .Where(new CharacterSearchSpec(name, race, @class));
    }
}