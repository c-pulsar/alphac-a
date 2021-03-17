using Newtonsoft.Json;

namespace AlphacA.Representations
{
  public class EditFormRepresentation : FormRepresentation
  {
    [JsonProperty("_canDelete")]
    public bool CanDelete { get; set; }
    public override RepresentationType Type => RepresentationType.EditForm;
  }
}