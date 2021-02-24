using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace AlphacA.Representations.Schemas
{
  public static class HtmlResourceViewGenerator
  {
    public static XElement Generate<T>(T instance)
    {
      var links = new List<XElement>();

      var properties = new XElement(
        "dl",
        instance
          .GetType()
          .GetProperties()
          .SelectMany(x => MakeField(x, instance, links))
          .ToArray());

      return new XElement("div", new XAttribute("class", "panel-group container"),
        MakeContainerRow(links.ToArray(), "Actions"),
        MakeContainerRow(properties, "Information"));
    }

    private static XElement[] MakeField(PropertyInfo property, object instance, List<XElement> links)
    {
      var displayNameAttr = property.GetCustomAttribute<DisplayNameAttribute>();

      var value = property.GetValue(instance);
      if (value == null)
      {
        return Array.Empty<XElement>();
      }

      var name = displayNameAttr != null ? displayNameAttr.DisplayName : property.Name;

      if (property.PropertyType.Equals(typeof(Uri)))
      {
        links.Add(MakeLink(name, value as Uri));
        return Array.Empty<XElement>();
      }

      return MakeDefault(name, value);
    }


    private static XElement[] MakeDefault(string displayName, object obj)
    {
      return new XElement[]
      {
        new XElement("dt", new XAttribute("class", "row"), displayName),
        new XElement("dd", obj.ToString())
      };
    }

    private static XElement MakeContainerRow(object content, string title)
    {
      return new XElement("div",
        new XAttribute("class", "panel panel-primary"),
        new XElement("div", new XAttribute("class", "panel-heading"), title),
        new XElement("div", new XAttribute("class", "panel-body text-center"), content));
    }

    private static XElement MakeLink(string displayName, Uri uri)
    {
      return new XElement(
          "a",
          new XAttribute("class", "btn btn-info"),
          new XAttribute("href", uri.ToString()),
          displayName);
    }
  }
}