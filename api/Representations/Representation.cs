using System.ComponentModel;
using AlphacA.Representations.Html;
using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public abstract class Representation
  {
    [ReadOnly(true)]
    [JsonProperty("_links", Order = -2)]
    public Link[] Links { get; set; }

    [ReadOnly(true)]
    [JsonProperty("_title", Order = -2)]
    public string Title { get; set; }

    [ReadOnly(true)]
    [JsonProperty("_type", Order = -2)]
    public string Type { get; set; }

    public virtual string Html()
    {
      return new HtmlBuilder<Representation>().Html(this);
    }
  }
}