using Newtonsoft.Json;
using AlphacA.Representations;

namespace AlphacA.Resources
{
  public class RootRepresentation : Representation
  {
    [JsonProperty("@users")]
    public string UsersUri { get; set; }

    public string MountText { get; set; }
    public string AmountText { get; set; }
  }
}