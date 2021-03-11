using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema.Generation;

namespace AlphacA.Representations
{
  public static class DependencyInjectionExtensions
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

    public static IServiceCollection AddJsonSchemaGeneration(this IServiceCollection services)
    {
      var settings = new JsonSchemaGeneratorSettings
      {
        SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
        {
          ContractResolver = new RepresentationSchemaContractResolver(),
        },
        FlattenInheritanceHierarchy = true,
        DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.NotNull
      };

      return services.AddTransient(_ => new JsonSchemaGenerator(settings));
    }
  }
}