using System;
using AlphacA.Representations.Html;

namespace AlphacA.Representations
{
  public class EditFormRepresentation : FormRepresentation
  {
    public Uri DeleteRedirectUri {get; set;}

    public override string Html()
    {
      return new EditFormHtmlBuilder().Html(this);
    }
  }
}