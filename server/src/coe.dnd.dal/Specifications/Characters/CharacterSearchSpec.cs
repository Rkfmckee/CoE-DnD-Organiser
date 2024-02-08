using System.Linq.Expressions;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.Players;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Characters;

public class CharacterSearchSpec : Specification<Character>
{
    private readonly Specification<Character> _spec;

    public CharacterSearchSpec(string name, string race, string @class) => 
        _spec = new CharacterByNameSpec(name)
            .Or(new CharacterByRaceSpec(race))
            .Or(new CharacterByClassSpec(@class));

    public override Expression<Func<Character, bool>> BuildExpression() => _spec.BuildExpression();
}