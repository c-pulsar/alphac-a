using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AlphacA.Representations.Html
{
  public abstract class FormHtmlBuilder<T> : HtmlBuilder<T> where T : Representation
  {
    protected FormHtmlBuilder(System.Security.Principal.IIdentity identity) : base(identity)
    {
    }

    public override string Html(T representation)
    {
      var panelGroup = this.PanelGroupHtml(
        this.PanelHeadlessHtml(this.IdentityHeaderHtml()),
        this.PanelHtml(
          representation.Links.Select(x => this.LinkHtml(x)).ToArray(),
          "Links"),
        this.PanelHtml(
          FormHtml(representation).ToArray(),
          representation.Title));

      return $"<!DOCTYPE html>{this.DocumentHtml(representation.Title, panelGroup)}";
    }

    protected IEnumerable<XElement> FormHtml(T representation)
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

      yield return FormScript(representation);

      yield return InitialisationScript(representation);
    }

    protected abstract XElement FormScript(T representation);

    protected abstract XElement InitialisationScript(T representation);
  }
}