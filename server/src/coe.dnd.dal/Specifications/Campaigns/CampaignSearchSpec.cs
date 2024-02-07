using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Campaigns;

public class CampaignSearchSpec : Specification<Campaign>
{
    private readonly Specification<Campaign> _spec;

    public CampaignSearchSpec(string name, string theme, string writer) => 
        _spec = new CampaignByNameSpec(name)
            .Or(new CampaignByThemeSpec(theme)
            .Or(new CampaignByWriterSpec(writer)));

    public override Expression<Func<Campaign, bool>> BuildExpression() => _spec.BuildExpression();
}