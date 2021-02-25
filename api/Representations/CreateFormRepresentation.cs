using System;
using AlphacA.Representations.Html;

namespace AlphacA.Representations
{
  public class CreateFormRepresentation : FormRepresentation
  {
    public Uri CreateUri { get; set; }

    public override string Html()
    {
      return new CreateFormHtmlBuilder().Html(this);
    }
  }
}