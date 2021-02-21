using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AlphacA.Serialisation
{
  public static class DependencyInjectionExtensions
  {
    public static IMvcBuilder AddJsonSerialisation(this IMvcBuilder mvcBuilder)
    {
      return mvcBuilder.AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ContractResolver = new RepresentationContractResolver();
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
      });
    }
  }
}