using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AlphacA.Auth
{
  public static class DependencyInjectionExtensions
  {
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
      services
        .AddJwtBearerOptions()
        .AddOpenIdConnectOptions()
        .AddContentBasedAuthentication()
        .AddJwtBearer()
        .AddCookieCustom()
        .AddOpenIdConnectCustom();

      return services;
    }

    private static AuthenticationBuilder AddContentBasedAuthentication(this IServiceCollection services)
    {
      return services
       .AddAuthentication(options =>
       {
         options.DefaultAuthenticateScheme = "content-based";
         options.DefaultSignInScheme = "content-based";
         options.DefaultChallengeScheme = "content-based";
       })
       .AddPolicyScheme("content-based", "Bearer or Cookie", options =>
       {
         options.ForwardDefaultSelector = context =>
         {
           if (context.Request.Headers.TryGetValue("accept", out StringValues values))
           {
             if (values.Any(x => x.Contains("html")))
             {
               return CookieAuthenticationDefaults.AuthenticationScheme;
             }
           }

           return JwtBearerDefaults.AuthenticationScheme;
         };
       });
    }

    private static AuthenticationBuilder AddCookieCustom(this AuthenticationBuilder authenticationBuilder)
    {
      return authenticationBuilder.AddCookie(options =>
      {
        options.LoginPath = new PathString("/auth/login");
        options.LogoutPath = new PathString("/auth/logout");
        options.ReturnUrlParameter = "redirectUri";
      });
    }

    private static AuthenticationBuilder AddOpenIdConnectCustom(this AuthenticationBuilder authenticationBuilder)
    {
      return authenticationBuilder.AddOpenIdConnect("Auth0", options =>
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

        options.Events = new OpenIdConnectEvents
        {
          // handle the logout redirection
          OnRedirectToIdentityProviderForSignOut = (context) =>
          {
            var logoutUri = $"{options.Authority}v2/logout?client_id={options.ClientId}";
            var postLogoutUri = context.Properties.RedirectUri;
            if (!string.IsNullOrEmpty(postLogoutUri))
            {
              if (postLogoutUri.StartsWith("/"))
              {
                // transform to absolute
                var request = context.Request;
                postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
              }
              logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
            }

            context.Response.Redirect(logoutUri);
            context.HandleResponse();

            return Task.CompletedTask;
          }
        };
      });
    }

    private static IServiceCollection AddJwtBearerOptions(this IServiceCollection services)
    {
      services
       .AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
       .Configure<AuthConfig>((options, config) =>
       {
         options.Authority = config.Authority;
         options.Audience = config.Audience;
       });

      return services;
    }

    private static IServiceCollection AddOpenIdConnectOptions(this IServiceCollection services)
    {
      services
       .AddOptions<OpenIdConnectOptions>("Auth0")
       .Configure<AuthConfig>((options, config) =>
       {
         options.Authority = config.Authority;
         options.ClientId = config.ClientId;
         options.ClientSecret = config.ClientSecret;
       });

      return services;
    }
  }
}