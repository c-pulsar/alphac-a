using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Pulsar.AlphacA.Representations.Schemas
{
  public static class JsonSchema
  {
    private static class SchemaPropertyType
    {
      public const string String = "string";
      public const string Object = "object";
    }

    public static JObject Generate<T>(T instance)
    {
      return new JObject(
        new JProperty("type", SchemaPropertyType.Object),
        new JProperty("properties", instance.GetType().ToObject()));
    }

    private static JProperty ToJProperty(this PropertyInfo propertyInfo)
    {
      return new JProperty(
        propertyInfo.GetPropertyName(),
        new JObject(propertyInfo.GetPropertyAttributes().ToArray()));
    }

    private static IEnumerable<JProperty> GetPropertyAttributes(this PropertyInfo propertyInfo)
    {
      var propertySchemaType = propertyInfo.PropertyType.GetSchemaType();

      yield return new JProperty("type", propertySchemaType);

      var displayNameAttr = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
      if (displayNameAttr != null)
      {
        yield return new JProperty("title", displayNameAttr.DisplayName);
      }

      var descriptionAttr = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
      if (descriptionAttr != null)
      {
        yield return new JProperty("description", descriptionAttr.Description);
      }

      var requiredAttr = propertyInfo.GetCustomAttribute<RequiredAttribute>();
      if (requiredAttr != null)
      {
        yield return new JProperty("required", true);
      }

      var emailAttr = propertyInfo.GetCustomAttribute<EmailAddressAttribute>();
      if (emailAttr != null)
      {
        yield return new JProperty("format", "email");
      }

      if (propertySchemaType == SchemaPropertyType.Object)
      {
        yield return new JProperty(
           "properties",
           new JObject(propertyInfo.PropertyType
             .GetProperties()
             .Filter()
             .Select(x => x.ToJProperty())));
      }
    }

    private static JObject ToObject(this Type type)
    {
      return new JObject(
        type.GetProperties().Filter().Select(x => x.ToJProperty()));
    }

    private static string GetSchemaType(this Type type)
    {
      if (type == typeof(string))
      {
        return SchemaPropertyType.String;
      }

      if (type.GetTypeInfo().IsClass)
      {
        return SchemaPropertyType.Object;
      }

      throw new NotSupportedException($"Property type {type} not implemented");
    }

    private static IEnumerable<PropertyInfo> Filter(this PropertyInfo[] properties)
    {
      return properties.Where(x =>
        x.GetSetMethod() != null && // has public setter
        x.GetGetMethod() != null && // has public getter
        x.GetCustomAttribute<DisplayNameAttribute>() != null); // has display name
    }

    private static string GetPropertyName(this PropertyInfo propertyInfo)
    {
      // convert to camel-case
      return Char.ToLowerInvariant(propertyInfo.Name[0]) + propertyInfo.Name[1..];
    }
  }
}