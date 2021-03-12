using System;

namespace AlphacA.Representations
{
  public class EditFormRepresentation : FormRepresentation
  {
    public Uri DeleteRedirectUri {get; set;}
    public override RepresentationType Type => RepresentationType.EditForm;
  }
}