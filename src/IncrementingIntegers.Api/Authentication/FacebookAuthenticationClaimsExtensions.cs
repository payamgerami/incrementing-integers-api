using System.Linq;
using System.Security.Claims;

namespace IncrementingIntegers.Api.Authentication
{
    public static class FacebookAuthenticationClaimsExtensions
    {
        public static string GetFaccebookUserIdClaim(this ClaimsPrincipal ClaimsPrincipal)
        {
            return ClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == FacebookAuthenticationClaims.FacebookUserId).Value;
        }
    }
}