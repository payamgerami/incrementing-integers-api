using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IncrementingIntegers.Api.Authentication
{

    public class FacebookAuthenticationHandler : AuthenticationHandler<FacebookAuthenticationOptions>
    {
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string token = null;
            string authorization = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorization))
            {
                return AuthenticateResult.Skip();
            }

            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization.Substring("Bearer ".Length).Trim();
            }

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Skip();
            }

            HttpClient httpClient = new HttpClient();

            try
            {
                JObject user = JObject.Parse(await httpClient.GetStringAsync($"https://graph.facebook.com/me?access_token={token}"));

                var claims = new List<Claim>
                {
                    new Claim(FacebookAuthenticationClaims.FacebookUserId, user["id"].ToString()),
                    new Claim(FacebookAuthenticationClaims.FacebookName, user["name"].ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Bearer");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, new AuthenticationProperties(), Options.AuthenticationScheme));

            }
            catch (Exception)
            {
                return AuthenticateResult.Skip();
            }
        }
    }
}
