using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Pulsar.AlphacA.Resources;
using Pulsar.AlphacA.Resources.Users;

namespace Pulsar.AlphacA.Representations
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddUriFactory(this IServiceCollection services)
    {
      return services
        .AddUriInfrastructure()
        .AddTransient<RootUriFactory>()
        .AddTransient<UserUriFactory>();
    }

    private static IServiceCollection AddUriInfrastructure(this IServiceCollection services)
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