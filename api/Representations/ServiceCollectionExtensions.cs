using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AlphacA.Representations
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddUriInfrastructure(this IServiceCollection services)
    {
      return services
        .AddHttpContextAccessor()
        .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
        .AddScoped(p => p
        .GetService<IUrlHelperFactory>()
        .GetUrlHelper(p.GetService<IActionContextAccessor>().ActionContext));
    }
  }
}