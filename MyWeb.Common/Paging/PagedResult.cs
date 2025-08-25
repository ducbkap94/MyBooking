namespace MyWeb.Common.Paging
{
    public class PagedResult<T>
    {
        public required List<T> Items { get; set; }
        public int TotalRecords { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; } 
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}