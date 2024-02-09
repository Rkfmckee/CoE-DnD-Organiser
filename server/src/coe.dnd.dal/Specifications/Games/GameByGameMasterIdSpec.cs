using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Games;

public class GameByGameMasterIdSpec : Specification<Game>
{
    private readonly int? _gameMasterId;
    
    public GameByGameMasterIdSpec(int? gameMasterId) => _gameMasterId = gameMasterId;

    public override Expression<Func<Game, bool>> BuildExpression() =>
        !_gameMasterId.HasValue ? ShowAll : x => x.GameMasterId == _gameMasterId;
}