using EInvoice.Services.Contracts;
using EInvoiceSolution.Core.Invoices.Models;
using Microsoft.AspNetCore.Mvc;

namespace EInvoice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] InvoiceModel invoiceModel, [FromHeader] string createdBy)
        {
            if (invoiceModel == null)
                return BadRequest("Invoice data is required.");

            try
            {
                var invoice = await _invoiceService.CreateInvoiceAsync(invoiceModel, createdBy);
                return Ok(new { invoice.InvoiceId, invoice.AccessKey, Status = invoice.Status });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
