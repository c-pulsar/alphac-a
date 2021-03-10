using System;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;

namespace AlphacA.Representations.Schemas
{
  public class RepresentationSchemaGenerator : JSchemaGenerator
  {
    public RepresentationSchemaGenerator()
    {
      this.ContractResolver = new CamelCasePropertyNamesContractResolver();
      this.DefaultRequired = Required.DisallowNull;
      this.SchemaReferenceHandling = SchemaReferenceHandling.None;
    }

    public override JSchema Generate(Type type)
    {
      var schema = base.Generate(type);

      foreach (var property in type.GetProperties())//.Where(x => IsReadOnly(x)))
      {
        var propertyName = GetPropertyName(property);
        if (schema.Properties.TryGetValue(propertyName, out JSchema propertySchema))
        {
          if (propertyName.StartsWith("_"))
          {
            schema.Properties.Remove(propertyName);
          }
          else
          {
            CheckReadOnly(property, propertySchema);
          }
        }
      }

      return schema;
    }

    private static string GetPropertyName(PropertyInfo propertyInfo)
    {
      var jsonProp = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
      if (jsonProp != null)
      {
        return jsonProp.PropertyName;
      }

      return char.ToLowerInvariant(propertyInfo.Name[0]) + propertyInfo.Name[1..];
    }

    private static void CheckReadOnly(PropertyInfo property, JSchema propertySchema)
    {
      var attribute = property.GetCustomAttribute<ReadOnlyAttribute>();
      if (attribute?.IsReadOnly == true)
      {
        propertySchema.ReadOnly = true;
      }
    }
  }
}