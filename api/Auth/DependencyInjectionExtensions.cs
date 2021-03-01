using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace AlphacA.Auth
{
  public static class DependencyInjectionExtensions
  {
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
      // Cookie configuration for HTTP to support cookies with SameSite=None
      //services.ConfigureSameSiteNoneCookies();

      // Cookie configuration for HTTPS
      // services.Configure<CookiePolicyOptions>(options =>
      // {
      //    options.MinimumSameSitePolicy = SameSiteMode.None
      // });

      services
        .AddOptions<OpenIdConnectOptions>("Auth0")
        .Configure<AuthConfig>((options, config) =>
      {
        options.Authority = $"https://{config.Domain}";
        options.ClientId = config.ClientId;
        options.ClientSecret = config.ClientSecret;
      });

      // Add authentication services
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      })
      .AddCookie()
      .AddOpenIdConnect("Auth0", options =>
      {
        // Set response type to code
        options.ResponseType = OpenIdConnectResponseType.Code;

        // Configure the scope
        options.Scope.Clear();
        options.Scope.Add("openid");

        // Ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
        options.CallbackPath = new PathString("/auth0/callback");


        // Configure the Claims Issuer to be Auth0
        options.ClaimsIssuer = "Auth0";
      });

      return services;
    }
  }
}