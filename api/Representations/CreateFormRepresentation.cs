using System;
using AlphacA.Representations.Html;

namespace AlphacA.Representations
{
  public class CreateFormRepresentation : FormRepresentation
  {
    public CreateFormRepresentation(Representation representation) : base(representation)
    {
    }

    public override string Html()
    {
      return new CreateFormHtmlBuilder().Html(this);
    }
  }
}