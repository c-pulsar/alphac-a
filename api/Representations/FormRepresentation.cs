using Newtonsoft.Json.Linq;

namespace Pulsar.AlphacA.Representations
{
  public class FormRepresentation : Representation
  {
    public JObject Schema { get; set; }

    public JObject Form { get; set; }
  }
}