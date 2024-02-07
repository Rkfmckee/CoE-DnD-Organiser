using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Campaigns;

public class CampaignByThemeSpec : Specification<Campaign>
{
    private readonly string _theme;
    
    public CampaignByThemeSpec(string theme) => _theme = theme;

    public override Expression<Func<Campaign, bool>> BuildExpression() =>
        string.IsNullOrWhiteSpace(_theme) ? ShowAll : x => x.Theme.Contains(_theme);
}