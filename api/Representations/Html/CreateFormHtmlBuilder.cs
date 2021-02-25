using System.Linq;
using System.Xml.Linq;

namespace AlphacA.Representations.Html
{
  public class CreateFormHtmlBuilder : FormHtmlBuilder<CreateFormRepresentation>
  {
    protected override XElement FormScript(CreateFormRepresentation representation)
    {
      return new XElement(
        "script",
        new XAttribute("type", "text/javascript"),
        new XAttribute("src", "/static/forms/create-form.js"), "");
    }

    protected override XElement InitialisationScript(CreateFormRepresentation representation)
    {
      return new XElement(
        "script",
        new XAttribute("type", "text/javascript"),
        $"buildFormFromSchema('{representation.CreateUri}', {representation.Schema});");
    }
  }
}