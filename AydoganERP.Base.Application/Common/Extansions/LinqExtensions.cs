namespace AydoganERP.Base.Application.Common.Extansions;

public static class LinqExtensions
{
    public static IEnumerable<IGrouping<TKey, TElement>> GroupByMany<TElement, TKey>(
        this IEnumerable<TElement> source,
        params Func<TElement, TKey>[] keySelectors)
    {
        if (keySelectors.Length == 0)
            return Enumerable.Empty<IGrouping<TKey, TElement>>();

        return source.GroupBy(keySelectors[0])
            .SelectMany(g => {
                var rest = g.AsEnumerable();
                if (keySelectors.Length > 1)
                    return rest.GroupByMany(keySelectors.Skip(1).ToArray());
                else
                    return new[] { g };
            });
    }
}
