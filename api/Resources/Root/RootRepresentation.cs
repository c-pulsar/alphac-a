using Newtonsoft.Json;
using AlphacA.Representations;

namespace AlphacA.Resources.Root
{
  public class RootRepresentation : Representation
  {
    [JsonProperty("@users")]
    public string UsersUri { get; set; }
  }
}