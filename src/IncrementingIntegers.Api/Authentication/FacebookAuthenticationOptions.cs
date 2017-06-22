using Microsoft.AspNetCore.Builder;

namespace IncrementingIntegers.Api.Authentication
{
    public class FacebookAuthenticationOptions : AuthenticationOptions
    {
        public FacebookAuthenticationOptions() : base()
        {
            AuthenticationScheme = "Bearer";
            AutomaticAuthenticate = true;
        }
    }
}
