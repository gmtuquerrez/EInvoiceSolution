using EInvoice.Infrastructure.Identity;

namespace EInvoiceSolution.SriCheckerConsole.Services
{
    public class SystemUserProvider : IUserProvider
    {
        public string GetCurrentUser() => "checker";
    }
}
