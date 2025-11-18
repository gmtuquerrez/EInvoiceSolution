namespace EInvoice.Infrastructure.Identity
{
    public interface IUserProvider
    {
        string GetCurrentUser();
    }
}
