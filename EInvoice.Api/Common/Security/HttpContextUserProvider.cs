using EInvoice.Infrastructure.Identity;
using System.Security.Claims;

namespace EInvoice.Api.Common.Security
{
    public class HttpContextUserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user?.FindFirst(ClaimTypes.Email)?.Value
                ?? "system";
        }
    }
}
