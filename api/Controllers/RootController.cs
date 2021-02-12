using Microsoft.AspNetCore.Mvc;
using Pulsar.AlphacA.Representations;

namespace Pulsar.AlphacA.Controllers
{
  [ApiController]
  [Route("")]
  public class RootController : ControllerBase
  {
    private readonly RootUriFactory rootUriFactory;

    public RootController(RootUriFactory rootUriFactory)
    {
      this.rootUriFactory = rootUriFactory;
    }

    [HttpGet("", Name = RootRoutes.GetRoot)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
    public ActionResult<RootRepresentation> GetRoot()
    {
      return new RootRepresentation
      {
        Uri = this.rootUriFactory.MakeRootUri(),
        Title = "This is the root representation",
      };
    }
  }
}