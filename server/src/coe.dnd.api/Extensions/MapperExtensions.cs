using System.Diagnostics.CodeAnalysis;
using AutoMapper;

namespace coe.dnd.api.Extensions;

[ExcludeFromCodeCoverage]
public static class MapperExtensions
{
    public static IMappingExpression<TSource, TDest> IgnoreAllNull<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
    {
        expression.ForAllMembers(opts => opts.Condition((_, _, srcMember) => (srcMember != null)));
        return expression;
    }
}