using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Players;

public class PlayerByNameSpec : Specification<Player>
{
    private readonly string _name;
    
    public PlayerByNameSpec(string name) => _name = name;

    public override Expression<Func<Player, bool>> BuildExpression() =>
        string.IsNullOrWhiteSpace(_name) ? ShowAll : x => x.Name.Contains(_name);
}