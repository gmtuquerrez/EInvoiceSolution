using EInvoice.Infrastructure.Identity;

namespace EInvoiceSolution.SignerConsole.Services
{
    public class SystemUserProvider : IUserProvider
    {
        public string GetCurrentUser() => "signer";
    }
}
