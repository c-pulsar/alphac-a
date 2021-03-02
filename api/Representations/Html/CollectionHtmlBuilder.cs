using System.Linq;
using System.Xml.Linq;

namespace AlphacA.Representations.Html
{
  public class CollectionHtmlBuilder : HtmlBuilder<RepresentationCollection>
  {
    public CollectionHtmlBuilder(System.Security.Principal.IIdentity identity) : base(identity)
    {
    }

    public override string Html(RepresentationCollection representation)
    {
      var panelGroup = this.PanelGroupHtml(
        this.PanelHtml(
          representation.Links.Select(x => this.LinkHtml(x)).ToArray(), 
          "Links"),
        this.PanelHtml(
          representation.Items.Select(x => MakeCollectionItem(x)).ToArray(), 
          representation.Title));

      return $"<!DOCTYPE html>{this.DocumentHtml(representation.Title, panelGroup)}";
    }

    private static XElement MakeCollectionItem(RepresentationCollectionItem item)
    {
      return new XElement(
        "a",
        new XAttribute("class", "btn btn-default btn-block"),
        new XAttribute("href", item.Reference.ToString()),
        item.Title);
    }
  }
}