using System;
using AlphacA.Representations.Html;
using Newtonsoft.Json.Linq;

namespace AlphacA.Representations
{
  public abstract class FormRepresentation : Representation
  {
    public JObject Schema { get; set; }

    public Uri PostUri { get; set; }

    public override string Html()
    {
      return new FormHtmlBuilder().Html(this);
    }
  }
}