using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Characters;

public class CharacterByRaceSpec : Specification<Character>
{
    private readonly string _race;
    
    public CharacterByRaceSpec(string race) => _race = race;

    public override Expression<Func<Character, bool>> BuildExpression() =>
        string.IsNullOrWhiteSpace(_race) ? ShowAll : x => x.Race.Contains(_race);
}