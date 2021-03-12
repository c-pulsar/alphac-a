using System;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class RepresentationCollection : Representation
  {
    [JsonProperty("_items", Order = -1)]
    public RepresentationCollectionItem[] Items { get; set; }
    public override RepresentationType Type => RepresentationType.Collection;
  }

  public class RepresentationCollectionItem
  {
    [JsonProperty("href")]
    public Uri Reference { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }
  }
}