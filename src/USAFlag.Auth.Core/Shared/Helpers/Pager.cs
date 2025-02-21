namespace USAFlag.Auth.Core.Shared.Helpers;

public class Pager

{
    public Pager(long totalItems, int? page, int pageSize)
    {
        var totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
        var currentPage = page != null ? (int)page : 1;
        var startPage = currentPage - 5;
        var endPage = currentPage + 4;

        if (startPage <= 0)
        {
            endPage = endPage - (startPage - 1);
            startPage = 1;
        }

        if (endPage > totalPages)
        {
            endPage = totalPages;
            if (endPage > 10)
            {
                startPage = endPage - 9;
            }
        }

        this.totalItems = totalItems;
        this.currentPage = currentPage;
        this.pageSize = pageSize;
        this.totalPages = totalPages;
        this.startPage = startPage;
        this.endPage = endPage;

        //To Calculate the starting Item number and Ending Item Number in a page. 
        startItemInPage = (currentPage - 1) * pageSize + 1;
        endItemInPage = currentPage * pageSize;

        if (currentPage == totalPages)
        {
            endItemInPage = totalItems;
        }
    }

    public long totalItems { get; set; }
    public int currentPage { get; set; }
    public long pageSize { get; set; }

    public int totalPages { get; set; }
    public int startPage { get; set; }
    public int endPage { get; set; }
    public long startItemInPage { get; set; }
    public long endItemInPage { get; set; }
}
