using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace AlphacA.Representations.Html
{
  public class HtmlBuilder<T> where T : Representation
  {
    private static readonly string[] IgnoreProperties = new string[]
    { "_links",  "_title", "_type", "_items" };

    public virtual string Html(T representation)
    {
      var panelGroup = this.PanelGroupHtml(
        this.PanelHtml(
          representation.Links.Select(x => this.LinkHtml(x)).ToArray(),
          "Links"),
        this.PanelHtml(
          representation
            .GetType()
            .GetProperties()
            .Select(x => PropertyHtml(x, representation))
            .Where(x => x != null)
            .ToArray(),
          representation.Title));

      return $"<!DOCTYPE html>{this.DocumentHtml(representation.Title, panelGroup)}";
    }

    protected XElement DocumentHtml(string title, XElement bodyContent)
    {
      return new XElement("html",
        new XElement("head",
          new XElement("meta", new XAttribute("charset", "utf-8")),
          new XElement("title", title),
          new XElement("link",
            new XAttribute("rel", "stylesheet"),
            new XAttribute("type", "text/css"),
            new XAttribute("href", "/static/jsonforms/deps/opt/bootstrap.css"))),
        new XElement("body", bodyContent));
    }

    protected XElement PanelGroupHtml(params XElement[] panels)
    {
      return new XElement("div",
         new XAttribute("class", "panel-group container"),
         panels);
    }

    protected XElement PanelHtml(object content, string title)
    {
      return new XElement("div",
        new XAttribute("class", "panel panel-primary"),
        new XElement("div", new XAttribute("class", "panel-heading"), title),
        new XElement("div", new XAttribute("class", "panel-body"), content));
    }

    protected XElement PropertyHtml(PropertyInfo property, object instance)
    {
      var displayNameAttr = property.GetCustomAttribute<DisplayNameAttribute>();

      var value = property.GetValue(instance);
      if (value == null)
      {
        return null;
      }

      var jsonAttr = property.GetCustomAttribute<JsonPropertyAttribute>();
      if (jsonAttr != null && IgnoreProperties.Any(x => x == jsonAttr.PropertyName))
      {
        return null;
      }

      var name = displayNameAttr != null ? displayNameAttr.DisplayName : property.Name;

      return this.PropertyDefaultHtml(name, value);
    }

    protected XElement PropertyDefaultHtml(string displayName, object obj)
    {
      return new XElement("div", new XAttribute("class", "row"),
        new XElement("div", new XAttribute("class", "col-xs-2"),
          new XElement("p", new XElement("strong", displayName))),
        new XElement("div", new XAttribute("class", "col-xs-10"), obj.ToString()));
    }

    protected XElement LinkHtml(Link link)
    {
      return new XElement(
          "a",
          new XAttribute("class", "btn btn-success"),
          new XAttribute("href", link.Reference.ToString()),
          link.Title);
    }
  }
}