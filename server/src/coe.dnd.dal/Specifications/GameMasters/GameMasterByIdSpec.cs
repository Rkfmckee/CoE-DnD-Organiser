using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.GameMasters;

public class GameMasterByIdSpec : Specification<GameMaster>
{
    private readonly int? _id;

    public GameMasterByIdSpec(int? id) => _id = id;

    public override Expression<Func<GameMaster, bool>> BuildExpression() =>
        x => x.Id == _id;
}