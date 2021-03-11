using Microsoft.AspNetCore.Mvc;
using NJsonSchema;
using NJsonSchema.Generation;

namespace AlphacA.Resources.Root
{
  [ApiController]
  [Route("")]
  public class RootController
  {
    private readonly RootRepresentationAdapter adapter;
    private readonly JsonSchemaGenerator schemaGenerator;

    public RootController(RootRepresentationAdapter adapter, JsonSchemaGenerator schemaGenerator)
    {
      this.adapter = adapter;
      this.schemaGenerator = schemaGenerator;
    }

    [HttpGet("", Name = RootRoutes.Root)]
    public ActionResult<RootRepresentation> GetRoot() => this.adapter.MakeRepresentation();

    [HttpGet("schema", Name = RootRoutes.Schema)]
    public ActionResult<JsonSchema> GetSchema()
    {
      return this.schemaGenerator.Generate(typeof(RootRepresentation));
    }
  }
}