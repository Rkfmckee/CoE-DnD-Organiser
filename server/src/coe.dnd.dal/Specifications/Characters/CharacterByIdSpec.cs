using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Characters;

public class CharacterByIdSpec : Specification<Character>
{
    private readonly int? _id;

    public CharacterByIdSpec(int? id) => _id = id;

    public override Expression<Func<Character, bool>> BuildExpression() =>
        x => x.Id == _id;
}