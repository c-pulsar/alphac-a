using System;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class RepresentationCollection : Representation
  {
    [JsonProperty("@create-form")]
    public Uri CreateForm { get; set; }
    public Uri[] Items { get; set; }
  }
}