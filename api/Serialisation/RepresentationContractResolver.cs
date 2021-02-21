using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AlphacA.Serialisation
{
  public class RepresentationContractResolver : DefaultContractResolver
  {
    public RepresentationContractResolver()
    {
      NamingStrategy = new CamelCaseNamingStrategy();
    }

    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
      return base
        .CreateProperties(type, memberSerialization)
        .OrderBy(p => p.PropertyName, new JsonPropertyComparer())
        .ToList();
    }
  }
}