using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Games;

public class GameByIdSpec : Specification<Game>
{
    private readonly int? _id;

    public GameByIdSpec(int? id) => _id = id;

    public override Expression<Func<Game, bool>> BuildExpression() =>
        x => x.Id == _id;
}