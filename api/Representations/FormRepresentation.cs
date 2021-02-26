using System;
using Newtonsoft.Json.Linq;

namespace AlphacA.Representations
{
  public abstract class FormRepresentation : Representation
  {
    public Uri PostUri {get; set;}
    public JObject Schema { get; set; }
  }
}