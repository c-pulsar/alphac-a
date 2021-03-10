using AlphacA.Representations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;

namespace AlphacA.Resources.Root
{
  [ApiController]
  [Route("")]
  public class RootController
  {
    private readonly RootRepresentationAdapter adapter;

    public RootController(RootRepresentationAdapter adapter)
    {
      this.adapter = adapter;
    }

    [HttpGet("", Name = RootRoutes.Root)]
    public ActionResult<RootRepresentation> GetRoot() => this.adapter.MakeRepresentation();

    [HttpGet("schema", Name = RootRoutes.Schema)]
    public ActionResult<JSchema> GetSchema() =>
      new RepresentationSchemaGenerator().Generate(typeof(RootRepresentation));
  }
}