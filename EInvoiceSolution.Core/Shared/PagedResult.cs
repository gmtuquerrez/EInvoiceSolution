namespace EInvoiceSolution.Core.Shared
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Records { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages
        => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
