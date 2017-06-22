using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace IncrementingIntegers.Api.Authentication
{
    public class FacebookAuthenticationMiddleware : AuthenticationMiddleware<FacebookAuthenticationOptions>
    {
        public FacebookAuthenticationMiddleware(RequestDelegate next, IOptions<FacebookAuthenticationOptions> options, ILoggerFactory loggerFactory, UrlEncoder encoder)
            : base(next, options, loggerFactory, encoder)
        {
        }

        protected override AuthenticationHandler<FacebookAuthenticationOptions> CreateHandler()
        {
            return new FacebookAuthenticationHandler();
        }
    }
}