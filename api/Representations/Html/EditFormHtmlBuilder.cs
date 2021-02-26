using System.Xml.Linq;

namespace AlphacA.Representations.Html
{
  public class EditFormHtmlBuilder : FormHtmlBuilder<EditFormRepresentation>
  {
    protected override XElement FormScript(EditFormRepresentation representation)
    {
      return new XElement(
        "script",
        new XAttribute("type", "text/javascript"),
        new XAttribute("src", "/static/forms/edit-form.js"), "");
    }

    protected override XElement InitialisationScript(EditFormRepresentation representation)
    {
      return new XElement(
        "script",
        new XAttribute("type", "text/javascript"),
        $"buildFormFromSchema('{representation.PostUri}', '{representation.DeleteRedirectUri}', {representation.Schema});");
    }
  }
}