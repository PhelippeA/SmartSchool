namespace SmartSchool.WebApi.Helpers
{
    public class PageParams
    {
        public const int maxPagesSize = 50;
        public int pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPagesSize ? maxPagesSize : value; }
        }
    }
}