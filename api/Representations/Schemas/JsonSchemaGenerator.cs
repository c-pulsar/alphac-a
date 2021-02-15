using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Pulsar.AlphacA.Representations.Schemas
{
  public static class JsonSchemaGenerator
  {
    private static class SchemaPropertyType
    {
      public const string String = "string";
      public const string Object = "object";
    }

    public static JObject GenerateJSchemaObject<T>(T instance)
    {
      var type = instance.GetType();
      var schemaType = GetSchemaPropertyType(type);
      if (schemaType == SchemaPropertyType.Object)
      {
        return new JObject().AddPropertyObject(type, instance);
      }

      throw new InvalidOperationException("Cannot generate schema for primitive type.");
    }

    private static JProperty MakeProperty<T>(PropertyInfo propertyInfo, T instance)
    {
      var displayNameAttr = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
      if (displayNameAttr == null)
      {
        return null;
      }

      var content = new JObject { new JProperty("title", displayNameAttr.DisplayName) }
        .AddDescription(propertyInfo)
        .AddRequired(propertyInfo)
        .AddEmail(propertyInfo);

      var propertyType = GetSchemaPropertyType(propertyInfo.PropertyType);
      if (propertyType == SchemaPropertyType.Object)
      {
        content.AddPropertyObject(propertyInfo.PropertyType, instance);
      }
      else
      {
        content.AddPropertyValue(propertyInfo, propertyType, instance);
      }

      return new JProperty(propertyInfo.GetPropertyName(), content);
    }

    private static string GetPropertyName(this PropertyInfo propertyInfo)
    {
      // convert to camel-case
      return Char.ToLowerInvariant(propertyInfo.Name[0]) + propertyInfo.Name[1..];
    }

    private static JObject AddDescription(this JObject jobj, PropertyInfo propertyInfo)
    {
      var descriptionAttr = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
      if (descriptionAttr != null)
      {
        jobj.Add(new JProperty("description", descriptionAttr.Description));
      }

      return jobj;
    }

    private static JObject AddEmail(this JObject jobj, PropertyInfo propertyInfo)
    {
      var emailAttr = propertyInfo.GetCustomAttribute<EmailAddressAttribute>();
      if (emailAttr != null)
      {
        jobj.Add(new JProperty("format", "email"));
      }

      return jobj;
    }

    private static JObject AddRequired(this JObject jobj, PropertyInfo propertyInfo)
    {
      var requiredAttr = propertyInfo.GetCustomAttribute<RequiredAttribute>();
      if (requiredAttr != null)
      {
        jobj.Add(new JProperty("required", true));
      }

      return jobj;
    }

    private static JObject AddPropertyValue<T>(
      this JObject jobj,
      PropertyInfo propertyInfo,
      string schemaType,
      T instance)
    {
      jobj.Add(new JProperty("type", schemaType));
      var value = propertyInfo.GetValue(instance);
      if (value != null)
      {
        jobj.Add(new JProperty("default", value));
      }

      return jobj;
    }

    private static JObject AddPropertyObject<T>(this JObject jobj, Type propertyType, T instance)
    {
      jobj.Add(new JProperty("type", "object"));
      jobj.Add(new JProperty(
        "properties",
        new JObject(propertyType
          .GetProperties()
          .Select(p => MakeProperty(p, instance))
          .Where(p => p != null)
          .ToArray())));

      return jobj;
    }

    private static string GetSchemaPropertyType(Type type)
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
  }
}