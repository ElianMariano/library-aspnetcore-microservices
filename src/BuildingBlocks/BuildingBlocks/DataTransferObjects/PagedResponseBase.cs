namespace BuildingBlocks.DataTransferObjects;

public class PagedResponseBase<TData> : ResponseBase<IReadOnlyList<TData>>
{
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    public bool HasNext => CurrentPage < TotalPages;

    public bool HasPrevious => CurrentPage > 1;

    public int TotalItems { get; set; }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public PagedResponseBase(IReadOnlyList<TData> data, int totalItems, int currentPage = 1, int pageSize = 12) : base(data)
    {
        Data = data;
        TotalItems = totalItems;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
}