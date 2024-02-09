using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Games;

public class GameSearchSpec : Specification<Game>
{
    private readonly Specification<Game> _spec;

    public GameSearchSpec(int? gameMasterId, int? campaignId) => 
        _spec = new GameByGameMasterIdSpec(gameMasterId)
            .Or(new GameByCampaignIdSpec(campaignId));

    public override Expression<Func<Game, bool>> BuildExpression() => _spec.BuildExpression();
}