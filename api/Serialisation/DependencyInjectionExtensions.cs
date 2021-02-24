using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AlphacA.Serialisation
{
  public static class DependencyInjectionExtensions
  {
    public static IMvcBuilder AddJsonSerialisation(this IMvcBuilder mvcBuilder)
    {
      return mvcBuilder.AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
          NamingStrategy = new CamelCaseNamingStrategy()
        };

        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
      });
    }
  }
}