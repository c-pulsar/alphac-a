using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace AlphacA.Representations.Schemas
{
  public class JsonPropertyBuilder
  {
    private readonly List<JProperty> properties = new();

    public void AddType(string type)
    {
      this.properties.Add(new JProperty("type", type));
    }

    public bool AddTitle(PropertyInfo propertyInfo)
    {
      var displayNameAttr = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
      if (displayNameAttr != null)
      {
        this.properties.Add(new JProperty("title", displayNameAttr.DisplayName));
        return true;
      }

      return false;
    }

    public bool AddDescription(PropertyInfo propertyInfo)
    {
      var descriptionAttr = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
      if (descriptionAttr != null)
      {
        this.properties.Add(new JProperty("description", descriptionAttr.Description));
        return true;
      }

      return false;
    }

    public bool AddRequired(PropertyInfo propertyInfo)
    {
      var requiredAttr = propertyInfo.GetCustomAttribute<RequiredAttribute>();
      if (requiredAttr != null)
      {
        this.properties.Add(new JProperty("required", true));
        return true;
      }

      return false;
    }

    public bool AddEmail(PropertyInfo propertyInfo)
    {
      var emailAttr = propertyInfo.GetCustomAttribute<EmailAddressAttribute>();
      if (emailAttr != null)
      {
        this.properties.Add(new JProperty("format", "email"));
        return true;
      }

      return false;
    }

    public bool AddReadOnly(PropertyInfo propertyInfo)
    {
      var readOnlyAttr = propertyInfo.GetCustomAttribute<ReadOnlyAttribute>();
      if (readOnlyAttr?.IsReadOnly == true)
      {
        this.properties.Add(new JProperty("readonly", true));
        return true;
      }

      return false;
    }

    public bool AddValue(PropertyInfo propertyInfo, object instance)
    {
      var value = propertyInfo.GetValue(instance);
      if (value != null)
      {
        this.properties.Add(new JProperty("default", value.ToString()));
        return true;
      }

      return false;
    }

    public IEnumerable<JProperty> MakeProperties()
    {
      return this.properties;
    }
  }
}