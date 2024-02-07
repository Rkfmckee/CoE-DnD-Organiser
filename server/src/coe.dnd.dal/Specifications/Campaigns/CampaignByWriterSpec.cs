using System.Linq.Expressions;
using coe.dnd.dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace coe.dnd.dal.Specifications.Campaigns;

public class CampaignByWriterSpec : Specification<Campaign>
{
    private readonly string _writer;
    
    public CampaignByWriterSpec(string writer) => _writer = writer;

    public override Expression<Func<Campaign, bool>> BuildExpression() =>
        string.IsNullOrWhiteSpace(_writer) ? ShowAll : x => x.Writer.Contains(_writer);
}