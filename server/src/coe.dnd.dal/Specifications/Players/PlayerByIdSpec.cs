using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Players;

public class PlayerByIdSpec : Specification<Player>
{
    private readonly int? _id;

    public PlayerByIdSpec(int? id) => _id = id;

    public override Expression<Func<Player, bool>> BuildExpression() =>
        x => x.Id == _id;
}