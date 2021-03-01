using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AlphacA.Auth
{
  public static class DependencyInjectionExtensions
  {
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
      services
        .AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
        .Configure<AuthConfig>((options, config) =>
        {
          options.Authority = config.Authority;
          options.Audience = config.Audience;
        });

      services
        .AddOptions<OpenIdConnectOptions>("Auth0")
        .Configure<AuthConfig>((options, config) =>
        {
          options.Authority = config.Authority;
          options.ClientId = config.ClientId;
          options.ClientSecret = config.ClientSecret;
        });

      services
        .AddAuthentication(options =>
        {
          // options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          // options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

          options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddJwtBearer()
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

    /*https://dev-wkngfk2k.au.auth0.com/authorize?
  response_type=token&
  client_id=pyxvaVrmLOWyDWl5eRldzHZWwdNqCVaY&
  connection=Username-Password-Authentication&
  redirect_uri=http://localhost:3010/auth0/callback&
  state=STATE*/
  }
}