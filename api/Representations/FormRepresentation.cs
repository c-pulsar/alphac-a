using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlphacA.Representations
{
  public class FormRepresentation : Representation
  {
    [JsonProperty("@destination")]
    public string DestinationUri { get; set; }

    public JObject Schema { get; set; }

    public JArray Form { get; set; }
  }
}