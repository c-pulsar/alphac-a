using System;
using AlphacA.Core;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class RepresentationCollection : Representation
  {
    [JsonProperty("@create-form")]
    public Uri CreateForm { get; set; }
    public IResourceDescriptor[] Items { get; set; }
  }
}