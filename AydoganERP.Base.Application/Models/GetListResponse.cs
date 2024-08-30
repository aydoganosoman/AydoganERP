namespace AydoganERP.Base.Application.Models;

public abstract class BasePageableModel
{
    public int From { get; set; }

    public int Index { get; set; }

    public int Size { get; set; }

    public int Count { get; set; }

    public int Pages { get; set; }

    public bool HasPrevious { get; set; }

    public bool HasNext { get; set; }
}

public class GetListResponse<T> : BasePageableModel
{
    private IList<T>? _items;
    public IList<T> Items
    {
        get
        {
            return _items ?? (_items = new List<T>());
        }
        set
        {
            _items = value;
        }
    }
}