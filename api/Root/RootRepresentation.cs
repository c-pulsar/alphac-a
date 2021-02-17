using Newtonsoft.Json;
using Pulsar.AlphacA.Representations;

namespace Pulsar.AlphacA.Root
{
  public class RootRepresentation : Representation
  {
    [JsonProperty("@users")]
    public string UsersUri { get; set; }

    public string MountText { get; set; }
    public string AmountText { get; set; }
  }
}