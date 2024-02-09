using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Games;

public class GameByCampaignIdSpec : Specification<Game>
{
    private readonly int? _campaignId;
    
    public GameByCampaignIdSpec(int? campaignId) => _campaignId = campaignId;

    public override Expression<Func<Game, bool>> BuildExpression() =>
        !_campaignId.HasValue ? ShowAll : x => x.CampaignId == _campaignId;
}