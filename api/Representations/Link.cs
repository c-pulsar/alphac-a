using System;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class Link
  {
    [JsonProperty("rel")]
    public string Relation { get; set; }

    [JsonProperty("href")]
    public Uri Reference { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    public static Link Make(string relation, Uri reference, string title = null)
    {
      return new Link
      {
        Relation = relation,
        Reference = reference,
        Title = title
      };
    }
  }
}