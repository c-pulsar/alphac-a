using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace AlphacA.Auth
{
  public static class DependencyInjectionExtensions
  {
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
      services
        .AddJwtBearerOptions()
        .AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer();

      return services;
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
  }
}