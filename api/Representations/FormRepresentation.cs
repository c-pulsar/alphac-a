using System;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public abstract class FormRepresentation : Representation
  {
    [JsonProperty("_postLocation")]
    public Uri PostLocation {get; set;}
  }
}