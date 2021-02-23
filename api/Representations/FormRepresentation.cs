using Newtonsoft.Json.Linq;

namespace AlphacA.Representations
{
  public abstract class FormRepresentation : Representation
  {
    public JObject Schema { get; set; }

    public abstract string TemplateId { get; }

    public abstract string ApplyTemplate(string template);
  }
}