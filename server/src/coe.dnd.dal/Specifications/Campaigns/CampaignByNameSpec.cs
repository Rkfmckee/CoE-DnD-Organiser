using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Campaigns;

public class CampaignByNameSpec : Specification<Campaign>
{
    private readonly string _name;
    
    public CampaignByNameSpec(string name) => _name = name;

    public override Expression<Func<Campaign, bool>> BuildExpression() =>
        string.IsNullOrWhiteSpace(_name) ? ShowAll : x => x.Name.Contains(_name);
}