using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace AlphacA.Representations.Schemas
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
        new JProperty("properties", instance.GetType().ToObject(instance)));
    }

    private static JProperty ToJProperty(this PropertyInfo propertyInfo, object instance)
    {
      return new JProperty(
        propertyInfo.GetPropertyName(),
        new JObject(propertyInfo.GetPropertyAttributes(instance).ToArray()));
    }

    private static IEnumerable<JProperty> GetPropertyAttributes(this PropertyInfo propertyInfo, object instance)
    {
      var propertySchemaType = propertyInfo.PropertyType.GetSchemaType();

      if (propertySchemaType == SchemaPropertyType.Object)
      {
        return new[]
        {
          new JProperty( "properties", propertyInfo.PropertyType.ToObject(instance))
        };
      }
      else
      {
        var builder = new JsonPropertyBuilder();
        builder.AddType(propertySchemaType);
        builder.AddTitle(propertyInfo);

        if (builder.AddValue(propertyInfo, instance))
        {
          if (!builder.AddReadOnly(propertyInfo))
          {
            builder.AddEmail(propertyInfo);
            builder.AddRequired(propertyInfo);
            builder.AddDescription(propertyInfo);
          }
        }
        else
        {
          builder.AddEmail(propertyInfo);
          builder.AddRequired(propertyInfo);
          builder.AddDescription(propertyInfo);
        }

        return builder.MakeProperties();
      }
    }

    private static JObject ToObject(this Type type, object instance)
    {
      return new JObject(
        type.GetProperties().Filter().Select(x => x.ToJProperty(instance)));
    }

    private static string GetSchemaType(this Type type)
    {
      if (type == typeof(string) || type == typeof(DateTime))
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
        !x.IsReadOnly());
    }

    private static bool IsReadOnly(this PropertyInfo propertyInfo)
    {
      var readOnlyAttr = propertyInfo.GetCustomAttribute<ReadOnlyAttribute>();
      if (readOnlyAttr != null)
      {
        return readOnlyAttr.IsReadOnly;
      }

      return false;
    }

    private static string GetPropertyName(this PropertyInfo propertyInfo)
    {
      // convert to camel-case
      return Char.ToLowerInvariant(propertyInfo.Name[0]) + propertyInfo.Name[1..];
    }
  }
}