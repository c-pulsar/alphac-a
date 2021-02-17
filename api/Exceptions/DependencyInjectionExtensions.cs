using Microsoft.Extensions.DependencyInjection;

namespace AlphacA.Exceptions
{
  public static class DependencyInjectionExtensions
  {
    public static IServiceCollection AddExceptions(this IServiceCollection services)
    {
      return services.AddTransient<UnhandledExceptionFilterAttribute>();
    }

    public static IMvcBuilder AddExceptionFilters(this IMvcBuilder mvcBuilder)
    {
      return mvcBuilder.AddMvcOptions(
        options => options.Filters.AddService<UnhandledExceptionFilterAttribute>());
    }
  }
}