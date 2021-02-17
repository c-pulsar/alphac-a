using Microsoft.Extensions.DependencyInjection;

namespace AlphacA.Resources.Root
{
  public static class DependencyInjectionExtensions
  {
    public static IServiceCollection AddRoot(this IServiceCollection services)
    {
      return services
        .AddTransient<RootUriFactory>()
        .AddTransient<RootRepresentationAdapter>();
    }
  }
}