using Microsoft.AspNetCore.Builder;

namespace IncrementingIntegers.Api.Authentication
{
    public static class FacebookAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseFacebookAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FacebookAuthenticationMiddleware>();
        }
    }
}