namespace BuildingBlocks.Commons.Models;

public class PageMetadata
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItemsCount { get; set; }
    public int TotalPageCount { get; set; }

    public PageMetadata(int currentPage, int pageSize, int totalItemsCount)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalItemsCount = totalItemsCount; 
        TotalPageCount = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
    }
}