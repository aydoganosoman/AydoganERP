using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.Paging;
public static class IQueryablePaginateExtensions
{
    public static async Task<IPaginate<TDestiny>> ToPaginateDestinyAsync<T, TDestiny>(this IQueryable<T> source,
        int index,
        int size,
        int from = 0,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        if (from > index)
        {
            throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");
        }

        int count = await source
            .CountAsync(cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);

        Type targetType = typeof(TDestiny);

        List<TDestiny> items = await source
            .Skip((index - from) * size)
            .Take(size)
            .Select(x => (TDestiny)Convert.ChangeType(x, targetType))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);

        return new Paginate<TDestiny>
        {
            Index = index,
            Size = size,
            From = from,
            Count = count,
            Items = items,
            Pages = (int)Math.Ceiling((double)count / (double)size)
        };
    }

    public static IPaginate<TDestiny> ToPaginateDestiny<T, TDestiny>(this IQueryable<T> source,
        int index,
        int size,
        int from = 0)
    {
        if (from > index)
        {
            throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");
        }

        int num = source
            .Count();

        Type targetType = typeof(TDestiny);

        List<TDestiny> items = source
            .Skip((index - from) * size)
            .Take(size)
            .Select(x => (TDestiny)Convert.ChangeType(x, targetType))
            .ToList();

        return new Paginate<TDestiny>
        {
            Index = index,
            Size = size,
            From = from,
            Count = num,
            Items = items,
            Pages = (int)Math.Ceiling((double)num / (double)size)
        };
    }

    public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size, int from = 0, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (from > index)
        {
            throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");
        }

        int count = await source
            .CountAsync(cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);

        List<T> ıtems = await source
            .Skip((index - from) * size)
            .Take(size)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);

        return new Paginate<T>
        {
            Index = index,
            Size = size,
            From = from,
            Count = count,
            Items = ıtems,
            Pages = (int)Math.Ceiling((double)count / (double)size)
        };
    }

    public static IPaginate<T> ToPaginate<T>(this IQueryable<T> source, int index, int size, int from = 0)
    {
        if (from > index)
        {
            throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");
        }

        int num = source
            .Count();

        List<T> ıtems = source
            .Skip((index - from) * size)
            .Take(size)
            .ToList();
        return new Paginate<T>
        {
            Index = index,
            Size = size,
            From = from,
            Count = num,
            Items = ıtems,
            Pages = (int)Math.Ceiling((double)num / (double)size)
        };
    }
}