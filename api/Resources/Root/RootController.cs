using Microsoft.AspNetCore.Mvc;

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
  }
}