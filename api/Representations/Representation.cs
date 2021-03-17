using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AlphacA.Representations
{
  public abstract class Representation
  {
    [JsonProperty("_links", Order = -7)]
    public Link[] Links { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("_type", Order = -6)]
    public virtual RepresentationType Type => RepresentationType.Representation;

    [JsonProperty("_resource", Order = -4)]
    public string Resource { get; set; }

    [JsonProperty("_title", Order = -2)]
    public string Title { get; set; }
  }

  public enum RepresentationType
  {
    Representation,
    Collection,
    CreateForm,
    EditForm,
    SearchForm
  }
}