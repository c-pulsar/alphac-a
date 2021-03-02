using System.Linq;
using System.Xml.Linq;

namespace AlphacA.Representations.Html
{
  public class CreateFormHtmlBuilder : FormHtmlBuilder<CreateFormRepresentation>
  {
    public CreateFormHtmlBuilder(System.Security.Principal.IIdentity identity) : base(identity)
    {
    }

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
        $"buildFormFromSchema('{representation.PostUri}', {representation.Schema});");
    }
  }
}