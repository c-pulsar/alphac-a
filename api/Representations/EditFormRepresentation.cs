using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class EditFormRepresentation : Representation
  {
    [JsonProperty("_deleteEnabled")]
    public bool DeleteEnabled { get; set; }

    public override RepresentationType Type => RepresentationType.EditForm;
  }
}