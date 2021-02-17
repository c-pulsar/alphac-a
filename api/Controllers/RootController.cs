using Microsoft.AspNetCore.Mvc;
using Pulsar.AlphacA.Representations;
using Pulsar.AlphacA.Representations.Users;

namespace Pulsar.AlphacA.Controllers
{
  [ApiController]
  [Route("")]
  public class RootController : ControllerBase
  {
    private readonly RootUriFactory rootUriFactory;
    private readonly UserUriFactory userUriFactory;

    public RootController(RootUriFactory rootUriFactory, UserUriFactory userUriFactory)
    {
      this.rootUriFactory = rootUriFactory;
      this.userUriFactory = userUriFactory;
    }

    [HttpGet("", Name = RootRoutes.GetRoot)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
    public ActionResult<RootRepresentation> GetRoot()
    {
      return new RootRepresentation
      {
        Uri = this.rootUriFactory.MakeRootUri(),
        ImageUri = this.rootUriFactory.MakeRootUri(),
        Title = "This is the root representation",
        UsersUri = this.userUriFactory.MakeCollectionUri(),
        AmountText = "Amount",
        MountText = "Mount"
      };
    }
  }
}