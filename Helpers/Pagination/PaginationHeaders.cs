namespace SmartSchool.WebApi.Helpers
{
    public class PaginationHeaders
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public PaginationHeaders(int currentPage, int totalPages, int pageSize, int totalItems)
        {
            this.CurrentPage = currentPage;
            this.TotalPages = totalPages;
            this.PageSize = pageSize;
            this.TotalItems = totalItems;

        }
    }
}