using AlphacA.Resources.Users.Domain;
using AlphacA.Resources.Users.Representations;
using Microsoft.Extensions.DependencyInjection;

namespace AlphacA.Resources.Users
{
  public static class DependencyInjectionExtensions
  {
    public static IServiceCollection AddUser(this IServiceCollection services)
    {
      return services
        .AddTransient<UserUriFactory>()
        .AddTransient<UserRepresentationAdapter>()
        .AddTransient<UserResourceHandler>();
    }
  }
}