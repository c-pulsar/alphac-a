using Newtonsoft.Json;
using AlphacA.Representations;
using System;

namespace AlphacA.Resources.Root
{
  public class RootRepresentation : Representation
  {
    [JsonProperty("@users")]
    public Uri Users { get; set; }
  }
}