namespace MyWeb.Common.Paging
{
    public static class PagingHelper
    {
        public static PagedResult<T> ToPagedResult<T>(
            this IQueryable<T> query, int page, int pageSize)
        {
            var totalRecords = query.Count();
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResult<T>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}