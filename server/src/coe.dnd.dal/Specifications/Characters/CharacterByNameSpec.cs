using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Characters;

public class CharacterByNameSpec : Specification<Character>
{
    private readonly string _name;
    
    public CharacterByNameSpec(string name) => _name = name;

    public override Expression<Func<Character, bool>> BuildExpression() =>
        string.IsNullOrWhiteSpace(_name) ? ShowAll : x => x.Name.Contains(_name);
}