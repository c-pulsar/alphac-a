using Microsoft.AspNetCore.Mvc;
using Pulsar.AlphacA.Root;

namespace Pulsar.AlphacA.Controllers
{
  [ApiController]
  [Route("")]
  public class RootController : ControllerBase
  {
    // [HttpGet("root", Name = RootApiRoutes.GetRoot)]
    // [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]

    [HttpGet("")]
    public ActionResult<RootRepresentation> GetRoot()
    {
      return new RootRepresentation
      {
        Uri = "TODO",
        Title = "This is the root representation",
      };
      //   return this.rootRepresentationMapper.MakeRepresentation();
    }
  }
}