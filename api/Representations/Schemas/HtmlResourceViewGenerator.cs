using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace AlphacA.Representations.Schemas
{
  public static class HtmlResourceViewGenerator
  {
    private static readonly string[] PropertiesToIgnore = new string[]
    { "_links",  "_title", "_type", "_items" };

    public static XElement RepresentationHtml(Representation representation)
    {
      var links = MakeLinks(representation.Links);

      var properties = representation
        .GetType()
        .GetProperties()
        .Select(x => MakeField(x, representation))
        .Where(x => x != null)
        .ToArray();

      return new XElement("div", new XAttribute("class", "panel-group container"),
        MakeContainerRow(links.ToArray(), "Links"),
        MakeContainerRow(properties, representation.Title));
    }

    public static XElement CollectionHtml(RepresentationCollection representation)
    {
      var links = MakeLinks(representation.Links);
      var items = MakeCollectionItems(representation.Items);

      return new XElement("div", new XAttribute("class", "panel-group container"),
        MakeContainerRow(links.ToArray(), "Links"),
        MakeContainerRow(items, representation.Title));
    }

    private static XElement[] MakeCollectionItems(RepresentationCollectionItem[] items)
    {
      return items.Select(x => MakeCollectionItem(x)).ToArray();
    }

    private static XElement MakeCollectionItem(RepresentationCollectionItem item)
    {
      return new XElement(
        "a",
        new XAttribute("class", "btn btn-link btn-default btn-block"),
        new XAttribute("href", item.Reference.ToString()),
        item.Title);
    }

    private static XElement[] MakeLinks(Link[] links)
    {
      return links.Select(x => MakeLink(x.Title, x.Reference)).ToArray();
    }

    //++++++++++++++++++++++

    // public static XElement Generate<T>(T instance, string title)
    // {
    //   var links = new List<XElement>();

    //   var properties = instance
    //       .GetType()
    //       .GetProperties()
    //       .Select(x => MakeField(x, instance, links))
    //       .Where(x => x != null)
    //       .ToArray();

    //   return new XElement("div", new XAttribute("class", "panel-group container"),
    //     MakeContainerRow(links.ToArray(), "Links"),
    //     MakeContainerRow(properties, title));
    // }

    private static XElement MakeField(PropertyInfo property, object instance)
    {
      var displayNameAttr = property.GetCustomAttribute<DisplayNameAttribute>();

      var value = property.GetValue(instance);
      if (value == null)
      {
        return null;
      }

      var jsonAttr = property.GetCustomAttribute<JsonPropertyAttribute>();
      if (jsonAttr != null && PropertiesToIgnore.Any(x => x == jsonAttr.PropertyName))
      {
        return null;
      }

      var name = displayNameAttr != null ? displayNameAttr.DisplayName : property.Name;

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