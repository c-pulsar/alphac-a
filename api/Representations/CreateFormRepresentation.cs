using System;
using System.Security.Principal;
using AlphacA.Representations.Html;

namespace AlphacA.Representations
{
  public class CreateFormRepresentation : FormRepresentation
  {
    public CreateFormRepresentation(Representation representation) : base(representation)
    {
    }

    public override string Html(IIdentity identity)
    {
      return new CreateFormHtmlBuilder(identity).Html(this);
    }
  }
}