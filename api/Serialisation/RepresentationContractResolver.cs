using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
      var property = base.CreateProperty(member, memberSerialization);

      var propertyInfo = member as PropertyInfo;
      if (propertyInfo != null)
      {
        TagUri(property, propertyInfo);
        TagTitle(property);
        TagType(property);
      }

      return property;
    }

    private static JsonProperty TagUri(JsonProperty property, PropertyInfo propertyInfo)
    {
      if (propertyInfo.PropertyType.Equals(typeof(Uri)) &&
         !property.PropertyName.StartsWith('@'))
      {
        property.PropertyName = $"@{property.PropertyName}";
      }

      return property;
    }

    private static JsonProperty TagTitle(JsonProperty property)
    {
      if (property.PropertyName.Equals("title"))
      {
        property.PropertyName = "_title";
      }

      return property;
    }

    private static JsonProperty TagType(JsonProperty property)
    {
      if (property.PropertyName.Equals("type"))
      {
        property.PropertyName = "_type";
      }

      return property;
    }
  }
}