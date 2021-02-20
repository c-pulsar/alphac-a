using Microsoft.Extensions.DependencyInjection;

namespace AlphacA.Core
{
  public static class DependencyInjectionExtensions
  {
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
      return services.AddTransient<IClock, Clock>();
    }
  }
}