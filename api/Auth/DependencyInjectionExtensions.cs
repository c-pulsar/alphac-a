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

    /*https://dev-wkngfk2k.au.auth0.com/authorize?
  response_type=token&
  client_id=pyxvaVrmLOWyDWl5eRldzHZWwdNqCVaY&
  connection=Username-Password-Authentication&
  redirect_uri=http://localhost:3010/auth0/callback&
  state=STATE*/
  }
}