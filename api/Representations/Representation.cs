using System.ComponentModel;
using System.Security.Principal;
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

    public virtual string Html(IIdentity identity)
    {
      return new HtmlBuilder<Representation>(identity).Html(this);
    }
  }
}