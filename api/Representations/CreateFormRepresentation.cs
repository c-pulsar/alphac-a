using Newtonsoft.Json.Linq;

namespace Pulsar.AlphacA.Representations
{
  public class CreateFormRepresentation : Representation
  {
    public JObject JsonSchema { get; set; }
  }
}