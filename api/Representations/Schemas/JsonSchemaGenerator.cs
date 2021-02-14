using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Pulsar.AlphacA.Representations.Schemas
{
  public class JsonSchemaGenerator
  {
    public JObject GenerateJSchemaObject<T>(T instance)
    {
      var type = instance.GetType();
      if (type.IsPrimitive)
      {
        throw new InvalidOperationException("Cannot generate schema for primitive type.");
      }

      var content = new JObject(type.GetProperties().Select(this.MakeProperty).Where(p => p != null));

      return new JObject
      {
        new JProperty("type", "object"),
        new JProperty("properties", content)
      };
    }

    private JProperty MakeProperty<T>(PropertyInfo propertyInfo, T instance)
    {
      var displayNameAttr = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
      if (displayNameAttr == null)
      {
        return null;
      }

      var content = new JObject();
      content.Add(new JProperty("title", displayNameAttr.DisplayName));

      var result = new JProperty(propertyInfo.Name, content);

      var descriptionAttr = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
      if (descriptionAttr != null)
      {
        content.Add(new JProperty("description", descriptionAttr.Description));
      }

      if (propertyInfo.PropertyType.IsPrimitive)
      {
        content.Add(new JProperty("type", "integer")); // TODO: get right type
        var value = propertyInfo.GetValue(instance);
        if (value != null)
        {
          content.Add(new JProperty("default", value));
        }
      }
      else
      {
        content.Add(new JProperty("type", "object"));
        content.Add(new JProperty(
          "properties",
          propertyInfo.PropertyType.GetProperties().Select(this.MakeProperty).Where(p => p != null)));
      }

      return result;
    }
  }
}