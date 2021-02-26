using System;
using AlphacA.Representations.Schemas;
using Newtonsoft.Json.Linq;

namespace AlphacA.Representations
{
  public abstract class FormRepresentation : Representation
  {
    protected FormRepresentation(Representation representation)
    {
      this.Schema = JsonSchema.Generate(representation);
    }

    public Uri PostUri {get; set;}
    public JObject Schema { get; }
  }
}