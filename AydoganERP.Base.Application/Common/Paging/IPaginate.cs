using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.Common.Paging;

public class PaginatedList<T>
{
    public int CurrentPage { get; private set; }
    public int From { get; private set; }
    public List<T> Items { get; private set; }
    public int PageSize { get; private set; }
    public int To { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages { get; private set; }
    public PaginatedList(List<T> items, int count, int currentPage, int pageSize)
    {
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        PageSize = pageSize;
        From = ((currentPage - 1) * pageSize) + 1;
        To = (From + pageSize) - 1;
        Items = items;
    }
    public bool HasPreviousPage
    {
        get
        {
            return (CurrentPage > 1);
        }
    }
    public bool HasNextPage
    {
        get
        {
            return (CurrentPage < TotalPages);
        }
    }
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int currentPage, int pageSize, string sortOn, string sortDirection)
    {
        var count = await source.CountAsync();
        if (!string.IsNullOrEmpty(sortOn))
        {
            if (sortDirection.ToUpper() == "ASC")
                source = source.OrderBy(x => x.GetType().GetProperty(sortOn));
            else
                source = source.OrderByDescending(x => x.GetType().GetProperty(sortOn));
        }

        source = source
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);

        var items = await source.ToListAsync();

        return new PaginatedList<T>(items, count, currentPage, pageSize);
    }
    public static PaginatedList<T> Create(IQueryable<T> source, int currentPage, int pageSize, string sortOn, string sortDirection)
    {
        var count = source.Count();
        if (!string.IsNullOrEmpty(sortOn))
        {
            if (sortDirection.ToUpper() == "ASC")
                source = source.OrderBy(x => x.GetType().GetProperty(sortOn));
            else
                source = source.OrderByDescending(x => x.GetType().GetProperty(sortOn));
        }

        source = source
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);

        var items = source.ToList();

        return new PaginatedList<T>(items, count, currentPage, pageSize);
    }

}
