using System;
using System.Security.Principal;
using AlphacA.Representations.Html;

namespace AlphacA.Representations
{
  public class EditFormRepresentation : FormRepresentation
  {
    public EditFormRepresentation(Representation representation) : base(representation)
    {
    }

    public Uri DeleteRedirectUri {get; set;}

    public override string Html(IIdentity identity)
    {
      return new EditFormHtmlBuilder(identity).Html(this);
    }
  }
}