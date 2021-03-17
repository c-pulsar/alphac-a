using System;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public abstract class FormRepresentation : Representation
  {
    [JsonProperty("_postLocation")]
    public Uri PostLocation { get; set; }

    [JsonProperty("_parentLocation")]
    public Uri ParentLocation { get; set; }
  }
}