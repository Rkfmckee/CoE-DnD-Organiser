using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Characters;

public class CharacterByClassSpec : Specification<Character>
{
    private readonly string _class;
    
    public CharacterByClassSpec(string @class) => _class = @class;

    public override Expression<Func<Character, bool>> BuildExpression() =>
        string.IsNullOrWhiteSpace(_class) ? ShowAll : x => x.ClassLevels.Contains(_class);
}