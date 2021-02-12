using Newtonsoft.Json;

namespace Pulsar.AlphacA.Representations
{
  public class RootRepresentation : Representation
  {
    [JsonProperty("@users")]
    public string UsersUri { get; set; }

    public string MountText { get; set; }
    public string AmountText { get; set; }
  }
}