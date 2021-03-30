using AlphacA.Resources.Players.Domain;
using AlphacA.Resources.Players.Representations;
using Microsoft.Extensions.DependencyInjection;

namespace AlphacA.Resources.Players
{
  public static class DependencyInjectionExtensions
  {
    public static IServiceCollection AddPlayer(this IServiceCollection services)
    {
      return services
        .AddTransient<PlayerUriFactory>()
        .AddTransient<PlayerRepresentationAdapter>()
        .AddTransient<PlayerResourceHandler>();
    }
  }
}