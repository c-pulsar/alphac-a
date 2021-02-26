using System;
using AlphacA.Representations.Html;

namespace AlphacA.Representations
{
  public class CreateFormRepresentation : FormRepresentation
  {
    public override string Html()
    {
      return new CreateFormHtmlBuilder().Html(this);
    }
  }
}