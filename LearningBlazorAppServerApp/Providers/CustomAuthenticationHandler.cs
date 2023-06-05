using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace LearningBlazorAppServerApp.Providers
{
    // https://www.c-sharpcorner.com/article/how-to-add-custom-authentication-in-blazor/

    public class CustomAuthenticationHandlerOptions : AuthenticationSchemeOptions
    {

    }

    public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationHandlerOptions>
    {
        public CustomAuthenticationHandler(IOptionsMonitor<CustomAuthenticationHandlerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }

        // Bu metot sadece uygulamayı browser da yeni açan kullanıların hepsinde,
        // tüm component lar için çalışır.
        //
        // Eğer daha önceden Cookie ye ya da başka bir yere eklenmiş bir
        // Authentication bilgisi elinizde varsa onu burada kontrol ederek
        // otomatik login işlemini sağlayabilirsiniz. Eğer elinizde böyle bir veri
        // yoksa zaten her uygulamaya kullanıcı giriş yaptığında tekrar login olması gerekir.
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Eğer eliniz de login işlemi için veri yoksa,
            // her uygulamayı açan login olması gerekecekse aşağıdaki kod ile
            // Fail Ticket oluşturma işlemi yapılır.
            return AuthenticateResult.Fail("Invalid User");

            // Eğer elinizde Request 'in Authorization ından ya da Cookie den vs bir login bilgisi
            // aşağıdakine benzer şekilde uygun kontrol ve Success Ticket oluşturma işlemi yapılır.
            string token = null;

            if (Request.Headers.ContainsKey("Authorization"))
            {
                string headerValue = Request.Headers["Authorization"];
                if (headerValue.StartsWith("Bearer "))
                {
                    token = headerValue.Substring("Bearer ".Length);
                }
            }

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Invalid User");
            }

            ClaimsIdentity identity = null;

            // TODO: Token kontrolü ve claims tanımları ile ClaimsIdentity oluşturulmalı.
            // DEL : Örnek olarak aşağıda sabit değerlerle ClaimsIdentity oluşturulmuştur.
            identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "muratbaseren"),
                }, "CustomScheme");

            AuthenticationTicket ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), "CustomScheme");
            return AuthenticateResult.Success(ticket);
        }
    }
}
