using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Players;

public class PlayerByEmailSpec : Specification<Player>
{
    private readonly string _email;
    
    public PlayerByEmailSpec(string email) => _email = email;

    public override Expression<Func<Player, bool>> BuildExpression() =>
        string.IsNullOrWhiteSpace(_email) ? ShowAll : x => x.EmailAddress.Contains(_email);
}