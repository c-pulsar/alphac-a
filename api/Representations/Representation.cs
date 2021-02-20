using System;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class Representation
  {
    [JsonProperty("@id")]
    public Uri Id { get; set; }

    [JsonProperty("_title")]
    public string Title { get; set; }

    [JsonProperty("_type")]
    public string Type { get; set; }

    [JsonProperty("@img")]
    public Uri Image { get; set; }
  }
}