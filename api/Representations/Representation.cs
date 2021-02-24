using System;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class Representation
  {
    [JsonProperty("_links")]
    public Link[] Links { get; set; }

    public Uri Id { get; set; }

    public string Title { get; set; }

    public string Type { get; set; }

    public Uri Image { get; set; }
  }
}