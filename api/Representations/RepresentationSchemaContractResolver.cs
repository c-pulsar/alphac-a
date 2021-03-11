using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AlphacA.Representations
{
  public class RepresentationSchemaContractResolver : CamelCasePropertyNamesContractResolver
  {
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
      return base
        .CreateProperties(type, memberSerialization)
        // exclude hypermedia controls
        .Where(x => !x.PropertyName.StartsWith('_'))
        .ToList();
    }
  }
}