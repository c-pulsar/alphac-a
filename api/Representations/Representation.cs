using Newtonsoft.Json;

namespace Pulsar.AlphacA.Representations
{
  public abstract class Representation
  {
    [JsonProperty("@id")]
    public string Uri { get; set; }

    [JsonProperty("_title")]
    public string Title { get; set; }

    [JsonProperty("@img")]
    public string ImageUri { get; set; }
  }
}