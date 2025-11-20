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
                var result = await _invoiceService.CreateInvoiceAsync(invoiceModel, createdBy);

                if (!result.Success)
                    return BadRequest(result.Errors);

                return Ok(new
                {
                    result.Data?.InvoiceId,
                    result.Data?.AccessKey,
                    result.Data?.Status
                });
            }
            catch (Exception ex)
            {
                // Solo para errores inesperados
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
