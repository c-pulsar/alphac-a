using Microsoft.Extensions.DependencyInjection;

namespace AlphacA.Resources
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddRoot(this IServiceCollection services)
    {
      return services
        .AddTransient<RootUriFactory>();
    }
  }
}