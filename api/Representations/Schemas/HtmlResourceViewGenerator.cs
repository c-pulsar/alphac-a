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
    public static XElement Generate<T>(T instance, string title)
    {
      var links = new List<XElement>();

      var properties = instance
          .GetType()
          .GetProperties()
          .Select(x => MakeField(x, instance, links))
          .Where(x => x != null)
          .ToArray();

      return new XElement("div", new XAttribute("class", "panel-group container"),
        MakeContainerRow(links.ToArray(), "Links"),
        MakeContainerRow(properties, title));
    }

    private static XElement MakeField(PropertyInfo property, object instance, List<XElement> links)
    {
      var displayNameAttr = property.GetCustomAttribute<DisplayNameAttribute>();

      var value = property.GetValue(instance);
      if (value == null)
      {
        return null;
      }

      var name = displayNameAttr != null ? displayNameAttr.DisplayName : property.Name;

      if (property.PropertyType.Equals(typeof(Uri)))
      {
        links.Add(MakeLink(name, value as Uri));
        return null;
      }

      return MakeGeneral(name, value);
    }

    private static XElement MakeGeneral(string displayName, object obj)
    {
      return new XElement("div", new XAttribute("class", "row"),
        new XElement("div", new XAttribute("class", "col-xs-2"),
          new XElement("p", new XElement("strong", displayName))),
        new XElement("div", new XAttribute("class", "col-xs-10"), obj.ToString()));
    }

    private static XElement MakeContainerRow(object content, string title)
    {
      return new XElement("div",
        new XAttribute("class", "panel panel-primary"),
        new XElement("div", new XAttribute("class", "panel-heading"), title),
        new XElement("div", new XAttribute("class", "panel-body"), content));
    }

    private static XElement MakeLink(string displayName, Uri uri)
    {
      return new XElement(
          "a",
          new XAttribute("class", "btn btn-success"),
          new XAttribute("href", uri.ToString()),
          displayName);
    }
  }
}