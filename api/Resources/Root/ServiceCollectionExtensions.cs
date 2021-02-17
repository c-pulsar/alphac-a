using Microsoft.Extensions.DependencyInjection;

namespace AlphacA.Resources.Root
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddRoot(this IServiceCollection services)
    {
      return services
        .AddTransient<RootUriFactory>()
        .AddTransient<RootRepresentationAdapter>();
    }
  }
}