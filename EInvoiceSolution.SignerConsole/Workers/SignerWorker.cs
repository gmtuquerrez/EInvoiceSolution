using EInvoice.Services.Contracts;
using EInvoiceSolution.Core.Invoices.Enums;
using EInvoiceSolution.Core.Invoices.Models.Filters;

namespace EInvoiceSolution.SignerConsole.Workers
{
    public class SignerWorker : ISignerWorker
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ICompanyCacheService _companyCache;

        public SignerWorker(IInvoiceService invoiceService,
            ICompanyCacheService companyCache)
        {
            _invoiceService = invoiceService;
            _companyCache = companyCache;
        }
        public async Task ExecuteAsync()
        {

            // Preload company cache
            await _companyCache.LoadAsync();

            int page = 1;
            const int pageSize = 100;

            while (true)
            {
                var criteria = new InvoiceCriteria
                {
                    StatusName = InvoiceStatuses.Generated,
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

                    try
                    {
                        var company = _companyCache.GetById(invoice.CompanyId);

                        if (company == null)
                        {
                            Console.WriteLine($"Warning: Company with ID {invoice.CompanyId} not found in cache.");
                            continue;
                        }

                        Console.WriteLine(
                            $"Invoice ID: {invoice.Id}, AccessKey: {invoice.AccessKey}, Status: {criteria.StatusName}");

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

                // Verify if we need to continue to the next page
                if (records.Count < pageSize)
                    break;

                page++;
            }

            Console.WriteLine("All invoices processed.");
        }
    }
}
