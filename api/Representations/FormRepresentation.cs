using System;
using Newtonsoft.Json.Linq;

namespace AlphacA.Representations
{
  public class FormRepresentation : Representation
  {
    public Uri Destination { get; set; }

    public JObject Schema { get; set; }

    public bool CanDelete { get; set; }
  }
}