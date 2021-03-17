using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class EditFormRepresentation : Representation
  {
    [JsonProperty("_canDelete")]
    public bool CanDelete { get; set; }

    public override RepresentationType Type => RepresentationType.EditForm;
  }
}