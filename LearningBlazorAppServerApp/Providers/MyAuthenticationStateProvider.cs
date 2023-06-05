using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace LearningBlazorAppServerApp.Providers
{
    public class MyAuthenticationStateProvider : AuthenticationStateProvider
    {
        private bool _isAuthenticated;

        public void NotifyStateChanged(bool isAuthenticated)
        {
            _isAuthenticated = isAuthenticated;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;

            if (_isAuthenticated)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "muratbaseren"),
                }, "CustomScheme");
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }
    }
}
