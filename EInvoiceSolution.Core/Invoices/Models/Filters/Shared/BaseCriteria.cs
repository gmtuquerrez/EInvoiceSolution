namespace EInvoiceSolution.Core.Invoices.Models.Filters
{
    public class BaseCriteria
    {
        private const int MaxPageSize = 100;

        private int _pageSize = 20;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int PageNumber { get; set; } = 1;
    }
}
