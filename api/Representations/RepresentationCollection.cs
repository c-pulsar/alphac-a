using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class RepresentationCollection : Representation
  {
    [JsonProperty("@create-form")]
    public string CreateFormUri { get; set; }
    public string[] Items { get; set; }
  }
}