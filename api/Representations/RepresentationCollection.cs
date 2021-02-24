using System;
using AlphacA.Representations.Html;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class RepresentationCollection : Representation
  {
    [JsonProperty("_items", Order = -1)]
    public RepresentationCollectionItem[] Items { get; set; }

    public override string Html()
    {
      return new CollectionHtmlBuilder().Html(this);
    }
  }

  public class RepresentationCollectionItem
  {
    [JsonProperty("href")]
    public Uri Reference { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }
  }
}