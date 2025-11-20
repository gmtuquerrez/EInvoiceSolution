
using EInvoice.Services.Contracts;
using EInvoiceSolution.Core.Invoices.Enums;
using EInvoiceSolution.Core.Invoices.Models.Filters;

namespace EInvoiceSolution.SriSenderConsole.Workers
{
    public class SenderWorker : ISenderWorker
    {
        private readonly IInvoiceService _invoiceService;

        public SenderWorker(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        public async Task ExecuteAsync()
        {
            int page = 1;
            const int pageSize = 100;

            while (true)
            {
                var criteria = new InvoiceCriteria
                {
                    StatusName = InvoiceStatuses.Signed,
                    PageNumber = page,
                    PageSize = pageSize
                };

                var result = await _invoiceService.GetInvoicesByCriteriaAsync(criteria);

                if (!result.Success)
                {
                    Console.WriteLine($"Error: {result.Message}");
                    return;
                }

                var records = result.Data?.Records?.ToList();

                if (records == null || records.Count == 0)
                {
                    Console.WriteLine("No more invoices found.");
                    break;
                }

                Console.WriteLine($"Processing page {page} ({records.Count} invoices)...");

                foreach (var invoice in records)
                {
                    Console.WriteLine(
                        $"Invoice ID: {invoice.Id}, AccessKey: {invoice.AccessKey}, Status: {criteria.StatusName}");
                }

                // Si vinieron menos del pagesize, es el último lote
                if (records.Count < pageSize)
                    break;

                page++;
            }

            Console.WriteLine("All invoices processed.");
        }
    }
}
