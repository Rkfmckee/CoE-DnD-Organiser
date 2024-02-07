using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Campaigns;

public class CampaignByIdSpec : Specification<Campaign>
{
    private readonly int? _id;

    public CampaignByIdSpec(int? id) => _id = id;

    public override Expression<Func<Campaign, bool>> BuildExpression() =>
        x => x.Id == _id;
}