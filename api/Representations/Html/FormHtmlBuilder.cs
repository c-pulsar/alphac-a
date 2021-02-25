using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AlphacA.Representations.Html
{
  public class FormHtmlBuilder : HtmlBuilder<FormRepresentation>
  {
    public override string Html(FormRepresentation representation)
    {
      var panelGroup = this.PanelGroupHtml(
        this.PanelHtml(
          representation.Links.Select(x => this.LinkHtml(x)).ToArray(),
          "Links"),
        this.PanelHtml(
          FormHtml(representation).ToArray(),
          representation.Title));

      return $"<!DOCTYPE html>{this.DocumentHtml(representation.Title, panelGroup)}";
    }

    private static IEnumerable<XElement> FormHtml(FormRepresentation representation)
    {
      yield return new XElement("form", "");

      yield return new XElement(
        "div",
        new XAttribute("id", "res"),
        new XAttribute("class", "text-center alert"), "");

      yield return new XElement(
        "script",
        new XAttribute("type", "text/javascript"),
        new XAttribute("src", "/static/jsonforms/deps/jquery.min.js"), "");

      yield return new XElement(
        "script",
        new XAttribute("type", "text/javascript"),
        new XAttribute("src", "/static/jsonforms/deps/underscore.js"), "");

      yield return new XElement(
        "script",
        new XAttribute("type", "text/javascript"),
        new XAttribute("src", "/static/jsonforms/deps/opt/jsv.js"), "");

      yield return new XElement(
        "script",
        new XAttribute("type", "text/javascript"),
        new XAttribute("src", "/static/jsonforms/lib/jsonform.js"), "");

      yield return new XElement(
        "script",
        new XAttribute("type", "text/javascript"),
        new XAttribute("src", "/static/forms/create-form.js"), "");

      yield return new XElement(
        "script",
        new XAttribute("type", "text/javascript"),
        $"buildFormFromSchema('{representation.PostUri}', {representation.Schema});");
    }
  }
}