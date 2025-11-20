using EInvoice.Infrastructure.Identity;

namespace EInvoiceSolution.SriSenderConsole.Services
{
    public class SystemUserProvider : IUserProvider
    {
        public string GetCurrentUser() => "signer";
    }
}
