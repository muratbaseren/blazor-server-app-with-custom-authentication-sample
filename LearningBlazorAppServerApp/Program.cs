using LearningBlazorAppServerApp.Data;
using LearningBlazorAppServerApp.Providers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using System.Net.Security;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace LearningBlazorAppServerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<AuthenticationStateProvider, MyAuthenticationStateProvider>();
            builder.Services.AddScoped<Data.AuthenticationService>();

            // Bu kýsým da bir authentication service eklemek istediðimiz de
            // göreceðiniz gibi CustomAuthenticationHandlerOptions ve CustomAuthenticationHandler
            // sýnýflarý gerekecektir.
            builder.Services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = "CustomScheme";
            }).AddScheme<CustomAuthenticationHandlerOptions, CustomAuthenticationHandler>("CustomScheme", null);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            // Bir de bu 2 satýrý eklemeyi unutmayalým!
            app.UseAuthentication();    // kimlik doðrulama (login check)
            app.UseAuthorization();     // yetki kontrolü (role check)

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }


}