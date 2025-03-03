namespace LavidaCoffee.Models.Utilities;

public class PagedList<T> : List<T>
{
    public int PageIndex { get; private set; }
    public int TotalNumberOfPages { get; private set; }
    public bool HasPrevious => PageIndex > 1;
    public bool HasNext => PageIndex < TotalNumberOfPages;
    
    public PagedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalNumberOfPages = (int)Math.Ceiling(count / (double)pageSize);
        // Adds specified 'items' to the 'PagedList'list
        AddRange(items);
    }
}
