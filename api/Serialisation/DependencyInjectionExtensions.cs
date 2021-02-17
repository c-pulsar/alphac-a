using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AlphacA.Serialisation
{
  public static class DependencyInjectionExtensions
  {
    public class OrderedContractResolver : DefaultContractResolver
    {
      public OrderedContractResolver()
      {
        this.NamingStrategy = new CamelCaseNamingStrategy();
      }

      protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
      {
        return base.CreateProperties(type, memberSerialization).OrderBy(p => p.PropertyName).ToList();
      }
    }

    public static IMvcBuilder AddJsonSerialisation(this IMvcBuilder mvcBuilder)
    {
      return mvcBuilder.AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ContractResolver = new OrderedContractResolver();
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
      });
    }
  }
}