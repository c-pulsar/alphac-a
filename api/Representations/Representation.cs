using AlphacA.Representations.Html;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public abstract class Representation
  {
    [JsonProperty("_links", Order = -2)]
    public Link[] Links { get; set; }

    [JsonProperty("_title", Order = -2)]
    public string Title { get; set; }

    [JsonProperty("_type", Order = -2)]
    public string Type { get; set; }

    public virtual string Html()
    {
      return new RepresentationHtmlBuilder().Html(this);
    }
  }
}