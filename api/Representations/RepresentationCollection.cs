using System;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class RepresentationCollection : Representation
  {
    [JsonProperty("@create-form")]
    public Uri CreateForm { get; set; }

    [JsonProperty("@search-form")]
    public Uri SearchForm { get; set; }

    public Representation[] Items { get; set; }
  }
}