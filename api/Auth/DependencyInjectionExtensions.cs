using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;


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
        .AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer();

      return services;
    }
  }
}