using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Players;

public class PlayerSearchSpec : Specification<Player>
{
    private readonly Specification<Player> _spec;

    public PlayerSearchSpec(string name, string email) => 
        _spec = new PlayerByNameSpec(name)
            .Or(new PlayerByEmailSpec(email));

    public override Expression<Func<Player, bool>> BuildExpression() => _spec.BuildExpression();
}